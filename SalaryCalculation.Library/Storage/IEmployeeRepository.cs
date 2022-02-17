using SalaryCalculation.Library.Model;

namespace SalaryCalculation.Library.Storage
{
    public interface IEmployeeRepository
    {
        bool AddEmployee(Employee e);
        Employee SearchEmployeeBySurname(string name);
    }
}
