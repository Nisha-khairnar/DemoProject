using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoProject.Models;
using DemoProject.Models.UserRepository;
namespace DemoProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    private readonly UserRepository.UserFunctions db = new UserRepository.UserFunctions();
    public IActionResult Index()
    {
        var user = db.GetAllUsers();
        return View(user);
    }
    #region AddUser
    public IActionResult _AddUser()
    {
        ViewBag.States = db.GetAllStates();
        return PartialView();
    }
    [HttpPost]
    public IActionResult AddUser(Users.User Model)
    {
        if (ModelState.IsValid)
        {
            db.AddEditUsers(Model);
        }
        return RedirectToAction("Index", "Home");
    }
    #endregion
    #region EditUser
    public IActionResult _EditUser(int Id)
    {
        var obj = db.GetUserDetailsById(Id);
        ViewBag.States = db.GetAllStates();
        ViewBag.Cities = db.GetAllCitiesByState(obj.StateId);
        return PartialView(obj);
    }
    [HttpPost]
    public IActionResult EditUser(Users.User Model)
    {
        if (ModelState.IsValid)
        {
            db.AddEditUsers(Model);
        }
        return RedirectToAction("Index", "Home");
    }
    #endregion
    #region DeleteUser
    public IActionResult DeleteUser(int Id)
    {
        db.DeleteUser(Id);
        return RedirectToAction("Index", "Home");
    }
    #endregion
    #region For cascading bind cities based on state
    public IActionResult GetAllCitiesByState(int Id)
    {
        var CitiesList = db.GetAllCitiesByState(Id);
        return Json(new { CitiesList });
    }
    #endregion

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
