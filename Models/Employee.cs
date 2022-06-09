using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Valsa.Models
{
    public class Employee
    {
        [Key]
        public int CustId { get; set; }
        public string Passw { get; set; }
    }
}
