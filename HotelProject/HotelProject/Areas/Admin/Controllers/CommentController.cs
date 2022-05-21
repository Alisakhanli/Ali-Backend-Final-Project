using HotelProject.Areas.Admin.Models;
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
    public class CommentController : Controller
    {
        private readonly AppDbContext _dbContext;


        public CommentController(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int id,LayoutViewModel model)
        {
            var room = await _dbContext.Rooms.Where(r => !r.IsDeleted && r.Id == id).FirstOrDefaultAsync();
            if (room==null)
            {
                return NotFound();
            }
            var user = await _dbContext.Users.Where(u => u.Id == model.roomCommentViewModel.UserId).FirstOrDefaultAsync();
            if (user==null)
            {
                return NotFound();
            }
            var comment = new RoomComment
            {
                Room = room,
                user = user,
                Text = model.roomCommentViewModel.Text,

            };
           await _dbContext.roomComments.AddAsync(comment);
           await _dbContext.SaveChangesAsync();
            return RedirectToAction("RoomDetails","Home",new { id = room.Id,area=""});
        }

        public async Task<IActionResult> Comments()
        {
            var roomComments = await _dbContext.roomComments.Include(g=>g.Room).Include(l=>l.user).Where(c => !c.IsDeleted).ToListAsync();

            return View(roomComments);
        }

        
        public async Task<IActionResult> EditComment(int id)
        {
            var comment = await _dbContext.roomComments.Where(c => !c.IsDeleted && c.Id == id).FirstOrDefaultAsync();

            return View(comment);
        }


        [HttpPost]

        public async Task<IActionResult> UpdateComment(int id,RoomComment model)
        {
            var updateComment = await _dbContext.roomComments.Where(c => !c.IsDeleted && c.Id == id).FirstOrDefaultAsync();
            updateComment.Text = model.Text;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Comments");
        }


        public async Task<IActionResult> DeleteComment(int id)
        {
            var deleteComment = await _dbContext.roomComments.Where(c => !c.IsDeleted && c.Id == id).FirstOrDefaultAsync();
            deleteComment.IsDeleted = true;
            deleteComment.DeletedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Comments");
        }

    }
}
