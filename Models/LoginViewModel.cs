using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Valsa.Models
{
    public class LoginViewModel
    {
        [Required]
        public int CustId { get; set; }

        [Required]
        
        public string Password { get; set; }
    }
}
