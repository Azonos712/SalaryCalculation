using SalaryCalculation.Library.Model;
using System;
using System.Collections.Generic;

namespace SalaryCalculation.Library.Storage
{
    public class Company
    {
        private readonly string _companyName;
        private readonly IEmployeeRepository _employeeRep;
        private readonly IJobReportRepository _jobReportRep;

        public Company(string name, IEmployeeRepository repository, IJobReportRepository jobReportRep)
        {
            _companyName = name;
            _employeeRep = repository;
            _jobReportRep = jobReportRep;
        }

        public static Employee GetEmployeeByRole(string surname, string role)
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

        public bool AddEmployee(string surname, string role)
        {
            return _employeeRep.AddEmployee(GetEmployeeByRole(surname, role));
        }

        public Employee SearchEmployee(string surname)
        {
            return _employeeRep.SearchEmployee(surname);
        }

        public bool AddJobReport(Employee whoAdds, JobReport jr)
        {
            return _jobReportRep.AddJobReport(whoAdds, jr);
        }

        public JobReport SearchJobReport(Employee employee, DateTime date)
        {
            return _jobReportRep.SearchJobReport(employee, date);
        }

        public List<JobReport> GetJobReportsForPeriodByEmployee(Employee employee, DateTime startDate, DateTime endDate)
        {
            var jobReports = new List<JobReport>();
            string line;
            //using (StreamReader sr = new StreamReader(_fileService.GetPathByEmployee(employee)))
            //{
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        string[] sLine = line.Split(',');
            //        if (startDate <= DateTime.Parse(sLine[0]) && DateTime.Parse(sLine[0]) <= endDate)
            //            if (sLine[1] == employee.Surname)
            //                jobReports.Add(new JobReport(employee, byte.Parse(sLine[2]), DateTime.Parse(sLine[0]), sLine[3]));
            //    }
            //}
            return jobReports;
        }

        public List<JobReport> GetJobReportsForPeriodByAllEmployees(DateTime startDate, DateTime endDate)
        {
            var jobReports = new List<JobReport>();
            string line;
            //using (StreamReader sr = new StreamReader(_fileService.PathToAllEmployees))
            //{
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        string[] sLine = line.Split(',');
            //        jobReports.AddRange(GetJobReportsForPeriodByEmployee(CreateEmployeeByRole(sLine[0], sLine[1]), startDate, endDate));
            //    }
            //}
            return jobReports;
        }
    }
}
