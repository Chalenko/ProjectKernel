using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace ProjectKernel.Controls
{
    /// <summary>
    /// Таблица с добавлением, удалением и просмотром записей.
    /// </summary>
    public partial class TableControl : UserControl
    {
        private bool canEdit = true;
        private bool readOnly = false;

        /// <summary>
        /// Элемент доступен только на чтение
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), System.ComponentModel.DefaultValue(false)]
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                CanEdit = !value;
                dgvTable.ReadOnly = value;
                tsmiAdd.Enabled = !value;
                csmiAdd.Enabled = !value;
                tsmiRemove.Enabled = !IsEmpty && !value;
                csmiRemove.Enabled = !IsEmpty && !value;
                tsmiOpen.Enabled = !IsEmpty && !value;
                csmiOpen.Enabled = !IsEmpty && !value;
            }
        }

        /// <summary>
        /// Происходит при щелчке мышью элемента управления Add.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler AddClick = new EventHandler((sender, e) => { });

        /// <summary>
        /// Происходит при щелчке мышью элемента управления Remove.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler RemoveClick = new EventHandler((sender, e) => { });

        /// <summary>
        /// Происходит при щелчке мышью элемента управления Open.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler OpenClick = new EventHandler((sender, e) => { });

        /// <summary>
        /// Возвращает или задает источник данных, для которого объект System.Windows.Forms.DataGridView отображает данные.
        /// </summary>
        /// <returns>
        /// Объект, содержащий данные, отображаемые для объекта System.Windows.Forms.DataGridView.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Произошла ошибка в источнике данных, и либо отсутствует обработчик для события System.Windows.Forms.DataGridView.DataError, либо обработчик задал для свойства System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException значение true.Обычно объект исключения может быть приведен к типу System.FormatException.
        /// </exception>
        public object DataSource
        {
            get { return dgvTable.DataSource; }
            set 
            { 
                dgvTable.DataSource = value;
                dgvTable.Refresh();
                dgvTable.Update();
            }
        }

        /// <summary>
        /// Таблица даннных пуста
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !(dgvTable.Rows.Count > 0);
            }
        }

        /// <summary>
        /// Возвращает объект System.Windows.Forms.DataGridView, который отображает данные.
        /// </summary>
        /// <returns>
        /// Объект System.Windows.Forms.DataGridView.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Произошла ошибка в источнике данных, и либо отсутствует обработчик для события System.Windows.Forms.DataGridView.DataError, либо обработчик задал для свойства System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException значение true.Обычно объект исключения может быть приведен к типу System.FormatException.
        /// </exception>
        public System.Windows.Forms.DataGridView DataGridView
        {
            get { return dgvTable; }
        }

        /// <summary>
        /// Возвращает объект System.Windows.Forms.ToolStripMenuItem, соответствующий кнопке добавления.
        /// </summary>
        /// <returns>
        /// Объект System.Windows.Forms.ToolStripMenuItem.
        /// </returns>
        public System.Windows.Forms.ToolStripMenuItem AddButton
        {
            get { return tsmiAdd; }
        }

        /// <summary>
        /// Возвращает объект System.Windows.Forms.ToolStripMenuItem, соответствующий кнопке изменения.
        /// </summary>
        /// <returns>
        /// Объект System.Windows.Forms.ToolStripMenuItem.
        /// </returns>
        public System.Windows.Forms.ToolStripMenuItem OpenButton
        {
            get { return tsmiOpen; }
        }

        /// <summary>
        /// Возвращает объект System.Windows.Forms.ToolStripMenuItem, соответствующий кнопке удаления.
        /// </summary>
        /// <returns>
        /// Объект System.Windows.Forms.ToolStripMenuItem.
        /// </returns>
        public System.Windows.Forms.ToolStripMenuItem RemoveButton
        {
            get { return tsmiRemove; }
        }

        /// <summary>
        /// Текст, сопоставленный с элементом добавления записей.
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string AddMenuItemText
        {
            get { return tsmiAdd.Text; }
            set
            {
                tsmiAdd.Text = value;
                csmiAdd.Text = value;
            }
        }

        /// <summary>
        /// Текст, сопоставленный с элементом удаления записей.
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string RemoveMenuItemText
        {
            get { return tsmiRemove.Text; }
            set
            {
                tsmiRemove.Text = value;
                csmiRemove.Text = value;
            }
        }

        /// <summary>
        /// Текст, сопоставленный с элементом открытия записей.
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string OpenMenuItemText
        {
            get { return tsmiOpen.Text; }
            set
            {
                tsmiOpen.Text = value;
                csmiOpen.Text = value;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), System.ComponentModel.DefaultValue(true)]
        public bool CanEdit
        {
            get { return canEdit; }
            set
            {
                canEdit = value;
                tsmiAdd.Enabled = value;
                csmiAdd.Enabled = value;
                tsmiRemove.Enabled = !IsEmpty && value;
                csmiRemove.Enabled = !IsEmpty && value;
                tsmiOpen.Enabled = !IsEmpty && value;
                csmiOpen.Enabled = !IsEmpty && value;
            }
        }

        /// <summary>
        /// Коструктор по умолчанию
        /// </summary>
        public TableControl()
        {
            InitializeComponent();
            //tsmiAdd.EnabledChanged += EnabledChanged;
            tsmiOpen.EnabledChanged += EnabledChanged;
            tsmiRemove.EnabledChanged += EnabledChanged;
        }

        new void EnabledChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.Enabled &= !IsEmpty;
        }

        private void dgvTable_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvTable.HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
            {
                dgvTable.CurrentCell = dgvTable[0, hit.RowIndex];
            }
            else
            {
                dgvTable.CurrentCell = null;
            }
        }

        private void dgvTable_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvTable.CurrentCell != null)
            {
                csmiRemove.Enabled = !IsEmpty && CanEdit && true;
                tsmiRemove.Enabled = !IsEmpty && CanEdit && true;
                csmiOpen.Enabled = !IsEmpty && CanEdit && true;
                tsmiOpen.Enabled = !IsEmpty && CanEdit && true;
            }
            else
            {
                csmiRemove.Enabled = false;
                tsmiRemove.Enabled = false;
                csmiOpen.Enabled = false;
                tsmiOpen.Enabled = false;
            }
        }

        private void dgvTable_DataSourceChanged(object sender, EventArgs e)
        {
            this.CanEdit = true;
            //tsmiRemove.Enabled = !IsEmpty;
            //csmiRemove.Enabled = !IsEmpty;
            //tsmiOpen.Enabled = !IsEmpty;
            //csmiOpen.Enabled = !IsEmpty;
        }

        private void csmiAdd_Click(object sender, EventArgs e)
        {
            tsmiAdd.PerformClick();
        }

        private void csmiRemove_Click(object sender, EventArgs e)
        {
            tsmiRemove.PerformClick();
        }

        private void csmiOpen_Click(object sender, EventArgs e)
        {
            tsmiOpen.PerformClick();
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            AddClick.Invoke(sender, e);
        }

        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            RemoveClick.Invoke(sender, e);
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            OpenClick.Invoke(sender, e);
        }

        private void dgvTable_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvTable.HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.Cell || hit.Type == DataGridViewHitTestType.RowHeader)
            {
                tsmiOpen.PerformClick();
            }
        }

        private void dgvTable_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows != null)
            {
                csmiRemove.Enabled = !IsEmpty && CanEdit && true;
                tsmiRemove.Enabled = !IsEmpty && CanEdit && true;
                csmiOpen.Enabled = !IsEmpty && CanEdit && true;
                tsmiOpen.Enabled = !IsEmpty && CanEdit && true;
            }
            else
            {
                csmiRemove.Enabled = false;
                tsmiRemove.Enabled = false;
                csmiOpen.Enabled = false;
                tsmiOpen.Enabled = false;
            }
        }
    }
}
