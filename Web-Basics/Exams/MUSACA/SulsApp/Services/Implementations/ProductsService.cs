namespace SulsApp.Services.Implementations
{
    using SulsApp.Models;
    using SulsApp.ViewModels.Products;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext db;

        public ProductsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(ProductCreateViewModel model)
        {
            var product = new Product()
            {
                Barcode = model.Barcode,
                Name = model.Name,
                Picture = model.Picture,
                Price = model.Price
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public ICollection<ProductsAllViewModel> GetAllProducts()
        {
            return this.db.Products.Select(x => new ProductsAllViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Name = x.Name,
                Picture = x.Picture,
                Price = x.Price
            }).ToList();
        }
    }
}
