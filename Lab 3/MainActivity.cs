using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Newtonsoft.Json;

namespace Lab3
{
    [Activity(Label = "Lab3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        QuoteBank quoteCollection;
        TextView quotationTextView;
        const string DATA_KEY = "data";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            quoteCollection = new QuoteBank();
            // Lets see if a quote collection exists
            if (savedInstanceState != null)
            {
                string json = savedInstanceState.GetString(DATA_KEY);
                if (json.Length == 0)
                {
                    quoteCollection = (QuoteBank)JsonConvert.DeserializeObject(json);
                }
            }
            // Create the quote collection and display the current quote
            quoteCollection.LoadQuotes();
            quoteCollection.GetNextQuote();

            quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);
            quotationTextView.Text = quoteCollection.CurrentQuote.Quotation + "\n  By:"+
                quoteCollection.CurrentQuote.Person;


            // Display another quote when the "Next Quote" button is tapped
            var nextButton = FindViewById<Button>(Resource.Id.nextButton);
            nextButton.Click += delegate {
                quoteCollection.GetNextQuote();
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation + "\n  By:" +
                quoteCollection.CurrentQuote.Person; 
            };

            EditText newQuoteInput = FindViewById<EditText>(Resource.Id.newQuoteText);
            EditText newQuoteByInput = FindViewById<EditText>(Resource.Id.newQuoteByText);

            // Add Quote Button
            Button addQuoteButton = FindViewById<Button>(Resource.Id.addQuoteButton);
            addQuoteButton.Click += delegate {
                bool error = false;
                if(newQuoteInput.Text.Length == 0)
                {
                    error = true;
                    Toast.MakeText(this, "You must enter a quote", ToastLength.Long).Show();
                }
                if(newQuoteByInput.Text.Length == 0)
                {
                    error = true;
                    Toast.MakeText(this, "You must enter a by line", ToastLength.Long).Show();
                }
                if (!error)
                {
                    // Add it to the quote collection
                    quoteCollection.Quotes.Add(new Quote()
                    {
                        Person = newQuoteByInput.Text,
                        Quotation = newQuoteInput.Text
                    });
                    // Clear Boxes
                    newQuoteByInput.Text = "";
                    newQuoteInput.Text = "";

                    Toast.MakeText(this, "Added quote to collection", ToastLength.Short).Show();
                }
               
             
            };
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {

            var json = JsonConvert.SerializeObject(this.quoteCollection);
          

            outState.PutString(DATA_KEY, json);
            Log.Debug(GetType().FullName, "Activity A - Saving instance state");

            // always call the base implementation!
            base.OnSaveInstanceState(outState);
        }
    }
}

