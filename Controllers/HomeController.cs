using INTEX2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Controllers
{
    public class HomeController : Controller
    {
        private CrashesDbContext _context { get; set; }

        public HomeController(CrashesDbContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CrashSummary()
        {
            var crashes = _context.crashdata.ToList();
            return View(crashes);
        }

    }
}
