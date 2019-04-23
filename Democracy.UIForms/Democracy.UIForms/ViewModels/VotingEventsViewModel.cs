namespace Democracy.UIForms.ViewModels
{
    using Common.Models;
    using Common.Services;
    using Democracy.UIForms.Helpers;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;


    public class VotingEventsViewModel : BaseViewModel
    {

        private readonly ApiService apiService;
        private List<VotingEvent> myVotingEvents;
        private ObservableCollection<VotingEventItemViewModel> votingEvents;
        private bool isRefreshing;

        public ObservableCollection<VotingEventItemViewModel> VotingEvents
        {
            get => this.votingEvents;
            set => this.SetValue(ref this.votingEvents, value);
        }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }


        public VotingEventsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadVotingEvents();
        }

        private async void LoadVotingEvents()
        {
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<VotingEvent>(
                url,
                "/api",
                "/VotingEvents",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            this.myVotingEvents = (List<VotingEvent>)response.Result;
            this.RefresVotingEventList();
        }

        public void AddProductToList(VotingEvent votingEvent)
        {
            this.myVotingEvents.Add(votingEvent);
            this.RefresVotingEventList();
        }

        private void RefresVotingEventList()
        {
            this.VotingEvents = new ObservableCollection<VotingEventItemViewModel>(
                this.myVotingEvents.Select(v => new VotingEventItemViewModel
                {
                    Id = v.Id,
                    ImageFullPath = v.ImageFullPath,
                    EventName = v.EventName,
                    Description = v.Description,
                    StartDate = v.StartDate,
                    EndDate = v.EndDate,
                    User = v.User
                })
                .OrderBy(v => v.StartDate)
                .ToList());
        }
    }

}

