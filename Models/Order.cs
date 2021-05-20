using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspaApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string User { get; set; } 
        public string Address { get; set; } 
        public string ContactPhone { get; set; } 

        public int BasketId { get; set; } 
        public Basket Basket { get; set; }
    }
}
