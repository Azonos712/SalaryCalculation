using SalaryCalculation.Library.Model;
using System.IO;
using System.Text;

namespace SalaryCalculation.Library.Storage
{
    public class FilesRepository : IRepository
    {
        private FilesInfo _filesInfo;

        public FilesRepository(string companyName)
        {
            _filesInfo = new FilesInfo(Directory.GetCurrentDirectory() + "\\" + companyName);
        }

        public bool AddEmployee(Employee e)
        {
            if (e == null)
                return false;

            if (FindEmployeeBySurname(e.Surname) != null)
                return false;

            using (StreamWriter sw = new StreamWriter(_filesInfo.PathToAllEmployees, true, Encoding.Default))
            {
                sw.WriteLine(e.ToString());
            }

            return true;
        }

        public Employee FindEmployeeBySurname(string name)
        {
            string line;

            using (StreamReader sr = new StreamReader(_filesInfo.PathToAllEmployees))
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
