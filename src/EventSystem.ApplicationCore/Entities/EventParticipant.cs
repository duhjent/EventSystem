namespace EventSystem.ApplicationCore.Entities
{
    public class EventParticipant
    {
        public int EventId { get; set; }
        public string UserId { get; set; }
        public Event Event { get; set; }
        public User User { get; set; }
    }
}