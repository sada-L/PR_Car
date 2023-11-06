using PR_Car;
class Program
{
    public static void Main(string[] args)
    {
        Avto avto1 = new Avto("av1", 0, 50, 25);
        Avto avto2 = new Avto("av2", 0, 100, 50);
        List<Avto> allAvtos = new List<Avto>() { avto1, avto2 };
        
        avto1.Menu(allAvtos);
        avto2.Menu(allAvtos);
    }
}