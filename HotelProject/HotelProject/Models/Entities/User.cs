using HotelProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public bool IsBlocked { get; set; } = false;

        public bool HasHotel { get; set; } = false;


        public string Photo { get; set; } = "default-avatar.jpg";

        public Hotel Hotel { get; set; }
    }
}
