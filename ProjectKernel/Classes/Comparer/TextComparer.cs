using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Comparer
{
    /// <summary>
    /// Сравенение текста
    /// </summary>
    public sealed class TextComparer : ComparerType
    {
        /// <summary>
        /// Инициализирует объект класса TextComparer
        /// </summary>
        public TextComparer() { }

        /// <summary>
        /// Инициализация объкта класса TextComparer c заданным направлением сортировки
        /// </summary>
        /// <param name="sortOrder">Направление сортировки</param>
        public TextComparer(System.ComponentModel.ListSortDirection sortOrder) : base(sortOrder) { }

        /// <summary>
        /// Сравнивает два указанных объекта System.String и возвращает целое число, которое показывает их относительное положение в порядке сортировки если оба объекта не равны null
        /// </summary>
        /// <param name="str1">Первая строка для сравнения</param>
        /// <param name="str2">Вторая строка для сравнения</param>
        /// <returns>32-разрядное знаковое целое число, выражающее лексическое отношение двух сравниваемых значений. Возвращаемое значение меньше нуля если значение параметра str1 меньше значения параметра str2. Нуль если значения параметров str1 и str2 равны. Больше нуля если значение параметра str1 больше значения параметра str2.</returns>
        protected override int compareNotNullValue(string str1, string str2)
        {
            return SortOrderModifier * System.String.Compare(str1, str2);
        }

        /// <summary>
        /// Описание типа сравнения
        /// </summary>
        /// <returns>Текстовое название объекта-сравнителя</returns>
        public override string GetDescription()
        {
            return "Текстовый";
        }
    }
}
