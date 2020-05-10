using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProjectKernel.Classes
{
    /// <summary>
    /// Элемент управления (контрол)
    /// </summary>
    public class Module
    {
        /// <summary>
        /// Уникальный идентификатор записи
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Наименование элемента управления
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Наименование формы
        /// </summary>
        public string Form { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Создание объекта класса Module по строке данных.
        /// </summary>
        /// <param name="queryResult">Строка данных</param>
        /// <returns></returns>
        public static Module ParseModule(DataRow queryResult)
        {
            Module module = new Module();

            if (queryResult == null)
                throw new ArgumentNullException("queryResult");

            Guid id;
            if (queryResult["id"] != null && Guid.TryParse(queryResult["id"].ToString(), out id))
                module.Id = id;
            else
                throw new ArgumentException("Can't parse Module. Invalid id parameter");

            module.Name = queryResult["name"].ToString();

            module.Form = queryResult["form"].ToString();

            module.Description = queryResult["description"].ToString();

            return module;
        }
    }

    /// <summary>
    /// Хранилище элементов управления
    /// </summary>
    public class ModuleStorage
    {
        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        private static DatabaseContext dbContext = DatabaseContext.Instance;

        /// <summary>
        /// Объект для работы с хранилищем
        /// </summary>
        private static ModuleStorage instance = new ModuleStorage();

        private ModuleStorage(DatabaseContext context)
            : base()
        {
            dbContext = context;
        }

        private ModuleStorage() { }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с хранилищем пользователей
        /// </summary>
        public static ModuleStorage Instance { get { return getInstance(); } }

        /// <summary>
        /// Метод для предоставления в окрущающую среду объекта для работы с хранилищем пользователей
        /// </summary>
        public static ModuleStorage getInstance()
        {
            return getInstance(dbContext);
        }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с хранилищем пользователей
        /// </summary>
        /// <param name="context">DatabaseContext объект</param>
        /// <returns>Объект для работы с хранилищем пользователей</returns>
        public static ModuleStorage getInstance(DatabaseContext context)
        {
            return new ModuleStorage(context);
        }

        /// <summary>
        /// Получение элемента управления из БД по идентификатору
        /// </summary>
        /// <param name="ID">Глобальный уникальный идентификатор записи</param>
        /// <returns>Объект типа Module</returns>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует элемента с таким ID</exception>
        public Module GetControl(Guid ID)
        {
            string query = string.Format("SELECT * " +
                                        "FROM [dbo].[Module] " +
                                        "WHERE id = '{0}'", ID.ToString());
            DataTable dt = dbContext.LoadFromDatabase(query, CommandType.Text);
            DataRow queryResult = (dt.Rows.Count != 0) ? (dt.Rows[0]) : (null);

            Module module = Module.ParseModule(queryResult);

            return module;
        }

        /// <summary>
        /// Получение элемента управления из БД по идентификатору
        /// </summary>
        /// <param name="controlName">Имя элемента управления</param>
        /// <param name="formName">Имя формы</param>
        /// <returns>Объект типа Module</returns>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует элемента</exception>
        public Module GetControl(string controlName, string formName)
        {
            string query = string.Format("SELECT * " +
                                        "FROM [dbo].[Module] " +
                                        "WHERE name = '{0}' AND form = '{1}'", controlName, formName);
            DataTable dt = dbContext.LoadFromDatabase(query, CommandType.Text);
            DataRow queryResult = (dt.Rows.Count != 0) ? (dt.Rows[0]) : (null);

            Module module = GetControl(queryResult.Field<Guid>("id"));

            return module;
        }

        /// <summary>
        /// Функция определяет доступен ли элемент управления для роли
        /// </summary>
        /// <param name="module">Элемент управления</param>
        /// <param name="role">Роль пользователя</param>
        /// <returns>Логическое значение доступен элемент или нет</returns>
        public bool ControlAvailable(Module module, Role role)
        {
            int count = (int)dbContext.ExecuteScalar(string.Format(
                "SELECT COUNT(*) " +
                "FROM [dbo].[ModuleRole] AS mr " +
                "WHERE module_id = '{0}' AND role_id = '{1}'", module.Id.ToString(), role.Id.ToString())
                , System.Data.CommandType.Text);
            if (count == 1) return true;
            else if (count == 0) return false;
            else throw new Exception(string.Format("Невозможно определить права для {0} и {1}.", module.Name, role.Name));
        }

        /// <summary>
        /// Сохранение значения доступности-недоступности элемента управления
        /// </summary>
        /// <param name="module">Элемент управления</param>
        /// <param name="role">Роль пользователя</param>
        /// <param name="isAvailable">Значение доступности-недоступности</param>
        public void SaveAvailableValue(Module module, Role role, bool isAvailable)
        {
            if (isAvailable)
            {
                AddRight(module, role);
            }
            else
            {
                DeleteRight(module, role);
            }
        }

        private void AddRight(Module module, Role role)
        {

            dbContext.ExecuteScalar(string.Format(
                "INSERT INTO [dbo].[ModuleRole] " +
                "VALUES " +
                "('{0}', '{1}', '{2}', GETDATE()) ", module.Id.ToString(), role.Id.ToString(), ((ProjectKernel.Classes.User.DBUser)ProjectKernel.Classes.User.CurrentUser.Instance).Id.ToString())
                , System.Data.CommandType.Text);
        }

        private void DeleteRight(Module module, Role role)
        {

            dbContext.ExecuteScalar(string.Format(
                "DELETE FROM [dbo].[ModuleRole] " +
                "WHERE module_id = '{0}' AND role_id = '{1}'", module.Id.ToString(), role.Id.ToString())
                , System.Data.CommandType.Text);
        }
    }
}
