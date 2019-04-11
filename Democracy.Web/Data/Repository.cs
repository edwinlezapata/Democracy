namespace Democracy.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Democracy.Web.Data.Entities;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<VotingEvent> GetVotingEvents()
        {
            return this.context.VotingEvents.OrderBy(p => p.EventName);
        }

        public VotingEvent GetVotingEvent(int id)
        {
            return this.context.VotingEvents.Find(id);
        }

        public void AddVotingEvent(VotingEvent votingevent)
        {
            this.context.VotingEvents.Add(votingevent);
        }

        public void UpdateVotingEvent(VotingEvent votingevent)
        {
            this.context.Update(votingevent);
        }

        public void RemoveVotingEvent(VotingEvent votingeven)
        {
            this.context.VotingEvents.Remove(votingeven);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool VotingEventExists(int id)
        {
            return this.context.VotingEvents.Any(p => p.Id == id);
        }
    }

}
