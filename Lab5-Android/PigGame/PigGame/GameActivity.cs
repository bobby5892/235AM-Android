using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PigGame.Objects;
using Newtonsoft.Json;

namespace PigGame
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        public Game Game;
        //Used to save state
        const string DATA_KEY = "data";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.GameActivity);
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