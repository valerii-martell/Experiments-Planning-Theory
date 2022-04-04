using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System;
using System.Reflection;

namespace EPTlab2
{
    [Activity(Label = "Experiment Planning Theory - lab2",
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

            var textX1min = FindViewById<EditText>(Resource.Id.editTextX1min);
            var textX1max = FindViewById<EditText>(Resource.Id.editTextX1max);
            var textX2min = FindViewById<EditText>(Resource.Id.editTextX2min);
            var textX2max = FindViewById<EditText>(Resource.Id.editTextX2max);
            var textYmin = FindViewById<EditText>(Resource.Id.editTextYmin);
            var textYmax = FindViewById<EditText>(Resource.Id.editTextYmax);

            var buttonCompute = FindViewById<Button>(Resource.Id.buttonCompute);

            buttonCompute.Click += (e, o) =>
            {
                double x1Min = Convert.ToDouble(textX1min.Text);
                double x1Max = Convert.ToDouble(textX1max.Text);
                double x2Min = Convert.ToDouble(textX2min.Text);
                double x2Max = Convert.ToDouble(textX2max.Text);
                double yMin = Convert.ToDouble(textYmin.Text);
                double yMax = Convert.ToDouble(textYmax.Text);

                Model model = new Model(x1Min, x1Max, x2Min, x2Max, yMin, yMax);
                model.DoExperiment();

                //Matrix
                for (int i = 0; i < model.Xmatr.GetLength(0); i++)
                {
                    for (int j = 0; j < model.Xmatr.GetLength(1); j++)
                    {
                        var bufTextView = FindViewById<TextView>(
                            Convert.ToInt32(
                                typeof(Resource.Id).InvokeMember(
                                    (("tableRow" + (i + 1) + "Col" + j)), BindingFlags.GetField, null, null, null)));
                        if (model.Xmatr[i, j] % 1 != 0)
                        {
                            bufTextView.Text = string.Format("{0:F2}", model.Xmatr[i, j]);
                        }
                        else
                        {
                            bufTextView.Text = string.Format("{0:F1}", model.Xmatr[i, j]);
                        }
                    }
                }
                for (int i = 0; i < model.Ymatr.GetLength(0); i++)
                {
                    for (int j = 0; j < model.Ymatr.GetLength(1); j++)
                    {
                        var bufTextView = FindViewById<TextView>(
                            Convert.ToInt32(
                                typeof(Resource.Id).InvokeMember(
                                    (("tableRow" + (i + 1) + "Col" + (j + 2))), BindingFlags.GetField, null, null, null)));
                        if (model.Ymatr[i, j] % 1 != 0)
                        {
                            bufTextView.Text = string.Format("{0:F2}", model.Ymatr[i, j]);
                        }
                        else
                        {
                            bufTextView.Text = string.Format("{0:F1}", model.Ymatr[i, j]);
                        }
                    }
                }
                for (int i = 0; i < model.Ym.Length; i++)
                {
                    var bufTextView = FindViewById<TextView>(
                        Convert.ToInt32(
                            typeof(Resource.Id).InvokeMember(
                                (("tableRow" + (i + 1) + "Col7")), BindingFlags.GetField, null, null, null)));
                    if (model.Ym[i] % 1 != 0)
                    {
                        bufTextView.Text = string.Format("{0:F2}", model.Ym[i]);
                    }
                    else
                    {
                        bufTextView.Text = string.Format("{0:F1}", model.Ym[i]);
                    }
                }
                FindViewById<TextView>(Resource.Id.normalTableRow1).Text = "Y = " + string.Format("{0:F2}", model.B[0]) +" + "+ string.Format("{0:F2}", model.B[1]) + "*x1" + string.Format("{0:F2}", model.B[2]) + "*x2";
                FindViewById<TextView>(Resource.Id.naturalTableRow1).Text = "Y = " + string.Format("{0:F2}", model.Alpha[0]) + " + " + string.Format("{0:F2}", model.Alpha[1]) + "*x1" + string.Format("{0:F2}", model.Alpha[2]) + "*x2";
                FindViewById<TextView>(Resource.Id.dispersionTableRow1).Text = model.Dispersion;
                
            }; 
        }
    }
}

