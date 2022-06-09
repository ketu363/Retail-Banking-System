using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Valsa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Valsa.Data;
namespace Dbfirstapp.Controllers
{
    public class LoginController : Controller
    {
        private readonly CreateAccountContext _context;
      
      
        
        public LoginController(CreateAccountContext context)
        {
            _context = context;
           
        }
       
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userdetails = await _context.Employee
                .SingleOrDefaultAsync(m => m.CustId == model.CustId && m.Passw == model.Password);
                if (userdetails == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }
             

            }
            else
            {
                return View("Index");
            }
            return RedirectToAction("Index", "Employee");
        }

        public void ValidationMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }

        }
    }
}