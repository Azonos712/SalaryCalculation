using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary
{
    enum Roles
    {
        Director,
        Worker,
        Freelancer
    }

    class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Roles Role { get; set; }
    }
}
