using System;
using System.Collections.Generic;

namespace Application.WebApi.Extensions
{
    public class EnumExtensions
    {
        public static List<EnumValue> GetValues<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                // For each value of this enumeration, add a new EnumValue instance
                values.Add(new EnumValue()
                {
                    Label = Enum.GetName(typeof(T), itemType),
                    Value = (int)itemType
                });
            }
            return values;
        }
    }
}
