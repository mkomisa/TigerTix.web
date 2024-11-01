namespace TigerTix.web.Data.Entities
{
    public class Event
    {
        public int EventId {get; set;}
        public string Title { get; set; }
        public string Date {get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
    }
}
