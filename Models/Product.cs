using System;
using System.Collections.Generic;

namespace AspaApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Place { get; set; }
        public string Start { get; set; }
        public int ProductCost { get; set; }
        public int Numberoftickets { get; set; }
        public byte[] ProductImage { get; set; }

        public int СategoryId { get; set; }

        public Сategory Сategory { get; set; }

        public List<Basket> Basket { get; set; } = new List<Basket>();
    }

}
