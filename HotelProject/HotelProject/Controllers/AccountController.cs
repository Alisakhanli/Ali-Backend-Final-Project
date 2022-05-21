using HotelProject.Data;
using HotelProject.Models;
using HotelProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var dbUser = await _userManager.FindByNameAsync(model.Register.UserName);
            if (dbUser != null)
            {
                ModelState.AddModelError("", "The user with this username already exists ");
                return View("Index");
            }

            User user = new User
            {
                Name = model.Register.UserName,
                UserName = model.Register.UserName,
                Email = model.Register.Email
            };

            var IdentityResult = await _userManager.CreateAsync(user, model.Register.Password);
            await _userManager.AddToRoleAsync(user, RoleConstants.User);
            if (!IdentityResult.Succeeded)
            {
                foreach (var item in IdentityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View("Index");
            }

            await _signInManager.SignInAsync(user, true);


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var user = await _userManager.FindByNameAsync(model.Login.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View("Index");
            }
            if (user.IsBlocked)
            {
                ModelState.AddModelError("", "User is blocked");
                return View("Index");
            }
           
            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Login.Password, true, false);

            if (!signinResult.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View("Index");

            }

            return RedirectToAction("Index", "Home");
        }
    }
}
