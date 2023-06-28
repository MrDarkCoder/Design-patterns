using System.ComponentModel.DataAnnotations;

namespace MessageBroker.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ExpriesAfter { get; set; } = DateTime.UtcNow.AddDays(1);

        [Required]
        public string? TopicMessage { get; set; }

        [Required]
        public string MessageStatus { get; set; } = "NEW";

        public int SubscriptionId { get; set; }
    }
}
