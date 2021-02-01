using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculationLibrary
{
    public enum Roles
    {
        Director,
        Worker,
        Freelancer
    }

    public class Employee
    {
        public string Surname { get; set; }
        public Roles Role { get; set; }

        public Employee(string surname, string role)
        {
            Surname = surname;
            Role = StrToRole(role);
        }

        private Roles StrToRole(string role)
        {
            switch (role)
            {
                case "руководитель":
                    return Roles.Director;
                case "сотрудник":
                    return Roles.Worker;
                case "фрилансер":
                    return Roles.Freelancer;
                default:
                    return Roles.Worker;
            }
        }
        public string RoleToStr()
        {
            switch (Role)
            {
                case Roles.Director:
                    return "руководитель";
                case Roles.Worker:
                    return "сотрудник";
                case Roles.Freelancer:
                    return "фрилансер";
                default:
                    return "не определена";
            }
        }
    }
}
