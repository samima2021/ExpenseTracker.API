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
        readonly protected DataContext _dataContext;
        public ExpenseCategoryRepository(DataContext context) : base(context)
        {
            _dataContext = context;

        }

        public IList<ExpenseVModel> GetAllExpenseCategory()
        {
            var list = (from c in _context.ExpenseCategories
                      
                        where c.IsRowDeleted.Equals(false)
                        select new ExpenseVModel
                        {
                           
                            CategoryName = c.CategoryName,
                          
                        }).ToList();
            return list;
        }

        public bool IsExpenseCategoryDuplicate(ExpenseCategory expenseCategory)
        {
            try
            {
                var categoryInDb = _context.ExpenseCategories.FirstOrDefault(c => c.CategoryName.ToLower().Replace(" ", "-") == expenseCategory.CategoryName.ToLower().Replace(" ","-"));

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
