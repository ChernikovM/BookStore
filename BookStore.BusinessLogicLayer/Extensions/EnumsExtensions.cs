using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BookStore.BusinessLogicLayer.Extensions
{
    public static class EnumsExtensions
    {
        public static string GetDescription(this IConvertible value, string propName = null)
        {
            string description = 
                value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;

            if(description is null || string.IsNullOrWhiteSpace(propName))
            {
                return description;
            }

            return $"{description} ({propName})";
        }
    }
}
