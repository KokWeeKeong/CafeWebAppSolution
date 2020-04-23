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
    public class CashierRepository : ICashierRepository
    {
        private AppDbContext db = new AppDbContext();

        public User CheckCashier(User user)
        {
            return db.User.Where(u => u.Username == user.Username && u.Roles == Roles.Cashier).SingleOrDefault();
        }

        public Table GetTable(int? id)
        {
            Table table = db.Table.Find(id);
            return table;
        }

        public IEnumerable<Table> GetTables()
        {
            var table = db.Table.Include(t => t.User);
            return table;
        }

        public void RemoveTable(Table table)
        {
            db.Table.Remove(table);
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
