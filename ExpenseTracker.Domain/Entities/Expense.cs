using ExpenseTracker.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    /// <summary>
    /// Represents Expenses table.
    /// </summary>
    public class Expense : BaseModel
    {
        /// <summary>
        /// Primary key of the table Expenses.
        /// </summary>
        [Key]
        public int ExpenseID { get; set; }
        /// <summary>
        /// Date of expense.
        /// </summary>
        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Expense date") ]
        [Column(TypeName = "smalldatetime")]
        public DateTime ExpenseDate { get; set; }
        /// <summary>
        /// Expense amount.
        /// </summary>
        [Required(ErrorMessage = "The Amount field is required!")]
        [Display(Name = "Amount")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Referance of the table ExpenseCategories.
        /// </summary>
        [Display(Name = "Expense category")]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual ExpenseCategory ExpenseCategory { get; set; }
    }
}
