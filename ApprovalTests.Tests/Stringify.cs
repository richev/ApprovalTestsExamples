﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Example.Services.Tests
{
    /// <summary>
    /// Helper methods for returning string representations of the properties of objects.
    /// </summary>
    public static class Stringify
    {
        /// <summary>
        /// Recursively gets all of the properties and their values for all of the items.
        /// </summary>
        public static string Items(IEnumerable items)
        {
            var sb = new StringBuilder();

            Items(sb, string.Empty, items);

            return sb.ToString();
        }

        private static void Items(StringBuilder sb, string prefix, IEnumerable items)
        {
            if (items == null)
            {
                sb.AppendLine(string.Format("{0} = {1}", prefix, ValueToString(items)));
                return;
            }

            var list = new ArrayList();

            foreach (var item in items)
            {
                list.Add(item);
            }

            sb.AppendLine(string.Format("{0}Count = {1}", DottedPrefix(prefix), list.Count));

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];

                if (item.GetType().IsGenericType && item.GetType().GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                {
                    var key = item.GetType().GetProperty("Key").GetValue(item);
                    var value = item.GetType().GetProperty("Value").GetValue(item);

                    Item(sb, string.Format("{0}[{1}].Key", prefix, i), key);
                    Item(sb, string.Format("{0}[{1}].Value", prefix, i), value);
                }
                else if (IsValueOrString(item.GetType()))
                {
                    sb.AppendLine(string.Format("{0}[{1}] = {2}", prefix, i, ValueToString(item)));
                }
                else
                {
                    Item(sb, string.Format("{0}[{1}]", prefix, i), item);
                }
            }
        }

        /// <summary>
        /// Recursively gets all of the properties and their values for all of the item.
        /// </summary>
        public static string Item(object item)
        {
            var sb = new StringBuilder();

            Item(sb, string.Empty, item);

            return sb.ToString();
        }

        private static void Item(StringBuilder sb, string prefix, object item)
        {
            if (IsValueOrString(item.GetType()))
            {
                sb.AppendLine(string.Format("{0} = {1}", prefix, ValueToString(item)));
            }
            else
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    if (IsValueOrString(prop.PropertyType))
                    {
                        sb.AppendLine(string.Format("{0}{1} = {2}", DottedPrefix(prefix), prop.Name, ValueToString(prop.GetValue(item))));
                    }
                    else if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                    {
                        Items(sb, string.Format("{0}{1}", DottedPrefix(prefix), prop.Name), prop.GetValue(item) as IList);
                    }
                    else
                    {
                        Item(sb, string.Format("{0}{1}", DottedPrefix(prefix), prop.Name), prop.GetValue(item));
                    }
                }
            }
        }

        private static bool IsValueOrString(Type type)
        {
            return type.IsValueType || type == typeof(string);
        }

        private static string DottedPrefix(string prefix)
        {
            return string.IsNullOrEmpty(prefix) ? string.Empty : string.Format("{0}.", prefix);
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
                return string.Format("'{0}'", value);
            }
            if (type == typeof(Guid))
            {
                return string.Format("{{{0}}}", value);
            }
            if (type.IsEnum)
            {
                return string.Format("{0} ({1})", value, (int)value);
            }

            return value.ToString();
        }
    }
}
