namespace Democracy.UIForms.ViewModels
{
    using System.Windows.Input;
    using Democracy.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(this.Login);

        public LoginViewModel()
        {
            this.Email = "edwinlezapata@gmail.com";
            this.Password = "123456";
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    "You Must Enter A Email",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    "You Must Enter A Password",
                    "Accept");
                return;
            }

            if (!this.Email.Equals("edwinlezapata@gmail.com") || !this.Password.Equals("123456"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    "Incorrect User Or Password", 
                    "Accept");
                return;
            }

            //await Application.Current.MainPage.DisplayAlert(
            //   "Ok", 
            //  "Welcome Friend!!!", 
            //  "Accept");

            MainViewModel.GetInstance().VotingEvents = new VotingEventsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new VotingEventsPage());
        }
    }
}
