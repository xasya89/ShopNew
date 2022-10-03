using ShopNew.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNew.BLL.Models.LoginModels
{
    public class SecurityTokenModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; }
    }
}
