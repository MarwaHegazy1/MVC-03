using ASP.NET_Core_MVC.DAL.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC.BLL.Interfaces
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        IGenericRepository<T> Repository<T>() where T : ModelBase;
        Task<int> Complete();
    }
}
