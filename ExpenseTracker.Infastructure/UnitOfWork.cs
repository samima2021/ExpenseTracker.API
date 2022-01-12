using ExpenseTracker.Infastructure.Contracts;
using ExpenseTracker.Infastructure.Repositories;
using ExpenseTracker.InfructureSqlServre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExpenseTracker.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DataContext dbcontext;
        public UnitOfWork(DataContext context)
        {
            this.dbcontext = context;

        }

        private IExpenseCategoryRepository expenseCategoryRepository;
        public IExpenseCategoryRepository ExpenseCategoryRepository
        {
            get
            {
                if (expenseCategoryRepository == null)
                    expenseCategoryRepository = new ExpenseCategoryRepository(dbcontext);

                return expenseCategoryRepository;
            }
        }
        public IExpenseRepository ExpenseRepository { get; }

        public void SaveChanges()
        {
            dbcontext.SaveChanges();
        }
    }
}
