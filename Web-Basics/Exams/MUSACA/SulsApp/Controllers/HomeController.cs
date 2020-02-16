namespace SulsApp.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attribues;
    using SIS.HTTP.Response;
    using SulsApp.Services;
    using SulsApp.ViewModels.Home;
    using System.Linq;
    using System.Collections.Generic;
    using SulsApp.ViewModels.Orders;

    public class HomeController : Controller
    {
        private readonly IOrdersService ordersService;

        public HomeController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var model = new IndexLoggedInUserViewModel();
                var totalMoney = 0m;
                var userOrders = new List<OrderIndexViewModel>();

                if (this.Role == "Admin")
                {
                    userOrders = this.ordersService.GetUsersOrders().ToList();
                }

                else
                {
                    userOrders = this.ordersService.GetCurrentUserOrders(this.UserId).ToList();
                }

                totalMoney = userOrders.Sum(x => x.Price);

                model = new IndexLoggedInUserViewModel()
                {
                    Orders = userOrders,
                    Total = totalMoney
                };

                return this.View(model, "/user-home");
            }

            return this.View("/guest-home");
        }
    }
}
