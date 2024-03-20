﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC.DAL.Modules
{
    //Model
    internal class Department
    {
        public int Id { get; set; }
                  // ErrorMessage => in View Module not here
      //  [Required(ErrorMessage ="Code is Required !!!")]// Mapped to not null constrain in DB, Validation in DB & App
        public string Code { get; set; }
        //[Required]
        public string Name { get; set; }// Option => ASP.NET Core 5
        public DateTime DateOfCreation { get; set; }
    }
}
