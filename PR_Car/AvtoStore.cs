namespace PR_Car;
public class AvtoStore {
    private List<Avto> _acc = new List<Avto>();
    public List<Avto> Acc { get { return _acc; } }
    //Добавление счета
    void AddAcc() {
        Console.Write
            ("Какое авто хотите создать:\n" +
             "1. Базовое\n" +
             "2. Грузовое\n" +
             "3. Автобус\n" +
             ">");
        switch (Convert.ToInt32(Console.ReadLine())) {
            case 1: _acc.Add(new Avto()); break;
            case 2:  _acc.Add(new Truck()); break;
            case 3:  _acc.Add(new Bus()); break;
            default: return;
        }
        Console.WriteLine("Авто добавлено");
    }
    //Удаление счета
    void DelAcc() {
        Console.Write("Какое авто хотите удалить: ");
        int index = Convert.ToInt32(Console.ReadLine());
        if (index >= 0 && index < _acc.Count)
            _acc.RemoveAt(index);
    }
    //Выбор счета
    Avto GetAcc() {
        Console.Write("Введите индекс авто: ");
        int index = Convert.ToInt32(Console.ReadLine());
        if (index >= 0 && index < _acc.Count)
            return _acc[index];
        return null;
    }
    //Вывод информации
    void Info() { 
        for(int i = 0; i < _acc.Count; i++) 
            Console.WriteLine($"Индекс авто: {i}, Номер: {_acc[i].Number}");
    } 
    //Интефейс управления счетами
    public Avto AccMenu() {
        while (true) {
            try {
                Console.Write
                ("------------------------------\n" +
                 "Выберете необходимое действие:\n" +
                 "0. Просмореть доступные авто\n" +
                 "1. Добавить авто\n" +
                 "2. Удалить авто\n" +
                 "3. Выбрать авто\n" +
                 "4. Выход\n" +
                 ">");
                switch (Convert.ToInt32(Console.ReadLine())) {
                    case 0: Info(); break;
                    case 1: AddAcc(); break;
                    case 2: DelAcc(); break;
                    case 3: return GetAcc();
                    case 4: return null;
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}