using System;
using System.Globalization;
using SalaryCalculationLibrary;
using SalaryCalculationLibrary.Model;

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
                var surname = Console.ReadLine().Trim().ToLower();
                currentEmployee = company.FindEmployeeBySurname(surname);

                if (currentEmployee != null)
                    break;

                Console.Write("Такого сотрудника не найдено! Попробуйте повторить ввод: ");
            }

            Console.WriteLine($"Добро пожаловать, {FirstCharToUpper(currentEmployee.Surname)}! Ваша роль - {currentEmployee.RoleToStr}");

            while (true)
            {
                Console.WriteLine("Выберите желаемое действие:");
                ShowMenu(currentEmployee);
                Console.Write("Напишите номер действия:");

                bool result = int.TryParse(Console.ReadLine().Trim(), out int numOfItem);
                if (!result)
                {
                    Console.WriteLine("Произошла ошибка при выборе пункта меню, повторите Ваш выбор!");
                    continue;
                }

                DoMenuItem(currentEmployee, company, numOfItem);
            }

            Console.ReadKey();
        }

        private static void ShowMenu(Employee employee)
        {
            if(employee is Director)
            {
                Console.WriteLine("(1). Добавить сотрудника");
                Console.WriteLine("(2). Просмотреть отчёт по всем сотрудникам");
                Console.WriteLine("(3). Просмотреть отчёт по конкретному сотруднику");
                Console.WriteLine("(4). Добавить часы работы");
                Console.WriteLine("(5). Выход из программы");
            }
            else if (employee is Worker || employee is Freelancer)
            {
                Console.WriteLine("(1). Добавить отработанные часы");
                Console.WriteLine("(2). Просмотр отработанных часов и зарплаты");
                Console.WriteLine("(3). Выход из программы");
            }
            else
            {
                Console.WriteLine("У вас нет прав просматривать данный раздел!");
            }
        }

        private static void DoMenuItem(Employee employee, Company company, int numOfItem)
        {
            if (employee is Director)
            {
                switch (numOfItem)
                {
                    case 1:
                        Console.Write("Фамилия добавляемого сотрудника:");
                        string surname = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Должность добавляемого сотрудника:");
                        string role = Console.ReadLine();
                        Console.WriteLine();
                        Employee newEmployee = company.CreateEmployeeByRole(surname, role);
                        bool result = company.AddNewEmployee(newEmployee);
                        Console.WriteLine(GetStringByResult(result));
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    default:
                        break;
                }
            }
            else if (employee is Worker || employee is Freelancer)
            {
                switch (numOfItem)
                {
                    case 1:
                    case 2:
                    case 3:
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("У вас нет прав просматривать данный раздел!\n");
            }
        }

        private static string GetStringByResult(bool result)
        {
            if (result)
                return "Действие успешно выполнено.";
            else
                return "Произошла ошибка, действие не выполнено.";
        }

        static string FirstCharToUpper(string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
        }
    }
}