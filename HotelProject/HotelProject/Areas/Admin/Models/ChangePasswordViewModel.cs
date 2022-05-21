using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }

        [Required,DataType(DataType.Password)]

        public string OldPassword { get; set; }
        
        [Required,DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required,DataType(DataType.Password),Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
