using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DMCloud.CSharp.Master.API;
using DMCloud.CSharp.Master.Tools;
using System.Collections;

namespace CloudKey.CSharp.UnitTest
{
    [TestClass]
    public class UnitTest
    {

        public static string USER_ID = System.Configuration.ConfigurationManager.AppSettings["USER_ID"].ToString();
        public static string API_KEY = System.Configuration.ConfigurationManager.AppSettings["API_KEY"].ToString();
        public static string VIDEO_ID = System.Configuration.ConfigurationManager.AppSettings["VIDEO_ID"].ToString();

        [TestMethod]
        public void TestCloudKey_Normalize()
        {
            Assert.AreEqual 
            (
                Helpers.Normalize
                (
                    DCArray.Create()
                           .Push("foo")
                           .Push(42)
                           .Push("bar")
                ), "foo42bar"
            );

            Assert.AreEqual 
            (
                Helpers.Normalize
                (
                    DCObject.Create()
                            .Push("yellow", 1)
                            .Push("red", 2)
                            .Push("pink", 3)
                ), "yellow1red2pink3"
            );
        }

        [TestMethod]
        public void TestCloudKey_getEmbedUrl()
        {
            try
            {   
                DMCloud.CSharp.Master.API.CloudKey cloud = new DMCloud.CSharp.Master.API.CloudKey(USER_ID, API_KEY);
                String[] referers = { "http://test.dmcloud.net" };
                cloud.MediaGetEmbedUrl(DMCloud.CSharp.Master.API.CloudKey.API_URL, VIDEO_ID, DMCloud.CSharp.Master.API.CloudKey.SECLEVEL_REFERER, "", "", "", null, referers, 0);
            }
            catch (Exception e)
            {
                e.GetBaseException();
            }
        }
    }
}
