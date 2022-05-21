using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models.Entities
{
    public class BaseSlider : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Photo { get; set; }


    }
}
