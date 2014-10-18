using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaseWebAssignment.Models
{
    public class City
    {
        public int ID { get; set; }
        public string name { get; set; }
        public virtual Country country { get; set; }
        public override string ToString()
        {
            return name + "\n" + country.ToString();
        }
    }
}
