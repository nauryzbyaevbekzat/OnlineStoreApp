using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspaApp.Models
{
    public class Baskethis1
    {
        public int BasketId { get; set; }
    
        public string ProductName { get; set; }

        public int ProductCost { get; set; }
        public byte[] ProductImage { get; set; }
        public string Email { get; set; }

    }
}
