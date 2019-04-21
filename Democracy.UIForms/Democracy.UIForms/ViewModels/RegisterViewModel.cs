namespace Democracy.UIForms.ViewModels
{
    using Common.Models;
    using Democracy.Common.Helpers;
    using Democracy.Common.Services;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class RegisterViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Country> countries;
        private Country country;
        private ObservableCollection<City> cities;
        private City city;
        private readonly ApiService apiService;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Occupations { get; set; }

        public string Stratum { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Password { get; set; }

        public string Confirm { get; set; }

        public Country Country
        {
            get => this.country;
            set
            {
                this.SetValue(ref this.country, value);
                this.Cities = new ObservableCollection<City>(this.Country.Cities.OrderBy(c => c.Name));
            }

        }

        public City City
        {
            get => this.city;
            set => this.SetValue(ref this.city, value);
        }

        public ObservableCollection<Country> Countries
        {
            get => this.countries;
            set => this.SetValue(ref this.countries, value);
        }

        public ObservableCollection<City> Cities
        {
            get => this.cities;
            set => this.SetValue(ref this.cities, value);
        }

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public ICommand RegisterCommand => new RelayCommand(this.Register);

        public RegisterViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.LoadCountries();
        }

        private async void LoadCountries()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Country>(
                url,
                "/api",
                "/Countries");

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var myCountries = (List<Country>)response.Result;
            this.Countries = new ObservableCollection<Country>(myCountries);
        }

        private async void Register()
        {
            if (string.IsNullOrEmpty(this.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter the first name.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter the last name.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            }

            if (!RegexHelper.IsValidEmail(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a valid email.",
                    "Accept");
                return;
            }

            if (this.Country == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must select a country.",
                    "Accept");
                return;
            }

            if (this.City == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must select a city.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Occupations))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an Occupation.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Stratum))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a Stratum.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Gender))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a Gender.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.PhoneNumber))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a Phone.",
                    "Accept");
                return;
            }

           // if (string.IsNullOrEmpty(this.BirthDay))
           // {
            //    await Application.Current.MainPage.DisplayAlert(
            //        "Error",
            //        "You must enter a BirthDay.",
            //        "Accept");
            //    return;
            //}

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");
                return;
            }

            if (this.Password.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You password must be at mimimun 6 characters.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password confirm.",
                    "Accept");
                return;
            }

            if (!this.Password.Equals(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "The password and the confirm do not match.",
                    "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new NewUserRequest
            {
                BirthDay = this.BirthDay,
                CityId = this.City.Id,
                Email = this.Email,
                FirstName = this.FirstName,
                Gender = this.Gender,
                LastName = this.LastName,
                Occupation = this.Occupations,
                Password = this.Password,
                Phone = this.PhoneNumber,
                Stratum = this.Stratum,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.RegisterUserAsync(
                url,
                "/api",
                "/Account",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                response.Message,
                "Accept");
            await Application.Current.MainPage.Navigation.PopAsync();
        }




    }

}
