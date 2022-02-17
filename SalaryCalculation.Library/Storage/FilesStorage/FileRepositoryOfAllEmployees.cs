using SalaryCalculation.Library.Model;
using System.IO;
using System.Text;

namespace SalaryCalculation.Library.Storage.FileStorage
{
    public class FileRepositoryOfAllEmployees : BaseFileRepository, IEmployeeRepository
    {
        public FileRepositoryOfAllEmployees(FilesInfo filesInfo) : base(filesInfo) { }

        public bool AddEmployee(Employee e)
        {
            if (e == null)
                return false;

            if (SearchEmployeeBySurname(e.Surname) != null)
                return false;

            using (StreamWriter sw = new StreamWriter(FilesInfo.PathToAllEmployees, true, Encoding.Default))
            {
                sw.WriteLine(e.ToString());
            }

            return true;
        }

        public Employee SearchEmployeeBySurname(string name)
        {
            string line;

            using (StreamReader sr = new StreamReader(FilesInfo.PathToAllEmployees, Encoding.Default))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');

                    if (sLine[0] == name)
                        return Company.GetEmployeeByRole(sLine[0], sLine[1]);
                }
            }

            return null;
        }


    }
}
