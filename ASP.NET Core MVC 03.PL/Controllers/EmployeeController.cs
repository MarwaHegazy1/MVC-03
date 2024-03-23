﻿using ASP.NET_Core_MVC.BLL.Interfaces;
using ASP.NET_Core_MVC.DAL.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace ASP.NET_Core_MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeesRepo;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeesRepo, IWebHostEnvironment env)
        {
            _employeesRepo = employeesRepo;
            _env = env;
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
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var employee = _employeesRepo.Get(id.Value);

            if (employee is null)
                return NotFound(); // 404

            return View(viewName, employee);
        }
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                _employeesRepo.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Employee");

                return View(employee);
            }
        }
    }

}
