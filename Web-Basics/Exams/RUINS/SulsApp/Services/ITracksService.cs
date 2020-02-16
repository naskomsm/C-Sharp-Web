namespace IRunes.Services
{
    using IRunes.ViewModels.Tracks;

    public interface ITracksService
    {
        void Create(CreateTrackInputModel model);

        CreateTrackDetailsViewModel GetTrackById(string id, string albumId);
    }
}
