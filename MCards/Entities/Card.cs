﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MCards.Entities
{
    public class Card
    {
        [Key]
        public int PK_Card { get; set; }
        public string CardName { get; set; }
        
        public int FK_CardType { get; set; }
        [ForeignKey("FK_CardType")]
        public virtual CardType CardType { get; set; }
        public int FK_Expansion { get; set; }
        [ForeignKey("FK_Expansion")]
        public virtual Expansion Expansion { get; set; }
        public int CardNumber { get; set; }

        public bool IsFoil {get;set;}
        public int FK_Condition { get; set; }
    }
    
}
