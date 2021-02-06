namespace SalaryCalculation.Library.Model
{
    public class Director : Employee
    {
        readonly decimal monthSalary = 200000;
        public Director(string surname) : base(surname)
        {

        }
        public override string GetRole() => "руководитель";
        public override decimal GetSalaryPerHour() => monthSalary / WorkStandarts.hoursInWorkMonth;
        public override decimal GetPaidByHours(int hours)
        {
            int overwork = hours > 160 ? 1 : 0;
            hours = overwork == 0 ? hours : 160;
            return GetSalaryPerHour() * hours + overwork * 20000;
        }
    }
}
