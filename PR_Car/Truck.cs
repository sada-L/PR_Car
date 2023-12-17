namespace PR_Car;

public class Truck : Avto {
    private int _weight;
    private int _weightMax = 2000;
    private double _kf;
    private void MoveToPlace() {
        if (_weight >= 100 && _weight < 1000) { 
            _kf = 0.4; 
        } else if (_weight >= 1000 && _weight <= 2000) {
            _kf = 0.8; 
        }
        SpeedDeterm();
        double dis = DistanceToPlace();
        double prob = dis;
        while (true) {
            double rem = Remains(dis);
            if (rem <= _fuelCount) {
                _fuelCount -= rem;
                _corA = _corB;
                _sumDistance += prob;
                Console.WriteLine("Вы проехали: {0:f} км, топлива осталось: {1:f} л, местоположение: {2},{3}",
                    prob,_fuelCount,_corA[0],_corB[1]);
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
                } else if (speed > 101 && speed <= _speedMax) { 
                    _fuelRate = 12.5; return;
                } else Console.WriteLine("Невозможно ехать с такой скоротью");
            } else Console.WriteLine("Невозможно ехать с такой скоротью");
        }
    }

    protected override void Move() {
        while (true)
        {
            Console.Write("Введите координаты места погрузки: ");
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
    private void Loading() {
        while (true) {
            Console.Write("Введите вес груза для погрузки: ");
            int weight = Convert.ToInt32(Console.ReadLine());
            if (weight > 0)
                if (weight + _weight < _weightMax) {
                    _weight += weight; return;
                }
                else Console.WriteLine("Нельзя погрузить больше");
            else Console.WriteLine("Груз не может иметь отрицательный вес");
        }
    }
    private void Unloading() {
        while (true) {
            Console.Write("Введите вес груза для разгрузки: ");
            int weight = Convert.ToInt32(Console.ReadLine());
            if (weight > 0)
                if (weight <= _weight) {
                    _weight -= weight; return;
                }
                else Console.WriteLine("Нельзя разгрузить больше, чем есть");
            else Console.WriteLine("Груз не может иметь отрицательный вес");
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

    public override void Menu(List<Avto> allAvtos) {
        while (true) {  
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