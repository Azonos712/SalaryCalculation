using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{
    public class Director : Employee
    {
        public override string RoleToStr { get => "руководитель"; }
        public Director(string surname) : base(surname)
        {

        }
    }
}
