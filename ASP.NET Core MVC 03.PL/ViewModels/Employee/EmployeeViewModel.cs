using ASP.NET_Core_MVC.DAL.Modules;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace ASP.NET_Core_MVC_03.PL.ViewModels.Employee
{
      public class EmployeeViewModel
    {
     
        // [Required(ErrorMessage = "Name is Required!")]
        [Required]
        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 Chars")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
            , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        //[DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }//text of type int
        public EmpType EmpType { get; set; }

        public int? DepartmentId { get; set; }
        public NET_Core_MVC.DAL.Modules.Department Department { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
    }
}
