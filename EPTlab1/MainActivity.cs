using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using System;
using System.Reflection;

namespace EPTlab1
{
    [Activity(Label = "Experiment Planning Theory - lab1", MainLauncher = true, Icon = "@drawable/icon", 
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
    ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var textLeftLimit = FindViewById<EditText>(Resource.Id.editTextLeftLimit);
            var textRightLimit = FindViewById<EditText>(Resource.Id.editTextRightLimit);
            var textA0 = FindViewById<EditText>(Resource.Id.editTextA0);
            var textA1 = FindViewById<EditText>(Resource.Id.editTextA1);
            var textA2 = FindViewById<EditText>(Resource.Id.editTextA2);
            var textA3 = FindViewById<EditText>(Resource.Id.editTextA3);

            var buttonCompute = FindViewById<Button>(Resource.Id.buttonCompute);

            buttonCompute.Click += (e, o) =>
            {
                double leftLimit = Convert.ToDouble(textLeftLimit.Text);
                double rightLimit = Convert.ToDouble(textRightLimit.Text);
                double a0 = Convert.ToDouble(textA0.Text);
                double a1 = Convert.ToDouble(textA1.Text);
                double a2 = Convert.ToDouble(textA2.Text);
                double a3 = Convert.ToDouble(textA3.Text);

                Model model = new Model(leftLimit, rightLimit, a0, a1, a2, a3);
                double[,] matrix = model.MatrixPlan;

                //Matrix plan
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        var bufTextView = FindViewById<TextView>(
                            Convert.ToInt32(
                                typeof(Resource.Id).InvokeMember(
                                    ("tableRow"+(i+2)+"Col"+(j+2)), BindingFlags.GetField, null, null, null)));
                        if (matrix[i, j]%1!=0)
                        {
                            bufTextView.Text = string.Format("{0:F3}", matrix[i, j]);
                        }else
                        {
                            bufTextView.Text = string.Format("{0:F1}", matrix[i, j]);
                        }
                    }
                }

                //Standard
                for (int i = 0; i < 4; i++)
                {
                    var bufTextView = FindViewById<TextView>(
                            Convert.ToInt32(
                                typeof(Resource.Id).InvokeMember(
                                    ("tableStandardRow2Col" + (i + 1)), BindingFlags.GetField, null, null, null)));
                    if (model.Standard[i] % 1 != 0)
                    {
                        bufTextView.Text = string.Format("{0:F3}", model.Standard[i]);
                    }
                    else
                    {
                        bufTextView.Text = string.Format("{0:F2}", model.Standard[i]);
                    }
                }

                //Interval
                for (int i = 0; i < 3; i++)
                {
                    var bufTextView = FindViewById<TextView>(
                            Convert.ToInt32(
                                typeof(Resource.Id).InvokeMember(
                                    ("tableIntervalRow2Col" + (i + 1)), BindingFlags.GetField, null, null, null)));
                    if (model.Interval[i] % 1 != 0)
                    {
                        bufTextView.Text = string.Format("{0:F3}", model.Interval[i]);
                    }
                    else
                    {
                        bufTextView.Text = string.Format("{0:F2}", model.Interval[i]);
                    }
                }

                //Optimum
                for (int i = 0; i < 4; i++)
                {
                    var bufTextView = FindViewById<TextView>(
                            Convert.ToInt32(
                                typeof(Resource.Id).InvokeMember(
                                    ("tableOptimumRow2Col" + (i + 1)), BindingFlags.GetField, null, null, null)));
                    if (matrix[model.Optimum, i] % 1 != 0)
                    {
                        bufTextView.Text = string.Format("{0:F3}", matrix[model.Optimum, i]);
                    }
                    else
                    {
                        bufTextView.Text = string.Format("{0:F2}", matrix[model.Optimum, i]);
                    }
                }

            };

            
        }
    }
}

