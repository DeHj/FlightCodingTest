using System.Collections.Generic;
using System.Linq;

namespace Gridnine.FlightCodingTest
{
    interface IFilter
    {
        /// <summary>
        /// Must return true if flight passed the filter
        /// </summary>
        bool Apply(Flight flight);
    }

    static class Filtrator
    {
        public static IList<Flight> ToFilter(IList<Flight> flightSet, IList<IFilter> filters)
        {
            // A little slower:
            //var result = flightSet.ToList();
            //foreach (var filter in filters)
            //{
            //    result = result.Where(filter.Apply).ToList();
            //}

            var result = new List<Flight>(flightSet.Count);

            for (int i = 0; i < flightSet.Count; i++)
            {
                bool isOk = true;
                foreach (var filter in filters)
                {
                    if (filter.Apply(flightSet[i]) == false)
                    {
                        isOk = false;
                        break;
                    }
                }
                if (isOk)
                    result.Add(flightSet[i]);
            }

            return result;
        }
    }
}
