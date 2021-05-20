using System.Collections.Generic;

namespace AspaApp.Models
{
    public class Сategory
    {
        public int СategoryId { get; set; }

        public string СategoryName { get; set; }


        public List<Product> Products { get; set; }

        public Сategory()
        {
            Products = new List<Product>();
        }

    }
}
