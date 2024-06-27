using FishingStore.Model.Entities;

namespace FishingStore.Model.Repos.Abstract
{
    public interface IUserRepo
    {
        IQueryable<User> GetUsers();
        User GetUserById(int id);
        User GetUserByName(string username);
        User GetUserByEmail(string email);
        void SaveUser(User user);
        void DeleteUser(int id);
    }
}
