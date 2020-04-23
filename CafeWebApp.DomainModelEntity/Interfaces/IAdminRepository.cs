using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWebApp.DomainModelEntity.Interfaces
{
    public interface IAdminRepository
    {
        User GetUser(int? id);
        IEnumerable<User> GetUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);
        Food GetFood(int? id);
        IEnumerable<Food> GetFoods();
        void AddFood(Food food);
        void UpdateFood(Food food);
        void RemoveFood(Food food);
        void Save();
        User CheckAdmin(User user);
        IEnumerable<Table> GetTables();
        void AddTable(Table table);
        User CheckUsername(User user);
        Food CheckFoodName(Food food);
    }
}
