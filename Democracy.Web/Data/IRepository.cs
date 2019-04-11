namespace Democracy.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    
    public interface IRepository
    {
        void AddVotingEvent(VotingEvent votingevent);

        VotingEvent GetVotingEvent(int id);

        IEnumerable<VotingEvent> GetVotingEvents();

        void RemoveVotingEvent(VotingEvent votingeven);

        Task<bool> SaveAllAsync();
        void UpdateVotingEvent(VotingEvent votingevent);

        bool VotingEventExists(int id);
    }
}