using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MCards.Entities
{
    public class Expansion
    {
        [Key]
        public int PK_Expansion { get; set; }
        public string ExpansionName { get; set; }
        public string ExpansionShortName { get; set; }
        public int ExpansionCards { get; set; }
    }
}
