using NUnit.Framework;
using SalaryCalculation.Library.Model;
using SalaryCalculation.Library.Storage;
using SalaryCalculation.Library.Storage.FileStorage;
using System;
using System.Collections.Generic;
using System.IO;

namespace SalaryCalculation.Tests
{
    [TestFixture]
    public class JobReportsFilesManageTests
    {
        private FilesInfo _fileInfo;
        private Company _company;
        private Worker w;
        private Director d;
        private Freelancer f;

        [SetUp]
        public void SetUp()
        {
            _fileInfo = new FilesInfo("TestCompany");
            _company = new Company(
                "TestCompany",
                new FileRepositoryOfAllEmployees(_fileInfo),
                new FileRepositoryOfJobReports(_fileInfo));

            w = new Worker("попов");
            f = new Freelancer("алексеев");
            d = new Director("иванов");
            _company.AddEmployee("попов", "сотрудник");
            _company.AddEmployee("алексеев", "фрилансер");
            _company.AddEmployee("иванов", "руководитель");

            _company.AddJobReport(w, new JobReport(w, 8, DateTime.Now.AddDays(-3), "work1"));
            _company.AddJobReport(w, new JobReport(w, 9, DateTime.Now.AddDays(-5), "work2"));

            _company.AddJobReport(f, new JobReport(f, 11, DateTime.Now.AddDays(-2), "work3"));
            _company.AddJobReport(f, new JobReport(f, 9, DateTime.Now.AddDays(-1), "work4"));

            _company.AddJobReport(d, new JobReport(d, 5, DateTime.Now.AddDays(-4), "work5"));
            _company.AddJobReport(d, new JobReport(d, 7, DateTime.Now.AddDays(-6), "work6"));

            _company.AddJobReport(d, new JobReport(w, 8, DateTime.Now.AddDays(-7), "work7"));
            _company.AddJobReport(d, new JobReport(f, 10, DateTime.Now.AddDays(-5), "work8"));
        }

        public static IEnumerable<TestCaseData> CorrectListOfEmployeesAndJobReports()
        {
            yield return new TestCaseData(new Worker("попов"), new JobReport(new Worker("попов"), 8, DateTime.Now, "work1t"));
            yield return new TestCaseData(new Freelancer("алексеев"), new JobReport(new Freelancer("алексеев"), 8, DateTime.Now, "work2t"));
            yield return new TestCaseData(new Director("иванов"), new JobReport(new Director("иванов"), 8, DateTime.Now, "work3t"));
            yield return new TestCaseData(new Director("иванов"), new JobReport(new Worker("попов"), 8, DateTime.Now.AddDays(-10), "work4t"));
            yield return new TestCaseData(new Director("иванов"), new JobReport(new Freelancer("алексеев"), 8, DateTime.Now.AddDays(-10), "work5t"));

        }

        [TestCaseSource(nameof(CorrectListOfEmployeesAndJobReports))]
        public void CorrectAddJobReport(Employee whoAdds, JobReport report)
        {
            Assert.AreEqual(_company.AddJobReport(whoAdds, report), true);
        }

        public static IEnumerable<TestCaseData> WrongListOfEmployeesAndJobReports()
        {
            yield return new TestCaseData(new Worker("попов"), new JobReport(new Freelancer("алексеев"), 8, DateTime.Now, "work1wt"));
            yield return new TestCaseData(new Freelancer("алексеев"), new JobReport(new Worker("попов"), 8, DateTime.Now, "work2wt"));
            yield return new TestCaseData(new Worker("попов"), new JobReport(new Director("иванов"), 8, DateTime.Now, "work3wt"));
            yield return new TestCaseData(new Freelancer("алексеев"), new JobReport(new Freelancer("алексеев"), 8, DateTime.Now.AddDays(-5), "work4wt"));
            yield return new TestCaseData(new Worker("попов"), new JobReport(new Worker("попов"), 8, DateTime.Now.AddDays(-3), "work5wt"));
            yield return new TestCaseData(new Worker("попов"), new JobReport(new Worker("попов"), 8, DateTime.Now.AddDays(3), "work6wt"));
        }

        [TestCaseSource(nameof(WrongListOfEmployeesAndJobReports))]
        public void WrongAddJobReport(Employee whoAdds, JobReport report)
        {
            Assert.AreEqual(_company.AddJobReport(whoAdds, report), false);
        }
        public static IEnumerable<TestCaseData> CorrectListOfSearchJobReport()
        {
            yield return new TestCaseData(new Worker("попов"), DateTime.Now.AddDays(-3));
            yield return new TestCaseData(new Freelancer("алексеев"), DateTime.Now.AddDays(-2));
            yield return new TestCaseData(new Director("иванов"), DateTime.Now.AddDays(-4));
        }

        [TestCaseSource(nameof(CorrectListOfSearchJobReport))]
        public void CorrectSearchForJobReport(Employee employee, DateTime date)
        {
            Assert.IsTrue(_company.SearchJobReport(employee, date) != null);
        }

        public static IEnumerable<TestCaseData> WrongListOfSearchJobReport()
        {
            yield return new TestCaseData(new Worker("попов"), DateTime.Now.AddDays(-25));
            yield return new TestCaseData(new Freelancer("алексеев"), DateTime.Now.AddDays(-25));
            yield return new TestCaseData(new Director("иванов"), DateTime.Now.AddDays(-25));
            yield return new TestCaseData(new Worker("капонов"), DateTime.Now.AddDays(-25));
            yield return new TestCaseData(new Director("шуфутинов"), DateTime.Now.AddDays(-25));
        }

        [TestCaseSource(nameof(WrongListOfSearchJobReport))]
        public void WrongSearchForJobReport(Employee employee, DateTime date)
        {
            Assert.IsTrue(_company.SearchJobReport(employee, date) == null);
        }

        public static IEnumerable<TestCaseData> ListOfSearchJobReportForPeriod()
        {
            yield return new TestCaseData(new Worker("попов"), 3);
            yield return new TestCaseData(new Freelancer("алексеев"), 3);
            yield return new TestCaseData(new Director("иванов"), 2);
            yield return new TestCaseData(new Worker("капонов"), 0);
            yield return new TestCaseData(new Director("шуфутинов"), 0);
        }

        [TestCaseSource(nameof(ListOfSearchJobReportForPeriod))]
        public void GetJobReportsForPeriodByEmployee(Employee employee, int numOfReports)
        {
            Assert.AreEqual(_company.GetJobReportsForPeriod(employee, DateTime.Now.AddDays(-30), DateTime.Now).Count, numOfReports);
        }

        [Test]
        public void GetJobReportsForPeriodByAllEmployees()
        {
            Assert.AreEqual(_company.GetJobReportsForPeriodByAllEmployees(DateTime.Now.AddDays(-30), DateTime.Now).Count, 8);
        }

        [TearDown]
        public void CleanFiles()
        {
            if (Directory.Exists(_fileInfo.StorageDirectory))
                Directory.Delete(_fileInfo.StorageDirectory, true);
        }
    }
}
