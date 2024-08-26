﻿using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class OrderSummaryModel
    {
        public string UserName { get; set; }
        public int OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
