using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Exporter
{
    public interface IDataAccessor
    {
        int ColumnCount();
        int RowCount();
        void PrintHeader(AExcelExporter exporter);
        void PrintContent(AExcelExporter exporter);
    }
}
