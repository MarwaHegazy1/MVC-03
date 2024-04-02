using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace ASP.NET_Core_MVC_03.PL.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }// Option => ASP.NET Core 5
        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        public ICollection<NET_Core_MVC.DAL.Modules.Employee> Employees { get; set; } = new HashSet<NET_Core_MVC.DAL.Modules.Employee>();
    }
}
