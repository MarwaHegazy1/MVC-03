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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //use inherite dbContext 
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(E=>E.Address.ToLower()== address.ToLower());
        }

        public IQueryable<Employee> SearchByNmae(string name)
        {
            return _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));       
        }
    }
}
