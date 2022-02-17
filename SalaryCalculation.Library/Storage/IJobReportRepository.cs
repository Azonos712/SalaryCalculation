using SalaryCalculation.Library.Model;
using System;

namespace SalaryCalculation.Library.Storage
{
    public interface IJobReportRepository
    {
        bool AddJobReportToEmployee(Employee whoAdds, JobReport jr);
        JobReport SearchJobReportBySurnameAndDate(Employee employee, DateTime date);
    }
}
