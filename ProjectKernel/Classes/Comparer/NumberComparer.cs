using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Comparer
{
    /// <summary>
    /// Сравенение чисел
    /// </summary>
    public sealed class NumberComparer : ComparerType
    {
        /// <summary>
        /// Инициализирует объект класса NumberComparer
        /// </summary>
        public NumberComparer() { }

        /// <summary>
        /// Инициализация объкта класса NumberComparer c заданным направлением сортировки
        /// </summary>
        /// <param name="sortOrder">Направление сортировки</param>
        public NumberComparer(System.ComponentModel.ListSortDirection sortOrder): base(sortOrder) { }

        /// <summary>
        /// Сравнивает два указанных объекта System.String и возвращает целое число, которое показывает их относительное положение в порядке сортировки если оба объекта не равны null
        /// </summary>
        /// <param name="str1">Первое число в текстовом формате для сравнения</param>
        /// <param name="str2">Второе число в текстовом формате для сравнения</param>
        /// <returns>32-разрядное знаковое целое число, выражающее лексическое отношение двух сравниваемых значений. Возвращаемое значение меньше нуля если значение параметра str1 меньше значения параметра str2. Нуль если значения параметров str1 и str2 равны. Больше нуля если значение параметра str1 больше значения параметра str2.</returns>
        /// <exception cref="System.ArgumentException">Значение одного из параметров str1 или str2 не является числом в допустимом формате.</exception>
        protected override int compareNotNullValue(string str1, string str2)
        {
            int CompareResult = 0;
            if (System.String.Compare(str1, str2) != 0)
            {
                int n1, n2;
                try
                {
                    n1 = int.Parse(str1);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Входная строка str1 имела неверный формат.", ex);
                }
                try
                {
                    n2 = int.Parse(str2);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Входная строка str2 имела неверный формат.", ex);
                }
                CompareResult = n1.CompareTo(n2);
            }

            return CompareResult * SortOrderModifier;
        }

        /// <summary>
        /// Описание типа сравнения
        /// </summary>
        /// <returns>Текстовое название объекта-сравнителя</returns>
        public override string GetDescription()
        {
            return "Числовой";
        }
    }
}
