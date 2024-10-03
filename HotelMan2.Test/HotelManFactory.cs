using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HotelMan2;
using HotelMan2.Models;

namespace HotelMan2.Test
{
    public class HotelManFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<HotelContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<HotelContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryHotelDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<HotelContext>();

                    db.Database.EnsureCreated();

                    SeedDatabase(db);
                }
            });
        }

        private void SeedDatabase(HotelContext db)
        {
            var roomStatus = new RoomStatus
            {
                RoomStatusName = "Available"
            };
            db.RoomStatuses.Add(roomStatus);

            var roomType = new RoomType
            {
                RoomTypeName = "Deluxe",
                Price = 20
            };
            db.RoomTypes.Add(roomType);

            var userRole = new UserRole
            {
                RoleName = "Admin",
            };
            db.UserRoles.Add(userRole);

            db.SaveChanges();

            var person = new Person
            {
                Name = "Vladan",
                Surename = "Luzajic",
                Email = "dadodmg@gmail.com",
                RoleId = db.UserRoles.FirstOrDefault().RoleId,
                UserName = "admin",
                Password = "admin"
            };
            db.Add(person);

            var room = new Room
            {
                RoomNumber = "11",
                RoomTypeId = db.RoomTypes.FirstOrDefault().RoomTypeId,
                StatusId = db.RoomStatuses.FirstOrDefault().RoomStatusId
            };
            db.Add(room);
            db.SaveChanges();

        }
    }
}
