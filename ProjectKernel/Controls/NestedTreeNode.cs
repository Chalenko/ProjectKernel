using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Controls
{
    /// <summary>
    /// Представляет узел объекта System.Windows.Forms.TreeView с передачей значения Checked/Unchecked в дочерние узлы.
    /// </summary>
    public class NestedTreeNode : ExtendedTreeNode
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса NestedTreeNode.
        /// </summary>
        public NestedTreeNode() : base() { }
  
        /// <summary>
        /// Инициализирует новый экземпляр класса NestedTreeNode с заданной текстовой меткой.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        public NestedTreeNode(string text) : base(text) { }

        /// <summary>
        /// Инициализирует новый объект класса NestedTreeNode с тектовой меткой и хранимым значением.
        /// </summary>
        /// <param name="text">Текстовая метка.</param>
        /// <param name="value">Хранимое значение.</param>
        public NestedTreeNode(string text, object value) : base(text, value: value) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса NestedTreeNode с заданной текстовой меткой и значением состояния.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        /// <param name="state">Состояние узла Checked.</param>
        public NestedTreeNode(string text, bool state) : base(text)
        {
            this.Checked = state;
        }
        
        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode по параметрам.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="state">Состояние узла Checked.</param>
        public NestedTreeNode(string text, object value, bool state) : base(text, value: value) 
        {
            this.Checked = state;
        }
        
        /// <summary>
        /// Инициализирует новый экземпляр класса NestedTreeNode с заданной текстовой меткой и дочерними узлами дерева.
        /// </summary>
        /// <param name="text">Метка нового узла дерева.</param>
        /// <param name="children">Массив дочерних объектов NestedTreeNode.</param>
        public NestedTreeNode(string text, NestedTreeNode[] children) : base (text, children: children) { }
        
        /// <summary>
        /// Инициализирует новый объект класса NestedTreeNode по текстовой метке, хранимому значению и списку дочерних узлов.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="children">Массив дочерних объектов NestedTreeNode.</param>
        public NestedTreeNode(string text, object value, NestedTreeNode[] children) : base(text, value, children) { }
        
        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode по параметрам.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="state">Состояние узла Checked.</param>
        /// <param name="children">Массив дочерних объектов NestedTreeNode.</param>
        public NestedTreeNode(string text, bool state, NestedTreeNode[] children) : base(text, children: children) 
        {
            this.Checked = state; 
        }

        /// <summary>
        /// Инициализирует новый объект класса TripleTreeNode по параметрам.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="state">Состояние узла Checked.</param>
        /// <param name="children">Массив дочерних объектов NestedTreeNode.</param>
        public NestedTreeNode(string text, object value, bool state, NestedTreeNode[] children) : base(text, value, children)
        {
            this.Checked = state;
        }

        /// <summary>
        /// Возвращает или задает значение, определяющее, находится ли узел дерева в выбранном состоянии.
        /// </summary>
        /// <returns>Значение true, если узел дерева находится в выбранном состоянии; в противном случае — значение false.</returns>
        [DefaultValue(false)]
        public virtual new bool Checked 
        { 
            get
            {
                return base.Checked;
            }
            set
            {
                //base.Checked = value;

                bool previousState = Checked;

                foreach (NestedTreeNode node in this.Nodes)
                {
                    node.Checked = value;
                }
                
                if (previousState != value) base.Checked = value;
            }
        }

        /// <summary>
        /// Получает родительский узел дерева для текущего узла дерева.
        /// </summary>
        /// <returns>Объект NestedTreeNode, который предоставляет родительский узел для текущего узла дерева.</returns>
        public new NestedTreeNode Parent
        {
            get
            {
                return GetParent<NestedTreeNode>();
            }
        }

        /// <summary>
        /// Добавляет дочерний узел к текущему узлу.
        /// </summary>
        /// <param name="child">Узел для добавления.</param>
        /// <exception cref="System.ArgumentNullException">Выбрасывается когда параметр сhild равен null.</exception>
        public void AddChild(NestedTreeNode child)
        {
            base.AddChild<NestedTreeNode>(child);
        }

        /// <summary>
        /// Получает дочерний узел.
        /// </summary>
        /// <param name="index">Индекс дочернего узла.</param>
        /// <returns>Возвращает дочерний узел.</returns>
        public NestedTreeNode GetChild(int index)
        {
            return base.GetChild<NestedTreeNode>(index);
        }

        /// <summary>
        /// Удаляет все дочерние узлы, с указанной текстовой меткой.
        /// </summary>
        /// <param name="text">Текстовая метка удаляемого узла.</param>
        public void RemoveChild(string text)
        {
            base.RemoveChild<NestedTreeNode>(text);
        }

        /// <summary>
        /// Создает новый дочерний узел с текстовой меткой и состоянием выбора узла.
        /// </summary>
        /// <param name="text">Метка нового узла.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="state">Состояние нового узла.</param>
        public void CreateChild(string text, object value = null, bool state = false)
        {
            this.AddChild(new NestedTreeNode(text, value, state));
        }

        /// <summary>
        /// Меняет текущее состояние выбора на противоположное.
        /// </summary>
        public void Invert()
        {
            this.Checked = !this.Checked;
        }

        /// <summary>
        /// Возвращает объект System.String, который представляет текущий объект System.Object.
        /// </summary>
        /// <returns>Объект System.String, представляющий текущий объект System.Object.</returns>
        public override string ToString()
        {
            return "NestedTreeNode: " + Text;
        }
    }
}
