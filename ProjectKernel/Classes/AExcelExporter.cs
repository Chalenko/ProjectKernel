using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace FinPlanUZD.Classes.Exporter
{
    abstract class AExcelExporter : AFileExporter
    {
        private Excel.Application excelApp;
        private Excel.Workbook excelAppWorkbook;
        private Excel.Worksheet excelWorksheet;
        //protected Excel.Range excelCells;

        protected int verticalOffset = 0;
        //protected DataSet ds;

        public AExcelExporter(string fileName) : base (fileName) { }

        protected override void OpenApp()
        {
            excelApp = new Excel.Application();
        }

        protected override void PrepareApp()
        {
            excelApp.SheetsInNewWorkbook = 1;
            excelApp.Workbooks.Add();

            excelAppWorkbook = excelApp.Workbooks[1];
            excelWorksheet = excelAppWorkbook.Worksheets.get_Item(1);

            excelAppWorkbook.SaveAs(FileName);
            excelWorksheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
        }

        protected override void ShowApp()
        {
//#if !DEBUG
            excelApp.Visible = true;
//#endif
            excelApp.DisplayAlerts = true;
        }

        protected override void SaveFile()
        {
            excelAppWorkbook.Save();
        }

        protected override void CloseApp()
        {
            excelApp.DisplayAlerts = false;
            excelAppWorkbook.Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            excelApp = null;
            excelAppWorkbook = null;
            excelWorksheet = null;
            System.GC.Collect();
        }

        protected override void PrintData(IDataAccessor accessor)
        {
            PrintHeader(accessor);
            PrintContent(accessor);
            PrintFooter(accessor);
        }

        protected abstract void PrintHeader(IDataAccessor accessor);
        protected abstract void PrintContent(IDataAccessor accessor);
        protected virtual void PrintFooter(IDataAccessor accessor) { }// = 0;

        public void PrintHeaderBorder(int columnCount)
        {
            string lu = "";
            string rd = "";

            lu = getTextNameCell(0, 0);
            rd = getTextNameCell(1, columnCount - 1);
            Excel.Range cells = (Excel.Range)excelWorksheet.get_Range(lu, rd);
            //setCellFormat();
        }

        public virtual void PrintCell(int row, int column, string value)
        {
            Excel.Range cell = (Excel.Range)excelWorksheet.Cells[verticalOffset + row + 1, column + 1];
            PrintCell(cell, value);
        }

        public virtual void PrintCell(Excel.Range cell, string value)
        {
            cell.Value2 = value;
        }

        protected string getTextNameCell(int row, int col)
        {
            string res = "";
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            res += alphabet[col];
            res += row + 1; //Excels rows starts with 1

            return res;
        }

        protected Excel.Range getCell(int row, int column)
        {
            return (Excel.Range)excelWorksheet.Cells[verticalOffset + row + 1, column + 1];
        }
    }
}
