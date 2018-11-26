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


namespace ComplaintDepartment
{
    public class ComplaintAdapter : SimpleAdapter, ISectionIndexer
    {
        List<IDictionary<string, object>> data;
        String[] sections;
        Java.Lang.Object[] sectionsObjects;
        Dictionary<string, int> complaintIndex;

        public ComplaintAdapter(Context context, List<IDictionary<string,object>> data, int resource, string[] from, int[] to) : base(context,data,resource,from,to){
            this.data = data;

            // Create a new Index
            complaintIndex = new Dictionary<string, int>();
            // For each Item in the adapater
            for (var i = 0; i < Count; i++)
            {
                var key = (string)data[i]["Contents"];
                
                  if (!complaintIndex.ContainsKey(key))
                  {
                    complaintIndex.Add(key, i);
                }
              
            }
            sections = new string[complaintIndex.Keys.Count];

            complaintIndex.Keys.CopyTo(sections, 0);

            sectionsObjects = new Java.Lang.Object[sections.Length];
            for (var i = 0; i < sections.Length; i++)
            {
                sectionsObjects[i] = new Java.Lang.String(sections[i]);
            }
        }
        public int GetPositionForSection(int sectionIndex)
        {
            return complaintIndex[sections[sectionIndex]];
        }

        public int GetSectionForPosition(int position)
        {
            return 1;
        }

        public Java.Lang.Object[] GetSections()
        {
            return sectionsObjects;
        }
    }
}