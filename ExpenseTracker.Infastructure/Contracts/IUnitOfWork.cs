using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infastructure.Contracts
{
   public interface IUnitOfWork
    {
        void SaveChanges();
        IExpenseCategoryRepository ExpenseCategoryRepository { get; }
        IExpenseRepository ExpenseRepository { get; }
    }
}
