using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team5_SC.Models
{
    public class ActivationCode
    {
        public ActivationCode()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }

        public virtual Guid ProductId { get; set; }
    }
}
