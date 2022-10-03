using Microsoft.EntityFrameworkCore;
using ShopNew.DAL;
using ShopNew.DAL.Models;

namespace ShopNew.Web.Extensions
{
    public class InitFirstValue
    {
        public static async Task Init(shop_newContext db)
        {

            if (await db.Organizations.CountAsync() == 0)
            {
                var organization1 = new Organization
                {
                    Alias = "ИП Тест",
                    ShortName = "ИП Тест",
                    FullName = "ИП Тест",
                    Inn = "312301001"
                };
                var shop1 = new Shop
                {
                    Organization = organization1,
                    Name = "Магазин 1",
                    Adress = "Белгород"
                };
                var shop2 = new Shop
                {
                    Organization = organization1,
                    Name = "Магазин 2",
                    Adress = "Белгород"
                };
                var user1 = new User
                {
                    Organization = organization1,
                    Name = "xasya",
                    Password = "kt38hmapq".CreateMd5(),
                    Email = "xasya89@mail.ru",
                    UserRole =UserRole.Admin
                };
                db.Organizations.Add(organization1);
                db.Shops.AddRange(shop1, shop2);
                db.Users.Add(user1);


                var organization2 = new Organization
                {
                    Alias = "ИП Организация",
                    ShortName = "ИП Организация",
                    FullName = "ИП Организация",
                    Inn = "312301002"
                };
                var shop3 = new Shop
                {
                    Organization = organization2,
                    Name = "Магазин 2-1",
                    Adress = "Белгород"
                };
                var shop4 = new Shop
                {
                    Organization = organization2,
                    Name = "Магазин 2-2",
                    Adress = "Белгород"
                };
                var user2 = new User
                {
                    Organization = organization2,
                    Name = "xasya89",
                    Password = "kt38hmapq".CreateMd5(),
                    Email = "xasya89@mail.ru",
                    UserRole = UserRole.Admin
                };
                db.Organizations.Add(organization2);
                db.Shops.AddRange(shop3, shop4);
                db.Users.Add(user2);

                await db.SaveChangesAsync();
            }
        }
    }
}
