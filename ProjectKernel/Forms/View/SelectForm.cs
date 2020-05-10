using ProjectKernel.Forms.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Forms.View
{
    public partial class SelectForm : Form
    {
        private SelectFormViewModel viewModel;
        public DataGridView DGVData { get { return dgvData; } }

        public Guid SelectedId { get { return viewModel.SelectedObjectId; } }
        
        public SelectForm(SelectFormViewModel vm)
        {
            InitializeComponent();
            this.viewModel = vm;
            backBind();
            dgvData.Columns["id"].Visible = false;
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            bind();
            viewModel.SelectionChanged();
            backBind();
        }

        private void dgvData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bind();
            btnOK.PerformClick();
        }

        private void bind()
        {
            viewModel.SelectedObjectId = Guid.Parse(dgvData.CurrentRow.Cells["id"].Value.ToString());
            viewModel.CurrentCell = dgvData.CurrentCell;
        }

        private void backBind()
        {
            dgvData.DataSource = viewModel.table;
            dgvData.CurrentCell = viewModel.CurrentCell;
            //int position = viewModel.table.Rows.Find("id", viewModel.SelectedObjectId);
            //if (position != -1)
            //{
            //    dgvData.CurrentCell = dgvData["id", position];
            //}
            btnOK.Enabled = viewModel.OKIsEnabled;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
