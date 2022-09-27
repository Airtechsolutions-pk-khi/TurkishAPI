using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace TurkishAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("api/push/android")]
        public void PushNotification()
        {
            try
            {
                //var applicationID = "AAAACf7C5VQ:APA91bFGYMRVBO7dya8OPZSgDLYmxQLsOCKqvLK0OuzQ4iNYpccSXYxpQWTHBE2T4RlIpC2hGXe5yvYU0UhgmiCnfkJ9_DtrCrNHu541FXHmHc4w7GqDv2Vv0k1CykXobhsUK7wKksyz";
                //var senderId = "42928891220"; 
                var applicationID = "AAAAzvc0NhY:APA91bG9ijRE-1EFIDAMLeI2Mwjpn-RE-8-JFwxoNhhL9-PIjaSKUHXFttW6InA1WW9aSPdbIxPi5WpBignnKABJU4xyiBRpKUaz96zeyAxI63nNaBaxLw1lVaSDsq3Gl3RKpSj2dc_H";
                var senderId = "888910657046"; 

                string deviceId = "fQ6krLJRTV6e97TmK-7Hks:APA91bHkUj_Vowc-5mr4epwABKf2EDLjrHhIv3wSP-uTbSWLsYy11j2cCgqwfPtQwuFAxopRo5EdqRp8XNHncPL_3g8NMkit9EcLUAbuYro2_yq09aGsAMXhS2knC5OG1O84buTZemSf";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = "test",
                        title = "teest",
                        icon = "myicon",
                        sound = "default"

                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
