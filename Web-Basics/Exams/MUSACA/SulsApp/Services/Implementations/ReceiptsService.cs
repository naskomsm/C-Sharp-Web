namespace SulsApp.Services.Implementations
{
    using SulsApp.Models;
    using SulsApp.Models.Enums;
    using SulsApp.ViewModels.Orders;
    using SulsApp.ViewModels.Receipt;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReceiptsService : IReceiptsService
    {
        private readonly ApplicationDbContext db;

        public ReceiptsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(ReceiptViewModel model)
        {
            var orders = this.db.Orders
                .Where(x => x.UserId == model.CashierId)
                .Where(x => x.Status == OrderStatus.Active)
                .ToList();

            var receipt = new Receipt()
            {
               UserId = model.CashierId,
               IssuedOn = model.IssuedOn,
               Orders = orders
            };

            foreach (var order in orders)
            {
                order.Status = OrderStatus.Completed;
            }

            this.db.Receipts.Add(receipt);
            this.db.SaveChanges();
        }

        public ICollection<ReceiptViewModel> GetAllReceipts()
        {
            return this.db.Receipts
             .Select(x => new ReceiptViewModel()
             {
                 Id = x.Id,
                 CashierId = x.Cashier.Id,
                 CashierName = x.Cashier.Username,
                 IssuedOn = x.IssuedOn,
                 Orders = x.Orders.Select(h => new OrderIndexViewModel()
                 {
                     Price = h.Product.Price * h.Quantity,
                     ProductName = h.Product.Name,
                     Quantity = h.Quantity
                 }).ToList(),
                 Total = x.Orders.Sum(y => y.Product.Price * y.Quantity)
             }).ToList();
        }

        public ICollection<ReceiptViewModel> GetReceiptsByUserId(string userId)
        {
            return this.db.Receipts
            .Where(x => x.UserId == userId)
            .Select(x => new ReceiptViewModel()
            {
                Id = x.Id,
                CashierId = x.Cashier.Id,
                CashierName = x.Cashier.Username,
                IssuedOn = x.IssuedOn,
                Orders = x.Orders.Select(h => new OrderIndexViewModel()
                {
                    Price = h.Product.Price * h.Quantity,
                    ProductName = h.Product.Name,
                    Quantity = h.Quantity
                }).ToList(),
                Total = x.Orders.Sum(y => y.Product.Price * y.Quantity)
            }).ToList();
        }
    }
}
