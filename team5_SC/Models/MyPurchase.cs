using System;
using System.Collections.Generic;

namespace team5_SC.Models
{
    public class MyPurchase
    {
        public MyPurchase()
        {
            Id = new Guid();
            Products = new List<Product>();
        }
        public Guid Id { get; set; }
        public int Qty { get; set; }
        public DateTime PurchaseDate { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}