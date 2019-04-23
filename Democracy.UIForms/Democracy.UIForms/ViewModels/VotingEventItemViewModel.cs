namespace Democracy.UIForms.ViewModels
{
    using Common.Models;
    using Democracy.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;

    public class VotingEventItemViewModel : VotingEvent
    {
        public ICommand SelectVotingEventCommand => new RelayCommand(this.SelectVotingEvent);

        private async void SelectVotingEvent()
        {
            MainViewModel.GetInstance().VotingEventDetail = new VotingEventDetailViewModel(this);
            await App.Navigator.PushAsync(new VotingEventDetailPage());
            
        }

    }
}
