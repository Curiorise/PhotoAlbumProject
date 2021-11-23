using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcPhotoAlbumProject.Models;
using MvcPhotoAlbumProject.Models.AppDBContext;
using MvcPhotoAlbumProject.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MvcPhotoAlbumProject.Controllers
{
    public class PhotoModelsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotoModelsController(AppDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        // GET: PhotoModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Photos.ToListAsync());
        }

        // GET: PhotoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoModel = await _context.Photos
                .FirstOrDefaultAsync(m => m.Id == id);

            var photoViewModel = new PhotoViewModel()
            {
                Id = photoModel.Id,
                Geolocation = photoModel.Geolocation,
                Tags = photoModel.Tags,
                CapturedDate = photoModel.CapturedDate,
                CapturedBy = photoModel.CapturedBy,
                ExistingPhoto = photoModel.Photo
            };

            if (photoModel == null)
            {
                return NotFound();
            }

            return View(photoModel);
        }

        // GET: PhotoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhotoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Geolocation,Tags,CapturedDate,CapturedBy,Photo")] PhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(photoViewModel);
                PhotoModel photoModel = new PhotoModel
                {
                    Geolocation = photoViewModel.Geolocation,
                    Tags = photoViewModel.Tags,
                    CapturedBy = photoViewModel.CapturedBy,
                    CapturedDate = photoViewModel.CapturedDate,
                    Photo = uniqueFileName
                };

                _context.Add(photoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(photoViewModel);
        }

        private string ProcessUploadedFile(PhotoViewModel photoViewModel)
        {
            string uniqueFileName = null;

            if (photoViewModel.UploadedPhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, FileLocation.FileUploadFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photoViewModel.UploadedPhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photoViewModel.UploadedPhoto.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        // GET: PhotoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoModel = await _context.Photos.FindAsync(id);
            var photosViewModel = new PhotoViewModel()
            {
                Id = photoModel.Id,
                Geolocation = photoModel.Geolocation,
                Tags = photoModel.Tags,
                CapturedBy = photoModel.CapturedBy,
                CapturedDate = photoModel.CapturedDate,
                ExistingPhoto = photoModel.Photo

            };

            if (photoModel == null)
            {
                return NotFound();
            }
            return View(photosViewModel);
        }

        // POST: PhotoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Geolocation,Tags,CapturedDate,CapturedBy,Photo")] PhotoViewModel model)
        {
            if(ModelState.IsValid)
            { 
                var photoModel = await _context.Photos.FindAsync(model.Id);
                photoModel.Geolocation = model.Geolocation;
                photoModel.Tags = model.Tags;
                photoModel.CapturedBy = model.CapturedBy;
                photoModel.CapturedDate = model.CapturedDate;

                if(model.UploadedPhoto != null)
                {
                    if(model.ExistingPhoto != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, FileLocation.FileUploadFolder, model.ExistingPhoto);
                        System.IO.File.Delete(filePath);
                    }

                    photoModel.Photo = ProcessUploadedFile(model);
                }
                
                _context.Update(photoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: PhotoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoModel = await _context.Photos
                .FirstOrDefaultAsync(m => m.Id == id);

            var photoViewModel = new PhotoViewModel()
            {
                Id = photoModel.Id,
                Geolocation = photoModel.Geolocation,
                Tags = photoModel.Tags,
                CapturedBy = photoModel.CapturedBy,
                CapturedDate = photoModel.CapturedDate,
                ExistingPhoto = photoModel.Photo
            };

            if (photoModel == null)
            {
                return NotFound();
            }

            return View(photoViewModel);
        }

        // POST: PhotoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoModel = await _context.Photos.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), FileLocation.DeleteFileFromFolder, photoModel.Photo);
            _context.Photos.Remove(photoModel);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoModelExists(int id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
