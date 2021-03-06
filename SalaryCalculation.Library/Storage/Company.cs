﻿using SalaryCalculation.Library.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SalaryCalculation.Library.Storage
{
    public class Company
    {
        private readonly string _companyName;
        private readonly FilesService _fileService;

        public Company(string name)
        {
            _companyName = name;
            _fileService = new FilesService(_companyName);
        }

        public Employee FindEmployeeBySurname(string surname)
        {
            string line;
            using (StreamReader sr = new StreamReader(_fileService.PathToAllEmployees))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');

                    if (sLine[0] == surname)
                        return CreateEmployeeByRole(sLine[0], sLine[1]);
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

            using (StreamWriter sw = new StreamWriter(_fileService.PathToAllEmployees, true, Encoding.Default))
            {
                sw.WriteLine(e.ToString());
            }

            return true;
        }

        public JobReport FindJobReportBySurnameAndDate(Employee employee, DateTime date)
        {
            string line;
            using (StreamReader sr = new StreamReader(_fileService.GetPathByEmployee(employee)))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');

                    if (sLine[0] == date.ToString("d"))
                        if (sLine[1] == employee.Surname)
                            return new JobReport(employee, byte.Parse(sLine[2]), date, sLine[3]);
                }
            }
            return null;
        }

        public bool AddJobReportToEmployee(Employee whoAdds, JobReport jr)
        {
            if (FindJobReportBySurnameAndDate(jr.WorkPerson, jr.WorkDay) != null)
                return false;

            if (whoAdds is Freelancer && DateTime.Today.AddDays(-2) > jr.WorkDay)
                return false;

            using (StreamWriter sw = new StreamWriter(_fileService.GetPathByEmployee(jr.WorkPerson), true, Encoding.Default))
            {
                sw.WriteLine(jr.WorkDay.ToString("d") + "," + jr.WorkPerson.Surname + "," + jr.Hours + "," + jr.Description);
            }

            return true;
        }

        public List<JobReport> GetJobReportsForPeriodByEmployee(Employee employee, DateTime startDate, DateTime endDate)
        {
            var jobReports = new List<JobReport>();
            string line;
            using (StreamReader sr = new StreamReader(_fileService.GetPathByEmployee(employee)))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');
                    if (startDate <= DateTime.Parse(sLine[0]) && DateTime.Parse(sLine[0]) <= endDate)
                        if (sLine[1] == employee.Surname)
                            jobReports.Add(new JobReport(employee, byte.Parse(sLine[2]), DateTime.Parse(sLine[0]), sLine[3]));
                }
            }
            return jobReports;
        }

        public List<JobReport> GetJobReportsForPeriodByAllEmployees(DateTime startDate, DateTime endDate)
        {
            var jobReports = new List<JobReport>();
            string line;
            using (StreamReader sr = new StreamReader(_fileService.PathToAllEmployees))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');
                    jobReports.AddRange(GetJobReportsForPeriodByEmployee(CreateEmployeeByRole(sLine[0], sLine[1]), startDate, endDate));
                }
            }
            return jobReports;
        }
    }
}
