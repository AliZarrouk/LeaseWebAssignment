using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LeaseWebAssignment.Models
{
    public class Country
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iso { get; set; }
        public string name { get; set; }
        public string code { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
