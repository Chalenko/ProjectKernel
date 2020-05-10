using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectKernel.Classes.Comparer;

namespace ProjectKernel.Controls
{
    public enum FilterType : byte
    {
        None = 0,
        Custom,
        CheckList
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>https://adgv.codeplex.com/</remarks>
    public class HeaderContextMenuStrip : ContextMenuStrip
    {
        private FilterType activeFilterType = FilterType.None;
        private SortOrder activeSortOrder = SortOrder.None;
        //private Hashtable _textStrings = new Hashtable();

        //private TreeNodeItemSelector[] _startingNodes = null;
        //private TreeNodeItemSelector[] _filterNodes = null;
        //private string _sortString = null;
        //private string _filterString = null;
        private static Point resizeStartPoint = new Point(1, 1);
        private Point resizeEndPoint = new Point(-1, -1);

        //datetime conversion strings, derived from InvariantCulture
        //private const string ConvertYearMonthDayFormat = "dd/MM/yyyy";
        //private const string ConvertYearMonthDayHourMinuteSecondFormat = "dd/MM/yyyy HH:mm:ss";

        public HeaderContextMenuStrip() : this (typeof(string))
        {
            //InitializeComponent();
        }

        public HeaderContextMenuStrip(Type dataType) : base()
        {
            InitializeComponent();

            DataType = dataType;
            
            /*
            foreach (var item in Enum.GetValues(typeof(ProjectKernel.Classes.Comparer.SortType)))
            {
                tscbSortType.Items.Add(item);
            }
            */

            Type ourtype = typeof(ProjectKernel.Classes.Comparer.ComparerType);//typeof(ProjectKernel.Classes.Comparer.Comparer); // Базовый тип
            IEnumerable<Type> list = System.Reflection.Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype));

            foreach (Type itm in list)
            {
                //tscbSortType.Items.Add(itm.GetMethod("getDescription"));
            }
            tscbSortType.Items.Clear();

            if (DataType == typeof(DateTime))
            {
                tsmiSortAsc.Image = Properties.Resources.MenuStrip_OrderASCnum;
                tsmiSortDesc.Image = Properties.Resources.MenuStrip_OrderDESCnum;
                tscbSortType.Items.Add(ComparerType.Date.GetDescription());
                tscbSortType.Items.Add(ComparerType.Text.GetDescription());
            }
            else if (DataType == typeof(bool))
            {
                tsmiSortAsc.Image = Properties.Resources.MenuStrip_OrderASCbool;
                tsmiSortDesc.Image = Properties.Resources.MenuStrip_OrderDESCbool;
                tscbSortType.Items.Add(ComparerType.Boolean.GetDescription());
                tscbSortType.Items.Add(ComparerType.Text.GetDescription());
            }
            else if (DataType == typeof(Int32) || DataType == typeof(Int64) || DataType == typeof(Int16) ||
                DataType == typeof(UInt32) || DataType == typeof(UInt64) || DataType == typeof(UInt16) ||
                DataType == typeof(Byte) || DataType == typeof(SByte) || DataType == typeof(Decimal) ||
                DataType == typeof(Single) || DataType == typeof(Double))
            {
                tsmiSortAsc.Image = Properties.Resources.MenuStrip_OrderASCnum;
                tsmiSortDesc.Image = Properties.Resources.MenuStrip_OrderDESCnum;
                tscbSortType.Items.Add(ComparerType.Number.GetDescription());
                tscbSortType.Items.Add(ComparerType.Text.GetDescription());
            }
            else
            {
                tsmiSortAsc.Image = Properties.Resources.MenuStrip_OrderASCtxt;
                tsmiSortDesc.Image = Properties.Resources.MenuStrip_OrderDESCtxt;
                tscbSortType.Items.Add(ComparerType.Text.GetDescription());
                tscbSortType.Items.Add(ComparerType.Natural.GetDescription());
            }

