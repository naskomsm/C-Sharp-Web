namespace IRunes.Services.Implementations
{
    using IRunes.ViewModels.Tracks;
    using SulsApp;
    using SulsApp.Models;
    using System.Linq;

    public class TracksService : ITracksService
    {
        private readonly ApplicationDbContext data;

        public TracksService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void Create(CreateTrackInputModel model)
        {
            var track = new Track()
            {
                Link = model.Link,
                Name = model.Name,
                Price = model.Price
            };

            var album = this.data.Albums.FirstOrDefault(x => x.Id == model.AlbumId);
            album.Tracks.Add(track);
            album.Price = album.Tracks.Sum(x => x.Price) * 0.87m;

            this.data.Tracks.Add(track);
            this.data.SaveChanges();
        }

        public CreateTrackDetailsViewModel GetTrackById(string id, string albumId)
        {
            return this.data
                .Tracks
                .Where(x => x.Id == id)
                .Select(x => new CreateTrackDetailsViewModel
                {
                    Id = x.Id,
                    Link = x.Link,
                    Name = x.Name,
                    Price = x.Price,
                    AlbumId = albumId
                })
                .FirstOrDefault();
        }
    }
}
