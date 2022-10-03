using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNew.DAL.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Inn { get; set; }
        public string? Kpp { get; set; }

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Shop> Shops { get; set; }
    }

}
