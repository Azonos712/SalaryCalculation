using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{

    public abstract class Employee
    {
        public string Surname { get; set; }
        public abstract string RoleToStr { get; }

        public Employee(string surname)
        {
            Surname = surname;
        }
    }
}
