using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Labb3WebbMVC.Models;
//using Microsoft.EntityFrameworkCore;

namespace Labb3WebbMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private CinemaContext ct;

        public HomeController(ILogger<HomeController> logger)//, CinemaContext ct)
        {
            _logger = logger;
            //this.ct = ct;
        }

        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            //return View(await ct.MovieList.ToListAsync());
            return View();
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
