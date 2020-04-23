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
    public class CustomerRepository : ICustomerRepository
    {
        private AppDbContext db = new AppDbContext();

        public void AddCart(Cart cart)
        {
            db.Cart.Add(cart);
            Save();
        }

        public IEnumerable<Cart> CartList(int? sessionId)
        {
            return db.Cart.Where(c => c.UserId == sessionId).ToList();
        }

        public Cart CheckCart(int? sessionId, int? id)
        {
            return db.Cart.Where(c => c.UserId == sessionId && c.FoodId == id).SingleOrDefault();
        }

        public User CheckCustomer(User user)
        {
            return db.User.Where(u => u.Username == user.Username && u.Roles == Roles.Customer).SingleOrDefault();
        }

        public Food CheckFood(int? id)
        {
            return db.Food.Where(f => f.FoodId == id).SingleOrDefault();
        }

        public Table CheckTable(int tableNumber)
        {
            return db.Table.Where(t => t.TableNumber == tableNumber).SingleOrDefault();
        }

        public Cart GetCart(int? id)
        {
            Cart cart = db.Cart.Find(id);
            return cart;
        }

        public IEnumerable<Cart> GetCarts()
        {
            return db.Cart.ToList();
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

        public void RemoveCart(Cart cart)
        {
            db.Cart.Remove(cart);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateTable(Table table)
        {
            db.Entry(table).State = EntityState.Modified;
            Save();
        }
    }
}
