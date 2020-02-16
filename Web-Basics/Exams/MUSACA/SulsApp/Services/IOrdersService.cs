namespace SulsApp.Services
{
    using SulsApp.ViewModels.Orders;
    using System.Collections.Generic;

    public interface IOrdersService
    {
        ICollection<OrderIndexViewModel> GetCurrentUserOrders(string userId);

        ICollection<OrderIndexViewModel> GetUsersOrders();

        void CreateOrder(string userId, string barcode, int quantity);
    }
}