            //tscbSortType.AutoSize = false;
            //tscbSortType.Width = tsmiSortAsc.Width;
            //tscbSortType.AutoSize = true;
            //customFilterLastFiltersListMenuItem.Enabled = DataType != typeof(bool);
            //customFilterLastFiltersListMenuItem.Checked = ActiveFilterType == FilterType.Custom;
            //MinimumSize = new Size(PreferredSize.Width, PreferredSize.Height);
            //resize
            //ResizeBox(MinimumSize.Width, MinimumSize.Height);
            //HeaderContextMenuStrip_Resize(this, null);
            
            //ResizeMenu(this.MinimumSize.Width, this.MinimumSize.Height);
            this.MinimumSize = this.PreferredSize;
            this.Size = this.PreferredSize;
            this.tscbSortType.Width = this.Width - 60;
            this.tschCheckListItems.Width = this.Width - 60;
        }

        private void InitializeComponent()
        {
            this.tscbSortType = new System.Windows.Forms.ToolStripComboBox();
            this.tsmiSortAsc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSortDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSortDrop = new System.Windows.Forms.ToolStripMenuItem();
            this.tssSortFilter = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFilterDrop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFilterCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddCustomFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.tssFilterCheck = new System.Windows.Forms.ToolStripSeparator();
            
            this.clItems = new System.Windows.Forms.TreeView();
            this.tschCheckListItems = new ToolStripControlHost(this.clItems);

            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.CheckFilterListPanel = new System.Windows.Forms.Panel();
            this.CheckFilterListButtonsPanel = new System.Windows.Forms.Panel();
            this.CheckFilterListButtonsControlHost = new System.Windows.Forms.ToolStripControlHost(this.CheckFilterListButtonsPanel);
            this.CheckFilterListControlHost = new System.Windows.Forms.ToolStripControlHost(this.CheckFilterListPanel);
            this.ResizeBoxControlHost = new System.Windows.Forms.ToolStripControlHost(new Control());//(this.ResizePictureBox);
            this.SuspendLayout();
            //
            // HeaderContextMenuStrip
            //
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            //this.AutoSize = false;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            //this.Size = new System.Drawing.Size(287, 340);//340
            //
            // tscbSortType
            //
            this.tscbSortType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tscbSortType.Name = "tscbSortType";
            this.tscbSortType.AutoSize = false;
            //this.tscbSortType.Size = new System.Drawing.Size(this.Width - 1, 22);
            //this.tscbSortType.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right)));
            this.tscbSortType.Sorted = true;
            this.tscbSortType.MouseLeave += new EventHandler(this.tscbSortType_MouseLeave);
            //this.tsmiSortAsc.Click += new System.EventHandler(this.tsmiSortAsc_Click);
            //this.tsmiSortAsc.MouseEnter += new System.EventHandler(this.tsmi);
            this.tscbSortType.ImageScaling = ToolStripItemImageScaling.None;
            //
            // tsmiSortAsc
            //
            this.tsmiSortAsc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiSortAsc.Name = "tsmiSortAsc";
            this.tsmiSortAsc.Text = "По возрастанию";
            //this.tsmiSortAsc.AutoSize = false;
            //this.tsmiSortAsc.Size = new System.Drawing.Size(this.Width - 1, 22);
            this.tsmiSortAsc.Click += new System.EventHandler(this.tsmiSortAsc_Click);
            //this.tsmiSortAsc.MouseEnter += new System.EventHandler(this.tsmi);
            this.tsmiSortAsc.ImageScaling = ToolStripItemImageScaling.None;
            // 
            // tsmiSortDesc
            // 
            this.tsmiSortDesc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiSortDesc.Name = "tsmiSortDesc";
            this.tsmiSortDesc.Text = "По убыванию";
            //this.tsmiSortDesc.AutoSize = false;
            //this.tsmiSortDesc.Size = new System.Drawing.Size(this.Width - 1, 22);
            this.tsmiSortDesc.Click += new System.EventHandler(this.tsmiSortDesc_Click);
            //this.tsmiSortDesc.MouseEnter += new System.EventHandler(this.tsmi);
            this.tsmiSortDesc.ImageScaling = ToolStripItemImageScaling.None;
            // 
            // tsmiSortDrop
            // 
            this.tsmiSortDrop.Enabled = false;
            this.tsmiSortDrop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiSortDrop.Name = "tsmiSortDrop";
            this.tsmiSortDrop.Text = "Удалить сортировку";
            //this.tsmiSortDrop.AutoSize = false;
            //this.tsmiSortDrop.Size = new System.Drawing.Size(this.Width - 1, 22);
            this.tsmiSortDrop.Click += new System.EventHandler(this.tsmiSortDrop_Click);
            this.tsmiSortDrop.ImageScaling = ToolStripItemImageScaling.None;
            // 
            // tssSortFilter
            // 
            this.tssSortFilter.Name = "tssSortFilter";
            //this.tssSortFilter.Size = new System.Drawing.Size(this.Width - 4, 6);
            // 
            // tsmiFilterDrop
            // 
            this.tsmiFilterDrop.Enabled = false;
            this.tsmiFilterDrop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiFilterDrop.Name = "tsmiFilterDrop";
            this.tsmiFilterDrop.Text = "Сбросить фильтр";
            //this.tsmiFilterDrop.AutoSize = false;
            //this.tsmiFilterDrop.Size = new System.Drawing.Size(this.Width - 1, 22);
            //this.tsmiFilterDrop.Click += new System.EventHandler(this.tsmiFilterDrop_Click);
            this.tsmiFilterDrop.ImageScaling = ToolStripItemImageScaling.None;
            // 
            // tsmiFilterCustom
            // 
            this.tsmiFilterCustom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiFilterCustom.Name = "tsmiFilterCustom";
            this.tsmiFilterCustom.Text = "Пользовательский фильтр";
            //this.tsmiFilterCustom.AutoSize = false;
            //this.tsmiFilterCustom.Size = new System.Drawing.Size(this.Width - 1, 22);
            this.tsmiFilterCustom.ImageScaling = ToolStripItemImageScaling.None;
            this.tsmiFilterCustom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddCustomFilter});
            // 
            // tsmiAddCustomFilter
            // 
            this.tsmiAddCustomFilter.Name = "tsmiAddCustomFilter";
            //this.tsmiAddCustomFilter.Size = new System.Drawing.Size(170, 22);
            this.tsmiAddCustomFilter.Text = "Добавить фильтр";
            // 
            // tssFilterCheck
            // 
            this.tssFilterCheck.Name = "tssFilterCheck";
            //this.tssFilterCheck.Size = new System.Drawing.Size(this.Width - 1, 6);
            //
            // clItems
            //
            this.clItems.Name = "clItems";
            //this.clItems.AutoSize = false;
            //this.clItems.Padding = new Padding(0);
            //this.clItems.Margin = new Padding(0);
            //this.clItems.Bounds = new Rectangle(4, 4, this.CheckFilterListPanel.Width - 8, this.CheckFilterListPanel.Height - 8);
            this.clItems.StateImageList = GetCheckImages();
            this.clItems.CheckBoxes = true;
            this.clItems.MouseLeave += new EventHandler(clItems_MouseLeave);
            this.clItems.MouseEnter += new EventHandler(clItems_MouseEnter);
            this.clItems.NodeMouseClick += new TreeNodeMouseClickEventHandler(clItems_NodeMouseClick);
            //this.clItems.KeyDown += new KeyEventHandler(CheckList_KeyDown);
            //this.clItems.MouseEnter += CheckList_MouseEnter;
            //this.clItems.NodeMouseDoubleClick += CheckList_NodeMouseDoubleClick;
            this.clItems.Dock = DockStyle.Fill;
            //this.clItems.Nodes.AddRange(new TreeNode[] {
            //new TreeNode("1"),
            //new TreeNode("2"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("3"),
            //new TreeNode("4")});
            this.clItems.Nodes.AddRange(new TripleTreeNode[] {
            new TripleTreeNode("1", 1),
            new TripleTreeNode("2", 2),
            new TripleTreeNode("3", 3),
            new TripleTreeNode("4", 4)});
            ((TripleTreeNode)(this.clItems.Nodes[2])).CreateChild("3.1", 31);
            ((TripleTreeNode)(this.clItems.Nodes[2])).CreateChild("3.2", 32);
            ((TripleTreeNode)(this.clItems.Nodes[2])).CreateChild("3.3", 33);
            //
            // CheckFilterListButtonsControlHost
            //
            this.tschCheckListItems.Name = "tschCheckListItems";
            this.tschCheckListItems.AutoSize = false;
            //this.tschCheckListItems.Size = new System.Drawing.Size(this.Width - 35, 24);
            this.tschCheckListItems.Padding = new Padding(0);
            this.tschCheckListItems.Margin = new Padding(0);
            //this.tschCheckListItems.Dock = DockStyle.Fill;
            //
            // HeaderContextMenuStrip
            //
            //this.Closed += new ToolStripDropDownClosedEventHandler(FilterContextMenu_Closed);
            //this.LostFocus += new EventHandler(FilterContextMenu_LostFocus);
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscbSortType,
            this.tsmiSortAsc,
            this.tsmiSortDesc,
            this.tsmiSortDrop,
            this.tssSortFilter,
            this.tsmiFilterDrop,
            this.tsmiFilterCustom,
            this.tssFilterCheck,
            this.tschCheckListItems
            //this.toolStripSeparator3MenuItem,
            //this.CheckFilterListControlHost,
            //this.CheckFilterListButtonsControlHost,
            //this.ResizeBoxControlHost
            });

            this.ResumeLayout(false);












            //
            // btnOK
            //
            this.btnOK.AutoSize = false;
            this.btnOK.Name = "okButton";
            this.btnOK.BackColor = System.Windows.Forms.Button.DefaultBackColor;
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Text = "Фильтр";
            this.btnOK.Location = new Point(this.CheckFilterListButtonsPanel.Width - 164, 0);
            //this.btnOK.Click += new EventHandler(okButton_Click);
            //
            // btnCancel
            //
            this.btnCancel.AutoSize = false;
            this.btnCancel.Name = "cancelButton";
            this.btnCancel.BackColor = System.Windows.Forms.Button.DefaultBackColor;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Location = new Point(this.CheckFilterListButtonsPanel.Width - 79, 0);
            //this.btnCancel.Click += new EventHandler(cancelButton_Click);
        }

        /// <summary>
        /// Get the current MenuStripSortOrder type
        /// </summary>
        public SortOrder ActiveSortOrder { get { return activeSortOrder; } }

        /// <summary>
        /// Get the current MenuStripFilterType type
        /// </summary>
        public FilterType ActiveFilterType { get { return activeFilterType; } }

        /// <summary>
        /// Get the DataType for the MenuStrip Filter
        /// </summary>
        public Type DataType { get; private set; }

        /// <summary>
        /// Get or Set the Filter Sort enabled
        /// </summary>
        public bool IsSortEnabled { get; set; }

        /// <summary>
        /// Get or Set the Filter Sort enabled
        /// </summary>
        public bool IsFilterEnabled { get; set; }

        /// <summary>
        /// Get or Set the Filter DateAndTime enabled
        /// </summary>
        public bool IsFilterDateAndTimeEnabled { get; set; }

        private void tscbSortType_MouseLeave(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void tsmiSortAsc_Click(object sender, EventArgs e)
        {
            tsmiSortAsc.Checked = true;
            tsmiSortDesc.Checked = false;
            tsmiSortDrop.Enabled = true;
            this.activeSortOrder = SortOrder.Ascending;
            //String oldsort = this.SortString;
            //this.SortString = "[{0}] ASC";

            //if (oldsort != this.SortString && this.SortChanged != null)
            //    SortChanged(this, new EventArgs());
        }

        private void tsmiSortDesc_Click(object sender, EventArgs e)
        {
            tsmiSortAsc.Checked = false;
            tsmiSortDesc.Checked = true;
            tsmiSortDrop.Enabled = true;
            this.activeSortOrder = SortOrder.Descending;
            //String oldsort = this.SortString;
            //this.SortString = "[{0}] ASC";

            //if (oldsort != this.SortString && this.SortChanged != null)
            //    SortChanged(this, new EventArgs());
        }

        private void tsmiSortDrop_Click(object sender, EventArgs e)
        {
            tsmiSortAsc.Checked = false;
            tsmiSortDesc.Checked = false;
            tsmiSortDrop.Enabled = false;
            this.activeSortOrder = SortOrder.None;
            //String oldsort = this.SortString;
            //this.SortString = "[{0}] ASC";

            //if (oldsort != this.SortString && this.SortChanged != null)
            //    SortChanged(this, new EventArgs());
        }








        private ImageList GetCheckImages()
        {
            ImageList images = new System.Windows.Forms.ImageList();
            Bitmap unCheckImg = new Bitmap(16, 16);
            Bitmap checkImg = new Bitmap(16, 16);
            Bitmap mixedImg = new Bitmap(16, 16);

            using (Bitmap img = new Bitmap(16, 16))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                    unCheckImg = (Bitmap)img.Clone();
                    CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
                    checkImg = (Bitmap)img.Clone();
                    CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal);
                    mixedImg = (Bitmap)img.Clone();
                }
            }

            images.Images.Add("uncheck", unCheckImg);
            images.Images.Add("check", checkImg);
            images.Images.Add("mixed", mixedImg);

            return images;
        }

        private void clItems_MouseLeave(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void clItems_MouseEnter(object sender, EventArgs e)
        {
            this.clItems.Focus();
        }

        private void clItems_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo HitTestInfo = this.clItems.HitTest(e.X, e.Y);
            if (HitTestInfo != null && HitTestInfo.Location == TreeViewHitTestLocations.StateImage)
            {
                TripleTreeNode node = (TripleTreeNode)(e.Node);
                if (node.CheckState == CheckState.Checked)
                    node.CheckState = CheckState.Unchecked;
                else
                    node.CheckState = CheckState.Checked;
            }
        }








        private void ResizeGrip_Paint(Object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.MenuStrip_ResizeGrip, 0, 0);
        }

        private void ResizePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ClearResizeBox();
            }
        }

        private void ResizePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    PaintResizeBox(e.X, e.Y);
            }
        }

        private void ResizePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (resizeEndPoint.X != -1)
            {
                ClearResizeBox();
                if (this.Visible)
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        int newWidth = e.X + this.Width - this.ResizeBoxControlHost.Width;
                        int newHeight = e.Y + this.Height - this.ResizeBoxControlHost.Height;

                        newWidth = Math.Max(newWidth, this.MinimumSize.Width);
                        newHeight = Math.Max(newHeight, this.MinimumSize.Height);

                        ResizeMenu(newWidth, newHeight);
                    }
            }
        }

        private void ClearResizeBox()
        {
            if (resizeEndPoint.X != -1)
            {
                Point StartPoint = this.PointToScreen(HeaderContextMenuStrip.resizeStartPoint);

                Rectangle rc = new Rectangle(StartPoint.X, StartPoint.Y, resizeEndPoint.X, resizeEndPoint.Y);

                rc.X = Math.Min(StartPoint.X, resizeEndPoint.X);
                rc.Width = Math.Abs(StartPoint.X - resizeEndPoint.X);

                rc.Y = Math.Min(StartPoint.Y, resizeEndPoint.Y);
                rc.Height = Math.Abs(StartPoint.Y - resizeEndPoint.Y);

                ControlPaint.DrawReversibleFrame(rc, Color.Black, FrameStyle.Dashed);

                resizeEndPoint.X = -1;
            }
        }

        private void PaintResizeBox(int X, int Y)
        {
            ClearResizeBox();

            X += this.Width - this.ResizeBoxControlHost.Width;
            Y += this.Height - this.ResizeBoxControlHost.Height;

            X = Math.Max(X, this.MinimumSize.Width - 1);
            Y = Math.Max(Y, this.MinimumSize.Height - 1);

            Point StartPoint = this.PointToScreen(HeaderContextMenuStrip.resizeStartPoint);
            Point EndPoint = this.PointToScreen(new Point(X, Y));

            Rectangle rc = new Rectangle();

            rc.X = Math.Min(StartPoint.X, EndPoint.X);
            rc.Width = Math.Abs(StartPoint.X - EndPoint.X);

            rc.Y = Math.Min(StartPoint.Y, EndPoint.Y);
            rc.Height = Math.Abs(StartPoint.Y - EndPoint.Y);

            ControlPaint.DrawReversibleFrame(rc, Color.Black, FrameStyle.Dashed);

            resizeEndPoint.X = EndPoint.X;
            resizeEndPoint.Y = EndPoint.Y;
        }

        private void ResizeMenu(int W, int H)
        {
            this.tscbSortType.Width = W - 1;
            this.tsmiSortAsc.Width = W - 1;
            this.tsmiSortDesc.Width = W - 1;
            this.tsmiSortDrop.Width = W - 1;
            this.tssSortFilter.Width = W - 1;
            this.tsmiFilterDrop.Width = W - 1;
            this.tsmiFilterCustom.Width = W - 1;
            this.tssFilterCheck.Width = W - 1;
            this.CheckFilterListControlHost.Size = new System.Drawing.Size(W - 35, H - 160);
            this.CheckFilterListPanel.Size = new System.Drawing.Size(W - 35, H - 160);
            this.clItems.Bounds = new Rectangle(4, 4, W - 35 - 8, H - 160 - 8);
            this.CheckFilterListButtonsControlHost.Size = new System.Drawing.Size(W - 35, 24);
            this.CheckFilterListButtonsControlHost.Size = new Size(W - 35, 24);
            this.btnOK.Location = new Point(W - 35 - 164, 0);
            this.btnCancel.Location = new Point(W - 35 - 79, 0);
            this.ResizeBoxControlHost.Margin = new Padding(W - 46, 0, 0, 0);
            this.Size = new Size(W, H);
        }

        private System.Windows.Forms.ToolStripComboBox tscbSortType;
        private System.Windows.Forms.ToolStripMenuItem tsmiSortAsc;
        private System.Windows.Forms.ToolStripMenuItem tsmiSortDesc;
        private System.Windows.Forms.ToolStripMenuItem tsmiSortDrop;
        private System.Windows.Forms.ToolStripSeparator tssSortFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmiFilterDrop;
        private System.Windows.Forms.ToolStripMenuItem tsmiFilterCustom;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddCustomFilter;
        private System.Windows.Forms.ToolStripSeparator tssFilterCheck;
        
        private System.Windows.Forms.TreeView clItems;
        private System.Windows.Forms.ToolStripControlHost tschCheckListItems;
        
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStripControlHost CheckFilterListControlHost;
        private System.Windows.Forms.ToolStripControlHost CheckFilterListButtonsControlHost;
        private System.Windows.Forms.ToolStripControlHost ResizeBoxControlHost;
        private System.Windows.Forms.Panel CheckFilterListPanel;
        private System.Windows.Forms.Panel CheckFilterListButtonsPanel;
    }
}
