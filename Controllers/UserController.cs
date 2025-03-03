using CartManagementMVC.Models;
using CartManagementMVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CartManagementMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userRepository.ValidateUser(username, password);
            if (user != null)
            {
                if (HttpContext.Session != null)
                    {
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetInt32("UserId", user.UserId);
                    }
                return RedirectToAction("Index", "Product");
            }
            ViewBag.Message = "Invalid Username or Password";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                bool result = _userRepository.SaveUser(user);
                if (result)
                {
                    return RedirectToAction("Login");
                }
                ViewBag.Message = "Error while registering";
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
