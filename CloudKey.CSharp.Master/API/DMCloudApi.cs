//-----------------------------------------------------------------------
// <copyright file="DMCloudApi.cs" company="Netquarks">
//     Netquarks : All rights reserved 2013.
// </copyright>
//-----------------------------------------------------------------------


namespace DMCloud.CSharp.Master.API
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DMCloud.CSharp.Master.Tools;
    using System.Web.Script.Serialization;

    #endregion

    public class DMCloudApi
    {
        #region static

        public static int SECLEVEL_NONE = 0;
        public static int SECLEVEL_DELEGATE = 1 << 0;
        public static int SECLEVEL_ASNUM = 1 << 1;
        public static int SECLEVEL_IP = 1 << 2;
        public static int SECLEVEL_USERAGENT = 1 << 3;
        public static int SECLEVEL_USEONCE = 1 << 4;
        public static int SECLEVEL_COUNTRY = 1 << 5;
        public static int SECLEVEL_REFERER = 1 << 6;

        public static string API_URL = System.Configuration.ConfigurationManager.AppSettings["API_URL"].ToString();
        public static string CDN_URL = System.Configuration.ConfigurationManager.AppSettings["CDN_URL"].ToString();
        public static string STATIC_URL = System.Configuration.ConfigurationManager.AppSettings["STATIC_URL"].ToString();

        #endregion

        #region fileds
        protected string user_id = null;
        protected string api_key = null;
        protected string base_url = null;
        protected string cdn_url = null;
        protected string proxy = null;

        #endregion

        #region ctor

        public DMCloudApi(string user_id, string api_key, string base_url)
        {
            this.user_id = user_id;
            this.api_key = api_key;
            this.base_url = CloudKey.API_URL;
            this.cdn_url = CloudKey.CDN_URL;
            this.proxy = "";
        }

        public DMCloudApi(string _user_id, string _api_key, string _base_url, string _cdn_url, string _proxy)
        {
            this.user_id = _user_id;
            this.api_key = _api_key;
            this.base_url = _base_url;
            this.cdn_url = _cdn_url;
            this.proxy = _proxy;
        }

        #endregion

        #region protected methods

        /// <summary>
        /// Send a JSON request in order to execute the function "file.upload"
        /// </summary>
        /// <param name="call"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DCObject Call(string call, DCObject args)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DCObject json_response = null;
            try
            {
                DCObject jo = DCObject.Create();
                jo.Push("call", call);
                jo.Push("args", args);
                string normalize = "args" + "jsonp_cb" + args["jsonp_cb"] + "status" + "true"
                    + "target" + args["target"] + "call" + jo["call"];
                string token2 = Helpers.Md5(normalize);
                string token = Helpers.Md5(this.user_id + normalize + this.api_key);
                jo.Push("auth", this.user_id + ":" + token); 
                string json_encoded = serializer.Serialize((object)jo); ;
                string response = Helpers.Curl(this.base_url + "/api", json_encoded);
                JavaScriptSerializer deserializer = new JavaScriptSerializer();
                Dictionary<object, Dictionary<object, object>> deserializedDictionary1 =
                    (Dictionary<object, Dictionary<object, object>>)deserializer.Deserialize(response, typeof(Dictionary<object, Dictionary<object, object>>));
                json_response = DCObject.Create();
                json_response.Push("status", (deserializedDictionary1["result"])["status"]);
                json_response.Push("url", deserializedDictionary1["result"]["url"]);
            }
            catch (Exception)
            {
                throw;
            }
            return json_response;
        }

        /// <summary>
        /// Send a JSON request in order to execute the function "media.create"
        /// </summary>
        /// <param name="call"></param>
        /// <param name="args"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected DCObject Call(string call, DCObject args, string name)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DCObject json_response = null;
            try
            {
                DCObject jo = DCObject.Create();
                jo.Push("call", call);
                jo.Push("args", args);
                string normalize = "args" + "meta" + "title" + name + "url" + args["url"] + "call" + jo["call"];
                string token2 = Helpers.Md5(normalize);
                string token = Helpers.Md5(this.user_id + normalize + this.api_key);
                jo.Push("auth", this.user_id + ":" + token); 
                string json_encoded = serializer.Serialize((object)jo); ;
                string response = Helpers.Curl(this.base_url + "/api", json_encoded);
                JavaScriptSerializer deserializer = new JavaScriptSerializer();
                Dictionary<object, Dictionary<object, object>> deserializedDictionary1 =
                    (Dictionary<object, Dictionary<object, object>>)deserializer.Deserialize(response, typeof(Dictionary<object, Dictionary<object, object>>));
                json_response = DCObject.Create();
                json_response.Push("id", (deserializedDictionary1["result"])["id"]);
            }
            catch (Exception)
            {
                throw;
            }
            return json_response;
        }

        #endregion

    }
}
