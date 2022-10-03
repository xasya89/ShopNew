using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopNew.BLL.Extensions;

namespace ShopNew.BLL.Models.LoginModels
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string PasswordMd5 { get => Password.CreateMd5(); }
    }
}
