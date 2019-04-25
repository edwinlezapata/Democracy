using System;
using System.Collections.Generic;
using System.Text;

namespace Democracy.UIForms.Helpers
{
    using Interfaces;
    using Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string About => Resource.About;

        public static string Accept => Resource.Accept;

        public static string CloseSession => Resource.Close_session;

        public static string EmailError => Resource.Email_Error;

        public static string EmailError2 => Resource.Email_Error2;

        public static string Emailorpassworderror => Resource.Email_or_password_error;

        public static string Error => Resource.Error;

        public static string ErrorCity => Resource.Error_City;

        public static string ErrorCountry => Resource.Error_Country;

        public static string ErrorGender => Resource.Error_Gender;

        public static string ErrorOccupation => Resource.Error_Occupation;

        public static string ErrorPhone => Resource.Error_Phone;

        public static string ErrorStratum => Resource.Error_Stratum;

        public static string FistNameError => Resource.Fist_name_error;

        public static string LastNameError => Resource.Last_name_error;

        public static string NotMatchPasswordError => Resource.Not_match_password_error;

        public static string PasswordConfirmError => Resource.Password_Confirm_Error;

        public static string PasswordError => Resource.Password_Error;

        public static string PasswordLenghError => Resource.Password_Lengh_Error;

        public static string Setup => Resource.Setup;

        public static string ValidEmail => Resource.Valid_Email;

        public static string Login => Resource.Login;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.Email_Place;

        public static string Password => Resource.Password;

        public static string PaswordPH => Resource.Password_Place_Holder;

        public static string RememberInDevice => Resource.Remember_me_in_this_device;

        public static string ForgotPassword => Resource.Forgot_Password;

        public static string AddNewUser => Resource.Add_New_User;

        public static string ModifyUser => Resource.Modify_User;
    }

}
