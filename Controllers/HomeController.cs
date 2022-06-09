



using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Valsa.Models;
using Valsa.Data;
using Microsoft.EntityFrameworkCore;

namespace Valsa.Controllers
{
    public class HomeController : Controller

    {
        private readonly CreateAccountContext _auc;

        public HomeController(CreateAccountContext auc)
        {
            _auc = auc;
        }


        public IActionResult CreateAccount()
        {
            return View();
        }
          public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(CustomerDetail uc)
        {
            try { 
          
                if (ModelState.IsValid)
                {
                    _auc.Add(uc);
                    _auc.SaveChanges();

                    Random random = new Random();

                    Account acc = new Account();
                    acc.account_number = "VAL" + uc.pan_no.ToUpper();
                    acc.account_pin = random.Next(10000, 90000);
                    acc.balance = uc.starting_balance;
                    acc.customer_id = uc.customer_id;

                    CustomerLogin cl = new CustomerLogin();
                    cl.customer_id = uc.customer_id;
                    cl.password = "Val@" + random.Next(1000, 9999);

                    _auc.Add(acc);
                    _auc.Add(cl);
                    _auc.SaveChanges();
                    ModelState.Clear();
                    ViewBag.message = "The user " + uc.first_name + "'s Customer id is: " + uc.customer_id;

            }
        }
            catch (DbUpdateException)
            {
                ModelState.Clear();
                ViewBag.Message = "Another User is registered with same details.";
                return View();
    }

       
            return View();
        }
        public IActionResult Search(int search)
        {
            var alldata = new ViewModel()
            {
               
                CustomerDetails = from m in _auc.customerDetails select m,
               
            };
            
                
                alldata.CustomerDetails = alldata.CustomerDetails.Where(s => s.customer_id == search);
         
            

            return View(alldata);
        }



        public IActionResult Search1(int search)
        {
            var alldata = new ViewModel()
            {
                Accounts = from m in _auc.accounts select m,
              
                CustomerLogins = from m in _auc.CustomerLogins select m
            };
            
                alldata.Accounts = alldata.Accounts.Where(s => s.customer_id == search);
             
                alldata.CustomerLogins = alldata.CustomerLogins.Where(s => s.customer_id == search);
            

            return View(alldata);
        }
        

        public IActionResult Search3(int search)
        {
            var alldata = new ViewModel()
            {
                Accounts = from m in _auc.accounts select m,
                CustomerDetails = from m in _auc.customerDetails select m,
                CustomerLogins = from m in _auc.CustomerLogins select m
            };
            
                alldata.Accounts = alldata.Accounts.Where(s => s.customer_id == search);
                alldata.CustomerDetails = alldata.CustomerDetails.Where(s => s.customer_id == search);
                alldata.CustomerLogins = alldata.CustomerLogins.Where(s => s.customer_id == search);
            

            return View(alldata);
        }

        public ActionResult Edit(int id)
        {
            CustomerDetail customer = _auc.customerDetails.Find(id);

            if (customer == null)
            {
                return View();
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(CustomerDetail customer)
        {
            if (ModelState.IsValid)
            {
                
               _auc.Entry(customer).State = EntityState.Modified;
                _auc.SaveChanges();
                ViewBag.Message = "The details have been updated successfully";
            }

            return View(customer);

        }


        public IActionResult PersonalLoan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PersonalLoan(Loan lc)
        {
            var loanCount = from m in _auc.loans
                            where m.account_number == lc.account_number && m.mobile_number == lc.mobile_number
                            select m;

            if (loanCount.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    var res = from ac in _auc.accounts
                              join cu in _auc.customerDetails on ac.customer_id equals cu.customer_id
                              where ac.account_number == lc.account_number && cu.mobile_number == lc.mobile_number
                              select new { AccountNo = ac.account_number, MobileNo = cu.mobile_number };

                    if (res.Count() != 0)
                    {
                        lc.date_time = DateTime.Now;
                        _auc.loans.Add(lc);
                        _auc.SaveChanges();
                        ViewBag.Message = "Application Submitted Successfully. Your Loan Id is: " + lc.loan_id;

                    }
                    else if (res.Count() == 0)
                    {
                        ViewBag.Message = "Please enter valid details.";
                    }
                }
            }
            else
            {
                ViewBag.Message = "You have already applied for a loan with loan id " + loanCount.FirstOrDefault().loan_id;

            }
            ModelState.Clear();
            return View();
        }

        public ActionResult EmployeeLoanList()
        {
            return View(_auc.loans.ToList());
        }

        public async Task<IActionResult> CloseLoan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanDel = await _auc.loans
                .FirstOrDefaultAsync(m => m.loan_id == id);
            if (loanDel == null)
            {
                return NotFound();
            }

            return View(loanDel);
        }

