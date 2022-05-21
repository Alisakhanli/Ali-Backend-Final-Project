using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models.Entities
{
    public class HotelAbout : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }



    }
}
