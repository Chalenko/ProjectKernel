using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProjectKernel.Classes.Exporter
{
    public class SimpleExcelExporter : AExcelExporter
    {
        public SimpleExcelExporter(string fileName) : base(fileName) { }

        protected override void PrintHeader(IDataAccessor accessor)
        {
            accessor.PrintHeader(this);

            verticalOffset++;
        }

        protected override void PrintContent(IDataAccessor accessor)
        {
            accessor.PrintContent(this);
            verticalOffset += accessor.RowCount();
        }

        public override void PrintCell(int row, int column, string value)
        {
            Excel.Range cell = getCell(row, column);
            cell.NumberFormat = "@";
            //setCellFormat(cell);
            base.PrintCell(cell, value);
        }
    }
}
