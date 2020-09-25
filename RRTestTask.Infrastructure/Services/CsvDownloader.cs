using System;
using RRTestTask.Abstraction.Services;
using MailKit;
using MimeKit;
using MailKit.Net.Imap;
using System.Linq;
using System.IO;

namespace RRTestTask.Services
{
    public class CsvDownloader : ICsvDownloader
    {
        public void DownloadAttachments(string host, string login, string password)
        {
            using (var client = new ImapClient())
            {
                client.Connect(host, 993, true);

                client.Authenticate(login, password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                GetMessagesWithAttachments(inbox);
            }
        }

        private static void GetMessagesWithAttachments(IMailFolder inbox)
        {
            for (int i = 0; i < inbox.Count; i++)
            {
                var message = inbox.GetMessage(i);
                GetAttachments(message);
            }
        }

        private static void GetAttachments(MimeMessage message)
        {
            foreach (var attachment in message.Attachments.Where(a => a.ContentType.Name.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase)))
            {
                var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;

                using (var stream = File.Create(fileName))
                {
                    if (attachment is MessagePart rfc822)
                    {
                        rfc822.Message.WriteTo(stream);
                    }
                    else
                    {
                        var part = attachment as MimePart;

                        part.Content.DecodeTo(stream);
                    }
                }
            }
        }
    }
}
