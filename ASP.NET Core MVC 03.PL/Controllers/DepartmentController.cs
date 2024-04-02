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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentsRepo;

        public DepartmentController(IUnitOfWork unitOfWork,IMapper mapper,
            IWebHostEnvironment env)
        // ,IDepartmentRepository departmentsRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
           // _departmentsRepo = departmentsRepo;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.Repository<Department>().GetAll();
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
                 _unitOfWork.Repository<Department>().Add(mappedDep);
                var count = _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(departmentVM); 
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = _unitOfWork.Repository<Department>().Get(id.Value);
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
                _unitOfWork.Repository<Department>().Update(mappedDep);
                _unitOfWork.Complete();
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
                _unitOfWork.Repository<Department>().Delete(mappedDep);
                _unitOfWork.Complete();
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
