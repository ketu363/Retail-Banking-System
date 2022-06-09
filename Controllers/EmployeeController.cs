using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Valsa.Models;
using Valsa.Data;
namespace footerrrrr.Controllers
{
    public class EmployeeController : Controller
    {
              private readonly CreateAccountContext _auc;

        public EmployeeController(CreateAccountContext auc)
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
        public IActionResult Feedbackview()
        {
               
            return View(_auc.FQ.ToList());
        
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
