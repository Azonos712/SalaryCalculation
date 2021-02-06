﻿using SalaryCalculation.Library;
using SalaryCalculation.Library.Model;
using SalaryCalculation.Library.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

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

            Console.WriteLine($"Добро пожаловать, {Utility.FirstCharToUpper(currentUser.Surname)}! Ваша роль - {currentUser.GetRole()}");

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


        private static void ShowMenu()
        {
            if (currentUser is Director)
            {
                Console.WriteLine("(1). Добавить сотрудника");
                Console.WriteLine("(2). Добавить часы работы");
                Console.WriteLine("(3). Просмотреть отчёт по всем сотрудникам");
                Console.WriteLine("(4). Просмотреть отчёт по конкретному сотруднику");
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
                        result = AddHoursForEmployee();
                        break;
                    case 3:
                        result = ShowJobReportByAllEmployees();
                        break;
                    case 4:
                        result = ShowJobReportByEmployee();
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
                        result = AddHoursForEmployee(currentUser);
                        break;
                    case 2:
                        result = ShowJobReportByEmployee(currentUser);
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

        static bool AddHoursForEmployee()
        {
            Console.Write("Фамилия сотрудника для которого будут добавлены часы:");
            string surname = Console.ReadLine();
            Employee employee = company.FindEmployeeBySurname(surname);

            if (employee != null)
                return AddHoursForEmployee(employee);
            else
                return false;
        }

        static bool ShowJobReportByEmployee()
        {
            Console.Write("Фамилия сотрудника для которого будут показаны часы:");
            string surname = Console.ReadLine();
            Employee employee = company.FindEmployeeBySurname(surname);

            if (employee != null)
                return ShowJobReportByEmployee(employee);
            else
                return false;
        }

        private static bool ShowJobReportByAllEmployees()
        {
            Console.Write("Дата начала:");
            bool result1 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime startDate);
            if (!result1)
                return false;

            Console.Write("Дата окончания:");
            bool result2 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime endDate);
            if (!result2)
                return false;

            Console.WriteLine();

            List<JobReport> result = company.GetJobReportsForPeriodByAllEmployees(startDate, endDate);

            Console.WriteLine($"Отчёт за период с {startDate:d} по {endDate:d}");

            var groupResult = result.GroupBy(x=>x.WorkPerson.Surname);

            decimal allMoney = 0;
            foreach (var item in groupResult)
            {
                int workHours = item.Sum(x => x.Hours);
                decimal money = item.FirstOrDefault().WorkPerson.GetPaidByHours(workHours);
                allMoney += money;
                Console.WriteLine($"{item.Key} отработал {workHours} часов и заработал за период {money}");
            }

            int allHours = result.Sum(x => x.Hours);

            Console.WriteLine($"Всего отработано {allHours} часов, сумма к выплате {allMoney}");

            return true;
        }

        private static bool AddHoursForEmployee(Employee employee)
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

            JobReport jr = new JobReport(employee, hours, date, description);
            return company.AddJobReportToEmployee(currentUser, jr);
        }

        private static bool ShowJobReportByEmployee(Employee employee)
        {
            Console.Write("Дата начала:");
            bool result1 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime startDate);
            if (!result1)
                return false;

            Console.Write("Дата окончания:");
            bool result2 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime endDate);
            if (!result2)
                return false;

            Console.WriteLine();

            List<JobReport> result = company.GetJobReportsForPeriodByEmployee(employee, startDate, endDate);

            Console.WriteLine($"Отчёт по сотруднику: {Utility.FirstCharToUpper(employee.Surname)} за период с {startDate:d} по {endDate:d}");
            foreach (var jr in result)
                Console.WriteLine($"{jr.WorkDay:d}, {jr.Hours} часов, {jr.Description}");

            int workHours = result.Sum(x => x.Hours);
            Console.WriteLine($"Итого: {workHours} часов, заработано: {employee.GetPaidByHours(workHours)}");

            return true;
        }

        private static void Exit() => Environment.Exit(0);

    }
}