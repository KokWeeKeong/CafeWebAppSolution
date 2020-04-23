using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWebApp.DomainModelEntity.Interfaces
{
    public interface ICashierRepository
    {
        Table GetTable(int? id);
        IEnumerable<Table> GetTables();
        void UpdateTable(Table table);
        void RemoveTable(Table table);
        void Save();
        User CheckCashier(User user);
    }
}
