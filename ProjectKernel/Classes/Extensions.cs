using ProjectKernel.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        public static bool IsNullOrWhitespace(this string text)
        {
            return text == null || text.Trim() == "";
        }

        public static string ListToString(this System.Collections.IList list)
        {
            StringBuilder result = new StringBuilder("");

            if (list.Count > 0)
            {
                result.Append(list[0].ToString());
                for (int i = 1; i < list.Count; i++)
                    result.AppendFormat("; {0}", list[i].ToString());
                //result.Append(".");
            }
            return result.ToString();
        }

        public static object[] PropertiesToArray(this object obj)
        {
            object[] result = new object[obj.GetType().GetProperties().Count()];

            int i = 0;
            foreach (var item in obj.GetType().GetProperties())
	        {
                result[i++] = item.GetValue(obj, null);
	        }

            return result;
        }

        public static System.Data.DataTable ChildsAsDataTable(this ExtendedTreeNode node)
        {
            System.Data.DataTable result = new System.Data.DataTable();
            result.Columns.Add("Text");
            Type nodeValueType = node.Value.GetType();

            foreach (var property in nodeValueType.GetProperties())
            {
                result.Columns.Add(property.Name);
            }

            int i = 0;
            foreach (ExtendedTreeNode child in node.Nodes)
            {
                result.Rows.Add(result.NewRow());
                result.Rows[i][0] = child.Text;
                int j = 1;
                foreach (var item in child.Value.PropertiesToArray())
                {
                    result.Rows[i][j++] = item;
                }
                i++;
            }
            
            return result;
        }

        public static System.Data.DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            Type t = typeof(T);
            System.Reflection.PropertyInfo[] pia = t.GetProperties();
            //Create the columns in the DataTable
            foreach (System.Reflection.PropertyInfo pi in pia)
            {
                if ((pi.PropertyType.IsGenericType) )
                {
                    Type typeOfColumn = pi.PropertyType.GetGenericArguments()[0];
                    dt.Columns.Add(pi.Name, typeOfColumn);
                }
                else
                    dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            //Populate the table
            foreach (T item in collection)
            {
                System.Data.DataRow dr = dt.NewRow();
                dr.BeginEdit();
                foreach (System.Reflection.PropertyInfo pi in pia)
                {
                    dr[pi.Name] = pi.GetValue(item, null);
                }
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
