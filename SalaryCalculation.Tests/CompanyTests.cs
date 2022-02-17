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
        //Employee worker = new Worker("иванов");
        //Employee director = new Director("афанасьев");
        //Employee freelancer = new Freelancer("акопян");

        //[SetUp]
        //public void Setup()
        //{
        //    company = new Company("TestCompany");
        //    worker = new Worker("иванов");
        //    director = new Director("афанасьев");
        //    freelancer = new Freelancer("акопян");
        //}

        [TestCase("", null, null)]
        [TestCase("архинян", null, null)]
        [TestCase("попов", "попов", "руководитель")]
        [TestCase("аксеньев", "аксеньев", "сотрудник")]
        [TestCase("сидоров", "сидоров", "фрилансер")]
        public void FindEmployeeBySurname(string surname, string realSurname, string role)
        {
            var e = company.FindEmployeeBySurname(surname);
            Assert.AreEqual(e?.Surname, realSurname);
            Assert.AreEqual(e?.GetRole(), role);
        }

        [TestCase(null, null, null)]
        [TestCase("архинян", null, null)]
        [TestCase("сидоров", "сотрудник", typeof(Worker))]
        [TestCase("сидоров", "руководитель", typeof(Director))]
        [TestCase("сидоров", "фрилансер", typeof(Freelancer))]
        public void CreateEmployeeByRole(string surname, string role, Type type)
        {
            var e = company.CreateEmployeeByRole(surname, role);
            Assert.AreEqual(e?.GetType(), type);
        }

        [Test]
        public void AddNewEmployee()
        {
            var e = company.CreateEmployeeByRole("попов", "руководитель");
            Assert.AreEqual(company.AddNewEmployee(e), false);
        }
    }
}