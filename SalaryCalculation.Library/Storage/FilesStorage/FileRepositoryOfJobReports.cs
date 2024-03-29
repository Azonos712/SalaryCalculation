﻿using SalaryCalculation.Library.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SalaryCalculation.Library.Storage.FileStorage
{
    public class FileRepositoryOfJobReports : BaseFileRepository, IJobReportRepository
    {
        public FileRepositoryOfJobReports(FilesInfo filesInfo) : base(filesInfo) { }

        public bool AddJobReport(Employee whoAdds, JobReport jr)
        {
            if (SearchJobReport(jr.WorkPerson, jr.WorkDay) != null)
                return false;

            if (DateTime.Now < jr.WorkDay)
                return false;

            if (!(whoAdds is Director) && !whoAdds.Equals(jr.WorkPerson))
                return false;

            if (whoAdds is Freelancer && DateTime.Today.AddDays(-2) > jr.WorkDay)
                return false;

            using (StreamWriter sw = new StreamWriter(FilesInfo.GetPathByEmployee(jr.WorkPerson), true, Encoding.Default))
            {
                sw.WriteLine(jr.WorkDay.ToString("d") + "," + jr.WorkPerson.Surname + "," + jr.Hours + "," + jr.Description);
            }

            return true;
        }

        public JobReport SearchJobReport(Employee employee, DateTime date)
        {
            string line;

            using (StreamReader sr = new StreamReader(FilesInfo.GetPathByEmployee(employee)))
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

        public List<JobReport> GetJobReportsForPeriod(Employee employee, DateTime startDate, DateTime endDate)
        {
            var jobReports = new List<JobReport>();
            string line;

            using (StreamReader sr = new StreamReader(FilesInfo.GetPathByEmployee(employee)))
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

            using (StreamReader sr = new StreamReader(FilesInfo.PathToAllEmployees))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');
                    jobReports.AddRange(GetJobReportsForPeriod(Company.GetEmployeeByRole(sLine[0], sLine[1]), startDate, endDate));
                }
            }

            return jobReports;
        }
    }
}
