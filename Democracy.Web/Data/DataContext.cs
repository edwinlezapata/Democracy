namespace Democracy.Web.Data
{
    using Democracy.Web.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DbSet<VotingEvent> VotingEvents { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }

}
