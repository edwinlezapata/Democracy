namespace Democracy.Web.Data
{
    using Democracy.Web.Helpers;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Voter");

            if (!this.context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Medellín" });
                cities.Add(new City { Name = "Bogotá" });
                cities.Add(new City { Name = "Calí" });

                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Colombia"
                });

                await this.context.SaveChangesAsync();
            }


            var user = await this.userHelper.GetUserByEmailAsync("edwinlezapata@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Edwin Leon",
                    LastName = "Zapata",
                    Email = "edwinlezapata@gmail.com",
                    UserName = "edwinlezapata@gmail.com",
                    Occupation = "Enginered",
                    Stratum = "1",
                    Gender = "M",
                    PhoneNumber = "3015017785",
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()

                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }


            if (!this.context.VotingEvents.Any())
            {
                this.AddVotingEvent("Event for the election of the president of the republic",
                    "In this voting event you can choose the president of the republic for the period 2020-2024", user);

                this.AddVotingEvent("Event for the election of the mayor of the city",
                    "In this voting event you can choose the mayor of the city for the period 2020-2023", user);

                this.AddVotingEvent("Event for the election of the city council",
                    "In this voting event you can choose the city council for the period 2020-2023", user);

                await this.context.SaveChangesAsync();
            }
        }

        private void AddVotingEvent(string name, string descripcion, User user)
        {
            this.context.VotingEvents.Add(new VotingEvent
            {
                EventName = name,
                Description = descripcion,
                User = user
            });
        }
    }
}
