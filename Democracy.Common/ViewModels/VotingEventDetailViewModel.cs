namespace Democracy.Common.ViewModels
{
    using System;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class VotingEventDetailViewModel: MvxViewModel<NavigationArgs>
    {
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private VotingEvent votingEvent;
        private bool isLoading;
        private MvxCommand<Candidate> candidateClickCommand;

        public VotingEventDetailViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.IsLoading = false;
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public VotingEvent VotingEvent
        {
            get => this.votingEvent;
            set => this.SetProperty(ref this.votingEvent, value);
        }



        public override void Prepare(NavigationArgs parameter)
        {
            this.votingEvent = parameter.VotingEvent;    
        }

        public ICommand CandidateClickCommand
        {
            get
            {
                this.candidateClickCommand = new MvxCommand<Candidate>(this.OnCandidateClickCommand);
                return candidateClickCommand;
            }
        }


        private void OnCandidateClickCommand(Candidate candidate)
        {
            this.dialogService.Confirm(
                "Confirm vote",
                $"You are sure that you want to vote for '{candidate.Name}' ",
                "Yes",
                "No",
                () => { this.VoteToCandidate(candidate); },
                null);
        }

        private async void VoteToCandidate(Candidate candidate)
        {
            var voting = new Voting
            {
                Email = Settings.UserEmail,
                Candidate = candidate.Id,
                VotingEvent = VotingEvent.Id
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.AddVoteAsync(
                "https://democracy.azurewebsites.net",
                "/api",
                "/Voting",
                voting,
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert(
                    "Information",
                    response.Message,
                    "Accept");
                return;
            }

            this.dialogService.Alert(
                "Ok",
                "Your vote for the candidate: " + candidate.Name + " has been successfully registered",
                "Accept");
        }
    }
}
