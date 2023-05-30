﻿using MambaMVC.Models;
using MambaMVC.ViewModels.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace MambaMVC.Controllers;



public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM register)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        AppUser newUser = new AppUser()
        {
            Name = register.Name,
            Surname = register.Surname,
            UserName = register.UserName,
            Email = register.EmailAddress,
        };
        IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }
        //Make Role
        //IdentityResult role = await _userManager.AddToRoleAsync(newUser, "Admin");
        //if (!role.Succeeded)
        //{
        //    foreach (IdentityError error in role.Errors)
        //    {
        //        ModelState.AddModelError("", error.Description);
        //        return View(register);
        //    }
        //}
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        AppUser appUser = await _userManager.FindByEmailAsync(loginVM.EmailAdress);
        if (appUser is null)
        {
            ModelState.AddModelError("", "Invalid Password or Email");
            return View();
        }
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Invalid Password or Email");
            return View();
        }
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    //public async Task<IActionResult> CreateRole()
    //{
    //    IdentityRole role = new IdentityRole("Admin");
    //    _roleManager.CreateAsync(role);
    //    return Json("Ok");
    //}
}
