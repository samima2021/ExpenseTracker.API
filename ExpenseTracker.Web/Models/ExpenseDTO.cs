using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Web.Models
{
    public class ExpenseDTO : BaseDTO
    {
        public int ExpenseID { get; set; }
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Expense date")]
        [Column(TypeName = "smalldatetime")]
        public DateTime ExpenseDate { get; set; }

        [Required(ErrorMessage = "The Amount field is required!")]
        [Display(Name = "Amount")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter the CategoryName")]
        public string CategoryName { get; set; }
        public List<ExpenseCategoryDTO> CategoryDropDownList { get; set; }
        public virtual ExpenseCategoryDTO ExpenseCategory { get; set; }
        
    }
}
