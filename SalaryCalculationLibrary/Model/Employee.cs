using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{
    public abstract class Employee
    {
        public string Surname { get; }
        public abstract string GetRole();
        public abstract string GetDataFileName();
        public abstract decimal GetSalaryPerHour();
        public Employee(string surname)
        {
            Surname = surname;
        }

        public override string ToString() => Surname + "," + GetRole();
        public decimal GetPaidByHours(int hours) => GetSalaryPerHour() * hours;
    }
}
