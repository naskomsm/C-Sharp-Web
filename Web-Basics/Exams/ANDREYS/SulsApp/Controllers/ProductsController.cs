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

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddProductInputModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if(model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Product name should be between 4 and 20 characters!");
            }

            this.productsService.Add(model);
            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var model = this.productsService.GetProductById(id);
            return this.View(model);
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.productsService.Delete(id);
            return this.Redirect("/");
        }
    }
}
