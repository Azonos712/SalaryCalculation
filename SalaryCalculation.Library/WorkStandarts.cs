namespace SalaryCalculation.Library
{
    class WorkStandarts
    {
        public const byte workDays = 5;
        public const byte workWeeks = 4;
        public const byte hoursInWorkDay = 8;
        public const byte hoursInWorkWeek = hoursInWorkDay * workDays;
        public const byte hoursInWorkMonth = hoursInWorkWeek * workWeeks;
    }
}
