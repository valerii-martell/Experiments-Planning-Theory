using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System;
using System.Reflection;

namespace EPTlab8
{
    [Activity(Label = "EPTlab8",
    MainLauncher = true,
    Icon = "@drawable/icon",
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
    ScreenOrientation = ScreenOrientation.Landscape)]
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
                var matrixTable = FindViewById<TableLayout>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("matrix1Table"), BindingFlags.GetField, null, null, null)));
                matrixTable.Visibility = Android.Views.ViewStates.Visible;

                matrixTable = FindViewById<TableLayout>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("matrix2Table"), BindingFlags.GetField, null, null, null)));
                matrixTable.Visibility = Android.Views.ViewStates.Visible;

                var tableView = FindViewById<TableLayout>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("equalTable"), BindingFlags.GetField, null, null, null)));
                tableView.Visibility = Android.Views.ViewStates.Visible;
            };
        }
    }
}

