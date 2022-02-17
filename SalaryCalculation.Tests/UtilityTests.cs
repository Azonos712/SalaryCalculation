using NUnit.Framework;
using SalaryCalculation.Library;

namespace SalaryCalculation.Tests
{
    internal class UtilityTests
    {
        [TestCase("", "")]
        [TestCase("попов", "Попов")]
        [TestCase("Петров", "Петров")]
        [TestCase("hello", "Hello")]
        public void UpFirstCharInString(string a, string b)
        {
            Assert.AreEqual(Utility.FirstCharToUpper(a), b);
        }
    }
}
