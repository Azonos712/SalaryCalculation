using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary
{
    class JobReport
    {
        public DateTime WorkDay { get; set; }
        public Employee WorkPerson { get; set; }
        public uint Hours { get; set; }
        public string Description { get; set; }
    }
}
