namespace SulsApp.Controllers
{
    using SIS.HTTP.Response;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attribues;
    using SulsApp.Services;
    using SulsApp.ViewModels.Orders;
    using SulsApp.ViewModels.Receipt;
    using SulsApp.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReceiptsController : Controller
    {
        private readonly IReceiptsService receiptsService;
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;

        public ReceiptsController(IReceiptsService receiptsService, IOrdersService ordersService, IUsersService usersService)
        {
            this.receiptsService = receiptsService;
            this.ordersService = ordersService;
            this.usersService = usersService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                this.Redirect("/Users/Login");
            }

            if (this.Role == "Admin")
            {
                var user = this.usersService.GetUserById(this.UserId);
                var model = new ProfileViewModel()
                {
                    User = user,
                    Receipts = this.receiptsService.GetAllReceipts()
                };

                return this.View(model, "AllReceipts");
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                this.Redirect("/Users/Login");
            }

            var model = new ReceiptViewModel();
            var userOrders = new List<OrderIndexViewModel>();
            var totalMoney = 0m;

            if (this.Role == "Admin")
            {
                userOrders = this.ordersService.GetUsersOrders().ToList();
            }
            else
            {
                userOrders = this.ordersService.GetCurrentUserOrders(this.UserId).ToList();
            }

            if (userOrders.Count == 0)
            {
                return this.Redirect("/");
            }

            totalMoney = userOrders.Sum(x => x.Price);

            model = new ReceiptViewModel()
            {
                IssuedOn = DateTime.UtcNow,
                CashierId = this.UserId,
                CashierName = this.Username,
                Orders = userOrders,
                Total = totalMoney
            };

            this.receiptsService.Create(model);

            return this.Redirect("/");
        }
    }
}
