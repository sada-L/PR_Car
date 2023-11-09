using PR_Car;
class Program
{
    public static void Main(string[] args)
    {
        AvtoStore avtoStore = new AvtoStore();

        while (true)
        {
            Console.WriteLine("Чтобы продолжить, нажмите Enter"); 
            ConsoleKeyInfo c = Console.ReadKey();
            if (c.Key == ConsoleKey.Enter)
            { 
                Avto schAvto = avtoStore.AccMenu();
                schAvto.Menu(avtoStore.Acc);
            }
            else
            {
                return;
            }
        }
    }
}