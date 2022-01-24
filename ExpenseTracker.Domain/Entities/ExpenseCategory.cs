using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    /// <summary>
    /// Represents ExpenseCategory table.
    /// </summary>
    public class ExpenseCategory : BaseModel
    {
        /// <summary>
        /// Primary key of the table ExpenseCategories.
        /// </summary>
        [Key]
        public int CategoryID { get; set; }

        /// <summary>
        /// Expense category name.
        /// </summary>
        [Required(ErrorMessage = "Please enter the CategoryName")]
        [StringLength(60)]
        [Display(Name = "Expense category")]
        public string CategoryName { get; set; }

        public virtual IQueryable<Expense>? Expenses { get; set; }

    }
}
