namespace Democracy.Web.Data
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IVotingRepository : IGenericRepository<Voting>
    {
        Task<int> AddVoteAsync(Voting Vote);

        IQueryable GetVoteWithAll();

        IQueryable GetVoteOfVotingEvent(int id);

        IQueryable GetVoteOfCandidate(int id);

        IQueryable GetUserVotes(string email);

        IQueryable GetUserVoteVotingEvent(string email, int idEvent);

        int GetNumberVotes(string email, int idEvent);

    }
}
