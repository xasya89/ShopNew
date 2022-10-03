using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNew.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }

    public enum UserRole
    {
        [Display(Name = "Администратор")]
        Admin,
        [Display(Name = "Менеджер")]
        Manager,
        [Display(Name = "Кассир")]
        Cashier
    }
}
