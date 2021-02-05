using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{
    public class Director : Employee
    {
        readonly decimal monthSalary;
        public Director(string surname) : base(surname)
        {
            monthSalary = 200000;
        }
        public override string GetRole() => "руководитель";
        public override string GetDataFileName() => "\\listOfDirectors.csv";
        public override decimal GetSalaryPerHour() => monthSalary / WorkStandarts.hoursInWorkMonth;
    }
}
