using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinPlanUZD.Classes.Exporter
{
    class SimpleExcelExporter : AExcelExporter
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
            //setCellFormat(cell);
            base.PrintCell(cell, value);
        }

        private void setCellFormat(Excel.Range cell)
        {
            cell.NumberFormat = "@";
            //cell.Borders.Color = 2;
            cell.Font.Name = "Times New Roman";
            cell.Font.Size = 8;
            cell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            cell.Rows.AutoFit();
            cell.WrapText = true;
        }
    }
}
