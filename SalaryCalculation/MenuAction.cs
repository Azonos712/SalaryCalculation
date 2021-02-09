using System;

namespace SalaryCalculation
{
    class MenuAction
    {
        public string Description { get; private set; }
        public string[] Access { get; private set; }
        public Action Operation { get; set; }

        public MenuAction(string descr, string[] access, Action action)
        {
            Description = descr;
            Access = access;
            Operation = action;
        }
    }
}
