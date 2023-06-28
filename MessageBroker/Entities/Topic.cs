using System.ComponentModel.DataAnnotations;

namespace MessageBroker.Entities
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
