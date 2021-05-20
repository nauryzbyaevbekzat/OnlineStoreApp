using Microsoft.AspNetCore.Http;
using System;

namespace AspaApp.Models
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Place { get; set; }
        public string Start { get; set; }
        public int ProductCost { get; set; }
        public int Numberoftickets { get; set; }
        public IFormFile ProductImage { get; set; }

        public int СategoryId { get; set; }
        public Сategory Сategory { get; set; }
    }
}
