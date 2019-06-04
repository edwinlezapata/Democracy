namespace Democracy.Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Democracy.Common.Helpers;
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
        private string occupations;
        private string stratum;
        private string gender;
        private DateTime birthDay;
        private string phoneNumber;
        private string password;
        private string confirmPassword;
        private bool isLoading;

        public RegisterViewModel(
            IMvxNavigationService navigationService,
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.LoadCountries();
            this.IsLoading = false;
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

        public string Occupations
        {
            get => this.occupations;
            set => this.SetProperty(ref this.occupations, value);
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

        public DateTime BirthDay
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

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        private async void LoadCountries()
        {
            this.IsLoading = true;

            var response = await this.apiService.GetListAsync<Country>(
                "https://democracy.azurewebsites.net",
                "/api",
                "/Countries");

            this.IsLoading = false;

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Countries = (List<Country>)response.Result;
        }

        private async void RegisterUser()
        {
            if (string.IsNullOrEmpty(this.FirstName))
            {
                this.dialogService.Alert("Error", "You must enter a first name.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                this.dialogService.Alert("Error", "You must enter a last name.", "Accept");
                return;
            }
            if (!RegexHelper.IsValidEmail(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter an email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Occupations))
            {
                this.dialogService.Alert("Error", "You must enter a Ocupation.", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Stratum))
            {
                this.dialogService.Alert("Error", "You must enter a stratum.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Gender))
            {
                this.dialogService.Alert("Error", "You must enter a gender.", "Accept");
                return;
            }

            if (this.BirthDay > DateTime.Now || this.BirthDay ==null)
            {
                this.dialogService.Alert("Error", "You must enter a birthday.", "Accept");
               return;
           }

              if (this.Countries == null)
            {
                this.dialogService.Alert("Error", "You must enter a country", "Accept");
                return;
            }
            
            if (this.Cities == null)
            {
               this.dialogService.Alert("Error", "You must enter a city", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.PhoneNumber))
            {
                this.dialogService.Alert("Error", "You must enter a phoneNumber.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.dialogService.Alert("Error", "You must enter a password.", "Accept");
                return;
            }

            if (!this.Password.Equals(this.ConfirmPassword))
            {
                this.dialogService.Alert("Error", "paswword don't mach ", "Aceptar");
                return;
            }

            this.IsLoading = true;
            
            var request = new NewUserRequest
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Occupation = this.occupations,
                Stratum = this.stratum,
                Gender = this.gender,
                BirthDay = this.birthDay,
                CityId = this.SelectedCity.Id,
                PhoneNumber = this.phoneNumber,
                Password = this.Password,
                
            };

            var response = await this.apiService.RegisterUserAsync(
                "https://democracy.azurewebsites.net",
                "/api",
                "/Account",
                request);

            this.IsLoading = false;

            if (!response.IsSuccess)
            {
                this.IsLoading = false;
                this.dialogService.Alert(
                    "Error", 
                    response.Message, 
                    "Accept");
                return;
            }

            this.dialogService.Alert(
                "Ok",
                "The user was created succesfully, you must " +
                "confirm your user by the email sent to you and then you could login with " +
                "the email and password entered.",
                "Accept",
                () => { this.navigationService.Close(this); }
             );
        }
    }
}
