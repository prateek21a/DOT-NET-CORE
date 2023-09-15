using Login_Form.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Login_Form.Controllers
{
    public class HomeController : Controller
    {
        public CodeFirstdbContext Context { get; }

        public HomeController(CodeFirstdbContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserTbl user)
        {
            var myUser = Context.UserTbls.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (myUser != null)
            {
                HttpContext.Session.SetString("UserSession", myUser.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed...";
            }
            return View();
        }

       
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                string userEmail = HttpContext.Session.GetString("UserSession");

                var user = Context.UserTbls.FirstOrDefault(x => x.Email == userEmail);

                if (user != null)
                {
                    // Access the user's role property and display it
                    ViewBag.MySession = $"{user.Role}";
                }
                else
                {
                    ViewBag.MySession = "Welcome"; // User not found
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Register()
        {
         
            var roles = new List<string>
            {
                "Admin",
                "Manager",
                "HR",
                "Employee"

            };
            ViewBag.Roles = roles;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserTbl user)
        {
            if (ModelState.IsValid)
            {
                await Context.UserTbls.AddAsync(user);
                await Context.SaveChangesAsync();
                TempData["Success"] = "Hurray, Registered Successfully!!";
                return RedirectToAction("Login");
            }

            // Get the list of roles again and pass it to the view in case of validation errors
            var roles = new List<string>
    {
        "Admin",
        "Manager",
        "HR",
        "Employee"
        
    };
            ViewBag.Roles = roles;

            return View();
        }




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
}
