﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseWebAssignment.Models
{
    public class Customer
    {
        public long registrationNbr { get; set; }
        public string companyName { get; set; }
        public Email email { get; set; }
        public Address address { get; set; }
        public string phoneNumber { get; set; }
        public string website { get; set; }
        public uint numberOfEmployees { get; set; }
        public uint maximumOutstandingOrderAmount { get; set; }
        public DateTime customerSince { get; set; }
        public virtual List<Contact> contacts { get; set; }
    }
}