using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_MVC_03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentsRepo;
        public DepartmentController(IDepartmentRepository departmentsRepo)
        {
            _departmentsRepo = departmentsRepo;
        }
        public IActionResult Index()
        { 
            return View();
        }
    }
}
