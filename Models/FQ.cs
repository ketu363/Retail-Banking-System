using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Valsa.Models
{
public class FQ
    {
    
        [Key]
         [Display(Name = "customer_id")]
        public int customer_id { get; set; }

        [Display(Name = "feedback")]
        public string Feedback { get; set; }

        
        [Display(Name = "queries")]
        public string Queries { get; set; }

        

        

    }

}