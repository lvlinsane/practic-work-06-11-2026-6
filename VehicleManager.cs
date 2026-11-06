class VehicleManager
{
    private List<Vehicle> vehicles = new List<Vehicle>();

    public void Add(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
    }

    public void FillTestData()
    {
        vehicles.Add(new Car("Toyota", "Camry", 2020, 0, 5, 8));
        vehicles.Add(new Car("Lada", "Vesta", 2018, 0, 5, 8));
        vehicles.Add(new Truck("KAMAZ", "5490", 2019, 0, 20000));
        vehicles.Add(new Bus("MAN", "Lion's City", 2021, 0, 40));
    }

    public Vehicle FindById(int id)
    {
        foreach (Vehicle v in vehicles)
        {
            if (v.Id == id)
            {
                return v;
            }
        }
        return null;
    }

    public void ShowAll()
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("Список транспорта пуст");
            return;
        }

        foreach (Vehicle v in vehicles)
        {
            v.DisplayInfo();
            Console.WriteLine("----------------------------");
        }
    }

    public void ShowStatistics()
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("Список транспорта пуст");
            return;
        }

        int cars = 0;
        int trucks = 0;
        int buses = 0;
        int running = 0;

        Vehicle oldest = vehicles[0];
        Vehicle newest = vehicles[0];

        foreach (Vehicle v in vehicles)
        {
            if (v is Car)
            {
                cars++;
            }
            else if (v is Truck)
            {
                trucks++;
            }
            else if (v is Bus)
            {
                buses++;
            }

            if (v.IsEngineRunning)
            {
                running++;
            }

            if (v.Year < oldest.Year)
            {
                oldest = v;
            }

            if (v.Year > newest.Year)
            {
                newest = v;
            }
        }

        Console.WriteLine("Всего транспортных средств: " + vehicles.Count);
        Console.WriteLine("Легковых автомобилей: " + cars);
        Console.WriteLine("Грузовых автомобилей: " + trucks);
        Console.WriteLine("Автобусов: " + buses);
        Console.WriteLine("С запущенным двигателем: " + running);
        Console.WriteLine("Самое старое: " + oldest.Brand + " " + oldest.Model + ", " + oldest.Year + " год");
        Console.WriteLine("Самое новое: " + newest.Brand + " " + newest.Model + ", " + newest.Year + " год");
    }
}
