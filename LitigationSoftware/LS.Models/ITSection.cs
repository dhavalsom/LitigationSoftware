﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Models
{
    public class ITSection : BaseEntity
    {
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
}
