using INTEX2.Models;
using INTEX2.Models.ViewModels;
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
        [HttpGet]
        public IActionResult CrashSummary(int pageNum = 1)
        {
            int pageSize = 1000;

            var x = new CrashesViewModel
            {
                Crashes = _context.crashdata
                .OrderBy(c => c.CRASH_ID)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList(),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = _context.crashdata.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }

            };

            return View(x);
        }
        [HttpPost]
        public IActionResult pageJump(int pageNum)
        {
            int pageSize = 1000;

            var x = new CrashesViewModel
            {
                Crashes = _context.crashdata
                .OrderBy(c => c.CRASH_ID)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList(),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = _context.crashdata.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }

            };

            return View("CrashSummary", x);
        }

    }
}
