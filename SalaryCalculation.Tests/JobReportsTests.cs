using NUnit.Framework;
using SalaryCalculation.Library.Model;
using SalaryCalculation.Library.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculation.Tests
{
    internal class JobReportsTests
    {
        Company company = new Company("TestCompany",new FilesRepository("TestCompany"));
        Employee worker = new Worker("иванов");

        [Test]
        public void FindJobReportBySurnameAndExistentDate()
        {
            Assert.IsTrue(company.FindJobReportBySurnameAndDate(worker, new DateTime(2021, 1, 1)) != null);
        }

        [Test]
        public void FindJobReportBySurnameAndNonExistentDate()
        {
            Assert.IsTrue(company.FindJobReportBySurnameAndDate(worker, new DateTime(2021, 2, 1)) == null);
        }

        [Test]
        public void AddJobReportToEmployee()
        {
            Assert.AreEqual(company.AddJobReportToEmployee(worker, new JobReport(worker, 7, DateTime.Parse("01.01.2021"), "work")), false);
        }
        [Test]
        public void GetJobReportsForPeriodByEmployee()
        {
            Assert.AreEqual(company.GetJobReportsForPeriodByEmployee(worker, DateTime.Parse("01.01.2021"), DateTime.Parse("05.01.2021")).Count, 3);
        }
        [Test]
        public void GetJobReportsForPeriodByAllEmployees()
        {
            Assert.AreEqual(company.GetJobReportsForPeriodByAllEmployees(DateTime.Parse("01.01.2021"), DateTime.Parse("05.01.2021")).Count, 5);
        }
    }
}
