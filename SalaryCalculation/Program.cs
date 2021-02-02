using System;
using System.Globalization;
using SalaryCalculationLibrary;
using SalaryCalculationLibrary.Model;

namespace SalaryCalculation
{
    class Program
    {
        static Company company;
        static Employee currentUser;
        static void Main()
        {
            company = new Company("SoftwareDevelopment");

            Console.Write("Доброго времени суток! Назовите вашу фамилию, пожалуйста: ");

            while (true)
            {
                var surname = Console.ReadLine().Trim().ToLower();
                currentUser = company.FindEmployeeBySurname(surname);

                if (currentUser != null)
                    break;

                Console.Write("Такого сотрудника не найдено! Попробуйте повторить ввод: ");
            }

            Console.WriteLine($"Добро пожаловать, {FirstCharToUpper(currentUser.Surname)}! Ваша роль - {currentUser.RoleToStr}");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите желаемое действие:");
                ShowMenu();
                Console.Write("Напишите номер действия:");

                bool result = int.TryParse(Console.ReadLine().Trim(), out int numOfItem);
                if (!result)
                {
                    Console.WriteLine("Произошла ошибка при выборе пункта меню, повторите Ваш выбор!");
                    continue;
                }

                Console.WriteLine();
                DoMenuItem(numOfItem);
            }
        }
        static string FirstCharToUpper(string s) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);

        private static void ShowMenu()
        {
            if(currentUser is Director)
            {
                Console.WriteLine("(1). Добавить сотрудника");
                Console.WriteLine("(2). Просмотреть отчёт по всем сотрудникам");
                Console.WriteLine("(3). Просмотреть отчёт по конкретному сотруднику");
                Console.WriteLine("(4). Добавить часы работы");
                Console.WriteLine("(5). Выход из программы");
            }
            else if (currentUser is Worker || currentUser is Freelancer)
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

        private static void DoMenuItem(int numOfItem)
        {
            bool result = false;
            if (currentUser is Director)
            {
                switch (numOfItem)
                {
                    case 1:
                        result = AddEmployee();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        Exit();
                        break;
                    default:
                        break;
                }
            }
            else if (currentUser is Worker || currentUser is Freelancer)
            {
                switch (numOfItem)
                {
                    case 1:
                        result = AddHours();
                        break;
                    case 2:
                        break;
                    case 3:
                        Exit();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("У вас нет прав просматривать данный раздел!\n");
            }

            Console.WriteLine(GetStringByResult(result));
        }
        private static string GetStringByResult(bool result)
        {
            if (result)
                return "Действие успешно выполнено.";
            else
                return "Произошла ошибка, действие не выполнено.";
        }

        private static bool AddHours()
        {
            Console.Write("Количество отработанных часов:");
            bool result1 = byte.TryParse(Console.ReadLine().Trim(), out byte hours);
            if (!result1)
                return false;

            Console.Write("Дата:");
            bool result2 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime date);
            if (!result2)
                return false;

            if (date > DateTime.Now)
                return false;

            Console.Write("Дополнительное описание:");
            string description = Console.ReadLine().ToLower();

            JobReport jr = new JobReport(currentUser, hours, date, description);
            return company.AddHoursToEmployee(jr);
        }

        private static bool AddEmployee()
        {
            Console.Write("Фамилия добавляемого сотрудника:");
            string surname = Console.ReadLine();
            Console.Write("Должность добавляемого сотрудника:");
            string role = Console.ReadLine();
            Console.WriteLine();

            Employee newEmployee = company.CreateEmployeeByRole(surname.ToLower(), role.ToLower());
            return company.AddNewEmployee(newEmployee);
        }

        private static void Exit() => Environment.Exit(0);

    }
}