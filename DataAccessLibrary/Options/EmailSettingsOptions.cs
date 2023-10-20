using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Options
{
    /* This is our options class that will hold all our values from our
     * secrets.json file.  We will use these values to fill in our credentials 
     * in the SendSMTPLibrary so that people can't see our smtp pw and 
     * so that it's not also uploaded to our version control repo.
     */

    public class EmailSettingsOptions
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpUseSSL { get; set; }
        public string FromMailboxAddressName { get; set; }
        public string FromMailboxAddressAddress { get; set; }
        public string AddMailboxAddress { get; set; }
        public string AuthenticateUserName { get; set; }
        public string AuthenticatePw { get; set; }
    }
}
