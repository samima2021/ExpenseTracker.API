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
        public ExpenseRepository(DataContext context) : base(context)
        {

        }
    }
}
