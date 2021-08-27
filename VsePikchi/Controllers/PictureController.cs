using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VsePikchi.Data;
using VsePikchi.Models;
using VkPicturesParser;
using System.Collections.Generic;

namespace VsePikchi.Controllers
{
    public class PictureController : Controller
    {
        private readonly AppDbContext _db;

        public PictureController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int id)
        {
            Picture picture = _db.Pictures.FirstOrDefault(p => p.Id == id);
            return View(picture);
        }

        public IActionResult DownloadImage(string url, string id)
        {
            string fileName = Path.GetFileName(url);
            using (WebClient client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(url), @$"C:\Users\Alexandr\Desktop\pikcha{id}.jpg");
            }
            return RedirectToAction("Index", "Picture", int.Parse(id));
        }

        public IActionResult RandomPicture()
        {
            Random rand = new Random();
            int toSkip = rand.Next(1, _db.Pictures.Count());
            int id = _db.Pictures.Skip(toSkip).Take(1).First().Id;

            return RedirectToAction("RandomGenerator", "Home", id);
        }
    }
}
