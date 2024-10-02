using HotelMan2.Models;
using HotelMan2.Models.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Booking = HotelMan2.Models.Booking;

namespace HotelMan2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly HotelContext _dbContext;
        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger, HotelContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet(Name = "GetBookings")]
        public IEnumerable<BookingDto> Get()
        {

            try
            {
                _logger.LogInformation("Fetching list of bookings from the database.");
                var bookings2 = _dbContext.Bookings.ToList();
                var bookingDto = bookings2.Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    TotalAmmount = b.TotalAmmount,
                    PersonId = b.PersonId,
                    RoomId = b.RoomId
                }).ToList();
                _logger.LogInformation("Successfully retrieved {Count} bookings from the database.", bookingDto.Count);
                return bookingDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching bookings from the database.");
                throw;
            }
        }

        [HttpPost(Name = "PostBooking")]
        public IActionResult PostBook(BookingDto bookingsDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("invalid model {0}", ModelState.Values);
                    return BadRequest(ModelState);
                }
                Booking booking = new Booking()
                {
                    CheckIn = bookingsDto.CheckIn,
                    CheckOut = bookingsDto.CheckOut,
                    PersonId = bookingsDto.PersonId,
                    RoomId = bookingsDto.RoomId,
                    TotalAmmount = bookingsDto.TotalAmmount,
                };
                var room = _dbContext.Rooms.Find(booking.RoomId);
                ModelState.ToString();
                _dbContext.Bookings.Add(booking);

                if (room == null)
                {
                    return NotFound("room not found");
                }
                else
                {
                    room.StatusId = 3;
                }

                _dbContext.SaveChanges();
                bookingsDto.BookingId = booking.BookingId;
                return Ok(bookingsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new booking.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult CancelBooking(Guid id)
        {
            try
            {
                var booking = _dbContext.Bookings.Find(id);

                if (booking == null)
                {
                    _logger.LogWarning("Booking with ID {Id} not found.", id);
                    return NotFound("Booking not found.");
                }
                _dbContext.Bookings.Remove(booking);
                _dbContext.SaveChanges();
                _logger.LogInformation("Booking with ID {Id} successfully deleted.", id);
                return NoContent();
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the booking with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
