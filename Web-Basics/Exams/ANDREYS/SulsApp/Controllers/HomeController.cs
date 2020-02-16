namespace SulsApp.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attribues;
    using SIS.HTTP.Response;
    using SulsApp.Services;
    using SulsApp.ViewModels.Products;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var model = new HomePageViewModel()
                {
                    Products = this.productsService.GetProductsForHomePage()
                };

                return this.View(model, "Home");
            }

            return this.View("Index");
        }
    }
}
