using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class RoomAddViewModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int AdultBedCount { get; set; }
        public int ChildBedCount { get; set; }

        public int RoomWidth { get; set; }

        public int NightlyPrice { get; set; }

        public IFormFile[] Files { get; set; }
       
      
       
        public int HotelId { get; set; }

    }
}
