using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWebApp.Web.Models
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