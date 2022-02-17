using NUnit.Framework;
using SalaryCalculation.Library;
using SalaryCalculation.Library.Model;
using SalaryCalculation.Library.Storage;
using System;

namespace SalaryCalculation.Tests
{
    public class CompanyTests
    {
        Company company = new Company("TestCompany");
        //Employee worker = new Worker("������");
        //Employee director = new Director("���������");
        //Employee freelancer = new Freelancer("������");

        //[SetUp]
        //public void Setup()
        //{
        //    company = new Company("TestCompany");
        //    worker = new Worker("������");
        //    director = new Director("���������");
        //    freelancer = new Freelancer("������");
        //}

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
    }
}