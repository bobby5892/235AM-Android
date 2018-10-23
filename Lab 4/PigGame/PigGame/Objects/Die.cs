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
    public class Die
    {
        int currentValue;
        /// <summary>
        ///  Property to Access the curentValue
        /// </summary>
        public int Value {
            get { return currentValue; }
        }
        private readonly int sides = 6;

        public Die()
        {
            this.Roll();
        }
        /// <summary>
        /// Overload - Lets you set how many sies
        /// </summary>
        /// <param name="sides">Number of Sides</param>
        public Die(int sides)
        {
            this.sides = sides;
            this.Roll();
        }
        public void Roll()
        {
            Random random = new Random();
            this.currentValue = random.Next(1, this.sides + 1);
        }
        public string Picture()
        {
            string fileName = "Die8Side"+ currentValue;
            if (this.currentValue > 8) {
                fileName = "Die8Side1";
            }
            return fileName;
        }
    }
}