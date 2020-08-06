using System;
using SQLite;

namespace Ymmv.Models
{
    public class FuelService
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int CarId { get; set; }
        public DateTime ServiceDate { get; set; }
        public double Liters { get; set; }
        public double Kilometers { get; set; }
        public int LifeTimeKilometers { get; set; }

        [Ignore]
        public double LitersPer100Kilometers => (Liters / Kilometers) * 100;

        [Ignore]
        public double Miles => Kilometers * 0.6213712;

        [Ignore]
        public double Gallons => Liters * 0.2641728166;

        [Ignore]
        public double MilesPerGallon => Miles / Gallons;
    }
}

