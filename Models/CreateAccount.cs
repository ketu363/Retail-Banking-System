using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Valsa.Models
{
    public class CustomerDetail
    {
        [Key]
        public int customer_id { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Please enter a valid first name")]

        [Column(TypeName = "varchar(50)")]
        public string first_name { get; set; }

        
        [Column(TypeName = "varchar(50)")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Please enter a valid last name")]

        public string last_name { get; set; }

        [Required(ErrorMessage ="Please select your gender")]
        [Column(TypeName = "varchar(50)")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Please enter your DOB")]
        public DateTime dob { get; set; }

        [Required(ErrorMessage = "Please enter your contact number")]
        [Column(TypeName = "varchar(10)")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid contact number")]
        public string mobile_number { get; set; }

        [Required(ErrorMessage = "Please Enter your Address")]
        [Column(TypeName = "varchar(max)")]
        public string address { get; set; }

        [Required(ErrorMessage = "PAN Number is required")]
        [Column(TypeName = "varchar(50)")]
        public string pan_no { get; set; }


        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression("^[\\w!#$%&'*+/=?`{|}~^-]+(?:\\.[\\w!#$%&'*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",ErrorMessage ="Please enter a valid email-id")]
        [Column(TypeName = "varchar(50)")]
        public string email { get; set; }

        [Required(ErrorMessage ="Please enter the starting balance")]
        [Column(TypeName="money")]
        [RegularExpression("^[1-9][0-9]{3}[0-9.]*", ErrorMessage = "The minimum balance is ₹1000")]
        public decimal starting_balance { get; set; }

    }

    public class Account
    {
        [Key]
        
        public string account_number { get; set; }

        [ForeignKey("CustomerDetail")]
        [Required]
        public int customer_id { get; set; }
        public CustomerDetail CustomerDetail { get; set; }

       
        [Required]
        public int account_pin { get; set; }


        [Required]
        [Column(TypeName = "money")]
        public decimal balance { get; set; }

    
    }

    public class CustomerLogin
    {
        [Key]
        public int customer_id { get; set; }


        [Required(ErrorMessage ="Please Enter the password")]
        [StringLength(50,MinimumLength =6,ErrorMessage ="The Password must have 6 characters or more")]

        public string password { get; set; }
    }

    public class EmployeeLogin
    {
        [Key]
        public int employee_id { get; set; }


        [Required(ErrorMessage = "Please Enter the password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The Password must have 6 characters or more")]

        public string password { get; set; }
    }

    public class ViewModel
    {
        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<CustomerDetail> CustomerDetails { get; set; }
        public IEnumerable<CustomerLogin> CustomerLogins { get; set; }
        public IEnumerable<Transaction>  transactionlist { get; set; }
    }



    public class Loan
    {
        [Key]
        public int loan_id { get; set; }

        [ForeignKey("Account")]
        [Required(ErrorMessage = "Please enter your account number")]
        public string account_number { get; set; }
        public Account Account { get; set; }

        [Required(ErrorMessage = "Please enter your contact number")]
        [Column(TypeName = "varchar(10)")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid contact number")]
        public string mobile_number { get; set; }

        [Required(ErrorMessage = "Please enter the loan amount")]
        [RegularExpression("^[1-9][0-9]{4}[0-9.]*", ErrorMessage = "Minimum loan amount is ₹10000")]
        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime date_time { get; set; }

        [Required(ErrorMessage = "Please select your employment type")]
        [Column(TypeName = "varchar(30)")]
        public string EmpType { get; set; }

        [Required(ErrorMessage = "Please enter your monthly income")]
        [Column(TypeName = "money")]
        public decimal income { get; set; }

        [Required(ErrorMessage = "Please enter your organization name")]
        [Column(TypeName = "varchar(100)")]
        public string orgName { get; set; }
        
        [Required(ErrorMessage = "Please select loan purpose")]
        [Column(TypeName = "varchar(max)")]
        public string purpose { get; set; }

    }



    public class Transaction
    {
        [Key]
        public int reference_id { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string transaction_type { get; set; }

        [ForeignKey("Account")]
        [Required(ErrorMessage ="Please enter your account number")]
        public string account_number { get; set; }
        public Account Account { get; set; }

        [Required(ErrorMessage = "Please enter your account pin")]
        public int account_pin { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string counterparty { get; set; }

        [Required]
        [Column(TypeName ="date")]
        public DateTime date_time { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal updated_balance { get; set; }

        [Required(ErrorMessage = "Please enter the amount to transfer")]
        [Column(TypeName = "money")]
        public decimal amount { get; set; }
    }
}
