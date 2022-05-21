using HotelProject.Data;
using HotelProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.DAL
{
    public class DataInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public DataInitializer(AppDbContext dbContext,RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {
            _dbContext.Database.Migrate();
            if (!_dbContext.Users.Any())
            {

                var user = new User {UserName="Admin",Email="aliais@code.edu.az",Name="Admin" };
                await _userManager.CreateAsync(user,"Admin123!");
               await _userManager.AddToRoleAsync(user, RoleConstants.Admin);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole(RoleConstants.Admin));
                await _roleManager.CreateAsync(new IdentityRole(RoleConstants.Moderator));
                await _roleManager.CreateAsync(new IdentityRole(RoleConstants.User));
                await _roleManager.CreateAsync(new IdentityRole(RoleConstants.CEO));
            }

            _dbContext.SaveChanges();
        }
    }
}
