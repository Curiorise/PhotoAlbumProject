using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPhotoAlbumProject.ViewModels
{
    public class EditPhotoViewModel : UploadPhotoViewModel
    {
        public int Id { get; set; }
        public string ExistingPhoto { get; set; }
    }
}
