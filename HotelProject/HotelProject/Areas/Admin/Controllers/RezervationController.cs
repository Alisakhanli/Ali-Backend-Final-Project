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
    public class RezervationController : Controller
    {
        private readonly AppDbContext _dbContext;


        public RezervationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public async Task<IActionResult> Index()
        {
            var rezervation = await _dbContext.reservations.Include(s=>s.Room).Include(d=>d.User).Where(r => !r.IsDeleted).ToListAsync();
            return View(rezervation);
        }

        public async Task<IActionResult> EditRezervation(int id)
        {
            var rezervation = await _dbContext.reservations.Where(r => !r.IsDeleted && r.Id == id).FirstOrDefaultAsync();
            return View(rezervation);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateRezervation(int id,Reservations reservations)
        {
            var reservation = await _dbContext.reservations.Where(r => !r.IsDeleted && r.Id==id).FirstOrDefaultAsync();
            reservation.CheckInDate = reservations.CheckInDate;
            reservation.CheckOutDate = reservations.CheckOutDate;
            reservation.AdultBedCount = reservations.AdultBedCount;
            reservation.ChildBedCount = reservations.ChildBedCount;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteRezervation(int id)
        {
            var reservation = await _dbContext.reservations.Where(r => !r.IsDeleted && r.Id == id).FirstOrDefaultAsync();
            reservation.IsDeleted = true;
            reservation.DeletedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
