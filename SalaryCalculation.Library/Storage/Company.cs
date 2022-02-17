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

        public List<JobReport> GetJobReportsForPeriod(Employee employee, DateTime startDate, DateTime endDate)
        {
            return _jobReportRep.GetJobReportsForPeriod(employee, startDate, endDate);
        }

        public List<JobReport> GetJobReportsForPeriodByAllEmployees(DateTime startDate, DateTime endDate)
        {
            return _jobReportRep.GetJobReportsForPeriodByAllEmployees(startDate,endDate);
        }
    }
}
