namespace PR_Car;

public class Avto
{
    private string _number;      //номер авто
    private double _fuelCount;   //количество топлива
    private double _fuelMax;     //максимум топлива
    private double _fuelRate;    //расход на 100 км
    private double _sumDistance; //пробег
    private List<int> _corA = new List<int>() {0,0}; //начальная координата
    private List<int> _corB;     //конечная координата

    public string Number
    {
        get { return _number; }
    }
    //Ввод информации
    public Avto()
    {
        Info();
    }
    void Info()
    {
        Console.Write("Введите номер, объём бака, рассход топлива\n" +
                      ">");
        string[] s =Console.ReadLine().Split(' ',',',';');
        _number = s[0];
        _fuelMax = Convert.ToDouble(s[1]);
        _fuelRate = Convert.ToDouble(s[2]);
    }
    //Вывод информации
    void Out()
    {
        Console.WriteLine
        ("--------------------------------\n" +
         "Номер: {0}\n" +
         "Толпиво: {1:f}\n" +
         "Местоположение: {2},{3}\n" +
         "Максимум топлива: {4}\n" +
         "Рассход на 100 км: {5}\n" +
         "Суммарный пробег: {6:f}\n" +
         "--------------------------------", 
            _number,_fuelCount,_corA[0],_corA[1],_fuelMax,_fuelRate,_sumDistance);
    }
    //Заправка
    void Refill()
    {
        bool exit = true;
        while (exit)
        {
            Console.Write("Сколько топлива заправить: ");
            double top = Convert.ToInt32(Console.ReadLine());
            if (top >= 0)
            {
                if (top + _fuelCount <= _fuelMax)
                {
                    _fuelCount += top;
                    exit = false;
                }
                else
                {
                    Console.WriteLine("Невозможно заправить больше максимума, попробуйте еще раз");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
            }
        }
    }
    //Цикл езды
    void Move()
    {
        // Console.WriteLine("Ввидите расстояние: ");
        // double dis = Convert.ToInt32(Console.ReadLine());
        double dis = Distance();
        double prob = dis;
        while (true)
        {
            double rem = Remains(dis);
            if (rem <= _fuelCount)
            {
                _fuelCount -= rem;
                _corA = _corB;
                _sumDistance += prob;
                Console.WriteLine("Вы проехали: {0:f} км, топлива осталось: {1:f} л, местоположение: {2},{3}",
                    prob,_fuelCount,_corA[0],_corB[1]);
                return;
            }
            else
            {
                Console.Write("Вам не хватило топлива, хотите заправиться: +/-\n" +
                              ">");
                string ans = Console.ReadLine();
                if (ans == "+")
                {
                    dis -= _fuelCount / (_fuelRate / 100);
                    _fuelCount = 0;
                    Refill();
                }
                else
                {
                    Console.WriteLine("Вы заглохли");
                    return;
                }
            }
        }
    }
    //Остаток топлива
    double Remains(double dis)
    {
        double d = Math.Round(_fuelRate / 100 * dis,2);
        return d;
    }
    //Расчет дистанции 
    double Distance()
    {
        _corB = new List<int>();
        Console.Write("Введите координаты: ");
        string[] s = Console.ReadLine().Split(',',' ',';');
        foreach (string s2 in s) {_corB.Add(Int32.Parse(s2));}
        double c = Math.Sqrt(
            Math.Pow(_corB[0] - _corA[0], 2) +
            Math.Pow(_corB[1] - _corA[1], 2)
        );
        return Math.Round(c, 2);
    }
    //Метод "авария"
    void Crash(List<Avto> allAvtos)
    {
        Random random = new Random();
        int ran1 = random.Next(0, allAvtos.Count);
        int ran2 = random.Next(0, allAvtos.Count);
        for (int i = 0; i < allAvtos.Count; i++)
        {
            for (int j = 0; j < allAvtos.Count; j++)
            {
                if (i != j)
                { 
                    if (allAvtos[i]._corA[0] == allAvtos[j]._corA[0] && 
                        allAvtos[i]._corA[1] == allAvtos[j]._corA[1])
                    {
                        /*allAvtos[i] = allAvtos[ran1];
                        allAvtos[j] = allAvtos[ran2];*/
                        Console.WriteLine("CRASH!!!");
                        /*Console.WriteLine(allAvtos[i]._number);
                        Console.WriteLine(allAvtos[j]._number);*/
                    }
                }
            }
        }
    }
    //Пользовательский интерфейс
    public void Menu(List<Avto> allAvtos)
    {
        while (true)
        {   
            Crash(allAvtos);
            Console.Write
            ("--------------------------------\n" +
             "Выберете необходимое действие:\n" +
             "0. Ввод данных\n" +
             "1. Показать данные авто\n" +
             "2. Заправиться\n" +
             "3. Передвижение\n" +
             "4. Выход\n" +
             ">");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 0: Info(); break;
                case 1: Out(); break;
                case 2: Refill(); break;
                case 3: Move(); break;
                case 4: return;
            }
        }
    }
}