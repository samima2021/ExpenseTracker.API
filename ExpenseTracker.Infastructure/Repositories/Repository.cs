﻿using ExpenseTracker.Infastructure.Contracts;
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
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _context;
        public Repository(DataContext context)
        {
            this._context = context;
        }
        public virtual T Add(T entity)
        {
            try
            {
                return _context.Set<T>().Add(entity).Entity;
            }
            catch
            {
                throw;
            }
        }


        public virtual  void Delete(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
            }
            catch
            {
                throw;
            } 
            
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>()
                    .AsNoTracking()
                    .FirstOrDefault(predicate);
            }
            catch
            {
                throw;
            }
        }
        public virtual T Get(int id)
        {
            try
            {
                return _context.Set<T>().Find(id);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking()
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking()
                    .Where(predicate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
