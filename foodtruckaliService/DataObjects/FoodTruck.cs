using Microsoft.Azure.Mobile.Server;

namespace foodtruckaliService.Controllers
{
    public class FoodTruck:EntityData
    {
        public string User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public Point Coordinates { get; set; }
    }
}