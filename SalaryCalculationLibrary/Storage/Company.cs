﻿using SalaryCalculationLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SalaryCalculationLibrary
{
    public class Company
    {
        readonly string companyName;
        readonly string storageDirectory;
        readonly string fileNameOfAllEmployees = "\\listOfAllEmployes.csv";
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

            if (!File.Exists(storageDirectory + fileNameOfAllEmployees))
                File.Create(storageDirectory + fileNameOfAllEmployees);

            if (!File.Exists(storageDirectory + new Worker("").DataFileName))
                File.Create(storageDirectory + new Worker("").DataFileName);

            if (!File.Exists(storageDirectory + new Director("").DataFileName))
                File.Create(storageDirectory + new Director("").DataFileName);

            if (!File.Exists(storageDirectory + new Freelancer("").DataFileName))
                File.Create(storageDirectory + new Freelancer("").DataFileName);
        }

        public Employee FindEmployeeBySurname(string surname)
        {
            string line;
            using (StreamReader sr = new StreamReader(storageDirectory + fileNameOfAllEmployees))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');
                    if (sLine[0] == surname)
                    {
                        return CreateEmployeeByRole(sLine[0], sLine[1]);
                    }
                }
            }
            return null;
        }
        public Employee CreateEmployeeByRole(string surname, string role)
        {
            switch (role)
            {
                case "сотрудник":
                    return new Worker(surname);
                case "руководитель":
                    return new Director(surname);
                case "фрилансер":
                    return new Freelancer(surname);
                default:
                    return null;
            }
        }

        public bool AddNewEmployee(Employee e)
        {
            if (e == null)
                return false;

            if (FindEmployeeBySurname(e.Surname) != null)
                return false;

            using (StreamWriter sw = new StreamWriter(storageDirectory + fileNameOfAllEmployees, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(e.ToString());
            }

            return true;
        }

        public bool AddHoursToEmployee(JobReport jr)
        {
            Employee employee = jr.WorkPerson;

            using (StreamWriter sw = new StreamWriter(storageDirectory + employee.DataFileName, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(jr.WorkDay.ToString("d") + "," + employee.Surname + "," + jr.Hours + "," + jr.Description);
            }

            return true;
        }
    }
}
