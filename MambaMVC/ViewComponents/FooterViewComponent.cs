using MambaMVC.MambaDataContext;
using MambaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaMVC.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    private readonly MambaDbContext _context;

    public FooterViewComponent(MambaDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Dictionary<string, Setting> setting = await _context.Settings.ToDictionaryAsync(x => x.Key);
        return View(setting);
    }
}
