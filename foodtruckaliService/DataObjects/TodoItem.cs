using Microsoft.Azure.Mobile.Server;

namespace foodtruckaliService.DataObjects
{
    public class TodoItem : EntityData
    {
        
        public string Text { get; set; }
        public bool IsAvailable { get; set; }
        public bool Complete { get; set; }
    }
}