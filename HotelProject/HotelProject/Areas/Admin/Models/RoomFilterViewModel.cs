using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class RoomFilterViewModel
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public int AdultBedCount { get; set; }
        public int ChildBedCount { get; set; }


    }
}
