using HotelProject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.ViewModel
{
    public class AddRoleViewModel
    {
        public User user { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}
