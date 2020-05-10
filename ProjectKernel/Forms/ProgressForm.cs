using ProjectKernel.Delegates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectKernel.Forms
{
    public partial class ProgressForm : Form
    {
        public AsyncFunctionDelegate function = null;

        private ProgressForm()
        {
            InitializeComponent();
        }

        public ProgressForm(AsyncFunctionDelegate func) : this()
        {
            function = func;
        }
        
        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                function(this.progressBar, this.label);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Close();
            }
        }

        
    }
}
