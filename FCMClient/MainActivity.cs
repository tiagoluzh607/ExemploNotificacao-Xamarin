using Android.App;
using Android.Widget;
using Android.OS;

using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Android.Gms.Common;

namespace FCMClient
{
    [Activity(Label = "FCMClient", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView msgText;
        const string TAG = "MainActivity";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            

            //Verifica se o google play service esta ativado e exibe no text view
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            msgText = FindViewById<TextView>(Resource.Id.msgText);

            //Quando o usuario clica em cima da notificação esse código pega oque estava escrito na notificacao e exibe no console
            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                }
            }

            IsPlayServicesAvailable();

            //Mostra o Tolken qundo o botão é clicado
            var logTokenButton = FindViewById<Button>(Resource.Id.logTokenButton);
            logTokenButton.Click += delegate {
                Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
            };
        }

        //Verifica se o play services esta instalado
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }
    }
}

