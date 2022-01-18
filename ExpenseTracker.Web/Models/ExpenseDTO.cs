using System;
using System.Collections.Generic;

namespace ExpenseTracker.Web.Models
{
    public class ExpenseDTO
    {
        public int ExpenseID { get; set; }

        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public virtual ExpenseCategoryDTO ExpenseCategory { get; set; }
        public List<ExpenseCategoryDTO>DropCategoryList{ get; set; }
    }
}
