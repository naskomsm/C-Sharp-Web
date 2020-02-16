namespace SulsApp
{
    using SIS.HTTP;
    using SulsApp.Services;
    using System.Collections.Generic;
    using SIS.MvcFramework.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using SulsApp.Services.Implementations;
    using IRunes.Services;
    using IRunes.Services.Implementations;

    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IAlbumsService, AlbumsService>();
            serviceCollection.Add<ITracksService, TracksService>();
        }

        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            db.Database.Migrate();
        }
    }
}
