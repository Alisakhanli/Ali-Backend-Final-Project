using AllupStore.Areas.Admin.Constants;
using HotelProject.Areas.Admin.Models;
using HotelProject.Areas.Admin.Utils;
using HotelProject.Data;
using HotelProject.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HotelController : Controller
    {
        
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _http;
        public HotelController(AppDbContext dbContext, IHttpContextAccessor http)
        {
            _dbContext = dbContext;
            _http = http;
        }
        public async Task<IActionResult> Index()
        {
            var user = await UserUtils.GetAuthUser(_dbContext, _http);
            if (user==null)
            {
                return NotFound();
            }
            if (user.HasHotel)
            {
                var hotel = await _dbContext.hotels.Include(h=>h.Rooms.Where(r=>!r.IsDeleted)).ThenInclude(h=>h.RoomPhotos).Where(h => h.UserId == user.Id && !h.IsDeleted).FirstOrDefaultAsync();
                return View(hotel);
            }
            else
            {
                return RedirectToAction("CreateHotels");
            }
            
        }

        public async Task<IActionResult> CreateHotels()
        {
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> CreateHotels(CreateHotelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await UserUtils.GetAuthUser(_dbContext, _http);
            if (user == null)
            {
                return NotFound();
            }
            var fileName = FileUtils.CreateFile(FileConstants.ImagePath, model.File);
            var hotels = new Hotel
            {
                Name = model.Name,
                Desc = model.Desc,
                Star = model.Star,
                Photo = fileName


            };
           await _dbContext.hotels.AddAsync(hotels);
            user.Hotel = hotels;
            user.HasHotel = true;
           await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateHotel(int id)
        {
            var hotel = await _dbContext.hotels.Where(h => !h.IsDeleted&& h.Id==id).FirstOrDefaultAsync();
            if (hotel==null)
            {
                return NotFound();
            }
            var model = new UpdateHotelViewModel
            {
                Id = hotel.Id,
                Desc = hotel.Desc,
                 Name = hotel.Name,
                 Star = hotel.Star,
            };
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateHotel(int id,UpdateHotelViewModel model)
        {
            var hotel = await _dbContext.hotels.Where(h => !h.IsDeleted && h.Id == id).FirstOrDefaultAsync();
            if (hotel==null)
            {
                return NotFound();
            }
            hotel.Name = model.Name;
            hotel.Desc = model.Desc;
            hotel.Star = model.Star;
            if (model.Photo!=null)
            {
               await FileUtils.DelteFile(Path.Combine(FileConstants.ImagePath, hotel.Photo));
              var fileName =  FileUtils.CreateFile(FileConstants.ImagePath, model.Photo);
                hotel.Photo = fileName;
            }
           await _dbContext.SaveChangesAsync();
            
            return RedirectToAction("Index","Hotel");
        }
    }
}
