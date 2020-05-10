using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Forms
{
    /// <summary>
    /// Действие для двойного щелчка мыши
    /// </summary>
    /// <remarks>Класс-мультитон</remarks>
    public abstract class DoubleClickAction
    {
        /// <summary>
        /// Действие - выбор текущей записи
        /// </summary>
        public static SelectActionClass SelectAction { get { return SelectActionClass.Instance; } }
        
        /// <summary>
        /// Действие - редактирование текущей записи
        /// </summary>
        public static EditActionClass EditAction { get { return EditActionClass.Instance; } }

        /// <summary>
        /// Вызов действия двойного щелчка мыши
        /// </summary>
        /// <param name="form">Форма с записями</param>
        public abstract void Perform(ISelectableForm form);
    }

    /// <summary>
    /// Действие - выбор текущей записи
    /// </summary>
    /// <remarks>Синглетон</remarks>
    public class SelectActionClass : DoubleClickAction
    {
        private static SelectActionClass instance;
        
        /// <summary>
        /// Предоставляет доступ к действию в окружающую среду
        /// </summary>
        internal static SelectActionClass Instance 
        { 
            get 
            {
                if (instance == null) instance = new SelectActionClass();
                return instance;
            } 
        }


        /// <summary>
        /// Закрытый конструктор
        /// </summary>
        private SelectActionClass() : base() { }

        /// <summary>
        /// Вызов действия двойного щелчка мыши
        /// </summary>
        /// <param name="form">Форма с записями</param>
        public override void Perform(ISelectableForm form)
        {
            form.SelectRecord();
        }
    }

    /// <summary>
    /// Действие - изменение текущей записи
    /// </summary>
    /// <remarks>Синглетон</remarks>
    public class EditActionClass : DoubleClickAction
    {
        private static EditActionClass instance;
        
        /// <summary>
        /// Предоставляет доступ к действию в окружающую среду
        /// </summary>
        internal static EditActionClass Instance 
        { 
            get 
            {
                if (instance == null) instance = new EditActionClass();
                return instance;
            } 
        }

        /// <summary>
        /// Закрытый конструктор
        /// </summary>
        private EditActionClass() : base() { }

        /// <summary>
        /// Вызов действия двойного щелчка мыши
        /// </summary>
        /// <param name="form">Форма с записями</param>
        public override void Perform(ISelectableForm form)
        {
            form.EditRecord();
        }
    }
}
