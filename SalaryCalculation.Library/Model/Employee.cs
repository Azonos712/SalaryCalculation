using System;

namespace SalaryCalculation.Library.Model
{
    public abstract class Employee : IEquatable<Employee>
    {
        public string Surname { get; private set; }
        public Employee(string surname)
        {
            Surname = surname;
        }
        public abstract string GetRole();
        public abstract decimal GetSalaryPerHour();
        public decimal GetPaidByHours(int hours)
        {
            int overTimeHours = hours > WorkStandarts.HOURS_IN_MONTH ? hours - WorkStandarts.HOURS_IN_MONTH : 0;
            int defaultHours = overTimeHours == 0 ? hours : WorkStandarts.HOURS_IN_MONTH;
            return CalcuclateDefaultPay(defaultHours) + CalcuclateOverTimePay(overTimeHours);
        }
        protected decimal CalcuclateDefaultPay(int defaultHours) => GetSalaryPerHour() * defaultHours;
        protected abstract decimal CalcuclateOverTimePay(int overTimeHours);
        public override string ToString() => Surname + "," + GetRole();

        public bool Equals(Employee other)
        {
            return this.Surname == other.Surname && this.GetRole() == other.GetRole();
        }
    }
}
