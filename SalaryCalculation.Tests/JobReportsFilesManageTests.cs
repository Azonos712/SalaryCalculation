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
            var test = new JobReport(w, 8, DateTime.Now.AddDays(-3), "work1");
            _company.AddJobReport(w, new JobReport(w, 8, DateTime.Now.AddDays(-3), "work1"));
            _company.AddJobReport(w, new JobReport(w, 9, DateTime.Now.AddDays(-5), "work2"));

            _company.AddJobReport(f, new JobReport(f, 11, DateTime.Now.AddDays(-2), "work3"));
            _company.AddJobReport(f, new JobReport(f, 9, DateTime.Now.AddDays(-1), "work4"));

            _company.AddJobReport(d, new JobReport(d, 5, DateTime.Now.AddDays(-4), "work5"));
            _company.AddJobReport(d, new JobReport(d, 7, DateTime.Now.AddDays(-6), "work6"));

            _company.AddJobReport(d, new JobReport(w, 8, DateTime.Now.AddDays(-3), "work7"));
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

        //тест на добавление часов другим сотрудникам
        [Test]
        public void FindJobReportBySurnameAndExistingtDate()
        {
            //Assert.IsTrue(_company.SearchJobReportBySurnameAndDate(w, new DateTime(2021, 1, 1)) != null);
        }

        [Test]
        public void FindJobReportBySurnameAndNonExistentDate()
        {
            //Assert.IsTrue(_company.SearchJobReportBySurnameAndDate(w, new DateTime(2021, 2, 1)) == null);
        }

        [Test]
        public void GetJobReportsForPeriodByEmployee()
        {
            //Assert.AreEqual(_company.GetJobReportsForPeriodByEmployee(w, DateTime.Parse("01.01.2021"), DateTime.Parse("05.01.2021")).Count, 3);
        }
        [Test]
        public void GetJobReportsForPeriodByAllEmployees()
        {
            //Assert.AreEqual(_company.GetJobReportsForPeriodByAllEmployees(DateTime.Parse("01.01.2021"), DateTime.Parse("05.01.2021")).Count, 5);
        }

        [TearDown]
        public void CleanFiles()
        {
            if (Directory.Exists(_fileInfo.StorageDirectory))
                Directory.Delete(_fileInfo.StorageDirectory, true);
        }
    }
}
