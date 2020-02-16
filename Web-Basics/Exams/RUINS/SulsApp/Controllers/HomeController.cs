namespace SulsApp.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attribues;
    using SIS.HTTP.Response;

    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                return this.View("Home");
            }

            return this.View("Index");
        }
    }
}
