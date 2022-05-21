using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class CreateHotelViewModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Star { get; set; }
        public IFormFile File { get; set; }





    }
}
