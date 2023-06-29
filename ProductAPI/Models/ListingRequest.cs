using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class ListingRequest
    {
        [Key]
        public int Id { get; set; }

        public string? RequestBody { get; set; }

        public string? EstimatedCompletionTime { get; set; }

        public string? RequestStatus { get; set; }

        public string RequestId { get; set; } = Guid.NewGuid().ToString();

    }

    public class ListingStatusDTO
    {
        public string? RequestStatus { get; set; }

        public string? EstimatedCompletionTime { get; set; }

        public string? ResourceURL { get; set; }
    }
}
