using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infastructure.Contracts
{
    public interface IExpenseCategoryRepository : IRepository<ExpenseCategory>
    {
        /// <summary>
        /// Checks whether expense category name is already exists.
        /// </summary>
        /// <param name="expenseCategory">ExpenseCategory</param>
        /// <returns>Boolean</returns>
        bool IsExpenseCategoryDuplicate(ExpenseCategory expenseCategory);
        IList<ExpenseVModel> GetAllExpenseCategory();
        bool IsDeleteID(int categoryId);
    }
}
