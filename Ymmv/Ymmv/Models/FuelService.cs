using System;

namespace Ymmv.Models
{
    public class FuelService
    {
        public int Id { get; set; }
        public DateTimeOffset ServiceDate { get; set; }
        public double Liters { get; set; }
        public double Kilometers { get; set; }
        public double LifetimeKilometers { get; set; }
        public double LitersPer100Kilometers
        {
            get
            {
                return (Liters / Kilometers) * 100;
            }
        }
    }
}

