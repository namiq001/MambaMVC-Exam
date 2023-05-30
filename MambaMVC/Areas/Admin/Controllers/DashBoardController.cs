//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MambaMVC.Areas.Admin.Controllers;
[Area("Admin")]
//Role Mentiqi
//[Authorize(Roles ="Admin")]
public class DashBoardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
