using SalaryCalculation.Library.Model;
using System;

namespace SalaryCalculation.Library.Storage
{
    public interface IJobReportRepository
    {
        bool AddJobReport(Employee whoAdds, JobReport jr);
        JobReport SearchJobReport(Employee employee, DateTime date);
    }
}
