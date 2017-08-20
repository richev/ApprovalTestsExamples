using System;
using System.Collections;
using System.Text;

namespace Example.Services.Tests
{
    /// <summary>
    /// Helper methods for returning string representations of the properties of objects.
    /// </summary>
    public static class Properties
    {
        /// <summary>
        /// Recursively gets all of the properties and their values for all of the itemsin the list.
        /// </summary>
        public static string OfList(IEnumerable items)
        {
            var sb = new StringBuilder();

            OfList(sb, string.Empty, items);

            return sb.ToString();
        }

        private static void OfList(StringBuilder sb, string prefix, IEnumerable items)
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

            sb.AppendLine(string.Format(
                "{0}{1}Count = {2}",
                prefix,
                string.IsNullOrEmpty(prefix) ? string.Empty : ".",
                list.Count));

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];

                if (IsValueOrString(item.GetType()))
                {
                    sb.AppendLine(string.Format("{0}[{1}] = {2}", prefix, i, ValueToString(item)));
                }
                else
                {
                    OfItem(sb, string.Format("{0}[{1}]", prefix, i), item);
                }
            }
        }

        private static void OfItem<T>(StringBuilder sb, string prefix, T item)
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                if (IsValueOrString(prop.PropertyType))
                {
                    sb.AppendLine(string.Format("{0}.{1} = {2}", prefix, prop.Name, ValueToString(prop.GetValue(item))));
                }
                else if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                {
                    OfList(sb, string.Format("{0}.{1}", prefix, prop.Name), prop.GetValue(item) as IList);
                }
                else
                {
                    OfItem(sb, string.Format("{0}.{1}", prefix, prop.Name), prop.GetValue(item));
                }
            }
        }

        private static bool IsValueOrString(Type type)
        {
            return type.IsValueType || type == typeof(string);
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
