using SalaryCalculation.Library.Model;

namespace SalaryCalculation.Library.Storage
{
    public interface IRepository
    {
        bool AddEmployee(Employee e);
        Employee FindEmployeeBySurname(string name);
    }
}
