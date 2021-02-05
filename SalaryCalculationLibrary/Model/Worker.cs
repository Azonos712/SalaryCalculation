using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{
    public class Worker : Employee
    {
        readonly decimal monthSalary;
        public Worker(string surname) : base(surname)
        {
            monthSalary = 120000;
        }
        public override string GetRole() => "сотрудник";
        public override string GetDataFileName() => "\\listOfWorkers.csv";
        public override decimal GetSalaryPerHour() => monthSalary / WorkStandarts.hoursInWorkMonth;
    }
}