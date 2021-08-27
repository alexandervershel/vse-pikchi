using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VsePikchi.Data;
using VsePikchi.Models;

namespace VsePikchi.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Picture> pictures = _db.Pictures;
            return View(pictures);
        }

        public IActionResult RandomGenerator(int id)
        {
            Random rand = new Random();
            int toSkip = rand.Next(1, _db.Pictures.Count());
            Picture picture = _db.Pictures.Skip(toSkip).Take(1).First();
            //return View(_db.Pictures.FirstOrDefault(p=>p.Id == id));
            return View(picture);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
