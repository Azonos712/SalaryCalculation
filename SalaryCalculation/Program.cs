using System;

namespace SalaryCalculation
{
    class Program
    {
        static void Main()
        {
            Console.Write("Доброго времени суток! Назовите вашу фамилию, пожалуйста: ");
            var currentUser = Console.ReadLine();
            string role = "не определена";
            Console.WriteLine($"Добро пожаловать, {currentUser}! Ваша роль - {role}");
        }
    }
}