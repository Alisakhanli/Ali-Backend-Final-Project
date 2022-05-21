using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Models
{
    public class BlogViewModel
    {
        public string Title { get; set; }

        public int Id { get; set; }
        public string Text { get; set; }

        public string ButtonText { get; set; }


        public IFormFile Image { get; set; }

        public string ButtonUrl { get; set; }
    }
}
