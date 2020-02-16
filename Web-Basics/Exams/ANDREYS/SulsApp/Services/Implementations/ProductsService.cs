namespace SulsApp.Services.Implementations
{
    using SulsApp.Models;
    using SulsApp.Models.Enums;
    using SulsApp.ViewModels.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext db;

        public ProductsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(AddProductInputModel model)
        {
            var category = (Category)Enum.Parse(typeof(Category), model.Category, true);
            var gender = (Gender)Enum.Parse(typeof(Gender), model.Gender, true);

            var product = new Product()
            {
                Category = category,
                Description = model.Description,
                Gender = gender,
                ImageUrl = model.ImageUrl,
                Name = model.Name,
                Price = model.Price
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var entity = this.db.Products.Find(id);
            this.db.Products.Remove(entity);
            this.db.SaveChanges();
        }

        public ProductDetailsPageViewModel GetProductById(string id)
        {
            return this.db.Products
                .Where(x => x.Id == id)
                .Select(x => new ProductDetailsPageViewModel()
                {
                    Id = x.Id,
                    Category = x.Category.ToString(),
                    Description = x.Description,
                    Gender = x.Gender.ToString(),
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl
                })
                .FirstOrDefault();
        }

        public ICollection<ProductHomePageViewModel> GetProductsForHomePage()
        {
            return this.db.Products
                .Select(x => new ProductHomePageViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToList();
        }
    }
}
