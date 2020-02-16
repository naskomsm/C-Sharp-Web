namespace IRunes.Controllers
{
    using IRunes.Services;
    using IRunes.ViewModels.Tracks;
    using SIS.HTTP.Response;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attribues;

    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;

        public TracksController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }

        public HttpResponse Create(CreateTrackAlbumIdModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.AlbumId) || string.IsNullOrWhiteSpace(model.AlbumId))
            {
                return this.Redirect("/");
            }

            return this.View(model);
        }

        [HttpPost]
        public HttpResponse Create(CreateTrackInputModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Track name should be between 4 and 20 characters.");
            }

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name)
                || string.IsNullOrWhiteSpace(model.Link) || string.IsNullOrEmpty(model.Link)
                || model.Price < 0)
            {
                return this.Redirect($"/Tracks/Create?albumId={model.AlbumId}");
            }


            this.tracksService.Create(model);

            return this.Redirect("/");
        }

        public HttpResponse Details(string trackId, string albumId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var model = this.tracksService.GetTrackById(trackId, albumId);

            return this.View(model);
        }
    }
}
