using Democracy.Common.Models;

namespace Democracy.UIForms.ViewModels
{
    public class VotingEventDetailViewModel
    {
        public VotingEvent VotingEvent { get; set; }

        public VotingEventDetailViewModel(VotingEvent votingEvent)
        {
            this.VotingEvent = votingEvent;
        }
    }
}
