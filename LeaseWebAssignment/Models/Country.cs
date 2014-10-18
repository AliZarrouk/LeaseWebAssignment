using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaseWebAssignment.Models
{
    public class Country
    {
        public string name { get; set; }
        public string code { get; set; }
        public int iso { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
