using FishingStore.Model.Entities;

namespace FishingStore.Model.Repos.Abstract
{
    public interface IRoleRepo
    {
        void SaveRole(Role role);
        void DeleteRole(int id);
        Role GetRoleByName(string name);
        
    }
}
