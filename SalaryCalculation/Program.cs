using System;
using SalaryCalculationLibrary;

namespace SalaryCalculation
{
    class Program
    {
        static void Main()
        {
            Company company = new Company();
            Console.Write("Доброго времени суток! Назовите вашу фамилию, пожалуйста: ");
            var currentUser = Console.ReadLine();
            string role = "не определена";
            Console.WriteLine($"Добро пожаловать, {currentUser}! Ваша роль - {role}");
        }
    }
}