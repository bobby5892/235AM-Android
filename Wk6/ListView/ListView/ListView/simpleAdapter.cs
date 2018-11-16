using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ListViewProject
{
    public class TideAdapter : SimpleAdapter, ISectionIndexer
    {
        List<IDictionary<string, object>> dataList;
        String[] sections;
        Java.Lang.Object[] sectionsObjects;
        Dictionary<string, int> tideDataIndex;

        public TideAdapter(Context context,
            List<IDictionary<string, object>> data,
            Int32 resource, String[] from,
            Int32[] to) : base(context, data, resource, from, to)
        {
            dataList = data;
            // Sort list by Part Of Speech (pos) field
          //  dataList.Sort((x, y) => String.Compare((string)x[XmlVocabFileParser.POS], (string)y[XmlVocabFileParser.POS]));
            BuildSectionIndex();
        }

        // ---- Implementation of ISectionIndexer  -------

        public int GetPositionForSection(int section)
        {
            return tideDataIndex[sections[section]];
        }

        public int GetSectionForPosition(int position)
        {
            return 1;
        }

        public Java.Lang.Object[] GetSections()
        {
            return sectionsObjects;
        }

        private void BuildSectionIndex()
        {
            Regex monthParse = new Regex(@"^[0-9]*\/(.[0-9]*)\/.[0-9]$");
            tideDataIndex = new Dictionary<string, int>();
            for(var i = 0; i < Count; i++)
            {
                var key = (string)dataList[i][XmlTideFileParser.DATE];
                Match match = monthParse.Match(key);
                if (!tideDataIndex.ContainsKey(match.Groups[1].Value))
                {
                    tideDataIndex.Add(match.Groups[1].Value, i);
                }
            }
            sections = new string[tideDataIndex.Keys.Count];

            tideDataIndex.Keys.CopyTo(sections, 0);

            sectionsObjects = new Java.Lang.Object[sections.Length];
            for(var i=0; i < sections.Length; i++)
            {
                sectionsObjects[i] = new Java.Lang.String(sections[i]);
            }
          }

    }
}