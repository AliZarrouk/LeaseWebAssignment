using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaseWebAssignment.Models
{
    public class Contact
    {
        public string name { get; set; }
        public Title title { get; set; }
        public List<ContactType> type { get; set; }
        // Address contains city
        public Address address { get; set; }
        public string phoneNumber { get; set; }
        public Email email { get; set; }
        public Contact parentContact { get; set; }
    }
}
