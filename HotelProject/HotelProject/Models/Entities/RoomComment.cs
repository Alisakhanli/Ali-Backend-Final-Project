using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models.Entities
{
    public class RoomComment : BaseEntity
    {
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        public string Text { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }

      
        public string UserId { get; set; }

        public int RoomId { get; set; }
    }
}
