namespace Democracy.Web.Data
{
    using Entities;
    using System.Linq;

    public interface IVotingEventRepository : IGenericRepository<VotingEvent>
    {
        IQueryable GetAllWithUsers();
    }

}
