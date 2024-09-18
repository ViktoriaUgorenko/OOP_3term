using System;

public partial class Airline
{
    private string destination;
    private int flightNumber;
    private string aircraftType;
    private DateTime departureTime;
    private DayOfWeek dayOfWeek;

    private readonly int id;

    private const string airlineName = "MyAirline";
    
    private static int numberOfFlights;

    /*конструкторы*/
    public Airline(string destination, int flightNumber, string aircraftType, DateTime departureTime, DayOfWeek dayOfWeek)
    {
        this.destination = destination;
        this.flightNumber = flightNumber;
        this.aircraftType = aircraftType;
        this.departureTime = departureTime;
        this.dayOfWeek = dayOfWeek;
        this.id = this.GetHashCode();
        numberOfFlights++;
    }

    public Airline(string destination, int flightNumber, string aircraftType, DateTime departureTime)
        : this(destination, flightNumber, aircraftType, departureTime, DayOfWeek.Monday)
    {
    }

   
    static Airline()
    {
        numberOfFlights = 0;
        Console.WriteLine("Статический конструктор класса Airline вызван.\n\n");
    }

    private Airline()
    {
        destination = "Berlin";
        flightNumber = 19;
        aircraftType = "Airline-678";
        departureTime = new DateTime(2024, 12, 24, 8, 30, 0);
        dayOfWeek = DayOfWeek.Monday;
        numberOfFlights++;
    }
    /*вариант его вызова*/
    public static Airline CreateDefaultAirline()
    {
        return new Airline();
    }

    public void UpdateFlightNumber(ref int newFlightNumber)
    {
        flightNumber = newFlightNumber;
    }

    public bool TryGetFlightNumber(out int flightNumber)
    {
        flightNumber = FlightNumber;
        return true;
    }

    public static void PrintClassInfo()
    {
        Console.WriteLine($"Класс Airline. Количество созданных объектов: {numberOfFlights}");
    }

    public void PrintFlightInfo()
    {
        Console.WriteLine($"-----------------------{airlineName}---------------------");
        Console.WriteLine($"Пункт назначения: {Destination}");
        Console.WriteLine($"Номер рейса: {FlightNumber}");
        Console.WriteLine($"Тип самолета: {AircraftType}");
        Console.WriteLine($"Время вылета: {DepartureTime}");
        Console.WriteLine($"День недели: {DayOfWeek}");
        Console.WriteLine($"ID: {ID}");
        Console.WriteLine("-----------------------------------------------------");
    }

    /*переопределения*/
    public override string ToString()
    {
        return $"Рейс {FlightNumber} - {Destination}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Airline other = (Airline)obj;
        return this.FlightNumber == other.FlightNumber;
    }

    public override int GetHashCode()
    {
        return FlightNumber.GetHashCode();
    }
}