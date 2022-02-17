using SalaryCalculation.Library.Model;
using System;
using System.Collections.Generic;

namespace SalaryCalculation.Library.Storage
{
    public interface IJobReportRepository
    {
        bool AddJobReport(Employee whoAdds, JobReport jr);
        JobReport SearchJobReport(Employee employee, DateTime date);
        List<JobReport> GetJobReportsForPeriod(Employee employee, DateTime startDate, DateTime endDate);
        List<JobReport> GetJobReportsForPeriodByAllEmployees(DateTime startDate, DateTime endDate);
    }
}
