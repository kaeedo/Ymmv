using System.Collections.Generic;
using System.Linq;

namespace Ymmv.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string PictureFilePath { get; set; }
        public IList<FuelService> FuelServices { get; } = new List<FuelService>();

        public string Name => $"{Make} {Model}";

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
