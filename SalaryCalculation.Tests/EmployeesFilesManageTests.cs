using NUnit.Framework;
using SalaryCalculation.Library.Storage;
using SalaryCalculation.Library.Storage.FileStorage;
using System.IO;

namespace SalaryCalculation.Tests
{
    public class EmployeesFilesManageTests
    {
        private FilesInfo _fileInfo;
        private Company _company;

        [SetUp]
        public void SetUp()
        {
            _fileInfo = new FilesInfo("TestCompany");
            _company = new Company(
                "TestCompany",
                new FileRepositoryOfAllEmployees(_fileInfo),
                new FileRepositoryOfJobReports(_fileInfo));
        }

        [Test, Order(1)]
        [TestCase(null, null, false)]
        [TestCase("�������", null, false)]
        [TestCase("�����", "���������", true)]
        [TestCase("��������", "������������", true)]
        [TestCase("�������", "���������", true)]
        public void CreateEmployeeByRole(string surname, string role, bool result)
        {
            Assert.AreEqual(_company.AddEmployeeToCompany(surname, role), result);
        }

        [Test, Order(2)]
        [TestCase("�����", "���������")]
        public void CreateTwoEqualsEmployees(string surname, string role)
        {
            Assert.AreEqual(_company.AddEmployeeToCompany(surname, role), true);
            Assert.AreEqual(_company.AddEmployeeToCompany(surname, role), false);
        }

        [Test, Order(3)]
        [TestCase("�����", "���������", "��������", "���������")]
        public void CreateTwoDifferentEmployees(string surname1, string role1, string surname2, string role2)
        {
            Assert.AreEqual(_company.AddEmployeeToCompany(surname1, role1), true);
            Assert.AreEqual(_company.AddEmployeeToCompany(surname2, role2), true);
        }

        [Test, Order(4)]
        [TestCase("�����", "������������")]
        [TestCase("��������", "���������")]
        [TestCase("�������", "���������")]
        public void FindExistingEmployeeBySurname(string surname, string role)
        {
            _company.AddEmployeeToCompany(surname, role);
            var e = _company.SearchEmployeeBySurname(surname);
            Assert.AreEqual(e?.Surname, surname);
            Assert.AreEqual(e?.GetRole(), role);
        }

        [Test, Order(4)]
        [TestCase("", null)]
        [TestCase("�������", null)]
        [TestCase("�����", "������������")]
        [TestCase("��������", "���������")]
        [TestCase("�������", "���������")]
        public void FindNonExistingEmployeeBySurname(string surname, string role)
        {
            var e = _company.SearchEmployeeBySurname(surname);
            Assert.AreEqual(e?.Surname, null);
            Assert.AreEqual(e?.GetRole(), null);
        }


        [TearDown]
        public void CleanFiles()
        {
            if (Directory.Exists(_fileInfo.StorageDirectory))
                Directory.Delete(_fileInfo.StorageDirectory, true);
        }
    }
}