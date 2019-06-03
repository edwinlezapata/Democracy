using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using Democracy.Common.ViewModels;
using Toolbar = global::Android.Support.V7.Widget.Toolbar;

namespace Democracy.UICross.Android.Views
{
    [Activity(Label = "@string/voting_events")]
    public class VotingEventsView : MvxAppCompatActivity<VotingEventsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.VotingEventsPage);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }
    }
}
