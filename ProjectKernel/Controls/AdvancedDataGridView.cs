using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Controls
{
    public partial class AdvancedDataGridView : DataGridView
    {
        private List<string> _sortOrderList = new List<string>();
        private List<string> _filterOrderList = new List<string>();
        private List<string> _filteredColumns = new List<string>();

        private bool _loadedFilter = false;
        private string _sortString = null;
        private string _filterString = null;
        private bool _filterAndSortEnabled = true;

        public AdvancedDataGridView()
        {
            InitializeComponent();
        }

        public bool FilterAndSortEnabled { get { return _filterAndSortEnabled; } set { _filterAndSortEnabled = value; } }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }


}
