namespace SulsApp.ViewModels.Receipt
{
    using SulsApp.ViewModels.Orders;
    using System;
    using System.Collections.Generic;

    public class ReceiptViewModel
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public ICollection<OrderIndexViewModel> Orders { get; set; }

        public decimal Total { get; set; }

        public string CashierId { get; set; }

        public string CashierName { get; set; }
    }
}
