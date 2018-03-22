using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace EPTlab4
{
    [Activity(Label = "Experiment Planning Theory - lab4",
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
                Model model = new Model();

                Object[][] data = new Object[4][];
                /*for (int i = 0; i < 4; i++)
                {
                    data[i][0] = x1[i];
                    data[i][1] = x2[i];
                    data[i][2] = x3[i];
                    data[i][3] = x1n[i];
                    data[i][4] = x2n[i];
                    data[i][5] = x3n[i];
                    data[i][y.Count + 6] = my[i];
                    data[i][y.Count + 7] = disp[i];
                }

                for (int i = 0; i < y.Count; i++)
                {
                    double[] temp = y[i];
                    data[0][i + 6] = temp[0];
                    data[1][i + 6] = temp[1];
                    data[2][i + 6] = temp[2];
                    data[3][i + 6] = temp[3];
                }*/
                String[] str = new String[8];
                str[0] = "x1";
                str[1] = "x2";
                str[2] = "x3";
                str[3] = "x1n";
                str[4] = "x2n";
                str[5] = "x3n";
                for (int i = 6; i < 6; i++)
                {
                    str[i] = "y" + (i - 5);
                }
                str[6] = "m(y)";
                str[7] = "dispersion";

                //Matrix
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        var bufTextView = FindViewById<TableLayout>(
                            Convert.ToInt32(
                                typeof(Resource.Id).InvokeMember(
                                    ("matrixTable"), BindingFlags.GetField, null, null, null)));
                        bufTextView.Visibility = Android.Views.ViewStates.Visible;
                        bufTextView = FindViewById<TableLayout>(
                            Convert.ToInt32(
                                typeof(Resource.Id).InvokeMember(
                                    ("matrixTable2"), BindingFlags.GetField, null, null, null)));
                        bufTextView.Visibility = Android.Views.ViewStates.Visible;
                        break;
                    }
                }
                var textView = FindViewById<TableLayout>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("normalTable"), BindingFlags.GetField, null, null, null)));
                textView.Visibility = Android.Views.ViewStates.Visible;
                textView = FindViewById<TableLayout>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("naturalTable"), BindingFlags.GetField, null, null, null)));
                textView.Visibility = Android.Views.ViewStates.Visible;
                textView = FindViewById<TableLayout>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("dispersionTable"), BindingFlags.GetField, null, null, null)));
                textView.Visibility = Android.Views.ViewStates.Visible;
                textView = FindViewById<TableLayout>(
                           Convert.ToInt32(
                               typeof(Resource.Id).InvokeMember(
                                   ("equationTable"), BindingFlags.GetField, null, null, null)));
                textView.Visibility = Android.Views.ViewStates.Visible;
            };
        }
    }
}

