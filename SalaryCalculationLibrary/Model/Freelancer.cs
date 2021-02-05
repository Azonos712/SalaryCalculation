using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{
    public class Freelancer : Employee
    {
        public Freelancer(string surname) : base(surname)
        {

        }
        public override string GetRole() => "фрилансер";
        public override string GetDataFileName() => "\\listOfFreelancers.csv";
        public override decimal GetSalaryPerHour() => 1000;
    }
}
