using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcPhotoAlbumProject.Models
{
    public class PhotoModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Geolocation")]
        public string Geolocation { get; set; }

        [Required]
        [StringLength(100)]
        public string Tags { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Captured date")]
        public DateTime CapturedDate { get; set; }


        [Required]
        [StringLength(255)]
        public string CapturedBy { get; set; }

        [Required]
        [Display(Name = "Photo")]
        public string Photo { get; set; }
    }
}

