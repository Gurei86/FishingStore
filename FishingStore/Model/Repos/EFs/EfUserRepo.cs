using FishingStore.Model.Entities;
using FishingStore.Model.Repos.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace FishingStore.Model.Repos.EFs
{
    public class EfUserRepo:IUserRepo
    {
        private readonly FishingStoreContext context;

        public EfUserRepo(FishingStoreContext context)
        {
            this.context = context;
        }

        public void DeleteUser(int id)
        {
            context.Users.Remove(new User() { Id = id });
            context.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(e => e.Email == email);
        }

        public User GetUserById(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByName(string username)
        {
            return context.Users.FirstOrDefault(e => e.FullName == username);
        }

        public IQueryable<User> GetUsers()
        {
            return context.Users;
        }

        public void SaveUser(User user)
        {
            if (user.Id == default(int)) 
            { context.Entry(user).State = EntityState.Added; }
            else 
            { context.Entry(user).State = EntityState.Modified; }
            context.SaveChanges();
        }
    }
}
