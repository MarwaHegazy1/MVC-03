using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.DAL.Modules;
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
        public IActionResult Create()
        { 
            return View();
        }
  
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _employeesRepo.Add(employee);
                if (count > 0)
                
                    return RedirectToAction(nameof(Index));

            }
            return View(employee);
        }
    }

}
