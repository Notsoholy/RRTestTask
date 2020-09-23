using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace RRTestTask.Abstraction.Services
{
    public interface ICsvDownloader
    {
        void DownloadAttachments();
    }
}
