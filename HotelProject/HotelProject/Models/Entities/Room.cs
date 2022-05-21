using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int AdultBedCount { get; set; }
        public int ChildBedCount { get; set; }

        public int RoomWidth { get; set; }

        public int NightlyPrice { get; set; }

        public List<Reservations> Reservations { get; set; }
        public List<RoomComment> roomComments { get; set; }
        public List<RoomPhoto> RoomPhotos { get; set; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }
        public List<RoomFeatures> RoomFeatures { get; set; }

        public int HotelId { get; set; }
    }
}
