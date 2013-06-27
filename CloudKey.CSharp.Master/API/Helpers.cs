//-----------------------------------------------------------------------
// <copyright file="Helpers.cs" company="Netquarks">
//     Netquarks : All rights reserved 2013.
// </copyright>
//-----------------------------------------------------------------------

namespace DMCloud.CSharp.Master.API
{
    #region using
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    #endregion

    /// <summary>
    /// class helper
    /// </summary>
    public class Helpers
    {
        #region public methods

        /// <summary>
        /// Create a WebRequest connection
        /// </summary>
        /// <param name="targetURL"></param>
        /// <param name="urlParameters"></param>
        /// <returns></returns>
        public static  string Curl(string targetURL, string urlParameters)
        {
            Uri url = new Uri(targetURL);
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(urlParameters);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            string ss = (((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

        public static string Normalize(Dictionary<Object, Object> data)
	    {
		    string normalize = "";
            foreach (var item in data)
            {
                normalize += item.Key;
                normalize += Normalizing(item.Value);
            }
		    return normalize;
	    }

        public static string Normalize(ArrayList data)
	    {
		    string normalize = "";
            foreach (var item in data)
	        {
		        normalize += Normalizing(item);
	        }
		    return normalize;
	    }

	    private static string Normalizing(Object o)
	    {
		    string normalize = "";
            if (o is Dictionary<string, string>)
	        {
                normalize = Normalize((Dictionary<Object, Object>)o);
	        }
            else if (o is ArrayList)
	        {
		        normalize = Normalize((ArrayList)o);
	        }
            else
	        {
                normalize = o.ToString();
	        }
		    
		    return normalize;
	    }

        /// <summary>
        /// Encode a string with MD5
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static  string Md5(string password)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// Sign the URL of the uploaded file
        /// </summary>
        /// <param name="url"></param>
        /// <param name="secret"></param>
        /// <param name="seclevel"></param>
        /// <param name="asnum"></param>
        /// <param name="ip"></param>
        /// <param name="useragent"></param>
        /// <param name="countries"></param>
        /// <param name="referers"></param>
        /// <param name="expires">temps d'expiration </param>
        /// <returns></returns>
        public static  string SignUrl(string url, string secret, int seclevel, string asnum, string ip, string useragent, List<string> countries, string[] referers, int expires)
        {
            Int64 time;
            DateTime st = new DateTime(1970, 1, 1);
            TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
            time = (Int64)t.TotalMilliseconds;
            int time2 = (int)((time / 1000) + 745600000);
            expires = (expires == 0) ? time2 : expires;
            expires = Math.Abs(expires);
            string[] tokens = Regex.Split(url, "\\?");
            string query = "";
            if (tokens.Length > 1)
            {
                url = tokens[0];
                query = tokens[1];
            }
            string secparams = "";
            List<string> public_secparams = new List<string>();
            string public_secparams_encoded = "";
            string rand = "";
            string letters = "abcdefghijklmnopqrstuvwxyz0123456789";
            for (int i = 0; i < 8; i++)
            {
                Random r = new Random();
                double d1 = r.NextDouble();
                double d2 = d1 * 35;
                int index = (int)Math.Round(d2);
                string s = letters.Substring(index, 1);
                rand += s;
            }
            string digest = Md5(seclevel + url + expires + rand + secret + secparams + public_secparams_encoded);
            string ss = url + "?" + (!query.Equals("") ? query + '&' : "") + "auth=" + expires + "-" + seclevel + "-" + rand + "-" + digest + (public_secparams_encoded != "" ? "-" + public_secparams_encoded : "");
            return ss;
        }

        #endregion
    }
}
