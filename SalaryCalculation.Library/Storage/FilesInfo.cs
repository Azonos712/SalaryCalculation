using SalaryCalculation.Library.Model;
using System.IO;

namespace SalaryCalculation.Library.Storage
{
    public class FilesInfo
    {
        public string StorageDirectory { get; private set; }
        private readonly string fileNameOfAllEmployees = "\\listOfAllEmployees.csv";
        private readonly string fileNameOfWorkers = "\\listOfWorkers.csv";
        private readonly string fileNameOfDirectors = "\\listOfDirectors.csv";
        private readonly string fileNameOfFreelancers = "\\listOfFreelancers.csv";

        public string PathToAllEmployees { get { return StorageDirectory + fileNameOfAllEmployees; } }
        public string PathToWorkers { get { return StorageDirectory + fileNameOfWorkers; } }
        public string PathToDirectors { get { return StorageDirectory + fileNameOfDirectors; } }
        public string PathToFreelancers { get { return StorageDirectory + fileNameOfFreelancers; } }

        public FilesInfo(string defaultDirectory)
        {
            StorageDirectory = defaultDirectory;
            CheckStorage();
        }

        private void CheckStorage()
        {
            if (!Directory.Exists(StorageDirectory))
                Directory.CreateDirectory(StorageDirectory);

            CheckFile(PathToAllEmployees);
            CheckFile(PathToWorkers);
            CheckFile(PathToDirectors);
            CheckFile(PathToFreelancers);
        }

        private void CheckFile(string path)
        {
            if (!File.Exists(path))
                using (FileStream fs = File.Create(path)) ;
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
