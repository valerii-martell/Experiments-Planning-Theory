using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System;
using System.Reflection;

namespace EPTlab7
{
    [Activity(Label = "EPTlab7",
    MainLauncher = true,
    Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var buttonCompute = FindViewById<Button>(Resource.Id.buttonCompute);

            buttonCompute.Click += (e, o) =>
            {

                //Matrix
                var table = FindViewById<TableLayout>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("table1"), BindingFlags.GetField, null, null, null)));
                table.Visibility = Android.Views.ViewStates.Visible;

                table = FindViewById<TableLayout>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("table2"), BindingFlags.GetField, null, null, null)));
                table.Visibility = Android.Views.ViewStates.Visible;

                var image = FindViewById<ImageView>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("image"), BindingFlags.GetField, null, null, null)));
                image.Visibility = Android.Views.ViewStates.Visible;
            };
        }
    }
}

