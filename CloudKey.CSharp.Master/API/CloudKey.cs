//-----------------------------------------------------------------------
// <copyright file="CloudKey.cs" company="Netquarks">
//     Netquarks : All rights reserved 2013.
// </copyright>
//-----------------------------------------------------------------------

namespace DMCloud.CSharp.Master.API
{
    #region using
    using DMCloud.CSharp.Master.Tools;
    using System;
    using System.Collections.Generic;
    #endregion

    /// <summary>
    /// the Main class to acces to the API
    /// </summary>
    public class CloudKey : DMCloudApi
    {
        #region Ctor
        
        public CloudKey(string userId, string apiKey, string baseUrl, string cdnUrl, string proxy) :
            base(userId, apiKey, baseUrl, cdnUrl, proxy)
        { }

        public CloudKey(string userId, string apiKey) :
            base(userId, apiKey, CloudKey.API_URL, CloudKey.CDN_URL, "")
        { }

        #endregion

        #region Public methods

        /// <summary>
        /// Upload a video file
        /// </summary>
        /// <param name="status"></param>
        /// <param name="jsonpCb"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public DCObject FileUpload(Boolean status, string jsonpCb, string target)
        {
            DCObject args = DCObject.Create();
            if (status)
            {
                args.Push("status", true);
            }

            if (!target.Equals(""))
            {
                args.Push("target", target);
            }
            if (!jsonpCb.Equals(""))
            {
                args.Push("jsonp_cb", jsonpCb);
            }
            return (DCObject)this.Call("file.upload", args);
        }

        /// <summary>
        /// Create a media
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string MediaCreate(string url, string name)
        {
            DCObject urlArgs = DCObject.Create().Push("url", url);
            DCObject titleargs = DCObject.Create().Push("title", name);

            urlArgs.Push("meta", titleargs);

            DCObject result = this.Call("media.create", urlArgs, name);
            return result.Pull("id");
        }

        /// <summary>
        /// Get the URL of the uploaded file
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expires"></param>
        /// <returns></returns>
        public string MediaGetEmbedUrl(string id)
        {
            return this.MediaGetEmbedUrl(CloudKey.API_URL, id, CloudKey.SECLEVEL_NONE, "", "", "", null, null, 0);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sign the URL of the uploaded file
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <param name="seclevel"></param>
        /// <param name="asnum"></param>
        /// <param name="ip"></param>
        /// <param name="useragent"></param>
        /// <param name="countries"></param>
        /// <param name="referers"></param>
        /// <param name="expires"></param>
        /// <returns></returns>
        public string MediaGetEmbedUrl(string url, string id, int seclevel, string asnum, string ip, string useragent, List<string> countries, string[] referers, int expires)
        {
            string _url = url + "/embed/" + this.user_id + "/" + id;
            return Helpers.SignUrl(_url, this.api_key, seclevel, asnum, ip, useragent, countries, referers, expires);
        }

        #endregion
    }
}
