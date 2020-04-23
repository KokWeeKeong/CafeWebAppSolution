using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWebApp.DomainModelEntity
{
    public class Food
    {
        public int FoodId { get; set; }
        public Category Category { get; set; }
        public string FoodName { get; set; }
        public decimal FoodPrice { get; set; }
        public string Remarks { get; set; }
        public ICollection<Cart> Cart { get; set; }
    }
    public enum Category
    {
        Rice, Drinks
    }
}
