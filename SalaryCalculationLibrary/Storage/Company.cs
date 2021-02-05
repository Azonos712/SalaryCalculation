using SalaryCalculationLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SalaryCalculationLibrary
{
    public class Company
    {
        readonly string companyName;
        readonly string storageDirectory;
        readonly string fileNameOfAllEmployees = "\\listOfAllEmployes.csv";
        public Company(string name)
        {
            companyName = name;
            storageDirectory = Directory.GetCurrentDirectory() + "\\" + companyName;
            CheckStorage();
        }

        private void CheckStorage()
        {
            if (!Directory.Exists(storageDirectory))
                Directory.CreateDirectory(storageDirectory);

            if (!File.Exists(storageDirectory + fileNameOfAllEmployees))
                File.Create(storageDirectory + fileNameOfAllEmployees);

            if (!File.Exists(storageDirectory + new Worker("").GetDataFileName()))
                File.Create(storageDirectory + new Worker("").GetDataFileName());

            if (!File.Exists(storageDirectory + new Director("").GetDataFileName()))
                File.Create(storageDirectory + new Director("").GetDataFileName());

            if (!File.Exists(storageDirectory + new Freelancer("").GetDataFileName()))
                File.Create(storageDirectory + new Freelancer("").GetDataFileName());
        }

        public Employee FindEmployeeBySurname(string surname)
        {
            string line;
            using (StreamReader sr = new StreamReader(storageDirectory + fileNameOfAllEmployees))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');
                    if (sLine[0] == surname)
                    {
                        return CreateEmployeeByRole(sLine[0], sLine[1]);
                    }
                }
            }
            return null;
        }
        public Employee CreateEmployeeByRole(string surname, string role)
        {
            switch (role)
            {
                case "сотрудник":
                    return new Worker(surname);
                case "руководитель":
                    return new Director(surname);
                case "фрилансер":
                    return new Freelancer(surname);
                default:
                    return null;
            }
        }

        public bool AddNewEmployee(Employee e)
        {
            if (e == null)
                return false;

            if (FindEmployeeBySurname(e.Surname) != null)
                return false;

            using (StreamWriter sw = new StreamWriter(storageDirectory + fileNameOfAllEmployees, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(e.ToString());
            }

            return true;
        }

        public bool AddJobReportToEmployee(JobReport jr)
        {
            Employee employee = jr.WorkPerson;

            if (employee is Freelancer && DateTime.Today.AddDays(-2) > jr.WorkDay)
                return false;

            using (StreamWriter sw = new StreamWriter(storageDirectory + employee.GetDataFileName(), true, System.Text.Encoding.Default))
            {
                sw.WriteLine(jr.WorkDay.ToString("d") + "," + employee.Surname + "," + jr.Hours + "," + jr.Description);
            }

            return true;
        }

        public JobReport FindJobReportBySurnameAndDate(Employee employee, DateTime date)
        {
            string line;
            using (StreamReader sr = new StreamReader(storageDirectory + employee.GetDataFileName()))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');
                    if (sLine[0] == date.ToString("d"))
                    {
                        if (sLine[1] == employee.Surname)
                        {
                            return new JobReport(employee, byte.Parse(sLine[2]), date, sLine[3]);
                        }
                    }
                }
            }
            return null;
        }

        public string ShowJobReportForPeriod(Employee employee, DateTime startDate, DateTime endDate)
        {
            string result = $"Отчёт по сотруднику: {Utility.FirstCharToUpper(employee.Surname)} за период с {startDate:d} по {endDate:d}\n";
            int workHours = 0;
            string line;
            using (StreamReader sr = new StreamReader(storageDirectory + employee.GetDataFileName()))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] sLine = line.Split(',');
                    if (startDate <= DateTime.Parse(sLine[0]) && DateTime.Parse(sLine[0]) <= endDate)
                    {
                        if (sLine[1] == employee.Surname)
                        {
                            result += $"{sLine[0]}, {sLine[2]} часов, {sLine[3]}" + '\n';
                            workHours += int.Parse(sLine[2]);
                        }
                    }
                }
            }
            decimal money = employee.GetPaidByHours(workHours);
            result += $"Итого: {workHours} часов, заработано: {money}";
            return result;
        }
    }
}
