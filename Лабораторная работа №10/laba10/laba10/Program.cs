using System;
using System.Collections.Generic;
using System.Linq;

namespace laba10
{
    public partial class Airline
    {
        private string destination;
        private int flightNumber;
        private string aircraftType;
        private DateTime departureTime;
        private DayOfWeek dayOfWeek;
        private int id;
        private static int idCounter = 0;

        public string Destination
        {
            get => destination;
            set => destination = !string.IsNullOrEmpty(value) ? value : throw new ArgumentException("Destination cannot be empty");
        }

        public int FlightNumber
        {
            get => flightNumber;
            set => flightNumber = value > 0 ? value : throw new ArgumentException("Flight number must be positive");
        }

        public string AircraftType
        {
            get => aircraftType;
            set => aircraftType = !string.IsNullOrEmpty(value) ? value : throw new ArgumentException("Aircraft type cannot be empty");
        }

        public DateTime DepartureTime
        {
            get => departureTime;
            set => departureTime = value > DateTime.MinValue ? value : throw new ArgumentException("Invalid departure time");
        }

        public DayOfWeek DayOfWeek
        {
            get => dayOfWeek;
            set => dayOfWeek = value;
        }

        public int ID => id;

        public Airline(string destination, int flightNumber, string aircraftType, DateTime departureTime, DayOfWeek dayOfWeek)
        {
            Destination = destination;
            FlightNumber = flightNumber;
            AircraftType = aircraftType;
            DepartureTime = departureTime;
            DayOfWeek = dayOfWeek;
            id = ++idCounter;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Destination: {Destination}, Flight: {FlightNumber}, Aircraft: {AircraftType}, Departure: {DepartureTime:HH:mm}, Day: {DayOfWeek}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] months = {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };

            Console.Write("Введите длину имени месяца (n): ");
            if (int.TryParse(Console.ReadLine(), out int n))
            {
                var monthsWithLengthN = months.Where(m => m.Length == n);
                Console.WriteLine($"Месяцы с длиной строки {n}: {string.Join(", ", monthsWithLengthN)}");
            }
            else
            {
                Console.WriteLine("Некорректный ввод числа.");
            }

            var summerAndWinterMonths = months.Where(m =>
                m == "June" || m == "July" || m == "August" ||
                m == "December" || m == "January" || m == "February");
            Console.WriteLine($"Летние и зимние месяцы: {string.Join(", ", summerAndWinterMonths)}");

            var monthsInAlphabeticalOrder = months.OrderBy(m => m);
            Console.WriteLine($"Месяцы в алфавитном порядке: {string.Join(", ", monthsInAlphabeticalOrder)}");

            var monthsWithUAndLength4OrMore = months.Where(m => m.Contains('u') && m.Length >= 4);
            Console.WriteLine($"Месяцы, содержащие букву 'u' и длиной имени не менее 4-х: {string.Join(", ", monthsWithUAndLength4OrMore)}");

            List<Airline> airlines = new List<Airline>
            {
                new Airline("Braslav", 100, "air-1", new DateTime(2024, 11, 23, 10, 30, 0), DayOfWeek.Monday),
                new Airline("Minsk", 101, "air-2", new DateTime(2024, 11, 24, 23, 45, 0), DayOfWeek.Tuesday),
                new Airline("Vitebsk", 102, "air-3", new DateTime(2024, 11, 25, 2, 0, 0), DayOfWeek.Wednesday),
                new Airline("Grodno", 103, "air-4", new DateTime(2024, 11, 26, 15, 20, 0), DayOfWeek.Thursday),
                new Airline("Brest", 104, "air-5", new DateTime(2024, 11, 27, 18, 50, 0), DayOfWeek.Friday),
                new Airline("Mogilev", 105, "air-6", new DateTime(2024, 11, 28, 6, 15, 0), DayOfWeek.Saturday),
                new Airline("Pinsk", 106, "air-7", new DateTime(2024, 11, 29, 13, 13, 0), DayOfWeek.Sunday),
                new Airline("Polotsk", 107, "air-8", new DateTime(2024, 11, 30, 17, 0, 0), DayOfWeek.Monday),
                new Airline("Borisov", 108, "air-9", new DateTime(2024, 12, 1, 19, 30, 0), DayOfWeek.Tuesday),
                new Airline("Orsha", 109, "air-10", new DateTime(2024, 12, 2, 4, 4, 0), DayOfWeek.Wednesday)
            };

            var earliestTime = airlines.Min(a => a.DepartureTime);
            Console.WriteLine($"\nСамое раннее время вылета: {earliestTime:HH:mm}");

            var firstMatchingTime = airlines.FirstOrDefault(a => a.DepartureTime.Hour == a.DepartureTime.Minute);
            if (firstMatchingTime != null)
            {
                Console.WriteLine($"\nПервый рейс с совпадением часов и минут: {firstMatchingTime}");
            }
            else
            {
                Console.WriteLine("\nНет рейсов, где часы и минуты совпадают.");
            }

            var groupedByTimeOfDay = airlines.GroupBy(a =>
            {
                var hour = a.DepartureTime.Hour;
                if (hour >= 0 && hour < 6) return "Ночь";
                if (hour >= 6 && hour < 12) return "Утро";
                if (hour >= 12 && hour < 18) return "День";
                return "Вечер";
            });

            Console.WriteLine("\nРейсы по времени суток:");
            foreach (var group in groupedByTimeOfDay)
            {
                Console.WriteLine($"{group.Key}:");
                foreach (var flight in group)
                {
                    Console.WriteLine($"  {flight}");
                }
            }

            var orderedByTime = airlines.OrderBy(a => a.DepartureTime);
            Console.WriteLine("\nРейсы, упорядоченные по времени:");
            foreach (var flight in orderedByTime)
            {
                Console.WriteLine(flight);
            }

            var aircraftTypes = new[]
            {
                new { Aircraft = "air-1", Capacity = 200 },
                new { Aircraft = "air-2", Capacity = 150 },
                new { Aircraft = "air-3", Capacity = 100 }
            };

            var joinedQuery = airlines.Join(
                aircraftTypes,
                a => a.AircraftType,
                t => t.Aircraft,
                (a, t) => new { a.Destination, a.DepartureTime, t.Capacity });

            Console.WriteLine("\nКастомный запрос с Join:");
            foreach (var item in joinedQuery)
            {
                Console.WriteLine($"Destination: {item.Destination}, Departure: {item.DepartureTime:HH:mm}, Capacity: {item.Capacity}");
            }

            var customQuery = airlines
                .Where(a => a.DepartureTime.Hour >= 6 && a.DepartureTime.Hour < 18) 
                .OrderBy(a => a.DepartureTime) 
                .GroupBy(a => a.DayOfWeek) 
                .Select(g => new 
                {
                    Day = g.Key,
                    Count = g.Count(), 
                    EarliestFlight = g.Min(a => a.DepartureTime),
                    Flights = g
                })
                .Where(group => group.Count > 1) 
                .Skip(1) 
                .Take(3); 

            Console.WriteLine("\nКастомный запрос с 5+ операторами:");
            foreach (var group in customQuery)
            {
                Console.WriteLine($"День недели: {group.Day}");
                Console.WriteLine($"  Количество рейсов: {group.Count}");
                Console.WriteLine($"  Самый ранний рейс: {group.EarliestFlight:HH:mm}");
                Console.WriteLine($"  Рейсы:");
                foreach (var flight in group.Flights)
                {
                    Console.WriteLine($"    {flight}");
                }
            }
        }
    }
}
