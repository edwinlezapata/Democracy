namespace Democracy.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Democracy.Web.Models;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class VotingEventRepository : GenericRepository<VotingEvent>, IVotingEventRepository
    {
        private readonly DataContext context;

        public VotingEventRepository(DataContext context) : base(context)
        {
            this.context = context;
        }


        public async Task AddVoteAsync(VoteViewModel model)
        {
            var voteEvent = await this.GetVoteEventWhitCandidateAsync(model.VoteId);
            if (voteEvent == null)
            {
                return;
            }

            voteEvent.Votings.Add(new Voting{Vote = model.Vote,});
            await this.context.SaveChangesAsync();

        }

        //Actions whit Candidates
        public async Task AddCandidateAsync(CandidateViewModel model)
        {
            var votingEvent = await this.GetVotingEventWithCandidatesAsync(model.VotingEventId);
            if (votingEvent == null)
            {
                return;
            }

            votingEvent.Candidates.Add(new Candidate
            {
                NameCandidate = model.NameCandidate,
                Proposal = model.Proposal,
                ImageUrl = model.ImageUrl,
               
            });
            this.context.VotingEvents.Update(votingEvent);
            await this.context.SaveChangesAsync();
        }

        public async Task<VotingEvent> GetVotingEventWithCandidatesAsync(int id)
        {
            return await this.context.VotingEvents
                .Include(c => c.Candidates)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<VotingEvent> GetVoteEventWhitCandidateAsync(int id)
        {
            return await this.context.VotingEvents
                .Include(v => v.Votings)
                .Include(c => c.Candidates)
                .Include(u => u.User)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Voting> GetVotingAsync(int id)
        {
            return await this.context.Votings.FindAsync(id);
        }

        public IQueryable GetVotingEventsWithCandidates()
        {
            return this.context.VotingEvents
            .Include(c => c.Candidates)
            .Include(u => u.User);
            //.Where(d => d.EndDate > DateTime.Now)
            //.OrderBy(e => e.EndDate);

        }

        public Task<Candidate> GetCandidateAsync(int id)
        {
            return this.context.Candidates.FindAsync(id);
        }

        public async Task<int> UpdateCandidateAsync(Candidate candidate)
        {
          
            var votingEvent = await this.context.VotingEvents.Where(c => c.Candidates.Any(ca => ca.Id == candidate.Id)).FirstOrDefaultAsync();
            if (votingEvent == null)
            {
                return 0;
            }
            this.context.Candidates.Update(candidate);
            await this.context.SaveChangesAsync();
            return votingEvent.Id;
        }

        public async Task<int> DeleteCandidateAsync(Candidate candidate)
        {
            var votingEvent = await this.context.VotingEvents.Where(c => c.Candidates.Any(ca => ca.Id == candidate.Id)).FirstOrDefaultAsync();
            if (votingEvent == null)
            {
                return 0;
            }
            this.context.Candidates.Remove(candidate);
            await this.context.SaveChangesAsync();
            return votingEvent.Id;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.VotingEvents
                .Include(u => u.User);
        }

        public IQueryable GetAllWithUsersAndCandidates()
        {
            return this.context.VotingEvents
                .Include(u => u.User)
                .Include(u => u.Candidates);
        }

        public IQueryable GetVotingEventWithId(int id)
        {
            return this.context.VotingEvents
            .Include(c => c.Candidates)
            .Where(c => c.Id == id);
        }

        public IQueryable GetVotingEventsWithCandidatesEnd()
        {
            return this.context.VotingEvents
           .Include(c => c.Candidates)
           .Where(d => d.EndDate < DateTime.Now)
           .OrderBy(e => e.EndDate);
        }
    }

}
