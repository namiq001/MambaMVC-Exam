using MambaMVC.MambaDataContext;
using MambaMVC.Models;
using MambaMVC.ViewModels.SettingVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class SettingController : Controller
{
    private readonly MambaDbContext _context;

    public SettingController(MambaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Setting> settingList = await _context.Settings.ToListAsync();
        return View(settingList);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        Setting? setting = await _context.Settings.FindAsync(Id);
        if(setting is null)
        {
            return NotFound();
        }
        EditSettingVM editSetting = new EditSettingVM()
        {
            Value = setting.Value,
        };
        return View(editSetting);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditSettingVM editSetting)
    {
        Setting? setting = await _context.Settings.FindAsync(Id);
        if(setting is null)
        {
            return NotFound();
        }
        if(!ModelState.IsValid)
        {
            return View(editSetting);
        }
        setting.Value = editSetting.Value;
        _context.Settings.Update(setting);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
