using System;

class Program
{
    static void Main(string[] args)
    {

        Airline[] flights = new Airline[]
        {
            new Airline("Minsk", 10, "Airline-112", new DateTime(2024, 10, 18, 11, 45, 0), DayOfWeek.Monday),
            new Airline("Astana", 11, "Airline-1876", new DateTime(2024, 12, 22, 3, 10, 0), DayOfWeek.Tuesday),
            new Airline("Moscow", 12, "Airline-965", new DateTime(2024, 10, 17, 14, 0, 0), DayOfWeek.Wednesday),
            new Airline("Warsaw", 13, "Airline-002", new DateTime(2024, 11, 29, 19, 50, 0), DayOfWeek.Wednesday),
            new Airline("London", 14, "Airline-666", new DateTime(2024, 11, 5, 18, 25, 0), DayOfWeek.Friday),
        };

        foreach (var flight in flights)
        {
            flight.PrintFlightInfo();
            Console.WriteLine();
        }


        Console.WriteLine("Исходный номер рейса: " + flights[0].FlightNumber);
        int newFlightNumber;
        do
        {
            Console.Write("Введите новый номер рейса: ");
        } while (!int.TryParse(Console.ReadLine(), out newFlightNumber));
        flights[0].UpdateFlightNumber(ref newFlightNumber);


        int retrievedFlightNumber;
        if (flights[0].TryGetFlightNumber(out retrievedFlightNumber))
        {
            Console.WriteLine("Новый номер рейса: " + retrievedFlightNumber);
        }
        else
        {
            Console.WriteLine("Не удалось получить номер рейса.");
        }
        flights[0].PrintFlightInfo();


        string destinationToSearch = "London";
        Console.WriteLine($"Список рейсов для пункта назначения '{destinationToSearch}':");
        foreach (var flight in flights)
        {
            if (flight.Destination == destinationToSearch)
            {
                flight.PrintFlightInfo();
                Console.WriteLine();
            }
        }


        DayOfWeek dayOfWeekToSearch = DayOfWeek.Wednesday;
        Console.WriteLine($"Список рейсов для дня недели '{dayOfWeekToSearch}':");
        foreach (var flight in flights)
        {
            if (flight.DayOfWeek == dayOfWeekToSearch)
            {
                flight.PrintFlightInfo();
                Console.WriteLine();
            }
        }


        var flightInfo = new
        {
            Destination = "Praga",
            FlightNumber = 099,
            AircraftType = "Airline-444",
            DepartureTime = new DateTime(2024, 11, 23, 14, 50, 0),
            DayOfWeek = DayOfWeek.Monday
        };

        Console.WriteLine("Информация о рейсе анонимного типа:");
        Console.WriteLine($"Пункт назначения: {flightInfo.Destination}");
        Console.WriteLine($"Номер рейса: {flightInfo.FlightNumber}");
        Console.WriteLine($"Тип самолета: {flightInfo.AircraftType}");
        Console.WriteLine($"Время вылета: {flightInfo.DepartureTime}");
        Console.WriteLine($"День недели: {flightInfo.DayOfWeek}");
        Console.WriteLine("-----------------------------------------------------");

        Airline defaultAirline = Airline.CreateDefaultAirline();
        Console.WriteLine("Информация о рейсе по умолчанию:");
        Console.WriteLine($"Пункт назначения: {defaultAirline.Destination}");
        Console.WriteLine($"Номер рейса: {defaultAirline.FlightNumber}");
        Console.WriteLine($"Тип самолета: {defaultAirline.AircraftType}");
        Console.WriteLine($"Время вылета: {defaultAirline.DepartureTime}");
        Console.WriteLine($"День недели: {defaultAirline.DayOfWeek}");
        Console.WriteLine("-----------------------------------------------------");

        Console.WriteLine("Equals номера рейса третьего рейса с первым рейсом: " + flights[2].Equals(flights[0]));
        Console.WriteLine("ToString: " + flights[2].ToString());

        Airline.PrintClassInfo();

    }
}