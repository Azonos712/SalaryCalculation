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
        public virtual decimal GetPaidByHours(int hours) => GetSalaryPerHour() * hours;
        public override string ToString() => Surname + "," + GetRole();
    }
}
