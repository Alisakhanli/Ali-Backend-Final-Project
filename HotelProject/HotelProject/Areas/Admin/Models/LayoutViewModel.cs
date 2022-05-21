using HotelProject.Models;
using HotelProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class LayoutViewModel
    {
        public List<Room> rooms { get; set; }
        public RoomFilterViewModel roomFilterViewModel { get; set; }
        public CommentSlider commentSlider { get; set; }

        public RoomCommentViewModel roomCommentViewModel { get; set; }
        public Room room { get; set; }
        public User user { get; set; }
        public RoomReservationViewModel reservationViewModel { get; set; }
        public RoomSlider roomSlider { get; set; }
        public List<Counter> counters { get; set; }
        public BaseSlider BaseSlider { get; set; }

        public List<BlogItem> BlogItems { get; set; }

        public RoomPhotoSlider RoomPhotoSliders { get; set; }
    }
}
