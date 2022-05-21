using HotelProject.Data;
using HotelProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Utils
{
    public static class UserUtils
    {
        public static async Task<User> GetAuthUser(AppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
           var email =  httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}
