namespace SulsApp.ViewModels.Home
{
    using SulsApp.ViewModels.Orders;
    using System.Collections.Generic;

    public class IndexLoggedInUserViewModel
    {
        public ICollection<OrderIndexViewModel> Orders { get; set; }

        public decimal Total { get; set; }
    }
}
