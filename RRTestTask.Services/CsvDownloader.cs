using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using RRTestTask.Abstraction.Services;
using S22.Imap;

namespace RRTestTask.Services
{
    public class CsvDownloader : ICsvDownloader
    {
        private readonly string _username = "rrtesttask";
        private readonly string _password = "testtaskdonotuse";

        public IEnumerable<MailMessage> DownloadAttachments()
        {
            using (ImapClient client = new ImapClient
                ("imap.gmail.com", 993, _username, _password, AuthMethod.Login, true))
            {
                IEnumerable<uint> uids = client.Search(SearchCondition.All());

                IEnumerable<MailMessage> messages = client.GetMessages(uids,
                    (Bodypart part) =>
                    {
                        if (part.Disposition.Type == ContentDispositionType.Attachment)
                        {
                            if (part.Type == ContentType.Text && part.Subtype.ToLower() == "csv")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        return true;
                    });
                return messages;
            }
        }
    }
}
