using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaseWebAssignment.Models
{
    public class Email
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string domainName { get; set; }
        public string tld { get; set; }

        public override string ToString()
        {
            string fullEmailAddress = userName + "@" + domainName + "." + tld;
            return fullEmailAddress;
        }
    }
}
