using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infastructure.Contracts
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        IEnumerable<Expense> getAllEnpenses();
        
    }
}
