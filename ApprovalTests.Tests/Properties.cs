using System;
using System.Collections;
using System.Text;

namespace Example.Services.Tests
{
    public class Properties
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
                sb.AppendFormat("{0} = {1}", prefix, ValueToString(items));
                return;
            }

            var list = new ArrayList();

            foreach (var item in items)
            {
                list.Add(item);
            }

            sb.AppendFormat(
                "{0}{1}Count = {2}{3}",
                prefix,
                string.IsNullOrEmpty(prefix) ? string.Empty : ".",
                list.Count,
                Environment.NewLine);

            for (var i = 0; i < list.Count; i++)
            {
                OfItem(sb, string.Format("{0}[{1}]", prefix, i), list[i]);
            }
        }

        private static void OfItem<T>(StringBuilder sb, string prefix, T item)
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                {
                    sb.AppendFormat("{0}.{1} = {2}{3}", prefix, prop.Name, ValueToString(prop.GetValue(item)), Environment.NewLine);
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
