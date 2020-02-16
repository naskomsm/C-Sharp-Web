namespace SulsApp.Controllers
{
    using SIS.HTTP.Response;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attribues;
    using SulsApp.Services;
    using SulsApp.ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var model = new ProductsAllViewModelCollection()
            {
                Products = this.productsService.GetAllProducts()
            };

            return this.View(model, "products-all");
        }

        public HttpResponse Create()
        {
            if(this.Role != "Admin")
            {
                return this.Redirect("/");
            }

            return this.View("product-create");
        }

        [HttpPost]
        public HttpResponse Create(ProductCreateViewModel model)
        {
            if (this.Role != "Admin")
            {
                return this.Redirect("/");
            }

            this.productsService.Create(model);

            return this.Redirect("/");
        }
    }
}
