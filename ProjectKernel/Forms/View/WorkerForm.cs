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
    /// <summary>
    /// Делегат функции, выполняющейся в фоновом режиме WorkerForm
    /// </summary>
    /// <param name="bw">BackgroundWorker для выполнения фоновой операции</param>
    public delegate void AsyncFunctionDelegate(System.ComponentModel.BackgroundWorker bw);

    /// <summary>
    /// Форма для выполнения фоновых операций
    /// </summary>
    public abstract partial class WorkerForm : Form
    {
        private AsyncFunctionDelegate function = null;

        private WorkerForm()
        {
            InitializeComponent();
            setProgressBarProperties();
        }

        /// <summary>
        /// Инициализирует новую форму для выполнения асинхронных опреаций по параметрам
        /// </summary>
        /// <param name="func">Делегат фукции, выполняющейся в фоновом режиме</param>
        protected WorkerForm(AsyncFunctionDelegate func) : this()
        {
            function = func;
        }

        /// <summary>
        /// Настройка свойств ProgressBar
        /// </summary>
        protected abstract void setProgressBarProperties();

        private void WorkerForm_Load(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            function(this.backgroundWorker);
            if (backgroundWorker.CancellationPending) e.Cancel = true;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pb.Value = e.ProgressPercentage;
            lbl.Text = e.UserState.ToString();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            else if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
                DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
            else
                DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
    }

    /// <summary>
    /// Форма для фоновых операций с маркированным ProgressBar
    /// </summary>
    public class WaitForm : WorkerForm
    {
        /// <summary>
        /// Инициализирует новую форму по параметрам
        /// </summary>
        /// <param name="func">Делегат фукции, выполняющейся в фоновом режиме</param>
        public WaitForm(AsyncFunctionDelegate func) : base(func) { }

        /// <summary>
        /// Задаем маркированный ProgressBar
        /// </summary>
        protected override void setProgressBarProperties()
        {
            pb.Style = ProgressBarStyle.Marquee;
        }
    }

    /// <summary>
    /// Форма для фоновых операций с континуальным ProgressBr
    /// </summary>
    public class ProgressForm : WorkerForm
    {
        /// <summary>
        /// Инициализирует новую форму по параметрам
        /// </summary>
        /// <param name="func">Делегат фукции, выполняющейся в фоновом режиме</param>
        public ProgressForm(AsyncFunctionDelegate func) : base(func) { }

        /// <summary>
        /// Задаем континуальный ProgressBar
        /// </summary>
        protected override void setProgressBarProperties()
        {
            pb.Style = ProgressBarStyle.Continuous;
        }
    }
}
