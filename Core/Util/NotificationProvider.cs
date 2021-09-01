using Core.Enums;
using Core.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Core.Util
{
    public class NotificationProvider
    {
        public static void Notify(CurrentStatusEnum status)
        {
            EmailNotification(status);
            PushNotification(status);
        }

        private static void EmailNotification(CurrentStatusEnum status)
        {

            new Thread(() =>
            {
                try
                {
                    SmtpClient client = new SmtpClient("smtp.gmail.com"); //for example gmail
                    client.Port = 587; //gmail server port
                                       //client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("", ""); //add credentials, email and password
                    client.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(""); //Email from
                    mailMessage.To.Add(""); //Email To
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = "<p><strong>Congratulations, your service request is: " + status.ToString() + "</strong></p>";
                    mailMessage.Subject = "Service request status";
                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    //do something
                }

            }).Start();

        }

        private static void PushNotification(CurrentStatusEnum status)
        {
            try
            {
                var _BaseAdress = "https://fcm.googleapis.com/fcm/send";

                var _KeyFirebase = "";

                var result = "-1";
                var webAddr = _BaseAdress;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add($"Authorization:key={_KeyFirebase}");

                httpWebRequest.Method = "POST";


                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var json = new
                    {
                        to = "xxxxxxxxxxxxxxxx", //device token
                        vibrate = new long[] { 1000, 1000, 1000, 1000, 1000 },
                        priority = "high",
                        data = new
                        {
                            //some data

                        },
                        notification = new
                        {
                            title = "New status!",
                            text = "New service request status: " + status.ToString(),
                            sound = "default",
                        }
                    };
                    streamWriter.Write(JsonConvert.SerializeObject(json));
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                var jsonObjeto = JObject.Parse(result);
                var resultadoNoti = jsonObjeto.ToObject<FirebaseResult>();
                if (resultadoNoti.failure == 1 && resultadoNoti.success == 0)
                {
                    // do something
                }
            }
            catch (Exception ex)
            {
                //do something
            }
        }
    }
}
