using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Comparer
{
    /// <summary>
    /// Сравенение логических выражений
    /// </summary>
    public sealed class BooleanComparer : ComparerType
    {
        /// <summary>
        /// Инициализирует объект класса BooleanComparer
        /// </summary>
        public BooleanComparer() { }

        /// <summary>
        /// Инициализация объкта класса BooleanComparer c заданным направлением сортировки
        /// </summary>
        /// <param name="sortOrder">Направление сортировки</param>
        public BooleanComparer(System.ComponentModel.ListSortDirection sortOrder) : base(sortOrder) { }

        /// <summary>
        /// Сравнивает два указанных объекта System.String и возвращает целое число, которое показывает их относительное положение в порядке сортировки если оба объекта не равны null
        /// </summary>
        /// <param name="str1">Первое значение в текстовом формате для сравнения</param>
        /// <param name="str2">Второе значение в текстовом формате для сравнения</param>
        /// <returns>32-разрядное знаковое целое число, выражающее отношение двух сравниваемых значений. Возвращаемое значение меньше нуля если значение параметра str1 меньше значения параметра str2. Нуль если значения параметров str1 и str2 равны. Больше нуля если значение параметра str1 больше значения параметра str2.</returns>
        /// <exception cref="System.ArgumentException">Значение одного из параметров str1 или str2 не является логическим значением в допустимом формате.</exception>
        protected override int compareNotNullValue(string str1, string str2)
        {
            int CompareResult = 0;
            if (System.String.Compare(str1, str2) != 0)
            {
                bool b1, b2;
                try
                {
                    b1 = bool.Parse(str1);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Входная строка str1 имела неверный формат.", ex);
                }
                try
                {
                    b2 = bool.Parse(str2);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Входная строка str2 имела неверный формат.", ex);
                }
                CompareResult = b1.CompareTo(b2);
            }

            return CompareResult * SortOrderModifier;
        }

        /// <summary>
        /// Описание типа сравнения
        /// </summary>
        /// <returns>Текстовое название объекта-сравнителя</returns>
        public override string GetDescription()
        {
            return "Логический";
        }
    }
}
