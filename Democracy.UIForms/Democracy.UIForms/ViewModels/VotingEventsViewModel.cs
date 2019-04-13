﻿namespace Democracy.UIForms.ViewModels
{
    using Common.Models;
    using Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;


    public class VotingEventsViewModel : BaseViewModel
    {

        private readonly ApiService apiService;
        private ObservableCollection<VotingEvent> votingEvents;
        private bool isRefreshing;

        public ObservableCollection<VotingEvent> VotingEvents
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

            var response = await this.apiService.GetListAsync<VotingEvent>(
                "https://democracy.azurewebsites.net",
                "/api",
                "/VotingEvents");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var myVotingEvents = (List<VotingEvent>)response.Result;
            this.VotingEvents = new ObservableCollection<VotingEvent>(myVotingEvents);
        }
    }
}
