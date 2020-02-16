namespace SulsApp.Models
{
    using SulsApp.Models.Enums;
    using System;

    public class Order
    {
        public Order()
        {
            this.Id = new Guid().ToString();
        }

        public string Id { get; set; }

        public OrderStatus Status { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public User Cashier { get; set; }

        public int Quantity { get; set; }

        public string UserId { get; set; }
    }
}
