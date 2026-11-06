class Vehicle
{
    private static int nextId = 101;

    private int year;
    private double speed;

    public int Id { get; private set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public bool IsEngineRunning { get; private set; }

    public int Year
    {
        get { return year; }
        set
        {
            int currentYear = DateTime.Now.Year;

            if (value < 1886 || value > currentYear)
            {
                throw new ArgumentException("Год выпуска должен быть от 1886 до " + currentYear);
            }

            year = value;
        }
    }

    public double Speed
    {
        get { return speed; }
        set
        {
            if (value < 0 || value > 500)
            {
                throw new ArgumentException("Скорость должна быть от 0 до 500 км/ч");
            }

            speed = value;
        }
    }

    public Vehicle(string brand, string model, int year, double speed)
    {
        Id = nextId;
        nextId++;

        Brand = brand;
        Model = model;
        Year = year;
        Speed = speed;
        IsEngineRunning = false;
    }

    public void StartEngine()
    {
        if (IsEngineRunning)
        {
            Console.WriteLine(Brand + " " + Model + ": двигатель уже запущен");
        }
        else
        {
            IsEngineRunning = true;
            Console.WriteLine(Brand + " " + Model + ": двигатель запущен");
        }
    }

    public void StopEngine()
    {
        if (!IsEngineRunning)
        {
            Console.WriteLine(Brand + " " + Model + ": двигатель уже остановлен");
        }
        else
        {
            IsEngineRunning = false;
            speed = 0;
            Console.WriteLine(Brand + " " + Model + ": двигатель остановлен");
        }
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine("ID: " + Id);
        Console.WriteLine("Марка: " + Brand);
        Console.WriteLine("Модель: " + Model);
        Console.WriteLine("Год выпуска: " + Year);
        Console.WriteLine("Скорость: " + Speed + " км/ч");

        if (IsEngineRunning)
        {
            Console.WriteLine("Двигатель: запущен");
        }
        else
        {
            Console.WriteLine("Двигатель: остановлен");
        }
    }

    public virtual double CalculateFuelConsumption(double distance)
    {
        if (distance < 0)
        {
            throw new ArgumentException("Расстояние не может быть отрицательным");
        }

        double consumptionPer100Km = 8;

        return distance * consumptionPer100Km / 100;
    }
}
