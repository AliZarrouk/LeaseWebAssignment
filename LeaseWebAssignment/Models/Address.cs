using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaseWebAssignment.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string streetName { get; set; }
        public string houseNumber { get; set; }
        public string postalCode { get; set; }
        public virtual City city { get; set; }

        public override string ToString()
        {
            string fullAdr = streetName + " " + houseNumber + "\n" + postalCode + "  " + city.ToString();
            return fullAdr;
        }
    }
}
