using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models.Entities
{
    public class Header : BaseEntity
    {
        public  string Name { get; set; }

        public string URL { get; set; }
    }
}
