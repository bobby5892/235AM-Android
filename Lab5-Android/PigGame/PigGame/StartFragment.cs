using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PigGame.Objects;
using Java.IO;
using Android.Graphics.Drawables;
using Newtonsoft.Json;

namespace PigGame
{
    public class StartFragment : Fragment
    {
        Game Game;
        TextView Player1Label;
        TextView Player2Label;
        EditText Player1Name;
        EditText Player2Name;
        const string DATA_KEY = "data";

        public override void OnCreate(Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
            {
                string json = savedInstanceState.GetString(DATA_KEY);
                if (json.Length != 0)
                {
                    Game = JsonConvert.DeserializeObject<Game>(json);
                }
            }
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.StartFragment, container, false);
            this.Player1Label = rootView.FindViewById<TextView>(Resource.Id.player1Label);
            this.Player2Label = rootView.FindViewById<TextView>(Resource.Id.player2Label);
            this.Player1Name = rootView.FindViewById<EditText>(Resource.Id.player1name);
            this.Player2Name = rootView.FindViewById<EditText>(Resource.Id.player2name);

       
            // Bind Button
            Button newGameButton = rootView.FindViewById<Button>(Resource.Id.newGameButton);
            newGameButton.Click += delegate
            {
                this.NewGame();
               
            };
            return rootView;
        }
        public void NewGame()
        {
            Game = new Game();
            // Save the Names

            Game.player1.Name = Player1Name.Text;
            Game.player2.Name = Player2Name.Text;

            Intent intent = new Intent(this.Activity, typeof(GameActivity));
            string json = JsonConvert.SerializeObject(this.Game, Formatting.Indented);
            Bundle savedInstanceState = new Bundle();
            savedInstanceState.PutString(DATA_KEY, json);
            this.Activity.StartActivity(intent, savedInstanceState);
        }
     
    }
}