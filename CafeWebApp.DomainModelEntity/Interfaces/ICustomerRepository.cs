using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWebApp.DomainModelEntity.Interfaces
{
    public interface ICustomerRepository
    {
        Cart GetCart(int? id);
        Food GetFood(int? id);
        Food CheckFood(int? id);
        Cart CheckCart(int? sessionId, int? id);
        Table CheckTable(int tableNumber);
        IEnumerable<Cart> CartList(int? sessionId);
        IEnumerable<Cart> GetCarts();
        IEnumerable<Food> GetFoods();
        User CheckCustomer(User user);
        void AddCart(Cart cart);
        void UpdateTable(Table table);
        void RemoveCart(Cart cart);
        void Save();
    }
}
