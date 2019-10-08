using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    class MainScreen
    {

        public bool checkEmail()
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add("nathanjackkerr@gmail.com");
            mail.From = new MailAddress("kerrwebdesign@gmail.com", "SmartCloud ATM", System.Text.Encoding.UTF8);
            mail.Subject = "Account Balance";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "<h4>Hello,</h4>"
                    + "<b>This is an automated email from a ATM request to view you account balance. Please do not reply to this email.</b> <br /><br />"
                    + ", here is your account balance: £?<br /><br />"
                    + "Kind Regards,"
                    + "<br />" +
                    "Smart Cloud ATM";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("animalshelterqubtest@gmail.com", "AnimalShelter10");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                //successfully send email
                client.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
