using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MCards.Entities;

namespace MCards.Dtos
{
    public class CardDto
    {
        public int PK_Card { get; set; }
        public string CardName { get; set; }
        
        public int FK_CardType { get; set; }

        public virtual CardType CardType { get; set; }
        public int FK_Expansion { get; set; }
        public virtual Expansion Expansion { get; set; }
        public int CardNumber { get; set; }

        public bool IsFoil { get; set; }
        public int FK_Condition { get; set; }
    }
}
