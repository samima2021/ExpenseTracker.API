using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.InfructureSqlServre
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionString = "Data Source=URANUS; Initial Catalog=ExpenseTracker; User Id=sa; Password=abcd123!";

        //    optionsBuilder.UseSqlServer(connectionString);
        //}
        /// <summary>
        /// ExpenseCategories table.
        /// </summary>
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        /// <summary>
        /// Expenses table.
        /// </summary>
        public DbSet<Expense> Expenses { get; set; }
    }
}
