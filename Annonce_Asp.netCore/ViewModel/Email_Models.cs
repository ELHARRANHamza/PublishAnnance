using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.ViewModel
{
    public class Email_Models
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string body { get; set; }
        public string FromEmail { get; set; }
        public string FromPassword { get; set; }
    }
}
