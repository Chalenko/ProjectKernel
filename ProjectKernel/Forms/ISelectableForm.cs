using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Forms
{
    /// <summary>
    /// Интерфейс списковой формы
    /// </summary>
    public interface ISelectableForm
    {
        /// <summary>
        /// Элемент управления содержащий кнопки выбора
        /// </summary>
        Control SelectButtons { get; }

        /// <summary>
        /// Устаовка действия по двойному клику мыши
        /// </summary>
        /// <param name="action">Действие по двойному щелчку мыши</param>
        void SetDoubleClickAction(DoubleClickAction action);

        /// <summary>
        /// Запоминание текущей записи в качестве выбранной записи
        /// </summary>
        void SelectRecord();

        /// <summary>
        /// Редактирование текущей записи
        /// </summary>
        void EditRecord();
    }
}
