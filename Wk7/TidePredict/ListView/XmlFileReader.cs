using System.Collections.Generic;
using Android.Runtime;
using System.Xml;
using System.IO;

namespace ListViewProject
{
    public class XmlTideFileParser
    {
        // constants to use for XML element names and dictionary keys
        public const string ITEM = "item";   // this element holds the ones below
        public const string DATE = "date";
        public const string DAY = "day";
        public const string TIME = "time";
        public const string HEIGHT = "pred_in_ft";
        public const string HI_LOW = "highlow";

        // This list will be filled with dictionary objects
        List<IDictionary<string, object>> tideList;   // backing variable for VocabList property

        public List<IDictionary<string, object>> TideList { get { return tideList; } }

        public XmlTideFileParser(Stream xmlStream)
        {
            // SimpleAdapter requires a list of JavaDictionary<string,object> objects
            tideList = new List<IDictionary<string, object>>();

            // Parse the xml file and fill the list of JavaDictionary objects with vocabulary data
            using (XmlReader reader = XmlReader.Create(xmlStream))
            {
                JavaDictionary<string, object> prediction = null;
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case ITEM:
                                // New word
                                prediction = new JavaDictionary<string, object>();
                                break;
                            case DATE:
                                // Add date
                                if (reader.Read() && prediction != null)
                                {
                                    prediction.Add(DATE, reader.Value.Trim());
                                }
                                break;
                            case DAY:
                                // Add day of the week
                                if (reader.Read() && prediction != null)
                                {
                                    prediction.Add(DAY, reader.Value.Trim());
                                }
                                break;
                            case TIME:
                                // Add time of day
                                if (reader.Read() && prediction != null)
                                {
                                    prediction.Add(TIME, reader.Value.Trim());
                                }
                                break;
                            case HEIGHT:
                                // Add tide height in inches
                                if (reader.Read() && prediction != null)
                                {
                                    prediction.Add(HEIGHT, reader.Value.Trim());
                                }
                                break;
                            case HI_LOW:
                                // Add H or L for high or low tied
                                if (reader.Read() && prediction != null)
                                {
                                    prediction.Add(HI_LOW, reader.Value.Trim());
                                }
                                break;
                        }
                    }
                    else if (reader.Name == ITEM)
                    {
                        // reached </item>
                        tideList.Add(prediction);
                        prediction = null;
                    }

                }
            }

        }
    }
}

/**** Sample XML ****

<item>
    <date>2018/01/01</date>
    <day>Mon</day>
    <time>12:16 AM</time>
    <pred_in_ft>6.08</pred_in_ft>
    <pred_in_cm>185</pred_in_cm>
    <highlow>H</highlow>
</item>

*/
