using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EmailSetting
    {
        public string MailToAddress = "sunnycool038@yahoo.com";
        public string MailFromAdress = "sunnycool03@gmail.com";
        public bool UseSsl = true;
        public string username = "sunnycool";
        public string password = "emma4real";
        public string ServerName = "smtp.example.com";
        public int serverPort = 187;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\sports_store_emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        public EmailSetting emailSetting;
        public EmailOrderProcessor(EmailSetting setting)
        {
            emailSetting = setting;
        }
        public void ProcessOrder(Cart cart, ShippingDetails ShippingInfo)
        {
            using(var smtpClient=new SmtpClient())
            {
                smtpClient.EnableSsl = emailSetting.UseSsl;
                smtpClient.Host = emailSetting.ServerName;
                smtpClient.Port = emailSetting.serverPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSetting.username, emailSetting.password);
                if (emailSetting.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSetting.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                .AppendLine("Anew order has been submitted")
                .AppendLine("---")
                .AppendLine("Items:");
                foreach(var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0}*{1} (subtotal: {2:c}", line.Quantity, line.Product.Name, subtotal);
                }
                body.AppendFormat("Total Order Value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("shipTo:")
                    .AppendLine(ShippingInfo.Name)
                    .AppendLine(ShippingInfo.Line1 ?? "")
                    .AppendLine(ShippingInfo.Line2 ?? "")
                    .AppendLine(ShippingInfo.Line3 ?? "")
                    .AppendLine(ShippingInfo.City)
                    .AppendLine(ShippingInfo.State)
                    .AppendLine(ShippingInfo.Country)
                    .AppendLine(ShippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift Wrap: {0}",
                    ShippingInfo.GiftWrap ? "Yes" : "No");
                MailMessage mailmessage = new MailMessage(
                    emailSetting.MailFromAdress,
                    emailSetting.MailToAddress,
                    "New Order Submitted",
                    body.ToString());
                if (emailSetting.WriteAsFile)
                {
                    mailmessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailmessage);
            }
        }
    }
}
