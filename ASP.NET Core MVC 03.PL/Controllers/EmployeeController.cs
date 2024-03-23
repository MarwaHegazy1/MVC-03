using ASP.NET_Core_MVC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeesRepo;

        public EmployeeController(IEmployeeRepository employeesRepo)
        {
            _employeesRepo = employeesRepo;
        }
        public IActionResult Index()
        {
            var employees = _employeesRepo.GetAll();
            return View(employees);
        }
    }
}
