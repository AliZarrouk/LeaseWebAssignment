using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaseWebAssignment.Models
{
    public class City
    {
        public string name { get; set; }
        public Country country { get; set; }
        public override string ToString()
        {
            return name + "\n" + country.ToString();
        }
    }
}
