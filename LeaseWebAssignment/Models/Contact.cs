using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LeaseWebAssignment.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string name { get; set; }
        public Title title { get; set; }
        public List<ContactType> type { get; set; }
        // Address contains city
        public virtual Address address { get; set; }
        public string phoneNumber { get; set; }
        public virtual Email email { get; set; }
        public virtual Contact parentContact { get; set; }
    }
}
