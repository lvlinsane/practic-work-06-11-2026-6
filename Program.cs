class Program
{
    static VehicleManager manager = new VehicleManager();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        manager.FillTestData();

        bool running = true;

        while (running)
        {
            ShowMenu();
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddVehicle();
                    break;
                case "2":
                    manager.ShowAll();
                    break;
                case "3":
                    FindVehicle();
                    break;
                case "4":
                    StartEngine();
                    break;
                case "5":
                    StopEngine();
                    break;
                case "6":
                    CalculateFuel();
                    break;
                case "7":
                    PerformOperation();
                    break;
                case "8":
                    manager.ShowStatistics();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Выход из программы");
                    break;
                default:
                    Console.WriteLine("Неверный пункт меню, попробуйте ещё раз");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("Система управления транспортом");
        Console.WriteLine("1. Добавить транспортное средство");
        Console.WriteLine("2. Просмотреть список транспорта");
        Console.WriteLine("3. Найти транспорт по ID");
        Console.WriteLine("4. Запустить двигатель");
        Console.WriteLine("5. Остановить двигатель");
        Console.WriteLine("6. Рассчитать расход топлива");
        Console.WriteLine("7. Выполнить операцию с транспортом");
        Console.WriteLine("8. Просмотреть статистику");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите действие: ");
    }

    static Vehicle GetVehicleById()
    {
        int id = ReadInt("Введите ID транспорта: ", 1, int.MaxValue);
        Vehicle vehicle = manager.FindById(id);

        if (vehicle == null)
        {
            Console.WriteLine("Транспорт с ID " + id + " не найден");
        }

        return vehicle;
    }

    static void AddVehicle()
    {
        Console.WriteLine("Что добавить?");
        Console.WriteLine("1. Car");
        Console.WriteLine("2. Truck");
        Console.WriteLine("3. Bus");
        int type = ReadInt("Выберите тип: ", 1, 3);

        string brand = ReadString("Введите марку: ");
        string model = ReadString("Введите модель: ");
        int year = ReadInt("Введите год выпуска: ", 1886, DateTime.Now.Year);

        try
        {
            Vehicle vehicle;

            if (type == 1)
            {
                int passengers = ReadInt("Введите количество пассажиров (1-9): ", 1, 9);
                double fuel = ReadPositiveDouble("Введите расход топлива на 100 км: ");
                vehicle = new Car(brand, model, year, 0, passengers, fuel);
            }
            else if (type == 2)
            {
                double capacity = ReadPositiveDouble("Введите грузоподъёмность (кг): ");
                vehicle = new Truck(brand, model, year, 0, capacity);
            }
            else
            {
                int seats = ReadInt("Введите количество мест: ", 1, 200);
                vehicle = new Bus(brand, model, year, 0, seats);
            }

            manager.Add(vehicle);
            Console.WriteLine("Транспорт добавлен, его ID: " + vehicle.Id);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }

    static void FindVehicle()
    {
        Vehicle vehicle = GetVehicleById();

        if (vehicle != null)
        {
            vehicle.DisplayInfo();
        }
    }

    static void StartEngine()
    {
        Vehicle vehicle = GetVehicleById();

        if (vehicle != null)
        {
            vehicle.StartEngine();
        }
    }

    static void StopEngine()
    {
        Vehicle vehicle = GetVehicleById();

        if (vehicle != null)
        {
            vehicle.StopEngine();
        }
    }

    static void CalculateFuel()
    {
        Vehicle vehicle = GetVehicleById();

        if (vehicle == null)
        {
            return;
        }

        double distance = ReadDouble("Введите расстояние (км): ");

        Console.WriteLine("Тип транспорта: " + vehicle.GetType().Name);
        Console.WriteLine("Марка: " + vehicle.Brand);
        Console.WriteLine("Модель: " + vehicle.Model);
        Console.WriteLine("Расход на 100 км: " + vehicle.CalculateFuelConsumption(100) + " л");
        Console.WriteLine("Общий расход топлива: " + vehicle.CalculateFuelConsumption(distance) + " л");
    }

    static void PerformOperation()
    {
        Vehicle vehicle = GetVehicleById();

        if (vehicle == null)
        {
            return;
        }

        if (vehicle is Car car)
        {
            if (car.OpenTrunk())
            {
                Console.WriteLine("Багажник открыт");
            }
            else
            {
                Console.WriteLine("Отказ: нельзя открыть багажник на скорости " + car.Speed + " км/ч");
            }
        }
        else if (vehicle is Truck truck)
        {
            double weight = ReadDouble("Введите вес груза (кг): ");

            if (truck.LoadCargo(weight))
            {
                Console.WriteLine("Загружено " + weight + " кг, груз " + truck.CurrentLoad + " из " + truck.LoadCapacity + " кг");
            }
            else
            {
                double free = truck.LoadCapacity - truck.CurrentLoad;
                Console.WriteLine("Отказ: груз не помещается или вес указан неверно (свободно " + free + " кг)");
            }
        }
        else if (vehicle is Bus bus)
        {
            int count = ReadInt("Введите количество пассажиров: ", 0, 1000);

            if (bus.BoardPassengers(count))
            {
                Console.WriteLine("Село " + count + " пассажиров, в автобусе " + bus.PassengersCount + " из " + bus.SeatsCount);
            }
            else
            {
                int freeSeats = bus.SeatsCount - bus.PassengersCount;
                Console.WriteLine("Отказ: не хватает мест или количество указано неверно (свободно " + freeSeats + ")");
            }
        }
        else
        {
            Console.WriteLine("Для этого типа транспорта операций нет");
        }
    }

    static int ReadInt(string text, int min, int max)
    {
        while (true)
        {
            Console.Write(text);
            string input = Console.ReadLine();
            int number;

            if (int.TryParse(input, out number) && number >= min && number <= max)
            {
                return number;
            }

            Console.WriteLine("Ошибка: введите целое число от " + min + " до " + max);
        }
    }

    static double ReadDouble(string text)
    {
        while (true)
        {
            Console.Write(text);
            string input = Console.ReadLine();
            double number;

            if (double.TryParse(input, out number) && number >= 0)
            {
                return number;
            }

            Console.WriteLine("Ошибка: введите неотрицательное число");
        }
    }

    static double ReadPositiveDouble(string text)
    {
        while (true)
        {
            Console.Write(text);
            string input = Console.ReadLine();
            double number;

            if (double.TryParse(input, out number) && number > 0)
            {
                return number;
            }

            Console.WriteLine("Ошибка: введите число больше 0");
        }
    }

    static string ReadString(string text)
    {
        while (true)
        {
            Console.Write(text);
            string input = Console.ReadLine();

            if (input != null && input.Trim() != "")
            {
                return input.Trim();
            }

            Console.WriteLine("Ошибка: строка не должна быть пустой");
        }
    }
}
