namespace Slimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            BMIManager manager = new BMIManager();
            Menu menu = new Menu(manager);
            menu.ShowMainMenu();
        }
    }
}