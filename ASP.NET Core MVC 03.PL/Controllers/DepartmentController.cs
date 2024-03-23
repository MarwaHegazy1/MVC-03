﻿using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.BLL.Repositories;
using ASP.NET_Core_MVC.DAL.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace ASP.NET_Core_MVC_03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentsRepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentsRepo, IWebHostEnvironment env)
        {
            _departmentsRepo = departmentsRepo;
            _env = env;
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

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var department = _departmentsRepo.Get(id.Value);

            if (department is null)
                return NotFound(); // 404

            return View(viewName, department);
        }
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//validate from App or not  13:00
        //[Authorize]//validate token 
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _departmentsRepo.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department");

                return View(department);
            }
        }

    }
}
