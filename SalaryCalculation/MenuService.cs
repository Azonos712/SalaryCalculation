﻿using SalaryCalculation.Library;
using SalaryCalculation.Library.Model;
using SalaryCalculation.Library.Storage;
using SalaryCalculation.Library.Storage.FileStorage;
using System;
using System.IO;

namespace SalaryCalculation
{
    class MenuService
    {
        private readonly Company _company;
        private FilesInfo _fileInfo;
        private Employee _currentEmployee;
        private Actions _actions;

        public MenuService(string companyName)
        {
            _fileInfo = new FilesInfo(companyName);
            _company = new Company(
                companyName,
                new FileRepositoryOfAllEmployees(_fileInfo),
                new FileRepositoryOfJobReports(_fileInfo));
        }

        public void Authentication()
        {
            Console.Write("Доброго времени суток! Назовите вашу фамилию, пожалуйста: ");

            while (true)
            {
                var surname = Console.ReadLine().Trim().ToLower();
                _currentEmployee = _company.SearchEmployee(surname);

                if (_currentEmployee != null)
                    break;

                Console.Write("Такого сотрудника не найдено! Попробуйте повторить ввод: ");
            }

            Authorization();
        }

        private void Authorization()
        {
            Console.WriteLine($"Добро пожаловать, {Utility.FirstCharToUpper(_currentEmployee.Surname)}! Ваша статус - {_currentEmployee.GetRole()}");
            Console.WriteLine();

            _actions = new Actions(_company, _currentEmployee);
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("Выберите желаемый пункт меню:");

                GetMenu();

                SelectMenuItem();
            }
        }

        private void GetMenu()
        {
            for (int i = 0; i < _actions.listOfAvailableActions.Count; i++)
                Console.WriteLine($"({i + 1}) {_actions.listOfAvailableActions[i].Description}");
        }

        private void SelectMenuItem()
        {
            Console.Write("Напишите номер действия:");

            bool result = int.TryParse(Console.ReadLine().Trim(), out int numOfItem);
            Console.WriteLine();

            if (!result || numOfItem <= 0 || numOfItem > _actions.listOfAvailableActions.Count)
                Console.WriteLine("Произошла ошибка при выборе пункта меню, повторите Ваш выбор!");
            else
                _actions.listOfAvailableActions[numOfItem - 1].Operation();

            Console.WriteLine();
        }
    }
}
