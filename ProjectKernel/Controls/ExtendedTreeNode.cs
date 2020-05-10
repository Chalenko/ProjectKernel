using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProjectKernel.Controls
{
    /// <summary>
    /// Представляет узел объекта System.Windows.Forms.TreeView с расширенными возможностями 
    /// </summary>
    public abstract class ExtendedTreeNode : TreeNode
    {
        /// <summary>
        /// Хранимое значение.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Инициализирует новый объект класса ExtendedTreeNode.
        /// </summary>
        public ExtendedTreeNode() : base()
        {
            Value = null;
        }

        /// <summary>
        /// Инициализирует новый объект класса ExtendedTreeNode с текстовой меткой.
        /// </summary>
        /// <param name="text">Текстовая метка</param>
        public ExtendedTreeNode(String text) : base(text) 
        {
            Value = null;
        }

        /// <summary>
        /// Инициализирует новый объект класса ExtendedTreeNode с тектовой меткой и хранимым значением.
        /// </summary>
        /// <param name="text">Текстовая метка</param>
        /// <param name="value">Хранимое значение</param>
        public ExtendedTreeNode(String text, object value) : base(text)
        {
            Value = value;
        }
        
        /// <summary>
        /// Инициализирует новый объект класса ExtendedTreeNode по текстовой метке и списку дочерних узлов.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="children">Массив дочерних объектов ExtendedTreeNode.</param>
        public ExtendedTreeNode(String text, ExtendedTreeNode[] children) : base(text, children) { }
        
        /// <summary>
        /// Инициализирует новый объект класса ExtendedTreeNode по текстовой метке, хранимому значению и списку дочерних узлов.
        /// </summary>
        /// <param name="text">Текстовая метка узла.</param>
        /// <param name="value">Хранимое значение.</param>
        /// <param name="children">Массив дочерних объектов ExtendedTreeNode.</param>
        public ExtendedTreeNode(String text, object value, ExtendedTreeNode[] children) : base(text, children)
        {
            Value = value;
        }

        ///// <summary>
        ///// Инициализирует новый объект класса ExtendedTreeNode по параметрам.
        ///// </summary>
        ///// <param name="text">Текстовая метка узла.</param>
        ///// <param name="value">Хранимое значение.</param>
        ///// <param name="state">Состояние узла.</param>
        //public ExtendedTreeNode(String text, object value, CheckState state) : base(text)
        //{
        //    Value = value;
        //    CheckState = state;
        //}

        /// <summary>
        /// Возвращает коллекцию объектов System.Windows.Forms.TreeNode, которая назначена текущему узлу дерева.
        /// </summary>
        /// <returns>Объект System.Windows.Forms.TreeNodeCollection, который предоставляет узлы дерева, назначенные текущему узлу дерева.</returns>
        /// <remarks>Для корректной работы со свойством Nodes необходимо явно приводить тип System.Windows.Forms.TreeNode к типу текущего класса или можно можно воспользоваться специальными методами AddChild, RemoveChild и GetChild.</remarks>
        [Browsable(false)]
        [ListBindable(false)]
//#if (!DEBUG)
//        [Obsolete("Для корректной работы со свойством Nodes необходимо явно приводить тип System.Windows.Forms.TreeNode к типу текущего класса или можно можно воспользоваться специальными методами AddChild, RemoveChild и GetChild.")]
//#endif
        public new TreeNodeCollection Nodes { get { return base.Nodes; } }

        /// <summary>
        /// Получает родительский узел дерева для текущего узла дерева.
        /// </summary>
        /// <typeparam name="T">Тип обобщения.</typeparam>
        /// <returns>Объект типа T, который предоставляет родительский узел для текущего узла дерева.</returns>
        protected virtual T GetParent<T>() where T : ExtendedTreeNode
        {
            if (base.Parent is T)
                return (T)base.Parent;
            else
                return null;
        }

        /// <summary>
        /// Добавляет дочерний узел к текущему узлу.
        /// </summary>
        /// <typeparam name="T">Тип обобщения.</typeparam>
        /// <param name="child">Узел для добавления.</param>
        /// <exception cref="System.ArgumentNullException">Выбрасывается когда параметр сhild равен null.</exception>
        protected virtual void AddChild<T>(T child) where T : ExtendedTreeNode
        {
            if (child == null) throw new ArgumentNullException("child");

            this.Nodes.Add(child);
        }

        /// <summary>
        /// Получает дочерний узел.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index">Индекс дочернего узла.</param>
        /// <returns>Возвращает дочерний узел.</returns>
        protected virtual T GetChild<T>(int index) where T : ExtendedTreeNode
        {
            return (T)(base.Nodes[index]);
        }

        public virtual T FindNode<T>(string text) where T : ExtendedTreeNode
        {
            if (this.Text == text)
                return (T)this;
            for (int i = 0; i < Nodes.Count; i++)
            {
                T child = GetChild<T>(i);
                if (child.Text == text)
                    return child;
                else
                {
                    T result = child.FindNode<T>(text);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Удаляет дочерний узел.
        /// </summary>
        /// <param name="child">Узел для удаления.</param>
        public virtual void RemoveChild(ExtendedTreeNode child)
        {
            this.Nodes.Remove(child);
        }
        
        /// <summary>
        /// Удаляет дочерний узел по его индексу.
        /// </summary>
        /// <param name="index">Индекс удаляемого узла.</param>
        public virtual void RemoveChild(int index)
        {
            this.Nodes.RemoveAt(index);
        }

        /// <summary>
        /// Удаляет все дочерние узлы, с указанной текстовой меткой.
        /// </summary>
        /// <typeparam name="T">Тип обобщения.</typeparam>
        /// <param name="text">Текстовая метка удаляемого узла.</param>
        protected virtual void RemoveChild<T>(string text) where T : ExtendedTreeNode
        {
            IEnumerable<T> ie = this.Nodes.Cast<T>();

            T[] arr = ie.Where(x => x.Text == text).ToArray();

            foreach (T child in arr)
            {
                child.Remove();
            }
        }

        /// <summary>
        /// Возвращает "глубокую" копию дерева
        /// </summary>
        /// <returns>Клонированный объект ExtendedTreeNode.</returns>
        public override object Clone()
        {
            ExtendedTreeNode n = (ExtendedTreeNode)(base.Clone());

            n.Value = this.Value;

            return n;
        }
    }
}
