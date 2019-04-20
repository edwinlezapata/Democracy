using Democracy.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Democracy.UIForms.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private static MainViewModel instance;

        public LoginViewModel Login { get; set; }

        public VotingEventsViewModel VotingEvents { get; set; }

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

        private void LoadMenus()
        {
            var menus = new List<Menu>
    {
        new Menu
        {
            Icon = "ic_info",
            PageName = "AboutPage",
            Title = "About"
        },

        new Menu
        {
            Icon = "ic_settings_applications",
            PageName = "SetupPage",
            Title = "Setup"
        },

        new Menu
        {
            Icon = "ic_exit_to_app",
            PageName = "LoginPage",
            Title = "Close session"
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
