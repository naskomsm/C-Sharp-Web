namespace SulsApp.Models
{
    using System;
    using System.Collections.Generic;

    public class Receipt
    {
        public Receipt()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public ICollection<Order> Orders { get; set; }

        public string UserId { get; set; }

        public User Cashier { get; set; }
    }
}
