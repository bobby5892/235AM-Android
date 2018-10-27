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
using Newtonsoft.Json;

namespace PigGame.Objects
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Player
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Score")]
        public int Score { get; set; }
        [JsonProperty("FinalRollComplete")]
        public bool FinalRollComplete { get; set; }
    }
}