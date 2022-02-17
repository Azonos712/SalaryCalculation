using SalaryCalculation.Library.Model;
using System.IO;

namespace SalaryCalculation.Library.Storage
{
    public class FilesRepository : IRepository
    {
        FilesInfo _filesInfo;
        public FilesRepository(string companyName)
        {
            _filesInfo = new FilesInfo(Directory.GetCurrentDirectory() + "\\" + companyName);
        }

        public Employee FindEmployeeBySurname(string name)
        {
            throw new System.NotImplementedException();
        }

        public string GetPathByEmployee(Employee employee)
        {
            switch (employee.GetRole())
            {
                case "сотрудник":
                    return PathToWorkers;
                case "руководитель":
                    return PathToDirectors;
                case "фрилансер":
                    return PathToFreelancers;
                default:
                    return null;
            }
        }
    }
}
