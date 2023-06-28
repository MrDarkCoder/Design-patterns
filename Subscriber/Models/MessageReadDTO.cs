using System.ComponentModel.DataAnnotations;

namespace Subscriber.Models
{
    public class MessageReadDTO
    {
        public int Id { get; set; }

        public DateTime ExpriesAfter { get; set; }

        public string? TopicMessage { get; set; }

        public string? MessageStatus { get; set; }
    }
}
