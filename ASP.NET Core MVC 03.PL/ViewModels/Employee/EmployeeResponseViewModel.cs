using ASP.NET_Core_MVC.DAL.Modules;
using System.ComponentModel.DataAnnotations;
using System;

namespace ASP.NET_Core_MVC_03.PL.ViewModels.Employee
{
    public class EmployeeResponseViewModel
    {
        public int Id { get; set; }    
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]    
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmpType EmpType { get; set; }

        public int? DepartmentId { get; set; }
        public NET_Core_MVC.DAL.Modules.Department Department { get; set; }
        public string ImageName { get; set; }

    }
}
