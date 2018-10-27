using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using PigGame.Objects;
using Java.IO;
using Android.Graphics.Drawables;
using Newtonsoft.Json;
using Android.Util;

namespace PigGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
       
        public Game Game;
        //Used to save state
        const string DATA_KEY = "data";
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
          
          // Lets Create The Game
            Game = new Game();
            // Deal with State
            if (savedInstanceState != null)
             {
                 string json = savedInstanceState.GetString(DATA_KEY);
                 if (json.Length != 0)
                 {
                     Game = JsonConvert.DeserializeObject<Game>(json);                   
                }
             }
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            string json = JsonConvert.SerializeObject(this.Game, Formatting.Indented);
            outState.PutString(DATA_KEY, json);
            base.OnSaveInstanceState(outState);
        }
    }
}