using ASP.NET_Core_MVC.BLL;
using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.BLL.Repositories;
using ASP.NET_Core_MVC.DAL.Modules;
using ASP.NET_Core_MVC_03.PL.Helpers;
using ASP.NET_Core_MVC_03.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IMapper mapper,IUnitOfWork unitOfWork,IWebHostEnvironment env)
            /// IEmployeeRepository employeesRepo,
            ///IDepartmentRepository departmentRepository , )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
           // _employeesRepo = employeesRepo;
           // _departmentRepository = departmentRepository;
            _env = env;
        }
        public async Task<IActionResult> Index(string searchInp)
        {   
            var employees = Enumerable.Empty<Employee>();
            var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;
            if (string.IsNullOrEmpty(searchInp))
                employees = await employeeRepo.GetAllAsync();
            else
                employees = employeeRepo.SearchByName(searchInp.ToLower());
          
            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmp);
        }
        public IActionResult Create()
        {
            return View();
        }
  
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName =await  DocumentSetting.UploadFile(employeeVM.Image, "images");

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee> (employeeVM);
               
                 _unitOfWork.Repository<Employee>().Add(mappedEmp);
                var count = await _unitOfWork.Complete();
                
                if (count > 0)
                {
                    TempData["Message"] = "Employee is created successfully";
                }
                   
                else
                    TempData["Message"] = "An Error Has Occured, Employee Not Created";

                return RedirectToAction(nameof(Index));

            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            //var employee = _employeesRepo.Get(id.Value);
            var employee =await _unitOfWork.Repository<Employee>().GetAsync(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (mappedEmp is null)
                return NotFound(); // 404

            if(viewName.Equals("Delete",StringComparison.OrdinalIgnoreCase))
            TempData["ImageName"] = employee.ImageName;
            return View(viewName, mappedEmp);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);

            try
            {               

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.Repository<Employee>().Update(mappedEmp);
              await _unitOfWork.Complete();

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
        public async Task<IActionResult> Delete(int? id)
        {
            return  await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                employeeVM.ImageName = TempData["ImageName"] as string;

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);              
                _unitOfWork.Repository<Employee>().Delete(mappedEmp);
               var count = await _unitOfWork.Complete();
                if(count > 0)
                {
                    DocumentSetting.DeleteFile(employeeVM.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
                return View(employeeVM);

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
