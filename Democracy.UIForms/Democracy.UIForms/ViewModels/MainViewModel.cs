using Democracy.Common.Models;
using Democracy.UIForms.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Democracy.UIForms.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private static MainViewModel instance;

        public User User { get; set; }

        public LoginViewModel Login { get; set; }

        public VotingEventsViewModel VotingEvents { get; set; }

        public VotingEventDetailViewModel VotingEventDetail { get; set; }

        public RegisterViewModel Register { get; set; }

        public RememberPasswordViewModel RememberPassword { get; set; }

        public ProfileViewModel Profile { get; set; }

        public ChangePasswordViewModel ChangePassword { get; set; }

        public MainViewModel()
        {
            instance = this;
            this.LoadMenus();

        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        public TokenResponse Token { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        private void LoadMenus()
        {
            var menus = new List<Menu>
    {
        new Menu
        {
            Icon = "ic_info",
            PageName = "AboutPage",
            Title = Languages.About
        },


        new Menu
        {
            Icon = "ic_person_pin",
            PageName = "ProfilePage",
            Title = Languages.ModifyUser
        },

        new Menu
        {
            Icon = "ic_settings_applications",
            PageName = "SetupPage",
            Title = Languages.Setup
        },

        new Menu
        {
            Icon = "ic_exit_to_app",
            PageName = "LoginPage",
            Title = Languages.CloseSession
        }
    };

            this.Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title
            }).ToList());
        }

    }



}
