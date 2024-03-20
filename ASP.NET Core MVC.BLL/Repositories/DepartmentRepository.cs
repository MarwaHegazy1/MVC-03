using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.DAL.Data;
using ASP.NET_Core_MVC.DAL.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC.BLL.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {   //Object member 
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Department entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }
        public Department Get(int id)
        {
            //Part 08 27:00
            //Search Local First then Search DB 
           // return _dbContext.Departments.Find(10);
           return _dbContext.Find<Department>(id);//EF Core 3.1 NEW Feature
        }
        public IEnumerable<Department> GetAll()
        {
           return _dbContext.Departments.AsNoTracking().ToList();
        }

       
    }
}
