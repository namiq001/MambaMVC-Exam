using MambaMVC.MambaDataContext;
using MambaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class WorkTypeController : Controller
{
    private readonly MambaDbContext _context;

    public WorkTypeController(MambaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<WorkType> workTypes = await _context.WorkTypes.ToListAsync();
        return View(workTypes);
    }
}
