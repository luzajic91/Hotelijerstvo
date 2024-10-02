using HotelMan2.Models;
using HotelMan2.Models.DataTransferObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Room = HotelMan2.Models.Room;

namespace HotelMan2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly HotelContext _dbContext;
        private readonly ILogger<BookingController> _logger;

        public RoomController(ILogger<BookingController> logger, HotelContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetRooms")]
        public IEnumerable<RoomDto> get()
        {
            try
            {
                _logger.LogInformation("fetching rooms");
                var rooms = _dbContext.Rooms.ToList();
                var roomsdto = rooms.Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomTypeId = r.RoomTypeId,
                    StatusId = r.StatusId
                }).ToList();
                _logger.LogInformation("successfully retrieved {count} bookings from the database.", roomsdto.Count);
                return roomsdto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "an error occurred while fetching bookings from the database.");
                throw;
            }
        }

        [HttpPut("id")]
        public IActionResult UpdateRoomStatus(Guid id, int status)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for Room update: {0}", ModelState.Values);
                    return BadRequest(ModelState);
                }

                Room room = _dbContext.Rooms.Find(id);
                if (room == null)
                {
                    return NotFound("room not found");
                }
                room.StatusId = status;
                _dbContext.SaveChanges();
                return Ok(room);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "an error occurred while fetching bookings from the database.");
                throw;
            }
        }


    }
}
