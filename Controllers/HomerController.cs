using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Valsa.Models;
using System.ComponentModel.DataAnnotations;
using Valsa.Data;
namespace footerrrrr.Controllers
{
    public class HomerController : Controller
    {
        private readonly CreateAccountContext _auc;

        public HomerController(CreateAccountContext auc)
        {
            _auc = auc;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

         public IActionResult Overview()
        {
            return View();
        }
        public IActionResult PodDetails()
        {
            return View();
        }
        public IActionResult Vision()
        {
            return View();
        }
         public IActionResult Ways()
        {
            return View();
        }
         public IActionResult Instaservices()
        {
            return View();
        }
         public IActionResult Cbg()
        {
            return View();
        }
        public IActionResult Transactions()
        {
            return View();
        }
        public IActionResult ImportantInfo()
        {
            return View();
        }
        public IActionResult WriteToUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult WriteToUs(FQ uc)
        {
             if (ModelState.IsValid)
            {
                _auc.Add(uc);
                _auc.SaveChanges();

               
                
                ModelState.Clear();
                ViewBag.message ="Thank You for giving feedback........It will be processed soon";

            } 

            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
