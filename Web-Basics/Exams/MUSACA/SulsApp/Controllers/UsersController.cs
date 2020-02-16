namespace SulsApp.Controllers
{
    using System;
    using System.Net.Mail;
    using SulsApp.Services;
    using SIS.MvcFramework;
    using SIS.HTTP.Logging;
    using SIS.HTTP.Response;
    using SIS.MvcFramework.Attribues;
    using SulsApp.ViewModels.Users;

    public class UsersController : Controller
    {
        private IUsersService usersService;
        private IReceiptsService receiptsService;
        private ILogger logger;

        public UsersController(IUsersService usersService, ILogger logger, IReceiptsService receiptsService)
        {
            this.receiptsService = receiptsService;
            this.usersService = usersService;
            this.logger = logger;
        }

        public HttpResponse Login()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersService.GetUserId(username, password);
            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userId, username);
            this.logger.Log("User logged in: " + username);

            var user = this.usersService.GetUserById(userId);
            this.Request.SessionData["Username"] = user.Username;
            this.Request.SessionData["Role"] = user.Role;

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords should be the same!");
            }

            if (input.Username?.Length < 5 || input.Username?.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters .");
            }

            if (input.Password?.Length < 6 || input.Password?.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters.");
            }

            if (!IsValid(input.Email))
            {
                return this.Error("Invalid email!");
            }

            if (this.usersService.IsUsernameUsed(input.Username))
            {
                return this.Error("Username already used!");
            }

            if (this.usersService.IsEmailUsed(input.Email))
            {
                return this.Error("Email already used!");
            }

            this.usersService.CreateUser(input.Username, input.Email, input.Password);
            this.logger.Log("New user: " + input.Username);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();
            return this.Redirect("/");
        }

        public HttpResponse Profile()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var user = this.usersService.GetUserById(this.UserId);
            var model = new ProfileViewModel()
            {
                User = user,
                Receipts = this.receiptsService.GetReceiptsByUserId(this.UserId)
            };
            return this.View(model);

        }

        private bool IsValid(string emailaddress)
        {
            try
            {
                new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
