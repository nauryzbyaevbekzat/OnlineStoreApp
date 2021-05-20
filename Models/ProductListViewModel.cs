using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AspaApp.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public SelectList Categories { get; set; }
        public string Name { get; set; }
    }
}