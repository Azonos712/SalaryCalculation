using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary.Model
{
    public class JobReport
    {
        public DateTime WorkDay { get; set; }
        public Employee WorkPerson { get; set; }
        public byte Hours { get; set; }
        public string Description { get; set; }

        public JobReport(Employee e, byte hours, DateTime date, string description)
        {
            WorkPerson = e;
            Hours = hours;
            WorkDay = date;
            Description = description;
        }
    }
}
