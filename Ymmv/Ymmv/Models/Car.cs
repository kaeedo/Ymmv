using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace Ymmv.Models
{
    public class Car
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string PictureFilePath { get; set; }

        [Ignore]
        public IList<FuelService> FuelServices { get; } = new List<FuelService>();

        [Ignore]
        public string Name => $"{Make} {Model}";

        [Ignore]
        public string LastRefeul
        {
            get
            {
                var refuel = FuelServices?.OrderBy(fs => fs.ServiceDate).FirstOrDefault();
                return refuel?.ServiceDate.ToString("d") ?? "No refuels added yet";
            }
        }
    }
}
