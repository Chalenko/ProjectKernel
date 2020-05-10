using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectKernel.Classes.User
{
    /// <summary>
    /// Класс для проверки пароля
    /// </summary>
    public static class PasswordVerificator
    {
        /// <summary>
        /// Проверка на совпадение "чистого" пароля и хешированного пароля
        /// Сравнение хеш-кода, получаемого для переданной пары "соль"-пароль, с хеш-кодом проверки
        /// </summary>
        /// <param name="salt">"Соль" пароля</param>
        /// <param name="password">Чистый пароль</param>
        /// <param name="expectedPasswordHash">Ожидаемый хеш-код пароля</param>
        /// <returns>true - если пароли совпадают, false - иначе</returns>
        public static bool VerifyPassword(string salt, string password, string expectedPasswordHash)
        {
            return SlowEquals(Password.GenerateHash(salt, password), expectedPasswordHash);
        }


        /// <summary>
        /// Сравнение двух строк методом с постоянной временной сложностью, зависящей только от длины строк
        /// </summary>
        /// <param name="str1">Первая строка для сравнения</param>
        /// <param name="str2">Вторая строка для сравнения</param>
        /// <returns>true - если строки совпадают, false - иначе</returns>
        public static bool SlowEquals(string str1, string str2)
        {
            int diff = 0;
            diff |= str1.Length ^ str2.Length;
            for (int i = 0; (i < str1.Length) && (i < str2.Length); i++)
            {
                diff |= Convert.ToByte(str1[i]) ^ Convert.ToByte(str2[i]);
            }
            return diff == 0;
        }
    }
}
