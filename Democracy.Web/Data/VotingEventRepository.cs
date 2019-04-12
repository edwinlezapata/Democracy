namespace Democracy.Web.Data
{
    using System.Linq;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class VotingEventRepository : GenericRepository<VotingEvent>, IVotingEventRepository
    {
        private readonly DataContext context;

        public VotingEventRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.VotingEvents.Include(v => v.User);
        }
    }

}
