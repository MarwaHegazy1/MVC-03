using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.BLL.Repositories;
using ASP.NET_Core_MVC.DAL.Modules;
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
            var departments = _departmentsRepo.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var count = _departmentsRepo.Add(department); 
                if (count > 0)
                    return RedirectToAction(nameof(Index));

            }
            return View(department);
            
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = _departmentsRepo.Get(id.Value);

            if (department is null)
                return NotFound(); // 404

            return View(department);
        }

    }
}
