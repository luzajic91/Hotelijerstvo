using HotelMan2.Models;
using HotelMan2.Models.DataTransferObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Housekeeping = HotelMan2.Models.Housekeeping;

namespace HotelMan2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousekeepingController : ControllerBase
    {
        private readonly HotelContext _dbContext;
        private readonly ILogger<BookingController> _logger;

        public HousekeepingController(ILogger<BookingController> logger, HotelContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet(Name ="GetHousekeepingLogs")]
        public IEnumerable<HousekeepingDto> Get()
        {
            try
            {
                _logger.LogInformation("Fetching list of bookings from the database.");
                var housekeeping = _dbContext.Housekeepings.ToList();
                var housekeepingDto = housekeeping.Select(h => new HousekeepingDto
                {
                    HousekeepingId = h.HousekeepingId,
                    RoomId = h.RoomId,
                    Description = h.Description,
                    HousekeepingDate = h.HousekeepingDate,
                    PersonId = h.PersonId
                }).ToList();
                return housekeepingDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching bookings from the database.");
                throw;
            }
        }

        [HttpPost(Name = "PostHousekeeping")]
        public IActionResult PostHouseKeeping(HousekeepingDto housekeepingdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("invalid model {0}", ModelState.Values);
                    return BadRequest(ModelState);
                }
                Housekeeping housekeeping = new Housekeeping()
                {
                    Description = housekeepingdto.Description,
                    HousekeepingDate = housekeepingdto.HousekeepingDate,
                    PersonId = housekeepingdto.PersonId,
                    RoomId = housekeepingdto.RoomId
                };
                _dbContext.Add(housekeeping);

                var room = _dbContext.Rooms.Find(housekeepingdto.RoomId);
                var person = _dbContext.People.Find(housekeepingdto.PersonId);

                if (room == null || person == null)
                {
                    return NotFound("either room or person not found");
                }
                else
                {
                    room.StatusId = 4;
                }

                _dbContext.SaveChanges();
                housekeepingdto.HousekeepingId = housekeeping.HousekeepingId;
                return Ok(housekeepingdto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
