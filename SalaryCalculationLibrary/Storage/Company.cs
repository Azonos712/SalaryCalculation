using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SalaryCalculationLibrary
{
    public class Company
    {
        readonly string[] namesOfLists = { "\\listOfAllEmployes.csv", "\\listOfDirectors.csv", "\\listOfWorkers.csv", "\\listOfFreelancers.csv" };

        public Company()
        {
            CheckStorage();
        }

        private void CheckStorage()
        {
            string currentDir = Directory.GetCurrentDirectory();
            foreach (var file in namesOfLists)
            {
                string currentFile = currentDir + file;
                if (!File.Exists(currentFile))
                {
                    File.Create(currentFile);
                }
            }
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
