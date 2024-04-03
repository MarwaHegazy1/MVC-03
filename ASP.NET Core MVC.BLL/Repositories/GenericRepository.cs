using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.DAL.Data;
using ASP.NET_Core_MVC.DAL.Modules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
          =>  _dbContext.Set<T>().Add(entity);
            //_dbContext.Add(entity);

      
        public void Update(T entity)
        =>   _dbContext.Set<T>().Update(entity);
            //_dbContext.Update(entity);
        
        public void Delete(T entity)
       =>  _dbContext.Set<T>().Remove(entity);
            //_dbContext.Remove(entity);
       
        public async Task<T> GetAsync(int id)
            => await _dbContext.FindAsync<T>(id);
        
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            //return _dbContext.Set<T>().AsNoTracking().ToList();
            if (typeof(T) == typeof(Employee))
                return  (IEnumerable<T>) await _dbContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            else
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
    }
}