        [HttpPost, ActionName("CloseLoan")]
        public async Task<IActionResult> CloseConfirmed(int id)
        {
            var employee = await _auc.loans.FindAsync(id);
            _auc.loans.Remove(employee);
            await _auc.SaveChangesAsync();
            return RedirectToAction(nameof(EmployeeLoanList));
        }

        public IActionResult Deposit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Deposit(Transaction tc)
        {
            var res = from m in _auc.accounts where m.account_number == tc.account_number && m.account_pin == tc.account_pin select m;

            
                if (res.Count() != 0)
                {
                    if (tc.amount <= 100000)
                    {
                        tc.transaction_type = "Deposit";
                        tc.date_time = DateTime.Now;
                        tc.updated_balance = res.First().balance + tc.amount;
                        _auc.transactions.Add(tc);

                        _auc.SaveChanges();
                        Account ac = _auc.accounts.Where(x => x.account_number == tc.account_number).First();
                        ac.balance = tc.updated_balance;
                        _auc.Entry(ac).State = EntityState.Modified;
                        _auc.SaveChanges();

                        ViewBag.Message = "Deposit successful. Your Account Balance is : " + tc.updated_balance;

                    }
                    else
                    {
                        ViewBag.Message = "Cannot deposit more than ₹1,00,000";

                    }
                }
                else
                {
                    ViewBag.Message = "Please enter valid account details";
                }
            
            ModelState.Clear();
            return View();
        }

        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Withdraw(Transaction tc)
        {
            var res = from m in _auc.accounts where m.account_number == tc.account_number && m.account_pin == tc.account_pin select m;
            try
            {
                Account ac = _auc.accounts.Where(x => x.account_number == tc.account_number).First();

                if (res.Count() != 0)
                {
                    if (ac.balance - tc.amount >= 1000)
                    {
                        tc.transaction_type = "Withdraw";
                        tc.date_time = DateTime.Now;
                        tc.updated_balance = res.First().balance - tc.amount;
                        _auc.transactions.Add(tc);
                        _auc.SaveChanges();
                        ac.balance = tc.updated_balance;
                        _auc.Entry(ac).State = EntityState.Modified;
                        _auc.SaveChanges();

                        ViewBag.Message = "Withdrawl successful. Your Account Balance is : " + tc.updated_balance;

                    }
                    else
                    {
                        ViewBag.Message = "Not enough funds.";

                    }
                }
                else
                {
                    ViewBag.Message = "Please enter valid account details";
                }
            }
            catch
            {
                ViewBag.Message = "Please enter valid account details";
                return View(tc);
            }

            ModelState.Clear();
            return View();
        }
        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Transfer(Transaction tc)
        {
            var res = from m in _auc.accounts where m.account_number == tc.account_number && m.account_pin == tc.account_pin select m;
            var qer = from m in _auc.accounts where m.account_number == tc.counterparty select m;
            try
            {
                Account ac = _auc.accounts.Where(x => x.account_number == tc.account_number).First();
                Account cc = _auc.accounts.Where(x => x.account_number == tc.counterparty).First();

                if (res.Count() != 0)
                {
                        if (tc.account_number != tc.counterparty)
                        {
                            if (ac.balance - tc.amount >= 1000)
                            {
                                tc.transaction_type = "Transfer";
                                tc.date_time = DateTime.Now;
                                tc.updated_balance = res.First().balance - tc.amount;
                                _auc.transactions.Add(tc);
                                _auc.SaveChanges();
                                ac.balance = tc.updated_balance;
                                cc.balance = qer.First().balance + tc.amount;
                                _auc.Entry(ac).State = EntityState.Modified;
                                _auc.Entry(cc).State = EntityState.Modified;
                                _auc.SaveChanges();

                                ViewBag.Message = "Transfer successful. Your Account Balance is : " + tc.updated_balance;

                            }
                            else
                            {
                                ViewBag.Message = "Not enough funds.";

                            }
                        }
                        else
                        {   
                            ViewBag.Message = "Counterparty account cannot be the same.";
                        }
                  
                }
                else
                { 
                    ViewBag.Message = "Please enter valid account details";
                }
            }
            catch
            {
                ViewBag.Message = "Please enter valid account details";
                return View(tc);
            }

            ModelState.Clear();
            return View();
        }

        
        public IActionResult TransactionHistory(int search)
        {
            
            var alldata = new ViewModel()
            {
                transactionlist = from m in _auc.transactions select m,
            };
         
            alldata.transactionlist = alldata.transactionlist.Where(m => m.account_pin == search);
            
            return View(alldata);
        }
        
    }

}

