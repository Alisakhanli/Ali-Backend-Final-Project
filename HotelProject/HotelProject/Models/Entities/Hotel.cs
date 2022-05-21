using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models.Entities
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public int Star { get; set; }
        public List<Room> Rooms { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public string Desc { get; set; }
    }
}
