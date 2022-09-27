using System.Configuration;
using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using DAL.Models;
using System.Web.Script.Serialization;

namespace BAL.Repositories
{
    public class BaseRepository : IDisposable
    {

        StreamWriter _sw;
        public db_a8354f_turkishpizzaEntities DBContext;
        public BaseRepository()
        {
            DBContext = new db_a8354f_turkishpizzaEntities();
        }

        public BaseRepository(db_a8354f_turkishpizzaEntities ContextDB)
        {
            DBContext = ContextDB;
        }

        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DBContext != null)
                {
                    DBContext.Dispose();
                    DBContext = null;

                }
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~BaseRepository()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public void ErrorLog(Exception e, string FnName, string FileName)
        {
            try
            {


                //ErrorLog1 Log = new ErrorLog1();
                //Log.Errorin = FnName + " : " + e.InnerException;
                //Log.ErrorMessage = e.Message;
                //Log.CreatedDate = DateTime.UtcNow;
                ////Log.UserID = 2;
                ////Log.CreatedBy= 2;
                //DBContext.ErrorLogs1.Attach(Log);
                //DBContext.ErrorLogs1.Add(Log);
                //DBContext.SaveChanges();
                ////function
                //LogWrite(Log.ErrorMessage, FileName);
            }
            catch
            {
            }
        }
        public void LogWrite(string msg, string fileName)
        {
            //var logPath = ConfigurationManager.AppSettings["LogPath"];
            //_sw = new StreamWriter(@logPath + fileName + DateTime.UtcNow.ToString("yyyyMMdd") + ".txt", true);

            _sw.WriteLine(DateTime.UtcNow.ToLongTimeString() + " " + msg);
            _sw.Close();
        }

        //public string CurrentDate(SessionInfo session)
        //{
        //    #region timmings

        //    DateTime t1 = DateTime.UtcNow.AddMinutes(session.UTC);
        //    DateTime t2 = Convert.ToDateTime(session.OpenTime.ToString());
        //    DateTime t3 = Convert.ToDateTime(session.CloseTime.ToString());

        //    string startday;

        //    TimeSpan diff = t3 - t2;

        //    DateTime NewDate = t2.AddHours(diff.Hours <= 0 ? (24 - (-diff.Hours)) : diff.Hours);

        //    if (t3.Date != NewDate.Date)
        //    {
        //        int b = DateTime.Compare(t1, t3);

        //        if (b == 1)
        //        {
        //            startday = DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
        //        }
        //        else
        //        {
        //            startday = DateTime.Today.AddDays(-1).ToString();
        //        }
        //    }
        //    else
        //    {
        //        startday = DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
        //    }
        //    return startday;
        //    #endregion timmings 
        //}


        public string DateFormat(string Date)
        {
            if (Date != "")
                return Convert.ToDateTime(Date).ToString("yyyy-MM-dd hh:mm:ss");
            else
                return "";
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public void Email(string _SubjectEmail, string _BodyEmail, string _To, string FromAddress, string Password, string SMTP, int Port)
        {
            try
            {
                using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                {
                    mail.From = new MailAddress(FromAddress, "MarnPos");
                    mail.To.Add(_To);
                    mail.Subject = _SubjectEmail;
                    mail.Body = _BodyEmail;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient(SMTP, Port))
                    {
                        smtp.Credentials = new NetworkCredential(FromAddress, Password);
                        smtp.EnableSsl = false;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void PushNotificationAndroid(PushNoticationBLL obj)
        {
            try
            {
                var applicationID = "AAAAzvc0NhY:APA91bG9ijRE-1EFIDAMLeI2Mwjpn-RE-8-JFwxoNhhL9-PIjaSKUHXFttW6InA1WW9aSPdbIxPi5WpBignnKABJU4xyiBRpKUaz96zeyAxI63nNaBaxLw1lVaSDsq3Gl3RKpSj2dc_H";
                var senderId = "888910657046";
                string deviceId = obj.DeviceID;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = obj.Message,
                        title = obj.Title,
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
