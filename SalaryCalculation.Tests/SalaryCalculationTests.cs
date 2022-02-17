using NUnit.Framework;
using SalaryCalculation.Library.Model;

namespace SalaryCalculation.Tests
{
    internal class SalaryCalculationTests
    {
        Employee worker = new Worker("иванов");
        Employee director = new Director("афанасьев");
        Employee freelancer = new Freelancer("акопян");

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
    }
}
