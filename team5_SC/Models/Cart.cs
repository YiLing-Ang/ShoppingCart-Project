using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team5_SC.Models
{
    public class Cart
    {
        public Cart()
        {
            Id = new Guid();
            //Products = new List<Product>();
        }
        
    
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    //public virtual Guid UserId { get; set; }

    public virtual User User { get; set; }

    public virtual Guid SessionId { get; set; }

    public virtual Product Product { get; set; }

     //public virtual ICollection<Product> Products { get; set; }

    }
}
