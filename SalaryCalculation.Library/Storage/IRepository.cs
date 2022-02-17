using SalaryCalculation.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculation.Library.Storage
{
    public interface IRepository
    {
        Employee FindEmployeeBySurname(string name);
    }
}
