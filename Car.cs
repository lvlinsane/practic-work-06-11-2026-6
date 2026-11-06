class Car : Vehicle
{
    private int passengerCapacity;
    private double fuelConsumption;

    public int PassengerCapacity
    {
        get { return passengerCapacity; }
        set
        {
            if (value < 1 || value > 9)
            {
                throw new ArgumentException("Количество пассажиров в легковом автомобиле должно быть от 1 до 9");
            }

            passengerCapacity = value;
        }
    }

    public double FuelConsumption
    {
        get { return fuelConsumption; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Расход топлива должен быть больше 0");
            }

            fuelConsumption = value;
        }
    }

    public Car(string brand, string model, int year, double speed,
               int passengerCapacity, double fuelConsumption)
        : base(brand, model, year, speed)
    {
        PassengerCapacity = passengerCapacity;
        FuelConsumption = fuelConsumption;
    }

    public bool OpenTrunk()
    {
        if (Speed > 0)
        {
            return false;
        }

        return true;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Тип: легковой автомобиль");
        Console.WriteLine("Количество пассажиров: " + PassengerCapacity);
        Console.WriteLine("Расход топлива на 100 км: " + FuelConsumption + " л");
    }

    public override double CalculateFuelConsumption(double distance)
    {
        if (distance < 0)
        {
            throw new ArgumentException("Расстояние не может быть отрицательным");
        }

        return distance * 8 / 100;
    }
}
