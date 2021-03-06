﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWebApp.DomainModelEntity
{
    public class Cart
    {
        public int CartId { get; set; }
        public Food Food { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int FoodQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
