using System;
using System.Collections.Generic;
using System.Text;

namespace HR.BLL.Common
{
    public class EmailSettings
    {

        public string SenderEmail { get; set; } = null!;
        public string SenderPassword { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string SmtpHost { get; set; } = null!;
        public int SmtpPort { get; set; }
    }
}
