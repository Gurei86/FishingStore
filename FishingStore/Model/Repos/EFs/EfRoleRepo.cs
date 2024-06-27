using FishingStore.Model.Entities;
using FishingStore.Model.Repos.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace FishingStore.Model.Repos.EFs
{
    public class EfRoleRepo:IRoleRepo
    {
        private readonly FishingStoreContext context;
        public EfRoleRepo(FishingStoreContext context)
        {
            this.context = context;
        }

        public void SaveRole(Role role)
        {
            if (role.Id == default(int)) 
            { context.Entry(role).State = EntityState.Added; }
            else 
            { context.Entry(role).State = EntityState.Modified; }
            context.SaveChanges();
        }

        public void DeleteRole(int id)
        {
            context.Roles.Remove(new Role() { Id = id });
            context.SaveChanges();
        }

        public Role GetRoleByName(string name)
        {
           return context.Roles.FirstOrDefault(x => x.Name == name);
            
        }
    }
}
