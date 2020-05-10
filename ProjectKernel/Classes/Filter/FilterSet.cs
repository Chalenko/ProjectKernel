using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Filter
{
    /*
    public struct FilterRecord
    {
        public string DataPropertyName;
        public string FilterString;
        public FilterType FilterType;

        public FilterRecord(string column, string filter, ADGVFilterType filterType)
        {
            DataPropertyName = column;
            FilterString = filter;
            FilterType = filterType;
        }
    }
    */

    public struct SortRecord
    {
        public string columnName;
        //public SortType SortType;
        //public string SortString;

        public SortRecord(string column)//(string column, string sort, SortType sortType)
        {
            columnName = column;
            //SortString = sort;
            //SortType = sortType;
        }
    }

    public class ADGVSortSet : List<SortRecord>
    {
        /*
        public void Add(ColumnHeaderCell cell)
        {
            if (cell != null && cell.OwningColumn != null)
            {
                this.RemoveAll(r => r.DataPropertyName == cell.OwningColumn.DataPropertyName);
                this.Add(new ADGVSortRecord(cell.OwningColumn.DataPropertyName, cell.SortString, cell.ActiveSortType));
            }
        }
        */

        public void Remove(string columnName)
        {
            this.RemoveAll(r => r.columnName == columnName);
        }

        /*
        public void Remove(ColumnHeaderCell cell)
        {
            if (cell != null && cell.OwningColumn != null)
                this.Remove(cell.OwningColumn.DataPropertyName);
        }
        */

        /*
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");

            foreach (SortRecord r in this)
            {
                sb.AppendFormat(r.SortString + ", ", r.DataPropertyName);
            }

            if (sb.Length > 4)
                sb.Length -= 4;

            return sb.ToString();
        }
        */
    }
}
