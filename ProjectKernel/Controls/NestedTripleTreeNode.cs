using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Controls
{
    /// <summary>
    /// Представляет узел объекта System.Windows.Forms.TreeView с тремя возможными состояниями выбора и с передачей этого значения в дочерние узлы.
    /// </summary>
    public class NestedTripleTreeNode : TripleTreeNode
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса NestedTripleTreeNode.
        /// </summary>
        public NestedTripleTreeNode() : base() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса NestedTripleTreeNode с заданной текстовой меткой.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        public NestedTripleTreeNode(string text) : base(text) { }

        /// <summary>
        /// Инициализирует новый объект класса NestedTripleTreeNode с тектовой меткой и хранимым значением.
        /// </summary>
        /// <param name="text">Текстовая метка.</param>
        /// <param name="value">Хранимое значение.</param>
        public NestedTripleTreeNode(string text, object value) : base(text, value) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса NestedTripleTreeNode с заданной текстовой меткой и значением состояния.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        /// <param name="state">Состояние узла.</param>
        public NestedTripleTreeNode(string text, System.Windows.Forms.CheckState state) : base(text, state) { }

        /// <summary>
        /// Инициализирует новый объект класса NestedTripleTreeNode по параметрам.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="state">Состояние узла.</param>
        public NestedTripleTreeNode(string text, object value, System.Windows.Forms.CheckState state) : base(text, value, state) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса NestedTripleTreeNode с заданной текстовой меткой и дочерними узлами дерева.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        /// <param name="children">Массив дочерних объектов NestedTripleTreeNode.</param>
        public NestedTripleTreeNode(string text, NestedTripleTreeNode[] children) : base (text, children: children) { }
        
        /// <summary>
        /// Инициализирует новый объект класса NestedTripleTreeNode по текстовой метке, хранимому значению и списку дочерних узлов.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="children">Массив дочерних объектов NestedTripleTreeNode.</param>
        public NestedTripleTreeNode(string text, object value, NestedTripleTreeNode[] children) : base(text, value, children) { }
        
        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode по параметрам.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="state">Состояние узла.</param>
        /// <param name="children">Массив дочерних объектов NestedTripleTreeNode.</param>
        public NestedTripleTreeNode(string text, System.Windows.Forms.CheckState state, NestedTripleTreeNode[] children) : base(text, state, children: children) { }

        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode по параметрам.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="state">Состояние узла.</param>
        /// <param name="children">Массив дочерних объектов NestedTripleTreeNode.</param>
        public NestedTripleTreeNode(string text, object value, System.Windows.Forms.CheckState state, NestedTripleTreeNode[] children) : base(text, value, state, children) { }

        /// <summary>
        /// Получает родительский узел дерева для текущего узла дерева.
        /// </summary>
        /// <returns>Объект NestedTripleTreeNode, который предоставляет родительский узел для текущего узла дерева.</returns>
        public new NestedTripleTreeNode Parent
        {
            get
            {
                return GetParent<NestedTripleTreeNode>();
            }
        }
        
        /// <summary>
        /// Возвращает или задает значение, определяющее состояние узла.
        /// </summary>
        public override CheckState CheckState
        {
            get
            {
                return base.CheckState;
            }
            set
            {
                if (CheckState != value)
                {
                    if (value != System.Windows.Forms.CheckState.Indeterminate)
                    {
                        foreach (TripleTreeNode child in Nodes)
                        {
                            child.CheckState = value;
                        }
                    }

                    base.CheckState = value;

                    if (this.Parent != null)
                    {
                        this.Parent.CorrectSelfCheckState();
                    }
                }
            }
        }

        /// <summary>
        /// Добавляет указанный узел к текущему в качестве дочернего.
        /// </summary>
        /// <param name="child">Узел для добавления в список дочерних узлов.</param>
        public void AddChild(NestedTripleTreeNode child)
        {
            base.AddChild<NestedTripleTreeNode>(child);

            this.CorrectSelfCheckState();
        }

        /// <summary>
        /// Получает дочерний узел по индексу.
        /// </summary>
        /// <param name="index">Индекс дочернего узла.</param>
        /// <returns>Возвращает дочерний узел.</returns>
        public new NestedTripleTreeNode GetChild(int index)
        {
            return base.GetChild<NestedTripleTreeNode>(index);
        }

        /// <summary>
        /// Удаляет указанный узел из списка дочерних узлов
        /// </summary>
        /// <param name="child">Узел для удаления из списка дочерних узлов.</param>
        public override void RemoveChild(ExtendedTreeNode child)
        {
            base.RemoveChild(child);

            CorrectSelfCheckState();
        }

        /// <summary>
        /// Удаляет дочерний узел по его индексу.
        /// </summary>
        /// <param name="index">Индекс удаляемого узла.</param>
        public override void RemoveChild(int index)
        {
            base.RemoveChild(index);

            CorrectSelfCheckState();
        }

        /// <summary>
        /// Удаляет все дочерние узлы, с указанной текстовой меткой.
        /// </summary>
        /// <param name="text">Текстовая метка удаляемого узла.</param>
        public override void RemoveChild(string text)
        {
            base.RemoveChild(text);

            CorrectSelfCheckState();
        }

        /// <summary>
        /// Создание в текущем узле дочернего узла по параметрам.
        /// </summary>
        /// <param name="text">Текстовая метка добавляемого узла.</param>
        /// <param name="value">Хранимое значение добавляемого узла.</param>
        /// <param name="state">Состояние добавляемого узла.</param>
        public override void CreateChild(string text, object value = null, CheckState state = System.Windows.Forms.CheckState.Unchecked)
        {
            this.AddChild(new NestedTripleTreeNode(text, value, state));
        }

        /// <summary>
        /// Возвращает строковое представление текущего объекта.
        /// </summary>
        /// <returns>Объект System.String, представляющий текущий объект System.Object.</returns>
        public override string ToString()
        {
            return "NestedTripleTreeNode: Text = [" + Text + "]; Value = [" + Value + "]; CheckState = [" + CheckState.ToString() + "]";
        }

        /// <summary>
        /// Проверряет все ли дочерние элементы находятся в одинаковом состоянии.
        /// </summary>
        protected bool HasIdenticalChildren
        {
            get
            {
                IEnumerable<TripleTreeNode> ie = this.Nodes.Cast<TripleTreeNode>();
                return ie.All(x => x.CheckState == System.Windows.Forms.CheckState.Checked) || ie.All(x => x.CheckState == System.Windows.Forms.CheckState.Unchecked);
            }
        }

        private void CorrectSelfCheckState()
        {
            if (Nodes.Count != 0)
                if (HasIdenticalChildren)
                    this.CheckState = GetChild(0).CheckState;
                else
                    this.CheckState = System.Windows.Forms.CheckState.Indeterminate;
        }
    }
}
