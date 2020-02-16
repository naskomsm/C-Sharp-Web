namespace IRunes.Services
{
    using IRunes.ViewModels.Albums;
    using System.Collections.Generic;

    public interface IAlbumsService
    {
        void Create(CreateAlbumInputModel model);

        ICollection<BasicAlbumViewModel> GetAllAlbumsNameId();

        AlbumDetailsViewModel GetAlbumById(string id);
    }
}
