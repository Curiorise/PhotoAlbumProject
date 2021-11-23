using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MvcPhotoAlbumProject.ViewModels
{
    public class PhotoViewModel : EditPhotoViewModel
    {
        [Required]
        [Display(Name = "Geolocation")]
        public string Geolocation { get; set; }

        [Required]
        public string Tags { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Captured date")]
        public DateTime CapturedDate { get; set; }


        [Required]
        public string CapturedBy { get; set; }

        public IFormFile Photo { get; set; }
    }
}
