using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC.DAL.Modules
{
    //Model
    public class Department: ModelBase
    {
                  // ErrorMessage => in View Module not here
      //  [Required(ErrorMessage ="Code is Required !!!")]// Mapped to not null constrain in DB, Validation in DB & App
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }// Option => ASP.NET Core 5
        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        
    }
}
