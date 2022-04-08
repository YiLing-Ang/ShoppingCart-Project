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
            Sessions = new List<Session>();
            MyPurchases = new List<MyPurchase>();
        }

        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<MyPurchase> MyPurchases { get; set; }

    }
}
