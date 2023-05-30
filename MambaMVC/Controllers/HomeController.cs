using MambaMVC.MambaDataContext;
using MambaMVC.Models;
using MambaMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaMVC.Controllers;

public class HomeController : Controller
{
    private readonly MambaDbContext _context;

    public HomeController(MambaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Team> teams = await _context.Teams.Include(x => x.WorkTypes).ToListAsync();
        HomeVM homeVM = new HomeVM()
        {
            Teams = teams,
        };
        return View(homeVM);
    }
}