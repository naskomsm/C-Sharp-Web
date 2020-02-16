namespace IRunes.Services.Implementations
{
    using IRunes.ViewModels.Albums;
    using IRunes.ViewModels.Tracks;
    using SulsApp;
    using SulsApp.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AlbumsService : IAlbumsService
    {
        private readonly ApplicationDbContext data;

        public AlbumsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void Create(CreateAlbumInputModel model)
        {
            var album = new Album()
            {
                Cover = model.Cover,
                Name = model.Name
            };

            this.data.Albums.Add(album);
            this.data.SaveChanges();
        }

        public AlbumDetailsViewModel GetAlbumById(string id)
        {
            return this
                .data
                .Albums
                .Where(x => x.Id == id)
                .Select(x => new AlbumDetailsViewModel
                {
                    Id = x.Id,
                    Cover = x.Cover,
                    Name = x.Name,
                    Price = x.Tracks.Sum(y => y.Price) * 0.87m,
                    Tracks = x.Tracks.Select(t => new CreateTrackDetailsViewModel
                    {
                        Id = t.Id,
                        Price = t.Price,
                        Link = t.Link,
                        Name = t.Name
                    }).ToList()
                })
                .FirstOrDefault();
        }

        public ICollection<BasicAlbumViewModel> GetAllAlbumsNameId()
        {
            return this.data.Albums.Select(x => new BasicAlbumViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
