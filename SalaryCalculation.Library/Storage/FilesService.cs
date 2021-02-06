using SalaryCalculation.Library.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SalaryCalculation.Library.Storage
{
    class FilesService
    {
        string storageDirectory;
        string fileNameOfAllEmployees = "\\listOfAllEmployes.csv";
        string fileNameOfWorkers = "\\listOfWorkers.csv";
        string fileNameOfDirectors = "\\listOfDirectors.csv";
        string fileNameOfFreelancers = "\\listOfFreelancers.csv";
        public string PathToAllEmployees { get { return storageDirectory + fileNameOfAllEmployees; } }
        public string PathToWorkers { get { return storageDirectory + fileNameOfWorkers; } }
        public string PathToDirectors { get { return storageDirectory + fileNameOfDirectors; } }
        public string PathToFreelancers { get { return storageDirectory + fileNameOfFreelancers; } }

        public FilesService(string companyName)
        {
            storageDirectory = Directory.GetCurrentDirectory() + "\\" + companyName;
            CheckStorage();
        }

        void CheckStorage()
        {
            if (!Directory.Exists(storageDirectory))
                Directory.CreateDirectory(storageDirectory);

            CheckFile(PathToAllEmployees);
            CheckFile(PathToWorkers);
            CheckFile(PathToDirectors);
            CheckFile(PathToFreelancers);
        }

        void CheckFile(string path)
        {
            if (!File.Exists(path))
                File.Create(path);
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
