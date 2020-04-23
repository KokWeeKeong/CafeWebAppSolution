using CafeWebApp.DomainModelEntity;
using CafeWebApp.DomainModelEntity.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWebApp.InfrastructurePersistence.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private AppDbContext db = new AppDbContext();
        public void AddFood(Food food)
        {
            db.Food.Add(food);
            Save();
        }

        public void AddTable(Table table)
        {
            db.Table.Add(table);
            Save();
        }

        public void AddUser(User user)
        {
            db.User.Add(user);
            Save();
        }

        public User CheckAdmin(User user)
        {
            return db.User.Where(u => u.Username == user.Username && u.Roles == Roles.Admin).SingleOrDefault();
        }

        public Food CheckFoodName(Food food)
        {
            return db.Food.Where(f => f.FoodName == food.FoodName).SingleOrDefault();
        }

        public User CheckUsername(User user)
        {
            return db.User.Where(u => u.Username == user.Username).SingleOrDefault();
        }

        public Food GetFood(int? id)
        {
            Food food = db.Food.Find(id);
            return food;
        }

        public IEnumerable<Food> GetFoods()
        {
            return db.Food.ToList();
        }

        public IEnumerable<Table> GetTables()
        {
            return db.Table.ToList();
        }

        public User GetUser(int? id)
        {
            User user = db.User.Find(id);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return db.User.ToList();
        }

        public void RemoveFood(Food food)
        {
            db.Food.Remove(food);
            Save();
        }

        public void RemoveUser(User user)
        {
            db.User.Remove(user);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateFood(Food food)
        {
            db.Entry(food).State = EntityState.Modified;
            Save();
        }

        public void UpdateUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            Save();
        }
    }
}
