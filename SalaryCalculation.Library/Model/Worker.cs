namespace SalaryCalculation.Library.Model
{
    public class Worker : Employee
    {
        private readonly decimal monthSalary = 120000;
        public Worker(string surname) : base(surname) { }
        public override string GetRole() => "сотрудник";
        public override decimal GetSalaryPerHour() => monthSalary / WorkStandarts.HOURS_IN_MONTH;
        protected override decimal CalcuclateOverTimePay(int overTimeHours)
        {
            return GetSalaryPerHour() * 2 * overTimeHours;
        }
    }
}