using formulaAirline.API.Models;
using formulaAirline.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace formulaAirline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IMessageProducer _messageProducer;

        // in-memory db
        public static readonly List<Booking> _bookings = new();

        public BookingController(IMessageProducer messageProducer, ILogger<BookingController> logger)
        {
            _messageProducer = messageProducer;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBooking(Booking booking)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            _bookings.Add(booking);

            _messageProducer.SendingMessage<Booking>(booking);

            await Task.CompletedTask;

            return Ok("Created");
        }
    }
}
