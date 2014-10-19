using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LeaseWebAssignment.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long registrationNbr { get; set; }
        public string companyName { get; set; }
        public virtual Email email { get; set; }
        public virtual Address address { get; set; }
        public virtual City city { get; set; }
        public virtual Country country { get; set; }
        public string phoneNumber { get; set; }
        public string website { get; set; }
        public uint numberOfEmployees { get; set; }
        public uint maximumOutstandingOrderAmount { get; set; }
        public DateTime customerSince { get; set; }
        public virtual List<Contact> contacts { get; set; }
    }
}