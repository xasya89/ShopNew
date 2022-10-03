using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopNew.DAL.Models
{
    public class GoodGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Shop Shop { get; set; }
        public int ShopId { get; set; }

        public IEnumerable<Good> Goods { get; set; }
    }
    public class Good
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; } = Guid.NewGuid();
        [Range(1,int.MaxValue, ErrorMessage = "Не выбрана группа")]
        public int GoodGroupId { get; set; }
        public GoodGroup GoodGroup { get; set; }
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        [Required(ErrorMessage = "Наименование не заполнено")]
        public string Name { get; set; }
        public string? Article { get; set; }
        [Range(1,10000, ErrorMessage = "Ед измерения не указана")]
        public Unit Unit { get; set; }
        [Range(1,100000, ErrorMessage = "Цена не указана")]
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;

        public List<BarCode> BarCodes { get; set; } = new();
    }

    public class BarCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int GoodId { get; set; }
        public Good Good { get; set; }
    }

    public enum Unit
    {
        [Display(Name = "шт")]
        PCE = 796, //штука
        [Display(Name = "тара")]
        BOTTLE = 798, //тара
        [Display(Name = "пакет")]
        BAG = 797, //пакет
        [Display(Name = "пиво")]
        Beer = 113, //пиво
        [Display(Name = "л")]
        Litr = 112, //литр
        [Display(Name = "кг")]
        KG = 166
    }
}
