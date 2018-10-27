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
    public class GameFragment : Fragment
    {
        const string DATA_KEY = "data";
        Game Game;
        EditText Player1Score;
        EditText Player2Score;
        EditText DisplayTurnText;
        EditText PointsRoundText;
        ImageView DieImage;

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
            // Use this to return your custom view for this Fragment
            View rootView = inflater.Inflate(Resource.Layout.GameFragment, container, false);

            // Bind things
            this.Player1Score = rootView.FindViewById<EditText>(Resource.Id.player1Score);
            this.Player2Score = rootView.FindViewById<EditText>(Resource.Id.player2Score);
            this.DisplayTurnText = rootView.FindViewById<EditText>(Resource.Id.displayTurnText);
            this.PointsRoundText = rootView.FindViewById<EditText>(Resource.Id.pointsRoundText);
            this.DieImage = rootView.FindViewById<ImageView>(Resource.Id.dieImage);

            Button rollButton = rootView.FindViewById<Button>(Resource.Id.rollButton);
            rollButton.Click += delegate
            {
                this.Game.Roll();
                this.UpdateUI();
            };

            Button endTurnButton = rootView.FindViewById<Button>(Resource.Id.endTurnButton);
            endTurnButton.Click += delegate
            {
                this.Game.EndTurn();
                this.UpdateUI();
            };

            Button newGameButton = rootView.FindViewById<Button>(Resource.Id.newGameButton);
            newGameButton.Click += delegate
            {
                this.Game = new Game();
                this.UpdateUI();
            };

            return rootView;

           // return base.OnCreateView(inflater, container, savedInstanceState);
        }
        void UpdateUI()
        {
            this.DisplayTurnText.Text = this.Game.CurrentPlayer();
            this.PointsRoundText.Text = this.Game.currentRound.ToString();

            //Image Swap Drawable icon = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.Icon, null);
            if (this.Game.die.Picture() == "Die8Side1") { this.DieImage.SetImageResource(Resource.Drawable.Die8Side1); }
            else if (this.Game.die.Picture() == "Die8Side2") { this.DieImage.SetImageResource(Resource.Drawable.Die8Side2); }
            else if (this.Game.die.Picture() == "Die8Side3") { this.DieImage.SetImageResource(Resource.Drawable.Die8Side3); }
            else if (this.Game.die.Picture() == "Die8Side4") { this.DieImage.SetImageResource(Resource.Drawable.Die8Side4); }
            else if (this.Game.die.Picture() == "Die8Side5") { this.DieImage.SetImageResource(Resource.Drawable.Die8Side5); }
            else if (this.Game.die.Picture() == "Die8Side6") { this.DieImage.SetImageResource(Resource.Drawable.Die8Side6); }
            else if (this.Game.die.Picture() == "Die8Side7") { this.DieImage.SetImageResource(Resource.Drawable.Die8Side7); }
            else if (this.Game.die.Picture() == "Die8Side8") { this.DieImage.SetImageResource(Resource.Drawable.Die8Side8); }

            // Check if game over
            if (this.Game.gameOver)
            {
                this.DisplayTurnText.Text = this.Game.winner.Name + " wins!";
                this.PointsRoundText.Text = this.Game.winner.Name + " wins!";
            }
        }
    }
}