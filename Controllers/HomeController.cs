using INTEX2.Models;
using INTEX2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace INTEX2.Controllers
{
    public class HomeController : Controller
    {
        private CrashesDbContext _context { get; set; }
        private InferenceSession _session;

        public HomeController(CrashesDbContext temp, InferenceSession session)
        {
            _context = temp;
            _session = session;
        }

        public IActionResult Index()
        {
            
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

            int pageSize = 100;

            var x = new CrashesViewModel
            {
                Crashes = _context.crashdata
                .Where(c => c.COUNTY_NAME == county || county == null)
                .OrderByDescending(c => c.CRASH_DATE)
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

        [HttpGet]
        public IActionResult Calculator()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Calculator(CrashData data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });

            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            ViewBag.predictedvalue = prediction.PredictedValue;
            ViewBag.predictedvalue = Math.Round(ViewBag.PredictedValue);
            result.Dispose();

            return View(data);
        }

    }
}

