using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Controls
{
    /*
    public enum NodeType : byte
    {
        Default,
        All,
        Empty
    }
    */

    /// <summary>
    /// Представляет узел объекта System.Windows.Forms.TreeView с тремя возможными состояниями выбора. Представляет собой тернарный узел.
    /// </summary>
    public class TripleTreeNode : ExtendedTreeNode
    {
        private CheckState checkState = CheckState.Unchecked;

        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode.
        /// </summary>
        public TripleTreeNode() : base() { }

        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode с текстовой меткой.
        /// </summary>
        /// <param name="text">Текстовая метка</param>
        public TripleTreeNode(String text) : base(text) { }

        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode с тектовой меткой и хранимым значением.
        /// </summary>
        /// <param name="text">Текстовая метка</param>
        /// <param name="value">Хранимое значение</param>
        public TripleTreeNode(String text, object value) : base(text, value) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса NestedTreeNode с заданной текстовой меткой и значением состояния.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        /// <param name="state">Состояние узла.</param>
        public TripleTreeNode(string text, CheckState state) : base(text)
        {
            this.CheckState = state;
        }
        
        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode по параметрам.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="state">Состояние узла.</param>
        public TripleTreeNode(String text, object value, CheckState state) : base(text, value: value)
        {
            CheckState = state;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса TripleTreeNode с заданной текстовой меткой и дочерними узлами дерева.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        /// <param name="children">Массив дочерних объектов TripleTreeNode.</param>
        public TripleTreeNode(string text, TripleTreeNode[] children) : base(text, children: children) { }
        
        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode по текстовой метке, хранимому значению и списку дочерних узлов.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="children">Массив дочерних объектов TripleTreeNode.</param>
        public TripleTreeNode(String text, object value, TripleTreeNode[] children) : base(text, value, children) { }

        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode по параметрам.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="state">Состояние узла.</param>
        /// <param name="children">Массив дочерних объектов TripleTreeNode.</param>
        public TripleTreeNode(string text, CheckState state, TripleTreeNode[] children) : base(text, children: children) 
        {
            this.CheckState = state; 
        }

        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode по текстовой метке, хранимому значению, состоянию выбора и списку дочерних узлов.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="state">Состояние узла.</param>
        /// <param name="children">Массив дочерних объектов TripleTreeNode.</param>
        public TripleTreeNode(String text, object value, CheckState state, TripleTreeNode[] children) : base(text, value, children)
        {
            CheckState = state;
        }

        ///// <summary>
        ///// Get Node NodeType.
        ///// </summary>
        //public NodeType NodeType { get; private set; }
        
        /// <summary>
        /// Возвращает или задает значение, определяющее, находится ли узел дерева в выбранном состоянии.
        /// </summary>
        /// <returns>Значение true, если узел дерева находится в выбранном состоянии; в противном случае — значение false.</returns>
        public new bool Checked
        {
            get
            {
                return checkState == CheckState.Checked;
            }
            set
            {
                base.Checked = value;
                CheckState = (value == true ? CheckState.Checked : CheckState.Unchecked);
            }
        }

        /// <summary>
        /// Получает родительский узел дерева для текущего узла дерева.
        /// </summary>
        /// <returns>Объект TripleTreeNode, который предоставляет родительский узел для текущего узла дерева.</returns>
        public new TripleTreeNode Parent
        {
            get
            {
                return GetParent<TripleTreeNode>();
            }
        }

        /// <summary>
        /// Возвращает или задает значение, определяющее состояние узла.
        /// </summary>
        public virtual CheckState CheckState
        {
            get
            {
                return checkState;
            }
            set
            {
                if (CheckState != value)
                {
                    checkState = value;
                    SetCheckImage();
                }
            }
        }

        /// <summary>
        /// Добавляет указанный узел к текущему в качестве дочернего.
        /// </summary>
        /// <param name="child">Узел для добавления в список дочерних узлов.</param>
        /// <exception cref="System.ArgumentNullException">Выбрасывается когда параметр сhild равен null.</exception>
        public void AddChild(TripleTreeNode child)
        {
            base.AddChild<TripleTreeNode>(child);
        }

        /// <summary>
        /// Получает дочерний узел.
        /// </summary>
        /// <param name="index">Индекс дочернего узла.</param>
        /// <returns>Возвращает дочерний узел.</returns>
        public TripleTreeNode GetChild(int index)
        {
            return base.GetChild<TripleTreeNode>(index);
        }

        /// <summary>
        /// Удаляет все дочерние узлы, с указанной текстовой меткой.
        /// </summary>
        /// <param name="text">Текстовая метка удаляемого узла.</param>
        public virtual void RemoveChild(string text)
        {
            base.RemoveChild<TripleTreeNode>(text);
        }

        /// <summary>
        /// Создание в текущем узле дочернего узла по параметрам.
        /// </summary>
        /// <param name="text">Текстовая метка добавляемого узла.</param>
        /// <param name="value">Хранимое значение добавляемого узла.</param>
        /// <param name="state">Состояние добавляемого узла.</param>
        public virtual void CreateChild(string text, object value = null, CheckState state = System.Windows.Forms.CheckState.Unchecked)
        {
            this.AddChild(new TripleTreeNode(text, value, state));
        }

        /// <summary>
        /// Возвращает "глубокую" копию дерева
        /// </summary>
        /// <returns>Клонированный объект TripleTreeNode.</returns>
        public override object Clone()
        {
            TripleTreeNode n = (TripleTreeNode)(base.Clone());

            n.CheckState = this.CheckState;

            return n;
        }

        /// <summary>
        /// Возвращает объект System.String, который представляет текущий объект System.Object.
        /// </summary>
        /// <returns>Объект System.String, представляющий текущий объект System.Object.</returns>
        public override string ToString()
        {
            return "TripleTreeNode: Text = [" + Text + "]; Value = [" + Value + "]; CheckState = [" + CheckState.ToString() + "]";
        }

        private void SetCheckImage()
        {
            if (checkState == System.Windows.Forms.CheckState.Unchecked)
            {
                StateImageIndex = 0;
                //ImageIndex = 0;
                //SelectedImageIndex = 0;
            }

            if (checkState == System.Windows.Forms.CheckState.Checked)
            {
                StateImageIndex = 1;
                //ImageIndex = 1;
                //SelectedImageIndex = 1;
            }

            if (checkState == System.Windows.Forms.CheckState.Indeterminate)
            {
                StateImageIndex = 2;
                //ImageIndex = 2;
                //SelectedImageIndex = 2;
            }
        }
    }
}
