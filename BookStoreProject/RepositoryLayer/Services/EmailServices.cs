using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailServices
    {
        public static void SendMail(string email, string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("banpay0@gmail.com", "lmatlbghwobbmqjy");


                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(email);
                msgObj.From = new MailAddress("banpay0@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.IsBodyHtml = true;
                msgObj.Body = $"<!DOCTYPE html>" +
                    $"<html lang=\"en\">" +
                    $"<head>" +
                    $"<meta charset=\"UTF-8\">" +
                    $"</head>" +
                    $"<body>" +
                    $"<h2> Dear Fundoo User, </h2>\n" +
                    $"<h3>please click on the below link to reset password</h3>" +
                    $"<a href='http://localhost:4200/reset-password/{token}'>clickhere </a>\n" +
                    $"<h3 style =\"color:#blue\">\n the link is valid for 1 hour</h3>" +
                    $"</body>" +
                    $"</html>";

                
                client.Send(msgObj);
            }
        }
    }
}
