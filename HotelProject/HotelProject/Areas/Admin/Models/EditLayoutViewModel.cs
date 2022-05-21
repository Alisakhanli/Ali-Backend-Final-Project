using HotelProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class EditLayoutViewModel
    {
       
        public CommentSlider CommentSlider { get; set; }
        public RoomSlider roomSliders { get; set; }
        public AddCounterViewModel AddCounterViewModel { get; set; }
        public List<Counter> counters { get; set; }
        public RoomPhotoSlider roomPhotoSlider { get; set; }
       public List<BlogItem> blogItems { get; set; }
        public BlogViewModel BlogViewModel { get; set; }
        public BaseSlider OriginalBaseSlider { get; set; }
        public BaseSliderViewModel BaseSlider { get; set; }
    }
}
