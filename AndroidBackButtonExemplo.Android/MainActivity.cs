using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AndroidBackButtonExemplo.Droid
{
    [Activity(Label = "AndroidBackButtonExemplo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //Confirmação se o App vai Fechar via Modal
        public override void OnBackPressed()
        {
            if (((AndroidBackButtonExemplo.App)App.Current).ConfirmaFecharApp)
            {
                using (var alert = new AlertDialog.Builder(this))
                {
                    alert.SetTitle("Vou fechar em");
                    alert.SetMessage("Tem certeza que deseja sair do APP?");
                    alert.SetPositiveButton("Sim", (sender, args) => { FinishAffinity(); });
                    alert.SetNegativeButton("Não", (sender, args) => { });

                    var dialog = alert.Create();
                    dialog.Show();
                }
                return;
            }
            base.OnBackPressed();
        }

        //Confirmação se o App vai Fechar via Toast
        //private bool _isBackPressed;
        //public override void OnBackPressed()
        //{
        //    if (((AndroidBackButtonExemplo.App)App.Current).ConfirmaFecharApp)
        //    {
        //        using (var alert = new AlertDialog.Builder(this))
        //        {
        //            Android.Widget.Toast.MakeText(this, "Pressione mais uma vez para fechar", Android.Widget.ToastLength.Short).Show();

        //            // O Alerta vai sumir em 2 segundos
        //            new Handler().PostDelayed(() =>
        //            {
        //                _isBackPressed = false;
        //            }, 2000);
        //        }
        //        return;
        //    }
        //    base.OnBackPressed();
        //}
    }
}