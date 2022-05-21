using HotelProject.Areas.Admin.Models;
using HotelProject.Areas.Admin.Utils;
using HotelProject.Data;
using HotelProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _http;
        public HomeController(AppDbContext dbContext, IHttpContextAccessor http)
        {
            _http = http;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var baseSlider = await _dbContext.baseSliders.Where(b => !b.IsDeleted).FirstOrDefaultAsync();
            var counter = await _dbContext.counters.Where(c => !c.IsDeleted).ToListAsync();
            var roomSlider = await _dbContext.roomSliders.FirstOrDefaultAsync();
            var commentSlider = await _dbContext.commentSliders.FirstOrDefaultAsync();
            var model = new LayoutViewModel
            {
                commentSlider = commentSlider,
                roomSlider = roomSlider,
                counters = counter,
                BaseSlider = baseSlider,
                BlogItems = await _dbContext.blogItems.Where(b => !b.IsDeleted).ToListAsync(),
                RoomPhotoSliders = await _dbContext.roomPhotoSliders.FirstOrDefaultAsync()
            };

            return View(model);
        }

      public async Task<IActionResult> About()
        {

            return View();
        }

        public async Task<IActionResult> Hotels()
        {
            var hotels = await _dbContext.hotels.Where(h => !h.IsDeleted).ToListAsync();

            return View(hotels);
        }

        [HttpPost]
        public async Task<IActionResult> FilterRooms(LayoutViewModel model)
        {
            var childBedCount = model?.roomFilterViewModel?.ChildBedCount;
            var checkOut = model?.roomFilterViewModel?.CheckOut;
            var checkIn = model?.roomFilterViewModel?.CheckIn;
            var adultBedCount = model?.roomFilterViewModel?.AdultBedCount;
            var originalModel = new LayoutViewModel
            {
                rooms = await _dbContext.Rooms.Where(r => !r.IsDeleted).ToListAsync()
            };

            if (checkOut < DateTime.Now)
            {
                checkOut = DateTime.Now.AddDays(1);
            }
            if (checkIn < DateTime.Now)
            {
                checkIn = DateTime.Now;
            }






            originalModel.rooms = await _dbContext.Rooms
                .Include(r => r.Reservations)
                .Include(r => r.RoomPhotos)
                .Where(r => !r.IsDeleted && r.Reservations.All(re => checkIn > re.CheckOutDate || re.IsDeleted) && adultBedCount <= r.AdultBedCount && childBedCount <= r.ChildBedCount)
                .ToListAsync();
          

            return View("Rooms", originalModel);
        }
        public async Task<IActionResult> Rooms()
        {
            var rooms = await _dbContext.Rooms.Include(i=>i.RoomPhotos).Where(r => !r.IsDeleted).ToListAsync();
            var model = new LayoutViewModel
            {
                rooms = rooms
            };
            return View(model);
        }

        public async Task<IActionResult> RoomDetails(int id)
        {

            var room = await _dbContext.Rooms.Include(p=>p.RoomPhotos).Include(c=>c.RoomFeatures).ThenInclude(p=>p.Feature).Include(a=>a.roomComments).ThenInclude(f=>f.user).Where(r => !r.IsDeleted && r.Id == id).FirstOrDefaultAsync();
            if (room==null)
            {
                return NotFound();
            }
            var model = new LayoutViewModel
            {
                room = room,
                user = await UserUtils.GetAuthUser(_dbContext,_http)
            };
            return View(model);
        }

        public async Task<IActionResult> Services()
        {
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }
    }
}
