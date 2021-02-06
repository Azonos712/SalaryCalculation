namespace SalaryCalculation.Library.Model
{
    public class Worker : Employee
    {
        readonly decimal monthSalary = 120000;
        public Worker(string surname) : base(surname)
        {

        }
        public override string GetRole() => "сотрудник";
        public override decimal GetSalaryPerHour() => monthSalary / WorkStandarts.hoursInWorkMonth;
        public override decimal GetPaidByHours(int hours)
        {
            int overwork = hours > 160 ? hours - 160 : 0;
            hours = overwork == 0 ? hours : 160;
            return GetSalaryPerHour() * hours + GetSalaryPerHour() * 2 * overwork;
        }
    }
}