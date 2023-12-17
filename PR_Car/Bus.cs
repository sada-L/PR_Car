namespace PR_Car;
public class Bus : Avto
{
    private int _passCount = 0;
    private int _passMax = 30;
    public Bus()
    {
          
    }
    protected override void Move()
    {
        Console.Write("Введите количество остановок\n" + ">");
        int trail = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < trail; i++)
        {
            Console.WriteLine("Введите расстояние между остановками:\n" + ">");
            double dis = Convert.ToDouble(Console.ReadLine()); 
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
                    break;
                }
                else
                {
                    Console.Write("Вам не хватило топлива, хотите заправиться: +/-\n" + ">");
                    string ans = Console.ReadLine();
                    if (ans == "+")
                    {
                        dis -= _fuelCount / (_fuelRate / 100);
                        _fuelCount = 0;
                        Console.WriteLine($"Топливо кончилось, вы проехали: {prob - dis}км");
                        Refill();
                    }
                    else { Console.WriteLine("Вы заглохли"); return; }
                }
            }
        }
    }
}