using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNew.DAL.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; } = new();
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public string OrgName { get; set; }
        public string? Inn { get; set; }
        public string? Kpp { get; set; }

        public IEnumerable<Good> Goods { get; set; }
    }
}
