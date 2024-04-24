using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.BLL.Repositories;
using ASP.NET_Core_MVC.DAL.Modules;
using ASP.NET_Core_MVC_03.PL.ViewModels;
using ASP.NET_Core_MVC_03.PL.ViewModels.Department;
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
        public async Task<IActionResult> Index()
        {
            var departments =await _unitOfWork.Repository<Department>().GetAllAsync();
            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentResponseViewModel>>(departments);
            return View(mappedDep);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                 _unitOfWork.Repository<Department>().Add(mappedDep);
                var count =await _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(departmentVM); 
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department =await _unitOfWork.Repository<Department>().GetAsync(id.Value);
            var mappedDep = _mapper.Map<Department, DepartmentResponseViewModel>(department);

            if (mappedDep is null)
                return NotFound(); // 404

            return View(viewName, mappedDep);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//validate from App or not  13:00
        //[Authorize]//validate token 
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {

            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.Repository<Department>().Update(mappedDep);
               await _unitOfWork.Complete();
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

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentResponseViewModel departmentVM)
        {
            try
            {
                var mappedDep = _mapper.Map<DepartmentResponseViewModel, Department>(departmentVM);
                _unitOfWork.Repository<Department>().Delete(mappedDep);
               await _unitOfWork.Complete();
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
