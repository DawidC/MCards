﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MCards.Entities
{
    public class CardType
    {
        [Key]
        public int PK_CardType { get; set; }
        public string CardTypeName { get; set; }
    }
}
