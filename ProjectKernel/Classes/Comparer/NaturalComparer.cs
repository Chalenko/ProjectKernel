using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.Comparer
{
    /// <summary>
    /// Натуральное сравнение. Сначала сравнивается текстовая часть выражения, потом числовая.
    /// </summary>
    /// <remarks>http://www.codeproject.com/Articles/22517/Natural-Sort-Comparer</remarks>
    public class NaturalComparer : ComparerType
    {
        private Dictionary<string, string[]> table;

        /// <summary>
        /// Инициализирует объект класса NaturalComparer
        /// </summary>
        public NaturalComparer()
        {
            table = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Инициализация объкта класса NaturalComparer c заданным направлением сортировки
        /// </summary>
        /// <param name="sortOrder">Направление сортировки</param>
        public NaturalComparer(System.ComponentModel.ListSortDirection sortOrder) : base(sortOrder)
        {
            table = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Сравнивает два указанных объекта System.String и возвращает целое число, которое показывает их относительное положение в порядке сортировки.
        /// </summary>
        /// <param name="str1">Первая строка для сравнения</param>
        /// <param name="str2">Вторая строка для сравнения</param>
        /// <returns>32-разрядное знаковое целое число, выражающее лексическое отношение двух сравниваемых значений. Возвращаемое значение меньше нуля если значение параметра str1 меньше значения параметра str2. Нуль если значения параметров str1 и str2 равны. Больше нуля если значение параметра str1 больше значения параметра str2.</returns>
        protected override int compareNotNullValue(string str1, string str2) { return SortOrderModifier * AscendingCompare(str1, str2); }

        private int AscendingCompare(string str1, string str2)
        {
            /*
            if (x == y)
            {
                return 0;
            }
            */

            string[] x1, y1;

            if (!table.TryGetValue(str1, out x1))
            {
                //x1 = System.Text.RegularExpressions.Regex.Split(x.Replace(" ", ""), "([0-9]+)");
                x1 = System.Text.RegularExpressions.Regex.Split(str1, "([0-9]+)");
                table.Add(str1, x1);
            }

            if (!table.TryGetValue(str2, out y1))
            {
                //y1 = System.Text.RegularExpressions.Regex.Split(y.Replace(" ", ""), "([0-9]+)");
                y1 = System.Text.RegularExpressions.Regex.Split(str2, "([0-9]+)");
                table.Add(str2, y1);
            }

            for (int i = 0; i < x1.Length && i < y1.Length; i++)
            {
                if (x1[i] != y1[i])
                {
                    return PartCompare(x1[i], y1[i]);
                }
            }

            if (y1.Length > x1.Length)
            {
                return -1;
            }
            else if (y1.Length < x1.Length)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        private int PartCompare(string left, string right)
        {
            int x, y;

            if (!int.TryParse(left, out x) || !int.TryParse(right, out y))
            {
                return left.CompareTo(right);
            }

            return x.CompareTo(y);
        }

        /// <summary>
        /// Описание типа сравнения
        /// </summary>
        /// <returns>Текстовое название объекта-сравнителя</returns>
        public override string GetDescription()
        {
            return "Естественное";
        }
    }
}
