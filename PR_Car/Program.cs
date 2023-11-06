using PR_Car;

class Program
{
    public static void Main(string[] args)
    {
        Avto a = new Avto("av1", 0, 50, 25);
        Avto b = new Avto("av2", 0, 50, 100);
        List<Avto> allAvtos = new List<Avto>();
        allAvtos.Add(a);
        allAvtos.Add(b);
        
        a.Menu(allAvtos);
        b.Menu(allAvtos);
    }
}