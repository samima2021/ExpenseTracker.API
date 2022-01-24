using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class ExpenseVModel
    {
        
        public int ExpenseID { get; set; }
        public int CategoryID { get; set; }

        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Please enter the CategoryName")]
        [StringLength(60)]
        [Display(Name = "Expense category")]
        public string CategoryName { get; set; }
    }
}
