using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.DAL.Modules;
using ASP.NET_Core_MVC_03.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NET_Core_MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeesRepo;
      //  private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IMapper mapper,IEmployeeRepository employeesRepo,/*IDepartmentRepository departmentRepository ,*/ IWebHostEnvironment env)
        {
            _mapper = mapper;
            _employeesRepo = employeesRepo;
           // _departmentRepository = departmentRepository;
            _env = env;
        }
        public IActionResult Index(string searchInp)
        {
            // TempData.Keep();

            //  ViewData["Message"] = "Hello ViewData";
            //  ViewBag.Message = "Hello ViewBag";
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchInp)) 
                 employees = _employeesRepo.GetAll();           
            else
                 employees = _employeesRepo.SearchByNmae(searchInp.ToLower());

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmp);
        }
        public IActionResult Create()
        {
           // ViewData["Departments"] = _departmentRepository.GetAll();
            return View();
        }
  
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee> (employeeVM);
                var count = _employeesRepo.Add(mappedEmp);

                if (count > 0)
                    TempData["Message"] = "Department is created successfully";
                else
                    TempData["Message"] = "An Error Has Occured, Department Not Created";

                return RedirectToAction(nameof(Index));

            }
            return View(employeeVM);
        }
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var employee = _employeesRepo.Get(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (mappedEmp is null)
                return NotFound(); // 404

            return View(viewName, mappedEmp);
        }
        public IActionResult Edit(int? id)
        {
           // ViewData["Departments"] = _departmentRepository.GetAll();
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employeeVM);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _employeesRepo.Update(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Employee");

                return View(employeeVM);
            }
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _employeesRepo.Delete(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Employee");

                return View(employeeVM);
            }
        }
    }

}
