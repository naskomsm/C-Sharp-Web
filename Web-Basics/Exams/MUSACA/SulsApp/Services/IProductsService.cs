namespace SulsApp.Services
{
    using SulsApp.ViewModels.Products;
    using System.Collections.Generic;

    public interface IProductsService
    {
        public ICollection<ProductsAllViewModel> GetAllProducts();

        public void Create(ProductCreateViewModel model);
    }
}
