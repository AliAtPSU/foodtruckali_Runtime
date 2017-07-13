using Microsoft.Azure.Mobile.Server;

namespace foodtruckaliService.DataObjects
{
    public class FoodTruck:EntityData
    {
        public string User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}