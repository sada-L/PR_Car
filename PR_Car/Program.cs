using PR_Car;
class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Чтобы начать работу, необходимо задать количество счетов.\n" +
                      "Введите количество авто, которое хотите создать:\n" +
                      ">");
        int quantityOfAccounts = Convert.ToInt32(Console.ReadLine());
        Avto[] accounts = new Avto[quantityOfAccounts];
        for (int i = 0; i < accounts.Length; i++) 
        {
            accounts[i] = new Avto();
        }
        while (true)
        {
            Console.WriteLine("Чтобы продолжить, нажмите Enter"); 
            ConsoleKeyInfo c = Console.ReadKey();
            if (c.Key == ConsoleKey.Enter)
            {
                AccountChoshing(accounts, quantityOfAccounts);
            }
            else
            {
                return;
            }
        }
        //Метод для выбора из массива объекта для применения метода
        void AccountChoshing(Avto[] avtos, int quantity) 
        {
            int nom = -1;
            Console.WriteLine("Необходимо выбрать авто чтобы продолжить.");
            while (nom > avtos.Length || nom < 0)
            {
                Console.Write($"Введите один из доступных номеров: \n" +
                              $"от 1 до {quantity}:\n" +
                              $">");
                nom = Convert.ToInt32(Console.ReadLine());
                if (nom > 0)
                {
                    if (nom <= avtos.Length)
                    {
                        avtos[nom - 1].Menu(avtos);
                    }
                    else { Console.WriteLine("Ошибка. Введите значение в заданом диапазоне."); }
                }
                else { Console.WriteLine("Ошибка. Введите значение в заданом диапазоне."); }
            }
        }
    }
}