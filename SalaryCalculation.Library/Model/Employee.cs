namespace SalaryCalculation.Library.Model
{
    public abstract class Employee
    {
        public string Surname { get; }
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
    }
}
