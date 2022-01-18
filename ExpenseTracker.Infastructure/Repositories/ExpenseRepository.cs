using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infastructure.Contracts;
using ExpenseTracker.InfructureSqlServre;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infastructure.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        readonly protected DataContext _dcontext;
        public ExpenseRepository(DataContext context) : base(context)
        {
            _dcontext = context;
        }

        public IList<ExpenseVModel> GetAllExpense()
        {
            var list = (from a in _context.Expenses
                        join c in _context.ExpenseCategories on a.CategoryID equals c.CategoryID
                        select new ExpenseVModel
                        {
                            ExpenseID = a.ExpenseID,
                            CategoryName = c.CategoryName,
                            Amount = a.Amount,
                            ExpenseDate = a.ExpenseDate,
                        }).ToList();
            return list;
        }

    }
}
