using HotelProject.Models;
using HotelProject.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

       public DbSet<Hotel> hotels { get; set; }
       public DbSet<Header> headers { get; set; }
       public DbSet<Footer> footers { get; set; }
       public DbSet<Counter> counters { get; set; }
       public DbSet<CommentsSection> commentsSections { get; set; }
       public DbSet<CommentsSectionItems> commentsSectionItems { get; set; }
       public DbSet<BaseSlider> baseSliders { get; set; }
       public DbSet<HotelAbout> HotelAbouts { get; set; }
       public DbSet<HotelFeatures> hotelFeatures { get; set; }
       public DbSet<Room> Rooms { get; set; }
       public DbSet<Features> Features { get; set; }
       public DbSet<RoomFeatures> RoomFeatures { get; set; }
       public DbSet<RoomPhoto> RoomPhotos { get; set; }
       
        public DbSet<RoomComment> roomComments { get; set; }

        public DbSet<Reservations> reservations { get; set; }
        public DbSet<CommentSlider> commentSliders { get; set; }
        public DbSet<RoomSlider> roomSliders { get; set; }
      public DbSet<BlogItem> blogItems { get; set; }

        public DbSet<RoomPhotoSlider> roomPhotoSliders { get; set; }






    }
}
