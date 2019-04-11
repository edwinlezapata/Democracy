namespace Democracy.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class SeedDb
    {
        private readonly DataContext context;
        private Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.VotingEvents.Any())
            {
                this.AddVotingEvent("Event for the election of the president of the republic",
                    "In this voting event you can choose the president of the republic for the period 2020-2024");

                this.AddVotingEvent("Event for the election of the mayor of the city",
                    "In this voting event you can choose the mayor of the city for the period 2020-2023");

                this.AddVotingEvent("Event for the election of the city council",
                    "In this voting event you can choose the city council for the period 2020-2023");

                await this.context.SaveChangesAsync();
            }
        }

        private void AddVotingEvent(string name, string descripcion)
        {
            this.context.VotingEvents.Add(new VotingEvent
            {
                EventName = name,
                Description = descripcion
            });
        }
    }
}
