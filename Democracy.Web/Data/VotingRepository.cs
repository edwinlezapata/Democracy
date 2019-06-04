namespace Democracy.Web.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class VotingRepository : GenericRepository<Voting>, IVotingRepository
    {
        private readonly DataContext context;

        public VotingRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<int> AddVoteAsync(Voting Vote)
        {
            var votingevent = await this.context.VotingEvents
                .Where(c => c.Candidates.Any(ca => ca.Id == Vote.Id))
                .FirstOrDefaultAsync();

            if (votingevent == null)
            {
                return 0;
            }

            this.context.Votings.Update(Vote);
            await this.context.SaveChangesAsync();
            return votingevent.Id;
        }


        #region API
        public IQueryable GetVoteWithAll()
        {
            return this.context.Votings
                .Include(u => u.User)
                .Include(e => e.VotingEvent)
                .Include(c => c.Candidate);
        }

        public IQueryable GetVoteOfVotingEvent(int id)
        {
            return this.context.Votings
                .Include(e => e.VotingEvent)
                .Include(u => u.User)
                .Include(c => c.Candidate)
                .Where(e => e.VotingEvent.Id == id);
        }

        public IQueryable GetVoteOfCandidate(int id)
        {
            return this.context.Votings
                .Include(e => e.VotingEvent)
                .Include(c => c.Candidate)
                .Include(u => u.User)
                .Where(c => c.Candidate.Id == id);
        }

        public IQueryable GetUserVotes(string email)
        {
            return this.context.Votings
                .Include(e => e.VotingEvent)
                .Include(c => c.Candidate)
                .Where(v => v.User.Email == email);

        }

        public IQueryable GetUserVoteVotingEvent(string email, int idEvent)
        {
            return this.context.Votings
                .Include(e => e.VotingEvent)
                .Include(c => c.Candidate)
                .Where(e => e.VotingEvent.Id == idEvent)
                .Where(v => v.User.Email == email);


        }
        public int GetNumberVotes(string idEmail, int idEvent)
        {
            int Count = this.context.Votings.Count(x => x.User.Email == idEmail && x.VotingEvent.Id == idEvent);
            return Count;
        }

        #endregion

    }
}
