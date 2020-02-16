namespace IRunes.Controllers
{
    using IRunes.Services;
    using IRunes.ViewModels.Albums;
    using SIS.HTTP.Response;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attribues;

    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var model = new AllAlbumsViewModel()
            {
                Albums = this.albumsService.GetAllAlbumsNameId()
            };

            return this.View(model);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateAlbumInputModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if(model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Album name should be between 4 and 20 characters.");
            }

            if (string.IsNullOrEmpty(model.Cover) || string.IsNullOrWhiteSpace(model.Cover)
                || string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrEmpty(model.Name))
            {
                return this.Redirect("/Albums/Create");
            }

            albumsService.Create(model);

            return this.Redirect("All");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var model = this.albumsService.GetAlbumById(id);

            return this.View(model);
        }
    }
}
