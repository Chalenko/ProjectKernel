using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProjectKernel.Forms.ViewModel
{
    public class SelectFormViewModel
    {
        public DataTable table;// = new DataTable();
        public System.Windows.Forms.DataGridViewCell CurrentCell { get; set; }
        public Guid SelectedObjectId { get; set; }
        public bool OKIsEnabled { get; set; }

        public SelectFormViewModel(DataTable viewTable)
        {
            table = viewTable;
        }

        public void SelectionChanged()
        {
            if (SelectedObjectId != null) OKIsEnabled = true; else OKIsEnabled = false;
        }
    }
}
