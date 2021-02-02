using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{
    public class Freelancer : Employee
    {
        public override string RoleToStr { get => "фрилансер"; }
        public override string DataFileName { get => "\\listOfFreelancers.csv"; }
        public Freelancer(string surname) : base(surname)
        {

        }
    }
}
