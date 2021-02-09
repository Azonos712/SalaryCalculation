namespace SalaryCalculation
{
    class Program
    {
        static void Main()
        {
            MenuService menuService = new MenuService("SoftwareDevelopment");

            menuService.Authorization();

            menuService.ShowMenu();
        }
    }
}