using System;
using SalaryCalculationLibrary;

namespace SalaryCalculation
{
    class Program
    {
        static void Main()
        {
            Company company = new Company("SoftwareDevelopment");
            Employee currentEmployee = null;

            Console.Write("Доброго времени суток! Назовите вашу фамилию, пожалуйста: ");

            while (true)
            {
                var surname = Console.ReadLine();
                currentEmployee = company.FindEmployeeBySurname(surname);

                if (currentEmployee != null)
                    break;
                
                Console.Write("Такого сотрудника не найдено! Попробуйте повторить ввод: ");
            }

            Console.WriteLine($"Добро пожаловать, {currentEmployee.Surname}! Ваша роль - {currentEmployee.RoleToStr()}");
            Console.ReadKey();
        }
    }
}