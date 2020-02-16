namespace SulsApp.Services
{
    using SulsApp.ViewModels.Products;
    using System.Collections.Generic;

    public interface IProductsService
    {
        void Add(AddProductInputModel model);

        ICollection<ProductHomePageViewModel> GetProductsForHomePage();

        ProductDetailsPageViewModel GetProductById(string id);

        void Delete(string id);
    }
}
