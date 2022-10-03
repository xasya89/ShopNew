using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopNew.BLL.Extensions
{
    public static class EnumExtension
    {
        public static string? GetDisplay(this Enum value) => value.GetType()
                        .GetMember(value.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?.GetName();

        public static IEnumerable<string> ToList<T>() => 
            Enum.GetValues(typeof(T)).Cast<T>()
            .Select(v=> v.GetType()
                        .GetMember(v.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?.GetName() ?? "").ToList();


        public static Dictionary<T, string> ToDictionary<T>()=>
            Enum.GetValues(typeof(T)).Cast<T>()
            .Select(v => new KeyValuePair<T, string>(  v, v.GetType().GetMember(v.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.GetName() ?? "" ))
            .ToDictionary(t=>t.Key, t=>t.Value);
    }
}
