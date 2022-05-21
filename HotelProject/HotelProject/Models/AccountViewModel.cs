using HotelProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class AccountViewModel
    {
        public LoginViewModel Login { get; set; }

        public RegisterViewModel Register { get; set; }
    }
}
