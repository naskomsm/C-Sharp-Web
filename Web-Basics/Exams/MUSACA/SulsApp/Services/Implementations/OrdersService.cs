namespace SulsApp.Services.Implementations
{
    using SulsApp.Models;
    using SulsApp.Models.Enums;
    using SulsApp.ViewModels.Orders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext db;

        public OrdersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateOrder(string userId, string barcode, int quantity)
        {
            var order = new Order()
            {
                Status = 0,
                UserId = userId,

            };
        }

        public ICollection<OrderIndexViewModel> GetCurrentUserOrders(string userId)
        {
            return this.db.Orders
                .Where(x => x.Cashier.Id == userId)
                .Where(x => x.Status == OrderStatus.Active)
                .Select(y => new OrderIndexViewModel
                {
                    Price = y.Product.Price * y.Quantity,
                    ProductName = y.Product.Name,
                    Quantity = y.Quantity
                }).ToList();
        }

        public ICollection<OrderIndexViewModel> GetUsersOrders()
        {
            return this.db.Orders
                    .Where(x => x.Status == OrderStatus.Active)
                 .Select(y => new OrderIndexViewModel
                 {
                     Price = y.Product.Price * y.Quantity,
                     ProductName = y.Product.Name,
                     Quantity = y.Quantity
                 }).ToList();
        }
    }
}
