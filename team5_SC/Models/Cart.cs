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
        }
        
    
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public virtual Guid UserId { get; set; }

    public virtual Guid SessionId { get; set; }

    public virtual Product Products { get; set; }

    public virtual Guid ProductId { get; set; }
    }
}
