using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System;
using System.Reflection;

namespace EPTlab6
{
    [Activity(Label = "EPTlab6",
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
                var bufTextView = FindViewById<TableLayout>(
                            Convert.ToInt32(
                                typeof(Resource.Id).InvokeMember(
                                    ("dispersionTable"), BindingFlags.GetField, null, null, null)));
                bufTextView.Visibility = Android.Views.ViewStates.Visible;

                var textView = FindViewById<HorizontalScrollView>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("beforeView"), BindingFlags.GetField, null, null, null)));
                textView.Visibility = Android.Views.ViewStates.Visible;

                textView = FindViewById<HorizontalScrollView>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("afterView"), BindingFlags.GetField, null, null, null)));
                textView.Visibility = Android.Views.ViewStates.Visible;

                textView = FindViewById<HorizontalScrollView>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("matrixView"), BindingFlags.GetField, null, null, null)));
                textView.Visibility = Android.Views.ViewStates.Visible;
            };
        }
    }
}

