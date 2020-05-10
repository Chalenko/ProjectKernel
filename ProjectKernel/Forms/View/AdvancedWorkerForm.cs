using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Forms
{
    /// <summary>
    /// Форма для выполнения фоновых операций
    /// </summary>
    public abstract partial class AdvancedWorkerForm : Form
    {
        /// <summary>
        /// Делегат фукции, выполняющийся асинхронно
        /// </summary>
        private AsyncFunctionDelegate function = null;

        private AdvancedWorkerForm()
        {
            InitializeComponent();
            setProgressBarProperties();
            foreach (Control c in Controls)
                c.KeyDown += AdvancedWorkerForm_KeyDown;
        }

        /// <summary>
        /// Инициализирует новую форму для выполнения асинхронных опреаций по параметрам
        /// </summary>
        /// <param name="func">Делегат фукции, выполняющейся в фоновом режиме</param>
        protected AdvancedWorkerForm(AsyncFunctionDelegate func) : this()
        {
            function = func;
        }

        /// <summary>
        /// Инициализирует новую форму для выполнения асинхронных опреаций по параметрам
        /// </summary>
        /// <param name="title">Заголовок окна</param>
        /// <param name="func">Делегат фукции, выполняющейся в фоновом режиме</param>
        protected AdvancedWorkerForm(string title, AsyncFunctionDelegate func) : this (func)
        {
            this.Text = title;
        }

        /// <summary>
        /// Настройка свойств ProgressBar
        /// </summary>
        protected abstract void setProgressBarProperties();
        
        private void AdvancedWorkerForm_Load(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
            //bw.RunWorkerAsync();
        }

        /// <summary>
        /// Обработка события Нажатие клавиши клавиатуры на AdvancedWorkerForm
        /// </summary>
        /// <param name="sender">Отправитель события</param>
        /// <param name="e">Аргументы события</param>
        private void AdvancedWorkerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel.PerformClick();
            }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы уверены что хотите отменить выполнение операции?", "Отмена операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                this.backgroundWorker.CancelAsync();
                this.Close();
                //this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                //if (bw.IsBusy)
                //    bw.Abort();
                //throw new OperationCanceledException();
                //this.Close();//backgroundWorker.CancelAsync();
            }
        }
    }

    /// <summary>
    /// Форма для фоновых операций с маркированным ProgressBar
    /// </summary>
    public class AdvancedWaitForm : AdvancedWorkerForm
    {
        /// <summary>
        /// Инициализирует новую форму для выполнения асинхронных опреаций по параметрам
        /// </summary>
        /// <param name="func">Делегат фукции, выполняющейся в фоновом режиме</param>
        public AdvancedWaitForm(AsyncFunctionDelegate func) : base(func) { }

        /// <summary>
        /// Инициализирует новую форму для выполнения асинхронных опреаций по параметрам
        /// </summary>
        /// <param name="title">Заголовок окна</param>
        /// <param name="func">Делегат фукции, выполняющейся в фоновом режиме</param>
        public AdvancedWaitForm(string title, AsyncFunctionDelegate func) : base(title, func) { }

        /// <summary>
        /// Задаем маркированный ProgressBar
        /// </summary>
        protected override void setProgressBarProperties()
        {
            pb.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
        }
    }

    /// <summary>
    /// Форма для фоновых операций с континуальным ProgressBar
    /// </summary>
    public class AdvancedProgressForm : AdvancedWorkerForm
    {
        /// <summary>
        /// Инициализирует новую форму для выполнения асинхронных опреаций по параметрам
        /// </summary>
        /// <param name="func">Делегат фукции, выполняющейся в фоновом режиме</param>
        public AdvancedProgressForm(AsyncFunctionDelegate func) : base(func) { }

        /// <summary>
        /// Инициализирует новую форму для выполнения асинхронных опреаций по параметрам
        /// </summary>
        /// <param name="title">Заголовок окна</param>
        /// <param name="func">Делегат фукции, выполняющейся в фоновом режиме</param>
        public AdvancedProgressForm(string title, AsyncFunctionDelegate func) : base(title, func) { }

        /// <summary>
        /// Задаем континуальный ProgressBar
        /// </summary>
        protected override void setProgressBarProperties()
        {
            pb.Style = ProgressBarStyle.Continuous;
        }
    }
}
