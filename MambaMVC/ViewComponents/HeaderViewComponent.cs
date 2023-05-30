using MambaMVC.MambaDataContext;
using MambaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaMVC.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
    private readonly MambaDbContext _context;

    public HeaderViewComponent(MambaDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Dictionary<string, Setting> setting = await _context.Settings.ToDictionaryAsync(x => x.Key);
        return View(setting);
    }
}
