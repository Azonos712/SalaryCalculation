namespace SalaryCalculation.Library.Model
{
    public class Freelancer : Employee
    {
        public Freelancer(string surname) : base(surname) { }
        public override string GetRole() => "фрилансер";
        public override decimal GetSalaryPerHour() => 1000;
        protected override decimal CalcuclateOverTimePay(int overTimeHours) => CalcuclateDefaultPay(overTimeHours);
    }
}
