using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace Ymmv.Models
{
    public class Car
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string PictureFilePath { get; set; }
        public FuelUnit PreferredFuelUnit { get; set; }
        public DistanceUnit PreferredDistanceUnit { get; set; }

        [Ignore]
        public IList<FuelService> FuelServices { get; set; } = new List<FuelService>();

        [Ignore]
        public string Name => $"{Make} {Model}";

        [Ignore]
        public DateTime? LastRefeul
        {
            get
            {
                var refuel = FuelServices?.OrderByDescending(fs => fs.ServiceDate).FirstOrDefault();
                return refuel?.ServiceDate;
            }
        }
    }
}
