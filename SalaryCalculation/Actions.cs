using SalaryCalculation.Library;
using SalaryCalculation.Library.Model;
using SalaryCalculation.Library.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalculation
{
    class Actions
    {
        private readonly List<MenuAction> listOfAllActions;
        public readonly List<MenuAction> listOfAvailableActions;
        private Company _company;
        private Employee _currentEmployee;

        public Actions(Company company, Employee employee)
        {
            _company = company;
            _currentEmployee = employee;
            listOfAllActions = CreateAllMenuAction();
            listOfAvailableActions = CreateAccessibleMenu(employee);
        }

        private List<MenuAction> CreateAllMenuAction()
        {
            var temp = new List<MenuAction>
            {
                new MenuAction("Добавить нового сотрудника", new string[] { "руководитель" }, AddEmployee),
                new MenuAction("Добавить отработанные часы для себя", new string[] { "руководитель", "сотрудник", "фрилансер"}, AddHoursForCurrentEmployee),
                new MenuAction("Добавить отработанные часы для другого сотрудника", new string[] { "руководитель",}, AddHoursForAnotherEmployee),
                new MenuAction("Просмотреть свой отчёт по работе", new string[] { "руководитель", "сотрудник", "фрилансер" }, ShowJobReportByCurrentEmployee),
                new MenuAction("Просмотреть отчёт по работе другого сотрудника", new string[] { "руководитель"}, ShowJobReportByAnotherEmployee),
                new MenuAction("Просмотреть отчёт по всем сотрудникам", new string[] { "руководитель" }, ShowJobReportByAllEmployees),
                new MenuAction("Выход из программы", new string[] { "руководитель", "сотрудник", "фрилансер" }, Exit)
            };

            return temp;
        }

        private List<MenuAction> CreateAccessibleMenu(Employee employee)
        {
            var temp = new List<MenuAction>();
            foreach (var action in listOfAllActions)
                if (Array.Exists(action.Access, x => x == employee.GetRole()))
                    temp.Add(action);

            return temp;
        }

        private void AddEmployee()
        {
            Console.Write("Фамилия добавляемого сотрудника:");
            string surname = Console.ReadLine().Trim().ToLower();
            Console.Write("Должность добавляемого сотрудника:");
            string role = Console.ReadLine().Trim().ToLower();
            Console.WriteLine();

            if (_company.AddEmployee(surname, role))
                Console.WriteLine("Сотрудник добавлен.");
            else
                Console.WriteLine("Произошла ошибка. Возможно такой сотрудник уже существует.");
        }

        private void AddHoursForCurrentEmployee()
        {
            AddHoursForEmployee(_currentEmployee);
        }

        private void AddHoursForAnotherEmployee()
        {
            Console.Write("Фамилия сотрудника для которого будут добавлены часы:");
            string surname = Console.ReadLine().Trim().ToLower();
            Employee employee = _company.SearchEmployee(surname);

            if (employee != null)
                AddHoursForEmployee(employee);
            else
                Console.WriteLine("Такого сотрудника не существует!");
        }

        private void AddHoursForEmployee(Employee workPerson)
        {
            try
            {
                Console.Write("Количество отработанных часов:");

                bool result1 = byte.TryParse(Console.ReadLine().Trim(), out byte hours);
                if (!result1)
                    throw new Exception("Произошла ошибка при вводе количества часов!");

                Console.Write("Дата:");
                bool result2 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime date);
                if (!result2)
                    throw new Exception("Произошла ошибка при вводе даты!");

                if (date > DateTime.Now)
                    throw new Exception("Нельзя заполнять отчёт передним числом!");

                Console.Write("Дополнительное описание:");
                string description = Console.ReadLine().Trim().ToLower();

                JobReport jr = new JobReport(workPerson, hours, date, description);

                if (_company.AddJobReport(_currentEmployee, jr))
                    Console.WriteLine("Отчёт добавлен.");
                else
                    Console.WriteLine("Произошла ошибка. Возможно отчёт на эту дату уже был составлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ShowJobReportByCurrentEmployee()
        {
            ShowJobReportByEmployee(_currentEmployee);
        }

        private void ShowJobReportByAnotherEmployee()
        {
            Console.Write("Фамилия сотрудника, чьи отчёты будут показаны:");
            string surname = Console.ReadLine().Trim().ToLower();
            Employee employee = _company.SearchEmployee(surname);

            if (employee != null)
                ShowJobReportByEmployee(employee);
            else
                Console.WriteLine("Такого сотрудника не существует!");
        }

        private void ShowJobReportByEmployee(Employee employee)
        {
            try
            {
                Console.Write("Дата начала:");
                bool result1 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime startDate);
                if (!result1)
                    throw new Exception("Произошла ошибка при вводе начальной даты!");

                Console.Write("Дата окончания:");
                bool result2 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime endDate);
                if (!result2)
                    throw new Exception("Произошла ошибка при вводе конечной даты!");

                Console.WriteLine();

                List<JobReport> result = _company.GetJobReportsForPeriodByEmployee(employee, startDate, endDate);

                Console.WriteLine($"Отчёт по сотруднику: {Utility.FirstCharToUpper(employee.Surname)} за период с {startDate:d} по {endDate:d}");
                foreach (var jr in result)
                    Console.WriteLine($"{jr.WorkDay:d}, {jr.Hours} часов, {jr.Description}");

                int workHours = result.Sum(x => x.Hours);
                Console.WriteLine($"Итого: {workHours} часов, заработано: {employee.GetPaidByHours(workHours)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ShowJobReportByAllEmployees()
        {
            Console.Write("Дата начала:");
            bool result1 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime startDate);
            if (!result1)
                throw new Exception("Произошла ошибка при вводе начальной даты!");

            Console.Write("Дата окончания:");
            bool result2 = DateTime.TryParse(Console.ReadLine().Trim(), out DateTime endDate);
            if (!result2)
                throw new Exception("Произошла ошибка при вводе конечной даты!");

            Console.WriteLine();

            List<JobReport> result = _company.GetJobReportsForPeriodByAllEmployees(startDate, endDate);

            Console.WriteLine($"Отчёт за период с {startDate:d} по {endDate:d}");

            var groupResult = result.GroupBy(x => x.WorkPerson.Surname);

            decimal allMoney = 0;
            foreach (var item in groupResult)
            {
                int workHours = item.Sum(x => x.Hours);
                decimal money = item.FirstOrDefault().WorkPerson.GetPaidByHours(workHours);
                allMoney += money;
                Console.WriteLine($"{Utility.FirstCharToUpper(item.Key)} отработал {workHours} часов и заработал за период {money}");
            }

            int allHours = result.Sum(x => x.Hours);

            Console.WriteLine($"Всего отработано {allHours} часов, сумма к выплате {allMoney}");
        }

        private void Exit() => Environment.Exit(0);

    }
}
