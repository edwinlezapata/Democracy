﻿namespace Democracy.Common.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Services;

    public class RegisterViewModel : MvxViewModel
    {
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private readonly IDialogService dialogService;
        private List<Country> countries;
        private List<City> cities;
        private Country selectedCountry;
        private City selectedCity;
        private MvxCommand registerCommand;
        private string firstName;
        private string lastName;
        private string email;
        private string occupation;
        private string stratum;
        private string gender;
        private string birthDay;
        private string phoneNumber;
        private string password;
        private string confirmPassword;

        public RegisterViewModel(
            IMvxNavigationService navigationService,
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.LoadCountries();
        }

        public ICommand RegisterCommand
        {
            get
            {
                this.registerCommand = this.registerCommand ?? new MvxCommand(this.RegisterUser);
                return this.registerCommand;
            }
        }

        public string FirstName
        {
            get => this.firstName;
            set => this.SetProperty(ref this.firstName, value);
        }

        public string LastName
        {
            get => this.lastName;
            set => this.SetProperty(ref this.lastName, value);
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        public string Occupation
        {
            get => this.occupation;
            set => this.SetProperty(ref this.occupation, value);
        }

        public string Stratum
        {
            get => this.stratum;
            set => this.SetProperty(ref this.stratum, value);
        }

        public string Gender
        {
            get => this.gender;
            set => this.SetProperty(ref this.gender, value);
        }

        public string BirthDay
        {
            get => this.birthDay;
            set => this.SetProperty(ref this.birthDay, value);
        }

        public string PhoneNumber
        {
            get => this.phoneNumber;
            set => this.SetProperty(ref this.phoneNumber, value);
        }

        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        public string ConfirmPassword
        {
            get => this.confirmPassword;
            set => this.SetProperty(ref this.confirmPassword, value);
        }

        public List<Country> Countries
        {
            get => this.countries;
            set => this.SetProperty(ref this.countries, value);
        }

        public List<City> Cities
        {
            get => this.cities;
            set => this.SetProperty(ref this.cities, value);
        }

        public Country SelectedCountry
        {
            get => selectedCountry;
            set
            {
                this.selectedCountry = value;
                this.RaisePropertyChanged(() => SelectedCountry);
                this.Cities = SelectedCountry.Cities;
            }
        }

        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                RaisePropertyChanged(() => SelectedCity);
            }
        }

        private async void LoadCountries()
        {
            var response = await this.apiService.GetListAsync<Country>(
                "https://democracy.azurewebsites.net",
                "/api",
                "/Countries");

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Countries = (List<Country>)response.Result;
        }

        private async void RegisterUser()
        {
            // TODO: Make the local validations
            var request = new NewUserRequest
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Occupation = this.occupation,
                Stratum = this.stratum,
                Gender = this.gender,
                //BirthDay = this.birthDay,
                PhoneNumber = this.phoneNumber,
                Password = this.Password,
                
            };

            var response = await this.apiService.RegisterUserAsync(
                "https://democracy.azurewebsites.net",
                "/api",
                "/Account",
                request);

            this.dialogService.Alert("Ok", "The user was created succesfully, you must " +
                "confirm your user by the email sent to you and then you could login with " +
                "the email and password entered.", "Accept");

            await this.navigationService.Close(this);
        }
    }

}
