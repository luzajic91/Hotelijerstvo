using HotelMan2.Models;
using HotelMan2.Models.DataTransferObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelMan2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly HotelContext _dbContext;
        private readonly ILogger<BookingController> _logger;

        public PersonController(ILogger<BookingController> logger, HotelContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        [HttpGet(Name = "GetPersons")]
        public IEnumerable<PersonDto> Get()
        {
            try
            {
                _logger.LogInformation("Fetching list of persons from the database.");
                var persons = _dbContext.People.ToList();
                var personsDto = persons.Select(x => new PersonDto
                {
                    Name = x.Name,
                    Bookings = x.Bookings,
                    Email = x.Email,
                    Housekeepings = x.Housekeepings,
                    PersonId = x.PersonId,
                    RoleId = x.RoleId,
                    Surename = x.Surename
                }).ToList();
                _logger.LogInformation("Successfully retrieved {Count} bookings from the database.", personsDto.Count);
                return personsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching bookings from the database.");
                throw;
            }
        }
    }
}
