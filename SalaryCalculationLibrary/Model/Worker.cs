using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{
    public class Worker : Employee
    {
        public override string RoleToStr { get => "сотрудник"; }
        public Worker(string surname) : base(surname)
        {

        }
    }
}