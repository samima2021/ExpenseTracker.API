using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infastructure.Contracts
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save data to the database
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Create properties for Expense categories
        /// </summary>
        IExpenseCategoryRepository ExpenseCategoryRepository { get; }
        /// <summary>
        /// Create properties for Expense
        /// </summary>
        IExpenseRepository ExpenseRepository { get; }

    }
}