namespace PR_Car;
public class Bus : Avto {
    private int _passCount = 0;
    private int _passMax = 30;
    private int[] _cordEnd = new int[2];
    //вывод информации
    protected override void Out() {
        Console.WriteLine
        ("--------------------------------\n" +
         "Номер: {0}\n" +
         "Толпиво: {1:f}\n" +
         "Местоположение: {2},{3}\n" +
         "Максимум топлива: {4}\n" +
         "Суммарный пробег: {5:f}\n" +
         "Количество пассажиров: {6:f}\n" +
         "--------------------------------", 
            _number,_fuelCount,_corA[0],_corA[1],_fuelMax,_sumDistance,_passCount);
    }
    private void MoveToEnd() {
        if (_weight >= 100 && _weight < 1000) { 
            _kf = 0.4; 
        } else if (_weight >= 1000 && _weight <= 2000) {
            _kf = 0.8; 
        }
        double dis = DistanceToPlace();
        double prob = dis;
        SpeedDeterm();
        while (true) {
            double rem = Remains(dis);
            if (rem <= _fuelCount) {
                _fuelCount -= rem;
                _corA = _corB;
                _sumDistance += prob;
                Console.WriteLine("Вы проехали: {0:f} км, топлива осталось: {1:f} л, местоположение: {2},{3}, расcход: {4}",
                    prob,_fuelCount,_corA[0],_corB[1],_fuelRate);
                return;
            } 
            else {
                Console.Write("Вам не хватило топлива, хотите заправиться: +/-\n" + ">");
                string ans = Console.ReadLine();
                if (ans == "+") {
                    dis -= _fuelCount / (_fuelRate / 100);
                    _fuelCount = 0;
                    Console.WriteLine($"Топливо кончилось, вы проехали: {prob - dis:f}км");
                    Refill();
                }
                else { 
                    Console.WriteLine("Вы заглохли"); 
                    return; 
                }
            }
        }
    }
    private double DistanceToPlace() {
        while (true) {
            try {
                _corB = new int[2];
                string[] s = Console.ReadLine().Split(',',' ',';');
                for(int i = 0; i < _corB.Length; i++) 
                    _corB[i] = Int32.Parse(s[i]);
                double c = Math.Sqrt(Math.Pow(_corB[0] - _corA[0], 2) + Math.Pow(_corB[1] - _corA[1], 2));
                return Math.Round(c, 2);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
    protected override void SpeedDeterm() {
        while (true) {
            Console.Write("Введите с какой скоростью поедете: ");
            double speed = Convert.ToInt32(Console.ReadLine());
            speed -= speed * _kf; 
            if (speed > 0) {
                if (speed <= 45) {
                    _fuelRate = 12; return;
                } else if (speed > 46 && speed <= 100) {
                    _fuelRate = 9; return;
                } else if (speed > 101 && speed <= 180) { 
                    _fuelRate = 12.5; return;
                } else Console.WriteLine("Невозможно ехать с такой скоротью");
            } else Console.WriteLine("Невозможно ехать с такой скоротью");
        }
    }
    protected override void Move() {
        while (true) {
            Console.Write("Введите координаты начала маршрута: ");
            MoveToPlace();
            Loading();
            Console.Write("Введите координаты места разгрузки: ");
            MoveToPlace();
            Unloading();
            Console.Write("Хотите продолжить: +/-\n" + ">");
            if (Console.ReadLine() == "-")
                return;
        }
    }
    private void Boarding() {
        Random random = new Random();
        int enter = random.Next(0, 30);
        int exit = random.Next(0, 30);
        _passCount -= exit;
        if (_passCount < 0)
            _passCount = 0;
        _passCount += enter;
        if (_passCount > 30)
            _passCount = 30;
    }
}