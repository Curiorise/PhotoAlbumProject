using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcPhotoAlbumProject.Controllers
{
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
