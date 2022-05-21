using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models.Entities
{
    public class RoomFeatures : BaseEntity
    {
        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public int FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public Features Feature { get; set; }
        public int RoomId { get; set; }
    }
}
