using HotelProject.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class RoomEditViewModel 
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int AdultBedCount { get; set; }
        public int ChildBedCount { get; set; }

        public int RoomWidth { get; set; }

        public int NightlyPrice { get; set; }

        public IFormFile[] RoomPhotos { get; set; }

        public List<Features> Features { get; set; }
        public List<RoomFeatures> RoomFeatures { get; set; }
        public Room RoomData { get; set; }
        public int Id { get; set; }


    }
}
