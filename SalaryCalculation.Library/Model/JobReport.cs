using System;

namespace SalaryCalculation.Library.Model
{
    public class JobReport
    {
        public Employee WorkPerson { get; set; }
        public DateTime WorkDay { get; set; }
        public byte Hours { get; set; }
        public string Description { get; set; }

        public JobReport(Employee e, byte hours, DateTime date, string description)
        {
            WorkPerson = e;
            WorkDay = date;
            Hours = hours;
            Description = description;
        }
    }
}