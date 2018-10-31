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
    
    public class SecondScreen : Activity
    {
      
        //Used to save state
        const string DATA_KEY = "data";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SecondScreen);
            // Lets Create The Game
      
        /*    Game = JsonConvert.DeserializeObject<Game>(Intent.GetStringExtra(DATA_KEY));

            // Deal with State
            if (savedInstanceState != null)
            {
                string json = savedInstanceState.GetString(DATA_KEY);
                if (json.Length != 0)
                {
                    Game = JsonConvert.DeserializeObject<Game>(json);
                }
            }*/
         
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            // https://www.newtonsoft.com/json/help/html/PreserveObjectReferences.htm

            // https://stackoverflow.com/questions/43992407/json-net-serialize-nested-complex-dictionary-object
            //var json = JsonConvert.SerializeObject(this.Game);
           // string json = JsonConvert.SerializeObject(this.Game, Formatting.Indented);

           // outState.PutString(DATA_KEY, json);
          //  Log.Debug(GetType().FullName, "Saving instance state");

            // always call the base implementation!
            base.OnSaveInstanceState(outState);
        }


    }
}