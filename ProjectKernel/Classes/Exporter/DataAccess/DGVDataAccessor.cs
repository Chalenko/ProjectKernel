using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProjectKernel.Classes.Exporter
{
    public class DGVDataAccessor : IDataAccessor
    {
        private DataGridView dgv;
        private int columnCount;
        private int rowCount;

        public DGVDataAccessor(DataGridView dgv)
        {
            this.dgv = dgv;
            columnCount = dgv.Columns.Cast<DataGridViewColumn>().Where<DataGridViewColumn>(x => x.Visible == true).Count();
            rowCount = dgv.Rows.Cast<DataGridViewRow>().Where<DataGridViewRow>(x => x.State.HasFlag(DataGridViewElementStates.Visible)).Count();
        }

        public int ColumnCount()
        {
            return columnCount;
        }
        public int RowCount()
        {
            return rowCount;
        }

        public void PrintHeader(AExcelExporter exporter)
        {
            int col = 0;
            foreach (DataGridViewColumn c in dgv.Columns.Cast<DataGridViewColumn>().Where<DataGridViewColumn>(x => x.Visible == true))
            {
                exporter.PrintCell(0, col, c.HeaderText);
                col++;
            }

            exporter.PrintHeaderBorder(ColumnCount());
        }

        public void PrintContent(AExcelExporter exporter)
        {
            IEnumerable<DataGridViewRow> visibleRows = dgv.Rows.Cast<DataGridViewRow>().Where(x => x.State.HasFlag(DataGridViewElementStates.Visible));
            /*
            int procCount = (int)(Math.Ceiling(Environment.ProcessorCount / 2.0));
            List<Thread> thr = new List<Thread>(procCount);
            int range = visibleRows.Count() / procCount;
            for (int i = 0; i < procCount; i++)
            {
                int start = i * range;
                int end = (i == procCount - 1) ? visibleRows.Count() : start + range;
                thr.Add(new Thread(() =>
                {
                    for (int row = start; row < end; row++)
                    {
                        int col = 0;
                        IEnumerable<DataGridViewColumn> visibleColumns = dgv.Columns.Cast<DataGridViewColumn>().Where(x => x.Visible == true);
                        foreach (DataGridViewColumn c in visibleColumns)
                        {
                            string value = dgv[c.Name, row].ValueType == typeof(DateTime) ? ((DateTime)(dgv[c.Name, row].Value)).Date.ToShortDateString() : dgv[c.Name, row].Value.ToString();
                            exporter.PrintCell(row, col, value);
                            col++;
                            if (exporter.bgw != null && exporter.bgw.CancellationPending)
                                return;
                        }
                        //if (exporter.bgw != null)
                        //exporter.bgw.ReportProgress((int)(100.0 * row / RowCount()), string.Format("{0}%", (int)(100.0 * row / RowCount())));
                        //row++;
                    }
                }));
            }
            foreach (var thread in thr) thread.Start();
            foreach (var thread in thr) thread.Join();
             * */
            int row = 0;
            foreach (DataGridViewRow r in visibleRows)
            {
                int col = 0;
                IEnumerable<DataGridViewColumn> visibleColumns = dgv.Columns.Cast<DataGridViewColumn>().Where(x => x.Visible == true);
                foreach (DataGridViewColumn c in visibleColumns)
                {
                    string value = (dgv[c.Name, r.Index].ValueType == typeof(DateTime) && dgv[c.Name, r.Index].Value.ToString() != "") ? ((DateTime)(dgv[c.Name, r.Index].Value)).Date.ToShortDateString() : dgv[c.Name, r.Index].Value.ToString();
                    exporter.PrintCell(row, col, value);
                    col++;
                    if (exporter.bgw != null && exporter.bgw.CancellationPending)
                        return;
                }
                if (exporter.bgw != null)
                    exporter.bgw.ReportProgress((int)(100.0 * row / RowCount()), string.Format("{0}%", (int)(100.0 * row / RowCount())));
                row++;
            }
        }
    }
}
