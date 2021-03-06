﻿namespace SulsApp
{
    using SIS.HTTP;
    using SulsApp.Services;
    using System.Collections.Generic;
    using SIS.MvcFramework.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using SulsApp.Services.Implementations;

    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();

            // TODO:
        }

        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            db.Database.Migrate();
        }
    }
}
