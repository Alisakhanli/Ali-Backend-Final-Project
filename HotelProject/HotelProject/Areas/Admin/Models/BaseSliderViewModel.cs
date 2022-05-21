using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class BaseSliderViewModel
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public IFormFile Photo { get; set; }
    }
}
