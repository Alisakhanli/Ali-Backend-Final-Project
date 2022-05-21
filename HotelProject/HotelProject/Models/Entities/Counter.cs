using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models.Entities
{
    public class Counter : BaseEntity
    {
        public int Count { get; set; }
        public string CountText { get; set; }

    }
}
