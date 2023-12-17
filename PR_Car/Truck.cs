namespace PR_Car;

public class Truck : Avto {
    private int _weight = 0;
    private int _weightMax = 2000;
    private void MoveToLoad() {
        double dis = DistanceToLoad();
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
                    Console.WriteLine($"Топливо кончилось, вы проехали: {prob - dis}км");
                    Refill();
                }
                else { 
                    Console.WriteLine("Вы заглохли"); 
                    return; 
                }
            }
        }
    }

    private void MoveToUnload() {
        
    }

    protected override void Move() {
        base.Move();
    }

    private void Loading() {
        while (true) {
            Console.Write("Введите вес груза для погрузки: ");
            int weight = Convert.ToInt32(Console.ReadLine());
            if (weight > 0)
                if (weight + _weight < _weightMax) {
                    _weightMax += weight; return;
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
                    _weightMax -= weight; return;
                }
                else Console.WriteLine("Нельзя разгрузить больше");
            else Console.WriteLine("Груз не может иметь отрицательный вес");
        }
    }
    private double DistanceToLoad() {
        while (true) {
            try {
                _corB = new int[2];
                Console.Write("Введите координаты точки погрузки: ");
                string[] s = Console.ReadLine().Split(',',' ',';');
                for(int i = 0; i < _corB.Length; i++) 
                    _corB[i] = Int32.Parse(s[i]);
                double c = Math.Sqrt(Math.Pow(_corB[0] - _corA[0], 2) + Math.Pow(_corB[1] - _corA[1], 2));
                return Math.Round(c, 2);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}