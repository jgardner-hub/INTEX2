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
            //ViewBag.Crashes = _context.crashdata.ToList();
            ViewBag.Counties = _context.crashdata.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            ViewBag.form = "add";
            return View();
        }

        [HttpPost]
        public IActionResult AddCrash(Crash c)
        {
            if (ModelState.IsValid)
            {
                if (c.CRASH_ID == 0)
                {
                    _context.crashdata.Add(c);
                }
                else
                {
                    _context.Update(c);
                }
                //var x = _context.crashdata.ToList();
                _context.SaveChanges();
                //ViewBag.Counties = _context.crashdata.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
                return RedirectToAction("AdminCrashSummary");
            }

            else
            {
                //ViewBag.Crashes = _context.crashdata.ToList();
                ViewBag.Counties = _context.crashdata.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
                return View(c);
            }

        }

        [HttpGet]
        public IActionResult Edit(int CrashId)
        {
            var input = _context.crashdata.Single(x => x.CRASH_ID == CrashId);
            ViewBag.Counties = _context.crashdata.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x).ToList();
            //ViewBag.Crashes = _context.crashdata.ToList();
            ViewBag.form = "edit";
            

            return View("AddCrash", input);
        }

        [HttpPost]
        public IActionResult Edit(Crash crash)
        {
            _context.Update(crash);
            _context.SaveChanges();
            //var x = _context.crashdata.ToList();
            return RedirectToAction("AdminCrashSummary");
        }

        [HttpGet]
        public IActionResult Delete(int CrashId)
        {
            var input = _context.crashdata.Single(x => x.CRASH_ID == CrashId);
            //_context.crashdata.Remove(input);
            //_context.SaveChanges();

            return View(input);
        }

        [HttpPost]
        public IActionResult Delete(Crash crash)
        {
            _context.crashdata.Remove(crash);
            _context.SaveChanges();

            return RedirectToAction("AdminCrashSummary");
        }

        public IActionResult CrashSummary(string county, int pageNum = 1)
        {

            int pageSize = 100;

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
                    TotalNumCrashes =
                    (county == null ?
                    _context.crashdata.Count()
                    : _context.crashdata.Where(x => x.COUNTY_NAME == county).Count()),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }

            };
            return View(x);
        }


        public IActionResult AdminCrashSummary(string county, int pageNum = 1)
        {
            int pageSize = 100;

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

    }
}

