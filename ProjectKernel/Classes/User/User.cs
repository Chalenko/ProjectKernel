using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes.User
{
    /// <summary>
    /// Тип пользователя
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// Пользователь системы Windows.
        /// </summary>
        System,
        /// <summary>
        /// Пользователь системы из БД. 
        /// </summary>
        /// <remarks>Используется при наличии таблицы Users в БД.</remarks>
        Database
    }

    /// <summary>
    /// Класс пользователя системы
    /// </summary>
    abstract public class User
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; protected set; }
        
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; protected set; }
        
        /// <summary>
        /// Фамилия и имя пользователя
        /// </summary>
        public string FullName { get { return Surname + " " + Name; } }
        
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; protected set; }
        
        /// <summary>
        /// Подразделение пользователя
        /// </summary>
        public string Department { get; protected set; }
        
        /// <summary>
        /// Группы пользователя
        /// </summary>
        public List<string> Groups { get; protected set; }
        
        /// <summary>
        /// Свойство, показывающее является ли пользователь администратором системы или нет
        /// </summary>
        public bool IsAdmin { get { return Groups.Where(x => x.Contains("Admin")).Count() > 0; } }
        
        /// <summary>
        /// Тип пользователя
        /// </summary>
        public UserType Type { get; protected set; }
    }

    /// <summary>
    /// Текущий пользователь
    /// </summary>
    public abstract class CurrentUser : User
    {
        private static User instance = new SystemUser();

        /// <summary>
        /// Свойство предоставляющее в окружающую среду текущего пользователя
        /// </summary>
        public static User Instance { get { return instance; } }

        /// <summary>
        /// Метод предоставляющий в окружающую среду текущего пользователя из БД
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Текущий пользователь</returns>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда значение аргумента login равно null или пусто</exception>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует пользователя с таким логином</exception>
        public static User getDBInstance(string login)
        {
            if (string.IsNullOrWhiteSpace(login)) throw new ArgumentNullException("login");
            try
            {
                instance = DBUserStorage.Instance.getUser(login);
            }
            catch (ArgumentException)
            {
                throw;
            }
            
            return Instance;
        }

        /// <summary>
        /// Метод предоставляющий в окружающую среду текущего пользователя системы Windows
        /// </summary>
        /// <returns>Текущий пользователь</returns>
        public static User getSystemInstance()
        {
            instance = new SystemUser();
            
            return Instance;
        }
    }
}
