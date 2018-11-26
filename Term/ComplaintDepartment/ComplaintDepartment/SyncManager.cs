using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ComplaintDepartment
{
    public class SyncManager
    {
        public bool HasInternet { get { return this.hasInternet; } }
        private bool hasInternet;

        public SyncManager()
        {
            hasInternet = CheckForInternetConnection();
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
                return false;
            }
        }
        /// <summary>
        /// Relative Path on Host to get Json from
        /// </summary>
        /// <param name="path">Example: /Complaint/</param>
        /// <returns></returns>
        public string GetJson(string path)
        {
            string url = Application.Context.GetString(Resource.String.base_url_for_api) + path;
           
            try
            {
                using (var client = new WebClient())
                    return client.DownloadString(url);
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Post Form To Web
        /// </summary>
        /// <param name="path">Relative Path</param>
        /// <param name="options">Parameters - Example name=bob&location=up</param>
        /// <returns></returns>
        public string PostWeb(string path, string options)
        {
            string url = Application.Context.GetString(Resource.String.base_url_for_api) + path;
            

            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string HtmlResult = client.UploadString(url, options);
                return HtmlResult;
            }
        }
    }
}