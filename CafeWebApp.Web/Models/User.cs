using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWebApp.Web.Models
{
    public class User
    {
        //public User()
        //{
        //    Id = Utility.GetId();
        //}
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
    //public static class Utility
    //{
    //    private static int Id = 1;
    //    public static int GetId()
    //    {
    //        return Id++;
    //    }
    //}
}