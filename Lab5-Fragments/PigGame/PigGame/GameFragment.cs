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
namespace PigGame
{
    public class GameFragment : Fragment
    {
      
         TextView Player1Label;
         TextView Player2Label;
         EditText Player1Name;
         EditText Player2Name;
         EditText Player1Score;
         EditText Player2Score;
         EditText DisplayTurnText;
         EditText PointsRoundText;
         ImageView DieImage;
        Game Game;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

          
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.Game = new Game();
            this.Game.player1.Name = Activity.Intent.GetStringExtra("player1Name") ?? "Player1";
            this.Game.player2.Name =  Activity.Intent.GetStringExtra("player1Name") ?? "Player2";


            // Use this to return your custom view for this Fragment
            View rootview = inflater.Inflate(Resource.Layout.GameFragment, container, false); 
            // Lets map our fields
            this.Player1Label = rootview.FindViewById<TextView>(Resource.Id.player1Label);
            this.Player2Label = rootview.FindViewById<TextView>(Resource.Id.player2Label);
            this.Player1Name = rootview.FindViewById<EditText>(Resource.Id.player1name);
            this.Player2Name = rootview.FindViewById<EditText>(Resource.Id.player2name);
            this.Player1Score = rootview.FindViewById<EditText>(Resource.Id.player1Score);
            this.Player2Score = rootview.FindViewById<EditText>(Resource.Id.player2Score);
            this.DisplayTurnText = rootview.FindViewById<EditText>(Resource.Id.displayTurnText);
            this.PointsRoundText = rootview.FindViewById<EditText>(Resource.Id.pointsRoundText);
            this.DieImage = rootview.FindViewById<ImageView>(Resource.Id.dieImage);
            // Image Field

            // Lets bind our buttons
            Button rollButton = rootview.FindViewById<Button>(Resource.Id.rollButton);
            rollButton.Click += delegate
            {
                //((FrontActivity)Activity).ShowNewProblem ();
                // ((SecondScreen)Activity).Game.Roll();
                this.Game.Roll();
                this.UpdateUI();
            };

            Button endTurnButton = rootview.FindViewById<Button>(Resource.Id.endTurnButton);
            endTurnButton.Click += delegate
            {
                // ((SecondScreen)Activity).Game.EndTurn();
                this.Game.EndTurn();
                this.UpdateUI();
            };

            Button newGameButton = rootview.FindViewById<Button>(Resource.Id.newGameButton);
            newGameButton.Click += delegate
            {
                // ((SecondScreen)Activity).Game = new Game();
                this.Game = new Game();
                this.UpdateUI();
            };
            return rootview;
        }
        /// <summary>
        /// This method updates all the text elements of the UI
        /// </summary>
        void UpdateUI()
        {
  
            this.Player1Score.Text = this.Game.player1.Score.ToString();
            this.Player2Score.Text = this.Game.player2.Score.ToString();
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

           

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}