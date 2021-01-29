using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary
{
    public enum Roles
    {
        Director,
        Worker,
        Freelancer
    }

    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Roles Role { get; set; }
    }
}
