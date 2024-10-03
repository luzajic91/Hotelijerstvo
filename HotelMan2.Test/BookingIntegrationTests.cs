using HotelMan2.Models;
using HotelMan2.Models.DataTransferObject;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMan2.Test
{
    public class BookingIntegrationTests : IClassFixture<HotelManFactory<Program>>
    {

        [CollectionDefinition("NoParallel", DisableParallelization = true)]
        public class NoParallel { }

        private readonly HttpClient _client;
        private readonly HotelManFactory<Program> _factory;

        public BookingIntegrationTests(HotelManFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }


        [Fact]
        public async Task Post_Booking_Should_Create_New_Booking()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var _hotelContext = scope.ServiceProvider.GetRequiredService<HotelContext>();

                var bookingDto = new BookingDto
                {
                    CheckIn = DateOnly.FromDateTime(DateTime.Now),
                    CheckOut = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                    PersonId = _hotelContext.People.FirstOrDefault().PersonId,
                    RoomId = _hotelContext.Rooms.FirstOrDefault().RoomId,
                    TotalAmmount = 100.00M
                };

                var content = new StringContent(JsonConvert.SerializeObject(bookingDto), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("/api/Booking", content);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
