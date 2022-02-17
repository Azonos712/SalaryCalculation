namespace SalaryCalculation.Library.Model
{
    public class Director : Employee
    {
        private readonly decimal monthSalary = 200000;
        public Director(string surname) : base(surname) { }
        public override string GetRole() => "руководитель";
        public override decimal GetSalaryPerHour() => monthSalary / WorkStandarts.HOURS_IN_MONTH;
        protected override decimal CalcuclateOverTimePay(int overTimeHours)
        {
            return overTimeHours != 0 ? 20000 : 0;
        }
    }
}
