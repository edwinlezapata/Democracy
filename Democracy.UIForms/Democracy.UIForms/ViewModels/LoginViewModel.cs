﻿namespace Democracy.UIForms.ViewModels
{
    using System.Windows.Input;
    using Democracy.Common.Helpers;
    using Democracy.Common.Models;
    using Democracy.Common.Services;
    using Democracy.UIForms.Helpers;
    using Democracy.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

        public bool IsRemember { get; set; }

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


        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(this.Login);

        public ICommand RegisterCommand => new RelayCommand(this.Register);

        public ICommand RememberPasswordCommand => new RelayCommand(this.RememberPassword);

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            //this.Email = "edwinlezapata@gmail.com";
            //this.Password = "123456";
            this.IsRemember = true;
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordError,
                    Languages.Accept);
                return;
            }
            this.IsRunning = true;

            this.IsEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.Emailorpassworderror,
                    Languages.Accept);
                return;
            }

            var token = (TokenResponse)response.Result;

            var response2 = await this.apiService.GetUserByEmailAsync(
                url,
                "/api",
                "/Account/GetUserByEmail",
                this.Email,
                "bearer",
                token.Token);

            var user = (User)response2.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.User = user;
            mainViewModel.Token = token;
            mainViewModel.VotingEvents = new VotingEventsViewModel();

            Settings.IsRemember = this.IsRemember;
            Settings.UserEmail = this.Email;
            Settings.UserPassword = this.Password;
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.User = JsonConvert.SerializeObject(user);

            Application.Current.MainPage = new MasterPage();
        }

        private async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        private async void RememberPassword()
        {
            MainViewModel.GetInstance().RememberPassword = new RememberPasswordViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RememberPasswordPage());
        }


    }
}
