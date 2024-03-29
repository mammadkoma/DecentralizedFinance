﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecentralizedFinance.ViewModels
{
    public class TokenViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string FullName { get; set; }
        public string TotalAmount { get; set; }
        public string Tvl { get; set; }
        public string GroupName { get; set; }
    }
}
