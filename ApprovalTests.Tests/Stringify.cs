using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Example.Services.Tests
{
    /// <summary>
    /// Methods for returning string representations of lists and objects, recursively looping
    /// over their properties.
    /// </summary>
    public static class Stringify
    {
        /// <summary>
        /// Gets a string that represents the list of items.
        /// </summary>
        public static string Items(IEnumerable items)
        {
            var sb = new StringBuilder();

            Items(sb, string.Empty, items);

            return sb.ToString();
        }

        /// <summary>
        /// Gets a string that represents the item.
        /// </summary>
        public static string Item(object item)
        {
            var sb = new StringBuilder();

            Item(sb, string.Empty, item);

            return sb.ToString();
        }

        private static void Items(StringBuilder sb, string prefix, IEnumerable items)
        {
            if (items == null)
            {
                sb.AppendLine($"{prefix} = {ValueToString(items)}");
                return;
            }

            var list = EnumerableToList(items);

            sb.AppendLine($"{DottedPrefix(prefix)}Count = {list.Count}");

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];

                if (item.GetType().IsGenericType && item.GetType().GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                {
                    var key = GetPropertyValue(item, "Key");
                    var value = GetPropertyValue(item, "Value");

                    Item(sb, $"{prefix}[{i}].Key", key);
                    Item(sb, $"{prefix}[{i}].Value", value);
                }
                else if (IsValueOrString(item.GetType()))
                {
                    sb.AppendLine($"{prefix}[{i}] = {ValueToString(item)}");
                }
                else
                {
                    Item(sb, $"{prefix}[{i}]", item);
                }
            }
        }

        private static void Item(StringBuilder sb, string prefix, object item)
        {
            if (item == null)
            {
                sb.AppendLine(ValueToString(item));
            }
            else if (IsValueOrString(item.GetType()))
            {
                sb.AppendLine($"{prefix} = {ValueToString(item)}");
            }
            else
            {
                foreach (var prop in item.GetType().GetProperties(BindingFlags.Public |
                                                                  BindingFlags.NonPublic |
                                                                  BindingFlags.Instance))
                {
                    if (IsValueOrString(prop.PropertyType))
                    {
                        sb.AppendLine($"{DottedPrefix(prefix)}{prop.Name} = {ValueToString(prop.GetValue(item))}");
                    }
                    else if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                    {
                        Items(sb, $"{DottedPrefix(prefix)}{prop.Name}", prop.GetValue(item) as IList);
                    }
                    else
                    {
                        Item(sb, $"{DottedPrefix(prefix)}{prop.Name}", prop.GetValue(item));
                    }
                }
            }
        }

        /// <summary>
        /// So that we can report the count of items in our tests.
        /// </summary>
        private static IList EnumerableToList(IEnumerable items)
        {
            var list = new ArrayList();

            foreach (var item in items)
            {
                list.Add(item);
            }

            return list;
        }

        private static object GetPropertyValue(object item, string propertyName)
        {
            var property = item.GetType().GetProperty(propertyName);

            if (property == null)
            {
                throw new Exception(
                    $"Could not find a property named '{propertyName}' on object of type {item.GetType().FullName}.");
            }

            var value = property.GetValue(item);

            return value;
        }

        private static bool IsValueOrString(Type type)
        {
            return type.IsValueType || type == typeof(string);
        }

        private static string DottedPrefix(string prefix)
        {
            return string.IsNullOrEmpty(prefix) ? string.Empty : $"{prefix}.";
        }

        private static string ValueToString(object value)
        {
            if (value == null)
            {
                return "<null>";
            }

            var type = value.GetType();

            if (type == typeof(string))
            {
                return $"'{value}'";
            }
            if (type == typeof(Guid))
            {
                return $"{{{value}}}";
            }
            if (type.IsEnum)
            {
                return $"{value} ({(int) value})";
            }

            return value.ToString();
        }
    }
}
