using AllupStore.Areas.Admin.Constants;
using HotelProject.Areas.Admin.Models;
using HotelProject.Areas.Admin.Utils;
using HotelProject.Data;
using HotelProject.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _http;
        public SettingsController(AppDbContext dbContext, IHttpContextAccessor http)
        {
            _dbContext = dbContext;
            _http = http;
        }
        public async Task<IActionResult> Settings()
        {
            var user = await UserUtils.GetAuthUser(_dbContext, _http);
            if (user == null)
            {
                return NotFound();
            }
            var model = new SettingsViewModel
            {
                User = user
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSettings(string id, SettingsViewModel model)
        {
            var user = await _dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (user!=null)
            {
                if (model.Photo != null)
                {
                    if (user.Photo != "default-avatar.jpg")
                    {
                        await FileUtils.DelteFile(Path.Combine(FileConstants.ImagePath, user.Photo));
                    }
                    var fileName = FileUtils.CreateFile(FileConstants.ImagePath, model.Photo);
                    user.Photo = fileName;
                }
                if (!string.IsNullOrEmpty(model.Name))
                {
                    user.Name = model.Name;
                }
            }
           await _dbContext.SaveChangesAsync();
            return RedirectToAction("Settings");
        }

        public async Task<IActionResult> EditLayout()
        {
            var baseSlider = await _dbContext.baseSliders.Where(s => !s.IsDeleted).FirstOrDefaultAsync();
            var counter = await _dbContext.counters.Where(c => !c.IsDeleted).ToListAsync();
            var roomSlider = await _dbContext.roomSliders.FirstOrDefaultAsync();
            var commentSlider = await _dbContext.commentSliders.FirstOrDefaultAsync();
            var model = new EditLayoutViewModel
            {
                CommentSlider = commentSlider,
                roomSliders = roomSlider,
                OriginalBaseSlider = baseSlider,
                blogItems = await _dbContext.blogItems.Where(b => !b.IsDeleted).ToListAsync(),
                BaseSlider = new BaseSliderViewModel
                {
                    Desc = baseSlider?.Desc,
                    Title = baseSlider?.Title,


                },
                counters = counter,
                roomPhotoSlider = await _dbContext.roomPhotoSliders.FirstOrDefaultAsync()
                
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LayoutSaveSettings(EditLayoutViewModel model)
        {
            var baseSlider = await _dbContext.baseSliders.Where(b => !b.IsDeleted).FirstOrDefaultAsync();
            if (baseSlider!=null)
            {
                baseSlider.Title = model.BaseSlider.Title;
                baseSlider.Desc = model.BaseSlider.Desc;
                if (model.BaseSlider.Photo!=null)
                {
                  await  FileUtils.DelteFile(Path.Combine(FileConstants.ImagePath, baseSlider.Photo));
                    var fileName = FileUtils.CreateFile(FileConstants.ImagePath, model.BaseSlider.Photo);
                    baseSlider.Photo = fileName;
                }
            }
            else
            {
                var fileName = "";
                if (model.BaseSlider.Photo != null)
                {
                    
                    var Name = FileUtils.CreateFile(FileConstants.ImagePath, model.BaseSlider.Photo);
                    fileName = Name;
                }
                var slider = new BaseSlider
                {
                    Title = model.BaseSlider.Title,
                    Desc = model.BaseSlider.Desc,
                    Photo = fileName


                };
              await  _dbContext.baseSliders.AddAsync(slider);
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("EditLayout");
        }


        [HttpPost]
        public async Task<IActionResult> AddBlog(EditLayoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (model.BlogViewModel.Image==null)
            {
                ModelState.AddModelError("Image","Please select an image");
                return View();
            }
            var fileName = FileUtils.CreateFile(FileConstants.ImagePath,model.BlogViewModel.Image);

            var blogItem = new BlogItem
            {
                Title = model.BlogViewModel.Title ??" ",
                Text = model.BlogViewModel.Text ?? " ",
                ButtonText = model.BlogViewModel.ButtonText ?? " ",
                ButtonUrl = model.BlogViewModel.ButtonUrl ?? " ",
                Image = fileName
            };
           await _dbContext.blogItems.AddAsync(blogItem);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("EditLayout");
        }


        public async Task<IActionResult> UpdateBlogItem(int id)
        {
            var blogItem = await _dbContext.blogItems.Where(b => !b.IsDeleted&&b.Id==id).FirstOrDefaultAsync();
            if (blogItem==null)
            {
                return NotFound();
            }
            var model = new BlogViewModel
            {
                ButtonText = blogItem.ButtonText,
                ButtonUrl = blogItem.ButtonUrl,
                Text = blogItem.Text,
                Title = blogItem.Title,
                Id= blogItem.Id,
            };
            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> UpdateBlogItem(int id,BlogViewModel model)
        {
            var _blogItem = await _dbContext.blogItems.Where(b => !b.IsDeleted && b.Id == id).FirstOrDefaultAsync();
            if (_blogItem==null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (model.Image == null)
            {
                ModelState.AddModelError("Image", "Please select an image");
                return View();
            }
           await FileUtils.DelteFile(Path.Combine(FileConstants.ImagePath,_blogItem.Image));
            var fileName = FileUtils.CreateFile(FileConstants.ImagePath, model.Image);


            _blogItem.Title = model.Title??"";
            _blogItem.Image = fileName??"";
            _blogItem.Text = model.Text??"";
            _blogItem.ButtonText = model.ButtonText??"";
            _blogItem.ButtonUrl = model.ButtonUrl??"";
           
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("EditLayout");
        }

    

        public async Task<IActionResult> DeleteBlogItem(int id)
        {
            var blogItem = await _dbContext.blogItems.Where(b => !b.IsDeleted && b.Id == id).FirstOrDefaultAsync();
            if (blogItem==null)
            {
                return NotFound();
            }
            blogItem.IsDeleted = true;
            blogItem.DeletedDate = DateTime.Now;
          await  _dbContext.SaveChangesAsync();
            
            return RedirectToAction("EditLayout");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhotoSlider(EditLayoutViewModel model)
        {
            var photoSlider = await _dbContext.roomPhotoSliders.FirstOrDefaultAsync();
            if (photoSlider == null)
            {
               var PhotoSlider = new RoomPhotoSlider
                {
                    Desc = model.roomPhotoSlider.Desc,
                    Title = model.roomPhotoSlider.Title,
                };
               await _dbContext.roomPhotoSliders.AddAsync(PhotoSlider);
            }
            else
            {
                photoSlider.Title = model.roomPhotoSlider.Title ?? "";
                photoSlider.Desc = model.roomPhotoSlider.Desc ?? "";
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

           
           await _dbContext.SaveChangesAsync();
            return RedirectToAction("EditLayout");
        }


        [HttpPost]
        public async Task<IActionResult> AddCounterItem(EditLayoutViewModel model)
        {
            var counterItem = new Counter
            {
                Count = model.AddCounterViewModel.Count,
                CountText = model.AddCounterViewModel.CountText
                
            };
          await _dbContext.counters.AddAsync(counterItem);
          await  _dbContext.SaveChangesAsync();
            return RedirectToAction("EditLayout");
        }


        public async Task<IActionResult> DeleteCounterItem(int id)
        {
            var counterItem = await _dbContext.counters.Where(c => !c.IsDeleted && c.Id == id).FirstOrDefaultAsync();
            if (counterItem==null)
            {
                return NotFound();
            }
            counterItem.IsDeleted = true;
            counterItem.DeletedDate = DateTime.Now;
          await _dbContext.SaveChangesAsync();
            return RedirectToAction("EditLayout");
        }

        public async Task<IActionResult> UpdateCounterItem(int id)
        {
            var counterItem = await _dbContext.counters.Where(c => !c.IsDeleted && c.Id == id).FirstOrDefaultAsync();
            if (counterItem == null)
            {
                return NotFound();
            }
            var model = new UpdateCounterViewModel
            {
                Count = counterItem.Count,
                CountText = counterItem.CountText,
                Id = counterItem.Id

            };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCounterItem(int id,UpdateCounterViewModel model)
        {
            var counterItem = await _dbContext.counters.Where(c => !c.IsDeleted && c.Id == model.Id).FirstOrDefaultAsync();
            if (counterItem==null)
            {
                return NotFound();
            }
            counterItem.Count = model.Count;
            counterItem.CountText = model.CountText;
          await  _dbContext.SaveChangesAsync();
            return RedirectToAction("EditLayout");
        }


        [HttpPost]
        public async Task<IActionResult> EditRoomSlider( EditLayoutViewModel model)
        {
            var roomSlider = await _dbContext.roomSliders.FirstOrDefaultAsync();
            if (roomSlider==null)
            {
                var NewRoomSlider = new RoomSlider
                {
                    Title = model.roomSliders.Title,
                    Desc = model.roomSliders.Desc,
                };
               await _dbContext.roomSliders.AddAsync(NewRoomSlider);
            }
            else
            {
                roomSlider.Title = model.roomSliders.Title;
                roomSlider.Desc = model.roomSliders.Desc;
            }
          

          await  _dbContext.SaveChangesAsync();

            return RedirectToAction("EditLayout");
        }



        [HttpPost]
        public async Task<IActionResult> EditCommentSlider(EditLayoutViewModel model)
        {
            var commentSlider = await _dbContext.commentSliders.FirstOrDefaultAsync();
            if (commentSlider == null)
            {
                var NewCommentSlider = new CommentSlider
                {
                    Title = model.CommentSlider.Title,
                    Desc = model.CommentSlider.Desc,
                };
                await _dbContext.commentSliders.AddAsync(NewCommentSlider);
            }
            else
            {
                commentSlider.Title = model.CommentSlider.Title;
                commentSlider.Desc = model.CommentSlider.Desc;
            }


            await _dbContext.SaveChangesAsync();

            return RedirectToAction("EditLayout");
        }
    }
}
