using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWebApp.DomainModelEntity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Roles { get; set; }
        public ICollection<Cart> Cart { get; set; }
    }
    public enum Roles
    {
        Admin, Cashier, Customer
    }
}
