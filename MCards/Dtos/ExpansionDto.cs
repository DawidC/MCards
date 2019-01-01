using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCards.Dtos
{
    public class ExpansionDto
    {
        public int PK_Expansion { get; set; }
        public string ExpansionName { get; set; }
        public string ExpansionShortName { get; set; }
        public int ExpansionCards { get; set; }
    }
}
