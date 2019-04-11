namespace Democracy.Web.Data
{
    using Entities;

    public class VotingEventRepository : GenericRepository<VotingEvent>, IVotingEventRepository
    {
        public VotingEventRepository(DataContext context) : base(context)
        {
        }
    }

}
