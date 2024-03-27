using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.BLL.Repositories;
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
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentsRepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IMapper mapper,IDepartmentRepository departmentsRepo, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _departmentsRepo = departmentsRepo;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _departmentsRepo.GetAll();
            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDep);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                var count = _departmentsRepo.Add(mappedDep); 
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(departmentVM); 
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = _departmentsRepo.Get(id.Value);
            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);

            if (mappedDep is null)
                return NotFound(); // 404

            return View(viewName, mappedDep);
        }
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//validate from App or not  13:00
        //[Authorize]//validate token 
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _departmentsRepo.Update(mappedDep);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department");

                return View(departmentVM);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _departmentsRepo.Delete(mappedDep);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department");

                return View(departmentVM);
            }
        }
    }
}
