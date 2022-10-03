﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNew.DAL.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<GoodGroup> GoodGroups { get; set; }
    }
}
