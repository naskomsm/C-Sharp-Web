namespace SulsApp.Services
{
    using SulsApp.ViewModels.Users;

    public interface IUsersService
    {
        void CreateUser(string username, string email, string password);

        string GetUserId(string username, string password);

        string GetUserUsername(string id);

        void ChangePassword(string username, string newPassword);

        bool IsUsernameUsed(string username);

        UserViewModel GetUserById(string id);

        bool IsEmailUsed(string email);

        int CountUsers();
    }
}
