using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Comparer
{
    /// <summary>
    /// Сравенение дат
    /// </summary>
    public sealed class DateComparer : ComparerType
    {
        /// <summary>
        /// Инициализирует объект класса DateComparer
        /// </summary>
        public DateComparer() : base() { }


        /// <summary>
        /// Инициализация объкта класса DateComparer c заданным направлением сортировки
        /// </summary>
        /// <param name="sortOrder">Направление сортировки</param>
        public DateComparer(System.ComponentModel.ListSortDirection sortOrder) : base(sortOrder) { }

        /// <summary>
        /// Сравнивает два указанных объекта System.String и возвращает целое число, которое показывает их относительное положение в порядке сортировки если оба объекта не равны null
        /// </summary>
        /// <param name="str1">Первая дата в текстовом формате для сравнения</param>
        /// <param name="str2">Вторая дата в текстовом формате для сравнения</param>
        /// <returns>32-разрядное знаковое целое число, выражающее лексическое отношение двух сравниваемых значений. Возвращаемое значение меньше нуля если значение параметра str1 меньше значения параметра str2. Нуль если значения параметров str1 и str2 равны. Больше нуля если значение параметра str1 больше значения параметра str2.</returns>
        /// <exception cref="System.FormatException">Одно из значений параметров x или y не является правильно отформатированной строкой, представляющей дату и время.</exception>
        protected override int compareNotNullValue(string str1, string str2)
        {
            int CompareResult = 0;
            if (System.String.Compare(str1, str2) != 0)
            {
                DateTime d1, d2;
                try
                {
                    d1 = DateTime.Parse(str1);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Входная строка str1 имела неверный формат.", ex);
                }
                try
                {
                    d2 = DateTime.Parse(str2);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Входная строка str2 имела неверный формат.", ex);
                }

                CompareResult = System.DateTime.Compare(d1, d2);
            }
            return CompareResult * SortOrderModifier;
        }

        /// <summary>
        /// Описание типа сравнения
        /// </summary>
        /// <returns>Текстовое название объекта-сравнителя</returns>
        public override string GetDescription()
        {
            return "Дата";
        }
    }
}
