using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{
    public class Freelancer : Employee
    {
        public override string RoleToStr { get => "фрилансер"; }
        public Freelancer(string surname) : base(surname)
        {

        }
    }
}
