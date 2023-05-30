using MambaMVC.MambaDataContext;
using MambaMVC.Models;
using MambaMVC.ViewModels.TeamVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace MambaMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class TeamController : Controller
{
    private readonly MambaDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public TeamController(IWebHostEnvironment environment, MambaDbContext context)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        List<Team> teams = await _context.Teams.Include(x => x.WorkTypes).ToListAsync();
        return View(teams);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        CreateTeamVM createTeam = new CreateTeamVM()
        {
            WorkTypes = await _context.WorkTypes.ToListAsync(),
        };
        return View(createTeam);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTeamVM createTeam)
    {
        createTeam.WorkTypes = await _context.WorkTypes.ToListAsync();
        if (!ModelState.IsValid) { return View(createTeam); }
        string newFileName = Guid.NewGuid().ToString() + createTeam.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "team", newFileName);
        using (FileStream stream = new FileStream(path, FileMode.CreateNew))
        {
            await createTeam.Image.CopyToAsync(stream);
        }
        Team Team = new Team()
        {
            Name = createTeam.Name,
            WorkTypeId = createTeam.WorkTypeId,
        };
        Team.ProfilImage = newFileName;

        _context.Teams.Add(Team);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int Id)
    {
        Team? Team = await _context.Teams.FindAsync(Id);
        if (Team is null) { return NotFound(); }
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "team", Team.ProfilImage);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        _context.Teams.Remove(Team);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        Team? Team = await _context.Teams.FindAsync(Id);
        if (Team is null)
        {
            return NotFound();
        }
        EditTeamVM editTeam = new EditTeamVM()
        {
            Name = Team.Name,
            WorkTypeId = Team.WorkTypeId,
            WorkTypes = await _context.WorkTypes.ToListAsync(),
            ImageName = Team.ProfilImage,
        };
        return View(editTeam);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditTeamVM editTeam)
    {
        Team? Team = await _context.Teams.FindAsync(Id);
        if (Team is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            editTeam.WorkTypes = await _context.WorkTypes.ToListAsync();
            return View(editTeam);
        }
        if (editTeam.ImageName is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "team", Team.ProfilImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string newFileName = Guid.NewGuid().ToString() + editTeam.Image.FileName;
            string newPath = Path.Combine(_environment.WebRootPath, "assets", "img", "team", newFileName);
            using (FileStream stream = new FileStream(newPath, FileMode.CreateNew))
            {
                await editTeam.Image.CopyToAsync(stream);
            }
            Team.ProfilImage = newFileName;
        }
        Team.Name = editTeam.Name;
        Team.WorkTypeId = editTeam.WorkTypeId;
        _context.Teams.Update(Team);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detalies (int Id)
    {
        Team? team = await _context.Teams.Include(x=>x.WorkTypes).FirstOrDefaultAsync(x=>x.Id == Id);
        if(team is null)
        {
            return NotFound();
        }
        return View(team);  
    }
}