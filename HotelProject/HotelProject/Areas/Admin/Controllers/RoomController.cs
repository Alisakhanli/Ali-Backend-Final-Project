using AllupStore.Areas.Admin.Constants;
using HotelProject.Areas.Admin.Models;
using HotelProject.Areas.Admin.Utils;
using HotelProject.Data;
using HotelProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomController : Controller
    {
        private readonly AppDbContext _dbContext;
        public RoomController(AppDbContext dbContext )
        {
            _dbContext = dbContext;
            
        }

        public async Task<IActionResult> AddRoom(int id)
        {
            var model = new RoomAddViewModel
            {
                HotelId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRezervation(int id, LayoutViewModel model)
        {
            var user = await _dbContext.Users.Where(u => u.Id == model.reservationViewModel.UserId).FirstOrDefaultAsync();

            if (model.reservationViewModel.CheckIn < DateTime.Now || model.reservationViewModel.CheckOut < DateTime.Now)
            {
                return RedirectToAction("Rooms", "Home", new
                {
                    area = ""
                });
            }

            var room = await _dbContext.Rooms.Include(r => r.Reservations)
                            .Where(r => !r.IsDeleted && r.Reservations.All(re => model.reservationViewModel.CheckIn > re.CheckOutDate))
                            .FirstOrDefaultAsync();

         

            if (user == null)
            {
                return NotFound();
            }



            if (room == null)
            {
                return RedirectToAction("Rooms", "Home", new
                {
                    area = ""
                });
            }




            var reservations = new Reservations
            {
                User = user,
                UserId = user.Id,
                CheckInDate = model.reservationViewModel.CheckIn,
                CheckOutDate = model.reservationViewModel.CheckOut,
                AdultBedCount = model.reservationViewModel.AdultBedCount,
                ChildBedCount = model.reservationViewModel.ChildBedCount,
                Room = room,



            };

            await _dbContext.reservations.AddAsync(reservations);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Rooms", "Home", new
            {
                area = ""
            });
        }

            [HttpPost]

        public async Task<IActionResult> AddRoom(int id,RoomAddViewModel model)
        {
            var hotel = await _dbContext.hotels.Where(h => !h.IsDeleted && h.Id == id).FirstOrDefaultAsync();
            if (hotel==null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("RoomName", "Bura bos ola bilmez");
                return View();
            }
            if (string.IsNullOrEmpty(model.Desc))
            {
                ModelState.AddModelError("RoomDesc", "Bura bos ola bilmez");
                return View();
            }
            if (model.AdultBedCount<0)
            {
                ModelState.AddModelError("AdultBedCount", "bu say 0-dan az ol bilmez");
                return View();
            }
            if (model.ChildBedCount < 0)
            {
                ModelState.AddModelError("ChildBedCount", "bu say 0-dan az ol bilmez");
                return View();
            }
            if (model.NightlyPrice < 0)
            {
                ModelState.AddModelError("NightlyPrice", "bu say 0-dan az ol bilmez");
                return View();
            }
            if (model.Files==null)
            {
                ModelState.AddModelError("Files", "Zehmet olmasa sekil secin");
                return View();
            }
            var room = new Room
            {
                Name = model.Name,
                Desc = model.Desc,
                AdultBedCount = model.AdultBedCount,
                ChildBedCount = model.ChildBedCount,
                NightlyPrice = model.NightlyPrice,
                RoomWidth = model.RoomWidth,
                Hotel = hotel,
                


            };
           await _dbContext.Rooms.AddAsync(room);
            foreach (var item in model.Files)
            {
                var fileName = FileUtils.CreateFile(FileConstants.ImagePath, item);
                var roomPhoto = new RoomPhoto
                {
                    Room = room,
                    Photo = fileName,

                };
                await _dbContext.RoomPhotos.AddAsync(roomPhoto);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index","Hotel");
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _dbContext.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (room==null)
            {
                return NotFound();
            }
            room.IsDeleted = true;
            room.DeletedDate = DateTime.Now;
          await  _dbContext.SaveChangesAsync();
            return RedirectToAction("Index","Hotel");
        }



        public async Task<IActionResult> Edit(int id)
        {
            
            var room = await _dbContext.Rooms.Include(r=>r.RoomFeatures.Where(r=>!r.IsDeleted)).Include(r=>r.RoomPhotos.Where(p=>!p.IsDeleted)).Where(r => !r.IsDeleted && r.Id == id).FirstOrDefaultAsync();
            var features = await _dbContext.Features.Where(f => !f.IsDeleted).ToListAsync();
            if (room==null)
            {
                return NotFound();
            }
            var model = new RoomEditViewModel
            {
                Id = room.Id,
                Name = room.Name,
                Desc = room.Desc,
                AdultBedCount = room.AdultBedCount,
                ChildBedCount = room.ChildBedCount,
                NightlyPrice = room.NightlyPrice,
                RoomWidth = room.RoomWidth,
                RoomData = room,
                Features = features,
                RoomFeatures = room.RoomFeatures

                

            };
            
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id,RoomEditViewModel model)
        {
            var room = await  _dbContext.Rooms.Where(r => r.Id == id && !r.IsDeleted).FirstOrDefaultAsync();
            if (room == null)
            {
                return NotFound();
            }
            if (model.RoomPhotos!=null)
            {
                foreach (var item in model.RoomPhotos)
                {
                  var fileName =   FileUtils.CreateFile(FileConstants.ImagePath, item);
                   var roomPhoto = new RoomPhoto
                   {
                       Photo = fileName,
                       Room = room,
                       
                   };
                   await _dbContext.RoomPhotos.AddAsync(roomPhoto);
                }
              
            }
            room.Name = model.Name;
            room.Desc = model.Desc;
            room.AdultBedCount = model.AdultBedCount;
            room.ChildBedCount = model.ChildBedCount;
            room.RoomWidth = model.RoomWidth;
            room.NightlyPrice = model.NightlyPrice;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index","Hotel");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoomPhoto(int id,int roomId)
        {
            
            
            var roomPhoto = await _dbContext.RoomPhotos.Where(r => r.RoomId == roomId && r.Id == id).FirstOrDefaultAsync();
            if (roomPhoto==null)
            {
                return NotFound();
            }
            roomPhoto.IsDeleted = true;
            roomPhoto.DeletedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Edit",new {
              id=roomId
            
            });
        }
        [HttpPost]

        public async Task<IActionResult> AddFeature(int id, int roomId)
        {
            var room = await _dbContext.Rooms.Where(r => r.Id == roomId && !r.IsDeleted).FirstOrDefaultAsync();
            if (room == null)
            {
                return NotFound();
            }
            var feature = await _dbContext.Features.Where(f => f.Id == id && !f.IsDeleted).FirstOrDefaultAsync();
            if (feature==null)
            {
                return NotFound();
            }
            var roomFeature = new RoomFeatures
            {
                Room = room,
                Feature = feature,
                

            };
            await _dbContext.RoomFeatures.AddAsync(roomFeature);
           await  _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFeature(int id,int roomId)
        {


            var roomFeature = await _dbContext.RoomFeatures.Where(r => r.RoomId == roomId && r.FeatureId == id).FirstOrDefaultAsync();
            if (roomFeature==null)
            {
                return NotFound();
            }
            roomFeature.IsDeleted = true;
            roomFeature.DeletedDate = DateTime.Now;
           await  _dbContext.SaveChangesAsync();
            return RedirectToAction("Edit",new {id=roomId });
        }
    }
}
