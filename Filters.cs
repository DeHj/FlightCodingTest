using System;
using System.Linq;

namespace Gridnine.FlightCodingTest
{
    /// <summary>
    /// Departure date filter
    /// </summary>
    class FilterDeparture : IFilter
    {
        private readonly DateTime from, to;

        /// <param name="from">Left border for departure date</param>
        /// <param name="to">Right border for departure date</param>
        public FilterDeparture(DateTime from, DateTime to)
        {
            this.from = from;
            this.to = to;
        }

        public bool Apply(Flight flight) => (flight.Segments.First().DepartureDate >= from) && (flight.Segments.First().DepartureDate <= to);
    }

    /// <summary>
    /// Arrival date filter
    /// </summary>
    class FilterArrival : IFilter
    {
        private readonly DateTime from, to;

        /// <param name="from">Left border for arrival date</param>
        /// <param name="to">Right border for arrival date</param>
        public FilterArrival(DateTime from, DateTime to)
        {
            this.from = from;
            this.to = to;
        }

        public bool Apply(Flight flight) => (flight.Segments.First().ArrivalDate >= from) && (flight.Segments.First().ArrivalDate <= to);
    }

    /// <summary>
    /// Validation filter
    /// </summary>
    class FilterCorrect : IFilter
    {
        public bool Apply(Flight flight)
        {
            for (int i = 0; i < flight.Segments.Count - 1; i++)
            {
                if (flight.Segments[i].ArrivalDate < flight.Segments[i].DepartureDate)
                    return false;
                if (flight.Segments[i+1].DepartureDate < flight.Segments[i].ArrivalDate)
                    return false;
            }

            if (flight.Segments.Last().ArrivalDate < flight.Segments.Last().DepartureDate)
                return false;

            return true;
        }
    }

    /// <summary>
    /// On earth time filter
    /// </summary>
    class FilterOnEarth : IFilter
    {
        private readonly TimeSpan onEarth;

        /// <param name="onEarth">Maximum time staying on earth</param>
        public FilterOnEarth(TimeSpan onEarth)
        {
            this.onEarth = onEarth;
        }

        public bool Apply(Flight flight)
        {
            var sum = new TimeSpan(0);
            for (int i = 0; i < flight.Segments.Count - 1; i++)
            {
                sum += flight.Segments[i + 1].DepartureDate - flight.Segments[i].ArrivalDate;
            }

            return sum <= onEarth;
        }
    }
}
