using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace team5_SC.Models
{
    public class User
    {
        public User()
        {
            Id = new Guid();
            MyPurchases = new List<MyPurchase>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] Password { get; set; }

        public virtual ICollection<MyPurchase> MyPurchases { get; set; }

    }
}
