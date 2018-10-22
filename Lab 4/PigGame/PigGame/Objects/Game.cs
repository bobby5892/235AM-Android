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

namespace PigGame.Objects
{
    public class Game
    {
        public Player currentPlayer;

        public Player player1;
        public Player player2;
        public Die die;
        public int currentRound;
        public bool canRoll;

        public Game() {
            /// Lets create all the peices.
             this.player1 = new Player() { Name = "Player 1",Score=0 };
             this.player2 = new Player() { Name = "Player 2", Score = 0 };

             this.die = new Die(8);

            this.currentPlayer = player1;

            this.currentRound = 0;
            this.canRoll = true;
            
        }
        /// <summary>
        /// Handles a players turn
        /// </summary>
        public void Roll()
        {
            if (canRoll)
            {
                this.die.Roll();
                this.currentRound += this.die.Value;
                if(this.die.Value == 8)
                {
                    this.canRoll = false;
                }
            }
        }
        public string PlayerOneIndicator()
        {
            if(currentPlayer.Name == player1.Name)
            {
                return "*" + player1.Name;
            }
            return "Player1";
        }
        public string PlayerTwoIndicator()
        {
            if (currentPlayer.Name == player2.Name)
            {
                return "*" + player2.Name;
            }
            return "Player2";
        }
        public void EndTurn()
        {
            // Unlock Die
            this.canRoll = true;


            // Swap Player && Reset Round and add score to player
            if (currentPlayer.Name == player1.Name)
            {
                player1.Score += this.currentRound;
                currentPlayer = player2;
            }
            else
            {
                player2.Score += this.currentRound;
                currentPlayer = player1;
            }

            this.currentRound = 0;
        }

        /// <summary>
        /// Return current Player
        /// </summary>
        /// <returns></returns>
        public string CurrentPlayer()
        {
            return currentPlayer.Name;
        }
        /// <summary>
        /// Grab the Image for te Die
        /// </summary>
        /// <returns></returns>
        public string DiceImage()
        {
            return this.die.Picture();
        }
    }
}