using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team5_SC.Models
{
    public class Product
    {
        public Product()
        {
            Id = new Guid();
            Carts = new List<Cart>();
            //MyPurchases = new List<MyPurchase>();
        }

        public Guid Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }

        //public virtual ICollection<MyPurchase> MyPurchases { get; set; }
    }
}
