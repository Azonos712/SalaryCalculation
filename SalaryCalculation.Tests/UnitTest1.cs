using NUnit.Framework;
using SalaryCalculation.Library;
using SalaryCalculation.Library.Model;
using SalaryCalculation.Library.Storage;
using System;

namespace SalaryCalculation.Tests
{
    public class Tests
    {
        Company company;
        Employee worker;
        Employee director;
        Employee freelancer;

        [SetUp]
        public void Setup()
        {
            company = new Company("TestCompany");
            worker = new Worker("������");
            director = new Director("���������");
            freelancer = new Freelancer("������");
        }

        [TestCase(0, 0)]
        [TestCase(1, 750)]
        [TestCase(160, 120000)]
        [TestCase(170, 135000)]
        public void CalculationSalaryPerHourByWorker(int hours, decimal money)
        {
            Assert.AreEqual(worker.GetPaidByHours(hours), money);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1250)]
        [TestCase(160, 200000)]
        [TestCase(170, 220000)]
        public void CalculationSalaryPerHourByDirector(int hours, decimal money)
        {
            Assert.AreEqual(director.GetPaidByHours(hours), money);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1000)]
        [TestCase(160, 160000)]
        [TestCase(170, 170000)]
        public void CalculationSalaryPerHourByFreelancer(int hours, decimal money)
        {
            Assert.AreEqual(freelancer.GetPaidByHours(hours), money);
        }

        [TestCase("", "")]
        [TestCase("�����", "�����")]
        [TestCase("������", "������")]
        [TestCase("hello", "Hello")]
        public void UpFirstCharInString(string a, string b)
        {
            Assert.AreEqual(Utility.FirstCharToUpper(a), b);
        }

        [TestCase("", null, null)]
        [TestCase("�������", null, null)]
        [TestCase("�����", "�����", "������������")]
        [TestCase("��������", "��������", "���������")]
        [TestCase("�������", "�������", "���������")]
        public void FindEmployeeBySurname(string surname, string realSurname, string role)
        {
            var e = company.FindEmployeeBySurname(surname);
            Assert.AreEqual(e?.Surname, realSurname);
            Assert.AreEqual(e?.GetRole(), role);
        }

        [TestCase(null, null, null)]
        [TestCase("�������", null, null)]
        [TestCase("�������", "���������", typeof(Worker))]
        [TestCase("�������", "������������", typeof(Director))]
        [TestCase("�������", "���������", typeof(Freelancer))]
        public void CreateEmployeeByRole(string surname, string role, Type type)
        {
            var e = company.CreateEmployeeByRole(surname, role);
            Assert.AreEqual(e?.GetType(), type);
        }

        [Test]
        public void AddNewEmployee()
        {
            var e = company.CreateEmployeeByRole("�����", "������������");
            Assert.AreEqual(company.AddNewEmployee(e), false);
        }

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