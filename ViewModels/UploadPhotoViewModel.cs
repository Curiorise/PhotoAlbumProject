using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MvcPhotoAlbumProject.ViewModels
{
    public class UploadPhotoViewModel
    {
        [Display(Name = "Image")]
        public IFormFile UploadedPhoto { get; set; }
    }
}
