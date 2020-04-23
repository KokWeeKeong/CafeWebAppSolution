using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWebApp.DomainModelEntity
{
    public class Table
    {
        public int TableId { get; set; }
        public TableStatus TableStatus { get; set; }
        public int TableNumber { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
    }
    public enum TableStatus
    {
        Empty, Occupied
    }
}
