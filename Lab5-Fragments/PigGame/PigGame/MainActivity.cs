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
using Android.Content;

namespace PigGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true,  WindowSoftInputMode = Android.Views.SoftInput.AdjustResize)]
    public class MainActivity : Activity
    {


        Button newGameButtonFront;

        // TextView Player1Label;
        //  TextView Player2Label;
   
            EditText Player1Name;
            EditText Player2Name;
     
       // EditText Player1Score;
       // EditText Player2Score;
      //  EditText DisplayTurnText;
       // EditText PointsRoundText;
      //  ImageView DieImage;
      //  Game Game;
        const string DATA_KEY = "data";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
          //  this.Game = new Game();
            Player1Name = FindViewById<EditText>(Resource.Id.player1name);
            Player2Name = FindViewById<EditText>(Resource.Id.player2name);
            newGameButtonFront = FindViewById<Button>(Resource.Id.newGameButtonFront);

            newGameButtonFront.Click += delegate
            {
                this.LoadSecondActivity();
            };

        }
        void LoadSecondActivity()
        {
            // Lets Detect he secondForm
            Button button = FindViewById<Button>(Resource.Id.endTurnButton);
            if(button != null)
            {
                // REALLY WET - BUT THE DRY WAY DIDNT WORK...
                // Lets map our fields
               // this.Player1Label = FindViewById<TextView>(Resource.Id.player1Label);
             //   this.Player2Label = FindViewById<TextView>(Resource.Id.player2Label);
                this.Player1Name = FindViewById<EditText>(Resource.Id.player1name);
                this.Player2Name = FindViewById<EditText>(Resource.Id.player2name);
               // this.Player1Score = FindViewById<EditText>(Resource.Id.player1Score);
              //  this.Player2Score = FindViewById<EditText>(Resource.Id.player2Score);
               // this.DisplayTurnText = FindViewById<EditText>(Resource.Id.displayTurnText);
             //   this.PointsRoundText = FindViewById<EditText>(Resource.Id.pointsRoundText);
              //  this.DieImage = FindViewById<ImageView>(Resource.Id.dieImage);
                // Image Field

                // Lets bind our buttons
             /*   Button rollButton = FindViewById<Button>(Resource.Id.rollButton);
                rollButton.Click += delegate
                {
                    this.Game.Roll();
                    this.UpdateUI();
                };

                Button endTurnButton = FindViewById<Button>(Resource.Id.endTurnButton);
                endTurnButton.Click += delegate
                {
                    this.Game.EndTurn();
                    this.UpdateUI();
                };

                Button newGameButton = FindViewById<Button>(Resource.Id.newGameButton);
                newGameButton.Click += delegate
                {
                    this.Game = new Game();
                    this.UpdateUI();
                    
                };

                // new game
                this.Game = new Game();
                */
            }
            else
            {
                Intent intent = new Intent(this, typeof(SecondScreen));
              //  string json = JsonConvert.SerializeObject(this.Game, Formatting.Indented);
                //Bundle savedInstanceState = new Bundle();
                // savedInstanceState.PutString(DATA_KEY, json);
                intent.PutExtra("player1Name", this.Player1Name.Text);
                intent.PutExtra("player2Name", this.Player2Name.Text);
                this.StartActivity(intent);

            }
            
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
           
            base.OnSaveInstanceState(outState);
        }
      /*  void UpdateUI()
        {
            // Text
            this.Player1Label.Text = this.Game.PlayerOneIndicator();
            this.Player2Label.Text = this.Game.PlayerTwoIndicator();
            // If its ghetto grab it from Game
            if (this.Player1Name.Text == "Player1")
            {
                this.Player1Name.Text = this.Game.player1.Name;
            }
            else
            {
                // update it
                this.Game.player1.Name = this.Player1Name.Text;
            }
            if (this.Player2Name.Text == "Player2")
            {
                this.Player2Name.Text = this.Game.player2.Name;
            }
            else
            {
                // update it
                this.Game.player2.Name = this.Player2Name.Text;
            }

            this.Player2Name.Text = this.Game.player2.Name;
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
        }
*/

    }
}