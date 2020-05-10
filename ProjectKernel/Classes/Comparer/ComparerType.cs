using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProjectKernel.Classes.Comparer
{
    /*
    public enum SortType : byte
    {
        Text = 0,
        Number,
        DateTime,
        Boolean,
        Natural
    }
    */

    /// <summary>
    /// Абстрактный класс сравнения
    /// </summary>
    public abstract class ComparerType : System.Collections.Generic.IComparer<string>
    {
        private int sortOrderModifier = 1;

        /// <summary>
        /// Инициализация объкта абстрактного класса ComparerType
        /// </summary>
        protected ComparerType()
        {
        }

        /// <summary>
        /// Инициализация объкта абстрактного класса ComparerType c заданным направлением сортировки
        /// </summary>
        /// <param name="sortOrder">Направление сортировки</param>
        protected ComparerType(ListSortDirection sortOrder) : this()
        {
            if (sortOrder == ListSortDirection.Descending)
            {
                sortOrderModifier = -1;
            }
            else if (sortOrder == ListSortDirection.Ascending)
            {
                sortOrderModifier = 1;
            }
        }

        /// <summary>
        /// Сравнивает два указанных объекта System.String и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
        /// </summary>
        /// <param name="str1">Первая строка для сравнения</param>
        /// <param name="str2">Вторая строка для сравнения</param>
        /// <returns>32-разрядное знаковое целое число, выражающее лексическое отношение двух сравниваемых значений. Возвращаемое значение меньше нуля если значение параметра str1 меньше значения параметра str2. Нуль если значения параметров str1 и str2 равны. Больше нуля если значение параметра str1 больше значения параметра str2.</returns>
        public int Compare(string str1, string str2)
        {
            if (str1 == null && str2 == null) return 0;
            if (str1 == null && str2 != null) return -1;
            if (str1 != null && str2 == null) return 1;
            return compareNotNullValue(str1, str2);
        }

        /// <summary>
        /// Определяет порядок сортировки. 1 - От меньшего к большему. -1 - от большего к меньшиму.
        /// </summary>
        public int SortOrderModifier
        {
            get
            { 
                return sortOrderModifier;
            }
            set
            {
                sortOrderModifier = value;
            }
        }

        /// <summary>
        /// Новый объект-сравнитель для чисел
        /// </summary>
        public static ComparerType Number { get { return new NumberComparer(); } }

        /// <summary>
        /// Новый объект-сравнитель для текста
        /// </summary>
        public static ComparerType Text { get { return new TextComparer(); } }

        /// <summary>
        /// Новый объект-сравнитель для дат
        /// </summary>
        public static ComparerType Date { get { return new DateComparer(); } }

        /// <summary>
        /// Новый натуральный объект-сравнитель
        /// </summary>
        public static ComparerType Natural { get { return new NaturalComparer(); } }

        /// <summary>
        /// Новый объект-сравнитель для логических переменных
        /// </summary>
        public static ComparerType Boolean { get { return new BooleanComparer(); } }

        /// <summary>
        /// Сравнивает два указанных объекта System.String и возвращает целое число, которое показывает их относительное положение в порядке сортировки если оба объекта не равны null
        /// </summary>
        /// <param name="str1">Первая строка для сравнения</param>
        /// <param name="str2">Вторая строка для сравнения</param>
        /// <returns>32-разрядное знаковое целое число, выражающее лексическое отношение двух сравниваемых значений. Возвращаемое значение меньше нуля если значение параметра str1 меньше значения параметра str2. Нуль если значения параметров str1 и str2 равны. Больше нуля если значение параметра str1 больше значения параметра str2.</returns>
        protected abstract int compareNotNullValue(string str1, string str2);

        /// <summary>
        /// Описание типа сравнения
        /// </summary>
        /// <returns>Текстовое название объекта-сравнителя</returns>
        public abstract string GetDescription();
    }
}
