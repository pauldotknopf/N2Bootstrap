using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using N2.Engine;
using N2.Web.Mail;

namespace N2Bootstrap.Library.Services
{
    [Service(typeof(IMailSender), Replaces=typeof(SmtpMailSender))]
    public class MailSender : SmtpMailSender
    {
        protected override SmtpClient GetSmtpClient()
        {
            Models.RootPage root = null;
            try
            {
                root = N2.Find.RootItem as Models.RootPage;
                if (root == null)
                    throw new InvalidOperationException("The root page must be present and be a bootstrap root.");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Mail sender can't find root item. " + ex.Message);
                return new SmtpClient("localhost", 25);
            }

            var client = new SmtpClient();
            client.Host = root.Host;
            client.Port = root.Port;
            client.UseDefaultCredentials = root.UseDefaultCredentials;
            client.EnableSsl = root.UseSSL;
            client.Credentials = root.UseDefaultCredentials ? CredentialCache.DefaultNetworkCredentials : new NetworkCredential(root.User, root.Password);
            return client;
        }
    }
}
