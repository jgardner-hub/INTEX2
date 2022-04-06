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
            ViewBag.Crashes = _context.crashdata.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult AddCrash()
        {
            ViewBag.Crashes = _context.crashdata.ToList();
            ViewBag.Counties = _context.crashdata.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddCrash(Crash c)
        {
            if (ModelState.IsValid)
            {
                _context.crashdata.Add(c);
                _context.SaveChanges();
                var x = _context.crashdata.ToList();
                return View("Index", x);
            }

            else
            {
                //ViewBag.Crashes = _context.crashdata.ToList();
                ViewBag.Counties = _context.crashdata.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
                return View(c);
            }

        }

        public IActionResult CrashSummary(string county, int pageNum = 1)
        {
            int pageSize = 1000;

            var x = new CrashesViewModel
            {
                Crashes = _context.crashdata
                .Where(c => c.COUNTY_NAME == county || county == null)
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

        public IActionResult AdminCrashSummary(string county, int pageNum = 1)
        {
            int pageSize = 1000;

            var x = new CrashesViewModel
            {
                Crashes = _context.crashdata
                .Where(c => c.COUNTY_NAME == county || county == null)
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

        //public IActionResult pageJump(int pageNum = 1)
        public IActionResult pageJump(string county, int pageNum = 1)
        {
            int pageSize = 100;
            pageNum = Convert.ToInt32(pageNum);

            var x = new CrashesViewModel
            {
                Crashes = _context.crashdata
                .Where(c => c.COUNTY_NAME == county || county == null)
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

            return RedirectToAction("CrashSummary", x);

        }

    }
}
