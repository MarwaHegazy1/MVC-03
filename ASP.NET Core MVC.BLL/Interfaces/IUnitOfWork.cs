using ASP.NET_Core_MVC.DAL.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC.BLL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        ///Or, IDepartmentRepository doesnt have extra Methods
        ///  public IGenericRepository<Department> DepartmentRepository { get; set; }
        int Complete();
    }
}
