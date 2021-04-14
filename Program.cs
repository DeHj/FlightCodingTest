using System;
using System.Collections.Generic;
using System.Text;


namespace Gridnine.FlightCodingTest
{
    static class FlightExtension
    {
        public static string ToStringExt(this Flight flight)
        {
            var result = new StringBuilder();

            for (int i = 0; i < flight.Segments.Count; i++)
            {
                result.Append($"{flight.Segments[i].DepartureDate} - {flight.Segments[i].ArrivalDate}; ");
            }

            return result.ToString();
        }
    }

    class Program
    {
        static void OnScreen(IList<Flight> flights)
        {
            foreach (var flight in flights)
            {
                Console.WriteLine(flight.ToStringExt());
            }
        }

        public static void Main()
        {
            var flights = new FlightBuilder().GetFlights();

            var filter1 = new FilterDeparture(DateTime.Now, DateTime.MaxValue);
            var filter2 = new FilterCorrect();
            var filter3 = new FilterOnEarth(new TimeSpan(2, 0, 0));

            var flights1 = Filtrator.ToFilter(flights, new List<IFilter> { filter1 });
            var flights2 = Filtrator.ToFilter(flights, new List<IFilter> { filter2 });
            var flights3 = Filtrator.ToFilter(flights, new List<IFilter> { filter3 });
            var flights123 = Filtrator.ToFilter(flights, new List<IFilter> { filter1, filter2, filter3 });



            Console.WriteLine("All flights:");
            OnScreen(flights);
            Console.WriteLine();

            Console.WriteLine("All flights difference flights departing in the past:");
            OnScreen(flights1);
            Console.WriteLine();

            Console.WriteLine("All flights difference flights that departs before it arrives:");
            OnScreen(flights2);
            Console.WriteLine();

            Console.WriteLine("Flights without more than two hours ground time:");
            OnScreen(flights3);
            Console.WriteLine();

            Console.WriteLine("Flights that passed all three filters:");
            OnScreen(flights123);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
