namespace PR_Car;
public class Avto {
    protected string _number;      //номер авто
    protected double _fuelCount;   //количество топлива
    protected double _fuelMax;     //максимум топлива
    protected double _fuelRate;    //расход на 100 км
    protected double _sumDistance; //пробег
    protected int[] _corA = new int[] {0,0}; //начальная координата
    protected int[] _corB;         //конечная координата
    protected double _speed;
    protected double _speedMax = 180;
    public string Number { get { return _number; } }
    //Ввод информации
    public Avto() => Info();
    protected void Info() {
        while (true) {
            try {
                Console.Write("Введите номер, объём бака\n" + ">");
                string[] s =Console.ReadLine().Split(' ',',',';');
                _number = s[0];
                _fuelMax = Convert.ToDouble(s[1]);
                return;
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
    //Вывод информации
    protected void Out() {
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
    protected void Refill() {
        while (true) {
            Console.Write("Сколько топлива заправить: ");
            double top = Convert.ToInt32(Console.ReadLine());
            if (top >= 0)
                if (top + _fuelCount <= _fuelMax) {
                    _fuelCount += top; return;
                }
                else Console.WriteLine("Невозможно заправить больше максимума, попробуйте еще раз");
            else Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
        }
    }
    //Цикл езды
    protected virtual void Move() {
        SpeedDeterm();
        double dis = Distance();
        double prob = dis;
        while (true) {
            double rem = Remains(dis);
            if (rem <= _fuelCount) {
                _fuelCount -= rem;
                _corA = _corB;
                _sumDistance += prob;
                Console.WriteLine("Вы проехали: {0:f} км, топлива осталось: {1:f} л, местоположение: {2},{3}, скорость: {4}",
                    prob,_fuelCount,_corA[0],_corB[1],_speed);
                _speed = 0;
                return;
            }
            else {
                Console.Write("Вам не хватило топлива, хотите заправиться: +/-\n" + ">");
                string ans = Console.ReadLine();
                if (ans == "+") {
                    dis -= _fuelCount / (_fuelRate / 100);
                    _fuelCount = 0;
                    Console.WriteLine($"Топливо кончилось, вы проехали: {prob - dis}км");
                    Refill();
                }
                else { Console.WriteLine("Вы заглохли"); return; }
            }
        }
    }
    //Остаток топлива
    protected virtual void SpeedDeterm() {
        while (true) {
            Console.Write("Введите с какой скоростью поедете: ");
            int speed = Convert.ToInt32(Console.ReadLine());
            if (speed > 0) {
                if (speed <= 45) {
                    _fuelRate = 12; return;
                } else if (speed > 46 && speed <= 100) {
                    _fuelRate = 9; return;
                } else if (speed > 101 && speed <= _speedMax) { 
                    _fuelRate = 12.5; return;
                } else Console.WriteLine("Невозможно ехать с такой скоротью");
            } else Console.WriteLine("Невозможно ехать с такой скоротью");

            _speed = speed;
        }
    }
    protected double Remains(double dis) => Math.Round(_fuelRate / 100 * dis, 2);
    
    //Расчет дистанции 
    protected virtual double Distance() {
        while (true) {
            try {
                _corB = new int[2];
                Console.Write("Введите координаты: ");
                string[] s = Console.ReadLine().Split(',',' ',';');
                for(int i = 0; i < _corB.Length; i++) 
                    _corB[i] = Int32.Parse(s[i]);
                double c = Math.Sqrt(Math.Pow(_corB[0] - _corA[0], 2) + Math.Pow(_corB[1] - _corA[1], 2));
                return Math.Round(c, 2);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
    //Метод "авария"
    protected void Crash(List<Avto> allAvtos) {
        /*Random random = new Random();
        int ran1 = random.Next(0, allAvtos.Count);
        int ran2 = random.Next(0, allAvtos.Count);*/
        for (int i = 0; i < allAvtos.Count; i++) 
            for (int j = 0; j < allAvtos.Count; j++)
                if (i != j)
                    if (allAvtos[i]._corA[0] == allAvtos[j]._corA[0] && allAvtos[i]._corA[1] == allAvtos[j]._corA[1])
                        Console.WriteLine("CRASH!!!");
    }
    //Пользовательский интерфейс
    public virtual void Menu(List<Avto> allAvtos) {
        while (true) {   
            Crash(allAvtos);
            Console.Write
            ("--------------------------------\n" +
             "Выберете необходимое действие:\n" +
             "1. Показать данные авто\n" +
             "2. Заправиться\n" +
             "3. Передвижение\n" +
             "4. Выход\n" +
             ">");
            switch (Convert.ToInt32(Console.ReadLine())) {
                case 1: Out(); break;
                case 2: Refill(); break;
                case 3: Move(); break;
                case 4: return;
            }
        }
    }
}