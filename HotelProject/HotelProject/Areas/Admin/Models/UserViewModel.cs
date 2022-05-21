using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public bool IsBlocked { get; set; }

      

        

    }
}
