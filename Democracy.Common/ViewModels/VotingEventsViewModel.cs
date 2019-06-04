namespace Democracy.Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class VotingEventsViewModel : MvxViewModel
    {
        private List<VotingEvent> votingEvents;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private MvxCommand<VotingEvent> itemClickCommand;

        public VotingEventsViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }

        public ICommand ItemClickCommand
        {
            get
            {
                this.itemClickCommand = new MvxCommand<VotingEvent>(this.OnItemClickCommand);
                return itemClickCommand;
            }
        }

        public List<VotingEvent> VotingEvents
        {
            get => this.votingEvents;
            set => this.SetProperty(ref this.votingEvents, value);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            this.LoadVotingEvents();
        }

        private async void OnItemClickCommand(VotingEvent votingEvent)
        {
            if (votingEvent.StartDate > DateTime.Now)
            {
                this.dialogService.Alert("Atention", "The voting event has not started yet", "Accept");
                return;
            }

            if (votingEvent.EndDate < DateTime.Now)
            {
                this.dialogService.Alert("Atention", "This event is over, then the results will be shown", "Accept");
                return;
            }

            await this.navigationService.Navigate<
                VotingEventDetailViewModel, NavigationArgs>
                (new NavigationArgs { VotingEvent = votingEvent });
        }

        private async void LoadVotingEvents()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.GetListAsync<VotingEvent>(
                "https://democracy.azurewebsites.net",
                "/api",
                "/VotingEvents",
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.VotingEvents = (List<VotingEvent>)response.Result;
            this.VotingEvents = this.VotingEvents.OrderBy(p => p.StartDate).ToList();
        }
    }
}
