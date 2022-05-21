
using HotelProject.Areas.Admin.Models;
using HotelProject.Areas.Admin.Utils;
using HotelProject.Areas.Admin.ViewModel;
using HotelProject.DAL;
using HotelProject.Data;
using HotelProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _http;
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor http)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _http = http;
        }

        public async Task<IActionResult> Index()
        {



            var email = _http.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var authUser = await _dbContext.Users.Where(p => p.Email == email).FirstOrDefaultAsync();
            if (authUser != null && authUser.IsBlocked)
            {
                return RedirectToAction("Logout", "Account", new { area="" }) ;
            }

            var users = await _dbContext.Users.ToListAsync();
            var userList = new List<UserViewModel>();

            foreach (var user in users)
            {
                userList.Add(new UserViewModel
                {
                    Id = user.Id,
                    FullName = user.Name,
                    Email = user.Email,
                    UserName = user.UserName,
                    Role = (await _userManager.GetRolesAsync(user))[0],
                    IsBlocked = user.IsBlocked
                });

            }

            return View(userList);
        }

        public async Task<IActionResult> AddRole(string id)
        {

            var user = await _dbContext.Users.FindAsync(id);
            var roles = await _dbContext.Roles.ToListAsync();
            AddRoleViewModel model = new AddRoleViewModel
            {
                user = user,
                Roles = roles,
            };
            if (user == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [ActionName("AddRole")]
        [HttpPost]

        public async Task<IActionResult> AddRolePost()
        {
            var id = Request.Form["userId"];
            var formRole = Request.Form["Role"].ToString();
            var role = _dbContext.Roles.Where(r => r.Name == formRole).FirstOrDefault();
            var user = await _dbContext.Users.FindAsync(id);
            var oldRole = _dbContext.UserRoles.Where(r => r.UserId == user.Id).FirstOrDefault();
            var oldRoleName = await _dbContext.Roles.FindAsync(oldRole.RoleId);

            if (user == null)
            {
                return NotFound();
            }

            await _userManager.RemoveFromRoleAsync(user, oldRoleName.Name);
            await _userManager.AddToRoleAsync(user, role.Name);


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var changePasswordViewModel = new ChangePasswordViewModel
            {
                Id = user.Id,
                Username = user.UserName
            };
            return View(changePasswordViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ChangePassword(string id, ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var changePasswordViewModel = new ChangePasswordViewModel
            {
                Id = user.Id,
                Username = user.UserName
            };

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                ModelState.AddModelError(nameof(ChangePasswordViewModel.OldPassword), "Old pass is incorrect");
                return View(changePasswordViewModel);
            }

            var identityResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        [HttpPost]

        public async Task<IActionResult> Activate(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.IsBlocked = false;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Deactivate(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.IsBlocked = true;
            await _dbContext.SaveChangesAsync();
           
            return RedirectToAction("Index", "User");
        }

       
    }
}
