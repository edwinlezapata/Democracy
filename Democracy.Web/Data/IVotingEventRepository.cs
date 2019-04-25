namespace Democracy.Web.Data
{
    using Democracy.Web.Models;
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IVotingEventRepository : IGenericRepository<VotingEvent>
    {
        IQueryable GetAllWithUsers();

        IQueryable GetVotingEventsWithCandidates();

        Task<VotingEvent> GetVotingEventWithCandidatesAsync(int id);

        Task<Candidate> GetCandidateAsync(int id);

        Task AddCandidateAsync(CandidateViewModel model);

        Task<int> UpdateCandidateAsync(Candidate candidate);

        Task<int> DeleteCandidateAsync(Candidate candidate);

        Task<Voting> GetVotingAsync(int id);

        Task AddVoteAsync(VoteViewModel model);

        

    }

}
