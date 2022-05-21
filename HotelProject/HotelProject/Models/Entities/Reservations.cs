using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models.Entities
{
    public class Reservations : BaseEntity
    {

        public string UserId { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }


        public DateTime CheckInDate { get; set; }
        public int AdultBedCount { get; set; }
        public int ChildBedCount { get; set; }


        public DateTime CheckOutDate { get; set; }



    }
}
