using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ProjectKernel.Classes.User
{
    /// <summary>
    /// Класс для работы с паролями
    /// </summary>
    public class Password
    {
        // http://www.internet-technologies.ru/articles/article_1807.html

        internal string salt;
        internal string hashPassword;
        
        /// <summary>
        /// Необходимо сменить пароль
        /// </summary>
        public bool HaveToChange { get; set; }
        // These constants may be changed without breaking existing hashes.
        /// <summary>
        /// Количество байт отведенное на "соль"
        /// </summary>
        public const int SALT_BYTES = 32;
        /// <summary>
        /// Количество байт отведенное на хешированный пароль
        /// </summary>
        public const int HASH_BYTES = 64;
        /// <summary>
        /// Количество итераций для генерации хешированного пароля
        /// </summary>
#if DEBUG
        public const int PBKDF2_ITERATIONS = 1;
#else
        public const int PBKDF2_ITERATIONS = 64000;
#endif

        private Password() { }

        /// <summary>
        /// Инициализирует объект класса Password по параметрам salt и password
        /// </summary>
        /// <param name="salt">"Соль" для пароля</param>
        /// <param name="password">Хешированный пароль</param>
        /// <param name="changePassword">Сменить пароль</param>
        public Password(string salt, string password, bool changePassword) : this()
        {
            // TODO: Complete member initialization
            this.salt = salt;
            this.hashPassword = password;
            this.HaveToChange = changePassword;
        }

        /// <summary>
        /// Генерирует "соль" для пароля
        /// </summary>
        /// <returns>Возвращает сгенерированую "соль"</returns>
        public static string GenerateSalt()
        {
            byte[] salt = new byte[SALT_BYTES];

            using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
            {
                csprng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Генерирует хеш-код пароля по "соли" и паролю
        /// </summary>
        /// <param name="salt">"Соль" для генерирования хеш-кода пароля</param>
        /// <param name="password">Хешируемый пароль</param>
        /// <returns>Возвращает хешированный пароль</returns>
        public static string GenerateHash(string salt, string password)
        {
            byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);
            return Convert.ToBase64String(hash);
        }

        private static byte[] PBKDF2(string password, string salt, int iterations, int outputBytes)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt)))
            {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }
}
