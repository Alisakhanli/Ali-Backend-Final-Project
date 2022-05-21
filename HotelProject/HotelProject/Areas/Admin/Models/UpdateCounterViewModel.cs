using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class UpdateCounterViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string CountText { get; set; }
    }
}
