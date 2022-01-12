using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infastructure.Contracts;
using ExpenseTracker.InfructureSqlServre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infastructure.Repositories
{
    public class ExpenseCategoryRepository : Repository<ExpenseCategory>, IExpenseCategoryRepository
    {
        public ExpenseCategoryRepository(DataContext context) : base(context)
        {

        }
        public bool IsExpenseCategoryDuplicate(ExpenseCategory expenseCategory)
        {
            try
            {
                var categoryInDb = _context.ExpenseCategories.FirstOrDefault(c => c.CategoryName.ToLower() == expenseCategory.CategoryName.ToLower());

                if (categoryInDb != null)
                {
                    if (categoryInDb.CategoryID != expenseCategory.CategoryID)
                        return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
