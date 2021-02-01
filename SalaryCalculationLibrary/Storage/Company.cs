using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SalaryCalculationLibrary
{
    public class Company
    {
        string companyName;
        string storageDirectory;
        readonly string[] namesOfLists = { "\\listOfAllEmployes.csv", "\\listOfDirectors.csv", "\\listOfWorkers.csv", "\\listOfFreelancers.csv" };
        public Company(string name)
        {
            companyName = name;
            storageDirectory = Directory.GetCurrentDirectory() + "\\" + companyName;
            CheckStorage();
        }

        private void CheckStorage()
        {
            if (!Directory.Exists(storageDirectory))
                Directory.CreateDirectory(storageDirectory);

            foreach (var file in namesOfLists)
            {
                string currentFile = storageDirectory + file;

                if (!File.Exists(currentFile))
                    File.Create(currentFile);
            }
        }

        public Employee FindEmployeeBySurname(string surname)
        {
            string line;
            using (StreamReader sr = new StreamReader(storageDirectory + namesOfLists[0]))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');
                    if (sLine[0] == surname)
                    {
                        return new Employee(sLine[0], sLine[1]);
                    }
                }
            }
            return null;
        }

        //public Employee[] GetListOfEmployees()
        //{
        //    var list = new Employee[5];
        //    return list;
        //}

        //public Employee[] GetListByEmployeesRole(Roles role)
        //{
        //    var list = new Employee[5];
        //    return list;
        //}
    }
}
