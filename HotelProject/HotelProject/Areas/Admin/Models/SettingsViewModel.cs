using HotelProject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class SettingsViewModel
    {
        public string Name { get; set; }
        public User User { get; set; }
        public IFormFile Photo { get; set; }
    }
}
