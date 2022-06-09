using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Valsa.Models
{
    public class CustomerLogins
    {
        [Key]
        public int customer_id { get; set; }
        public string password { get; set; }
    }
}
