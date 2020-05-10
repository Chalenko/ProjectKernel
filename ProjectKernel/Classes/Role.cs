using ProjectKernel.Classes.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ProjectKernel.Classes
{
    /// <summary>
    /// Класс роли пользователя
    /// </summary>
    public class Role
    {
        private List<DBUser> users = new List<DBUser>();
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Создатель
        /// </summary>
        public DBUser Creator { get; protected set; }

        /// <summary>
        /// Последний модификатор
        /// </summary>
        public DBUser Modifier { get; protected set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; protected set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime? ModifyDate { get; protected set; }

        /// <summary>
        /// Наименование роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание роли
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Список пользователей текущей роли
        /// </summary>
        public List<DBUser> Users { get { return users; } }

        /// <summary>
        /// Конструктор роли по умолчанию
        /// </summary>
        public Role() { Id = Guid.NewGuid(); }

        /// <summary>
        /// Конструктор роли с параметрами
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="description">Описание</param>
        public Role(string name, string description) : this() 
        {
            if (!string.IsNullOrWhiteSpace(name)) Name = name; else throw new ArgumentNullException("name");
            Description = description;
        }

        /// <summary>
        /// Создание объекта класса DBUser по строке данных.
        /// </summary>
        /// <param name="queryResult">Строка данных</param>
        /// <returns>Объект класса DBUser</returns>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда queryResult равен null</exception>
        public static Role ParseRole(DataRow queryResult)
        {
            Role role = new Role();

            if (queryResult == null)
                throw new ArgumentNullException("queryResult");

            Guid id;
            if (queryResult["id"] != null && Guid.TryParse(queryResult["id"].ToString(), out id))
                role.Id = id;
            else
                throw new ArgumentException("Can't parse Role. Invalid id parameter");

            role.Name = queryResult["name"].ToString();

            //if (queryResult["first_name"] == null)
            //    throw new ArgumentException("Can't parse BDUser. Invalid first_name parameter");
            role.Description = queryResult["description"].ToString();

            role.Creator = DBUserStorage.Instance.getUser(Guid.Parse(queryResult["creator_id"].ToString()));
            role.CreateDate = DateTime.Parse(queryResult["create_date"].ToString());
            Guid modifierId;
            if (queryResult["modifier_id"] != null && Guid.TryParse(queryResult["modifier_id"].ToString(), out modifierId))
                role.Modifier = DBUserStorage.Instance.getUser(modifierId);
            role.ModifyDate = queryResult.Field<DateTime?>("modify_date");

            return role;
        }

        internal void toDictionary(Dictionary<string, object> paramsDictionary)
        {
            paramsDictionary.Add("@id", this.Id.ToString());
            paramsDictionary.Add("@name", this.Name);
            paramsDictionary.Add("@description", this.Description);
        }

        /// <summary>
        /// Проверяет доступность элемента управления для текущей роли
        /// </summary>
        /// <param name="controlName">Наименование элемента управления</param>
        /// <param name="formName">Наименование родительской формы</param>
        /// <returns>Доступен ли элемент управления</returns>
        public bool IsAvailable(string controlName, string formName)
        {
            Module module = ModuleStorage.Instance.GetControl(controlName, formName);
            return ModuleStorage.Instance.ControlAvailable(module, this);
        }

        /// <summary>
        /// Проверяет доступность элемента управления для текущей роли
        /// </summary>
        /// <param name="module">Элемент управления</param>
        /// <returns>Доступен ли элемент управления</returns>
        public bool IsAvailable(Module module)
        {
            return ModuleStorage.Instance.ControlAvailable(module, this);
        }
    }

    /// <summary>
    /// Хранилище ролей
    /// </summary>
    public class DBRoleStorage
    {
        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        private static DatabaseContext dbContext = DatabaseContext.Instance;

        /// <summary>
        /// Объект для работы с хранилищем
        /// </summary>
        private static DBRoleStorage instance = new DBRoleStorage();

        private DBRoleStorage(DatabaseContext context)
            : base()
        {
            dbContext = context;
        }

        private DBRoleStorage() { }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с хранилищем ролей
        /// </summary>
        public static DBRoleStorage Instance { get { return getInstance(); } }

        /// <summary>
        /// Метод для предоставления в окрущающую среду объекта для работы с хранилищем ролей
        /// </summary>
        /// <returns>Объект для работы с хранилищем ролей</returns>
        public static DBRoleStorage getInstance()
        {
            return getInstance(dbContext);
        }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с хранилищем ролей
        /// </summary>
        /// <param name="context">DatabaseContext объект</param>
        /// <returns>Объект для работы с хранилищем ролей</returns>
        public static DBRoleStorage getInstance(DatabaseContext context)
        {
            return new DBRoleStorage(context);
        }

        /// <summary>
        /// Представление записей из БД в виде таблицы
        /// </summary>
        public DataTable GetTable()
        {
            string query = "SELECT id, name, description, creator_id, create_date, modifier_id, modify_date" + 
                            "FROM [dbo].[Role] " +
                            "ORDER BY [name] ";
            return dbContext.LoadFromDatabase(query, CommandType.Text);
        }

        /// <summary>
        /// Представление записей из БД в виде таблицы
        /// </summary>
        public DataTable GetView()
        {
            string query = "SELECT * FROM [dbo].[Role_view] ORDER BY 'name'";
            return dbContext.LoadFromDatabase(query, CommandType.Text);
        }
     
        /// <summary>
        /// Получение роли пользователя системы из БД по идентификатору
        /// </summary>
        /// <param name="ID">Глобальный уникальный идентификатор записи</param>
        /// <returns>Объект типа Role</returns>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует роли с таким ID</exception>
        public Role GetRole(Guid ID)
        {
            string query = string.Format("SELECT * " +
                                        "FROM [dbo].[Role] " +
                                        "WHERE id = '{0}'", ID.ToString());
            DataTable dt = dbContext.LoadFromDatabase(query, CommandType.Text);
            DataRow queryResult = (dt.Rows.Count != 0) ? (dt.Rows[0]) : (null);

            Role role = Role.ParseRole(queryResult);

            DataTable dtUsers = dbContext.LoadFromDatabase(string.Format("SELECT * FROM [dbo].[UserRole] WHERE role_id = '{0}'", role.Id.ToString()), CommandType.Text);
            foreach (DataRow row in dtUsers.Rows)
            {
                DBUser user = DBUserStorage.Instance.getUser(row.Field<Guid>("user_id"));
                role.Users.Add(user);
            }

            return role;
        }

        /// <summary>
        /// Получение роли пользователя системы из БД по наименованию
        /// </summary>
        /// <param name="name">Наименование роли</param>
        /// <returns>Объект типа Role</returns>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует роли с таким наименованием</exception>
        public Role GetRole(string name)
        {
            string query = string.Format("SELECT * " +
                                        "FROM [dbo].[Role] " +
                                        "WHERE name = '{0}'", name);
            DataTable dt = dbContext.LoadFromDatabase(query, CommandType.Text);
            DataRow queryResult = (dt.Rows.Count != 0) ? (dt.Rows[0]) : (null);

            Role role = GetRole(queryResult.Field<Guid>("id"));

            return role;
        }

        /// <summary>
        /// Добавление новой роли в таблицу
        /// </summary>
        /// <param name="role">Объект класса Role</param>
        /// <returns>ID новой записи</returns>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда параметр role равен null</exception>
        public Guid Add(Role role)
        {
            string recordId;
            
            if (role == null)
                throw new ArgumentNullException("role");

            string queryRole = String.Format(
                                "INSERT INTO [dbo].[Role] " +
                                "([id] ,[name] ,[description] ,[creator_id], [create_date]) " +
                                "VALUES " +
                                "(@id ,@name ,@description, '{0}', GETDATE()) " +
                                "SELECT @id ", ((DBUser)CurrentUser.Instance).Id);
            Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
            role.toDictionary(paramsDictionary);

            recordId = dbContext.ExecuteScalar(queryRole, CommandType.Text, paramsDictionary).ToString();
            return Guid.Parse(recordId);
        }

        /// <summary>
        /// Удаление записи из таблицы
        /// </summary>
        /// <param name="ID">Глобальный уникальный идентификатор записи</param>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует роли с таким ID</exception>
        public void Delete(Guid ID)
        {
            List<SqlCommand> comm = new List<SqlCommand>();
            string queryUserRole = string.Format("DELETE FROM [dbo].[UserRole] WHERE role_id = '{0}'", ID.ToString());
            comm.Add(dbContext.CreateCommand(queryUserRole, CommandType.Text));
            
            string queryRole = string.Format("DELETE FROM [dbo].[Role] WHERE id = '{0}'", ID.ToString());
            comm.Add(dbContext.CreateCommand(queryRole, CommandType.Text));
            //if (affectedRowCount == 0) throw new ArgumentException("Role is not exist");

            dbContext.ExecuteTransaction(comm);
        }

        /// <summary>
        /// Сохранение состояния существующего объекта в БД.
        /// Суть - апдейт, при передаче объекта отсутствующего в базе апдейт ничего не изменит
        /// </summary>
        /// <param name="role">Объект класса Role</param>
        /// <exception cref="System.ArgumentNullException">Иcключение выбрасывается когда параметр role равен null</exception>
        public void Update(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            using (TransactionScope scope = new TransactionScope())
            {
                string queryUpdate = string.Format("UPDATE [dbo].[Role] " +
                                            "SET [name] = @name ,[description] = @description, [modifier_id] = '{1}' ,[modify_date] = GETDATE() " +
                                            "WHERE [id] = '{0}'", role.Id, ((DBUser)CurrentUser.Instance).Id);

                Dictionary<string, object> paramsDictionaryUpdate = new Dictionary<string, object>();
                role.toDictionary(paramsDictionaryUpdate);
                dbContext.ExecuteCommand(queryUpdate, CommandType.Text, paramsDictionaryUpdate);

                scope.Complete();
            }
        }

        /// <summary>
        /// Представление списка пользователей относящихся к указанной роли в виде таблицы
        /// </summary>
        /// <param name="role">Роль пользователей</param>
        /// <returns>Таблица с пользователями принадлежащими роли</returns>
        public DataTable GetUserView(Role role)
        {
            string query = string.Format("SELECT * FROM [dbo].[UserRole_view] WHERE role_id = '{0}' ORDER BY 'login'", role.Id.ToString());
            return dbContext.LoadFromDatabase(query, CommandType.Text);
        }
    }

    /// <summary>
    /// Хранилище соответствий пользователь - роль
    /// </summary>
    public class UserRoleAssignimentStorage
    {
        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        private static DatabaseContext dbContext = DatabaseContext.Instance;

        /// <summary>
        /// Объект для работы с хранилищем
        /// </summary>
        private static UserRoleAssignimentStorage instance = new UserRoleAssignimentStorage();

        private UserRoleAssignimentStorage(DatabaseContext context)
            : base()
        {
            dbContext = context;
        }

        private UserRoleAssignimentStorage() { }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с хранилищем соответствий пользователь-роль
        /// </summary>
        public static UserRoleAssignimentStorage Instance { get { return getInstance(); } }

        /// <summary>
        /// Метод для предоставления в окрущающую среду объекта для работы с хранилищем соответствий пользователь-роль
        /// </summary>
        /// <returns>Объект для работы с хранилищем соответствий пользователь-роль</returns>
        public static UserRoleAssignimentStorage getInstance()
        {
            return getInstance(dbContext);
        }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с хранилищем соответствий пользователь-роль
        /// </summary>
        /// <param name="context">DatabaseContext объект</param>
        /// <returns>Объект для работы с хранилищем соответствий пользователь-роль</returns>
        public static UserRoleAssignimentStorage getInstance(DatabaseContext context)
        {
            return new UserRoleAssignimentStorage(context);
        }

        /// <summary>
        /// Определяет содержится ли объект в таблице соответствий
        /// </summary>
        /// <param name="roleId">Идентификатор роли</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Логической выражение содержится или нет</returns>
        public bool IsExist(Guid roleId, Guid userId)
        {
            string existQuery = string.Format(
                "SELECT COUNT(*) " +
                "FROM [dbo].[UserRole] AS ur " +
                "WHERE ur.user_id = '{0}' AND ur.role_id = '{1}'"
                , userId.ToString(), roleId.ToString());
            int count = (int)(dbContext.ExecuteScalar(existQuery, CommandType.Text));
            return count > 0;
        }

        /// <summary>
        /// Определяет содержится ли объект в таблице соответствий
        /// </summary>
        /// <param name="roleName">Наименование роли</param>
        /// <param name="userLogin">Логин пользователя</param>
        /// <returns>Логической выражение содержится или нет</returns>
        public bool IsExist(string roleName, string userLogin)
        {
            string existQuery = string.Format(
                "SELECT COUNT(ur.*) " +
                "FROM [dbo].[UserRole] AS ur " +
                "LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id " +
                "LEFT JOIN [dbo].[Role] AS r ON ur.role_id = r.id " + 
                "WHERE u.login = '{0}' AND r.name = '{1}'"
                , userLogin, roleName);
            int count = (int)(dbContext.ExecuteScalar(existQuery, CommandType.Text));
            return count > 0;
        }

        /// <summary>
        /// Добавление соответствия между ролью и пользователем
        /// </summary>
        /// <param name="roleId">Идентификатор роли</param>
        /// <param name="userId">Идентификатор пользователя</param>
        public void Add(Guid roleId, Guid userId)
        {
            string existQuery = string.Format(
                "INSERT INTO [dbo].[UserRole] " +
                "VALUES " + 
                "('{0}', '{1}', '{2}', GETDATE())"
                , userId.ToString(), roleId.ToString(), ((DBUser)CurrentUser.Instance).Id.ToString());
            dbContext.ExecuteScalar(existQuery, CommandType.Text);
        }

        /// <summary>
        /// Удаление соответствия между ролью и пользователем
        /// </summary>
        /// <param name="roleId">Идентификатор роли</param>
        /// <param name="userId">Идентификатор пользователя</param>
        public void Delete(Guid roleId, Guid userId)
        {
            string existQuery = string.Format(
                "DELETE FROM [dbo].[UserRole] " +
                "WHERE user_id = '{0}' AND role_id = '{1}' "
                , userId.ToString(), roleId.ToString());
            dbContext.ExecuteScalar(existQuery, CommandType.Text);
        }

        /*
        /// <summary>
        /// Представление записей из БД в виде таблицы
        /// </summary>
        public DataTable GetTable()
        {
            string query = "SELECT id, name, description, creator_id, create_date, modifier_id, modify_date" +
                            "FROM [dbo].[Role] " +
                            "ORDER BY [name] ";
            return dbContext.LoadFromDatabase(query, CommandType.Text);
        }

        /// <summary>
        /// Представление записей из БД в виде таблицы
        /// </summary>
        public DataTable GetView()
        {
            string query = "SELECT * FROM [dbo].[Role_view] ORDER BY 'name'";
            return dbContext.LoadFromDatabase(query, CommandType.Text);
        }

        /// <summary>
        /// Получение роли пользователя системы из БД по идентификатору
        /// </summary>
        /// <param name="ID">Глобальный уникальный идентификатор записи</param>
        /// <returns>Объект типа Role</returns>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует роли с таким ID</exception>
        public Role GetRole(Guid ID)
        {
            string query = string.Format("SELECT * " +
                                        "FROM [dbo].[Role] " +
                                        "WHERE id = '{0}'", ID.ToString());
            DataTable dt = dbContext.LoadFromDatabase(query, CommandType.Text);
            DataRow queryResult = (dt.Rows.Count != 0) ? (dt.Rows[0]) : (null);

            Role role = Role.ParseRole(queryResult);

            DataTable dtUsers = dbContext.LoadFromDatabase(string.Format("SELECT * FROM [dbo].[UserRole] WHERE role_id = '{0}'", role.Id.ToString()), CommandType.Text);
            foreach (DataRow row in dtUsers.Rows)
            {
                DBUser user = DBUserStorage.Instance.getUser(row.Field<Guid>("user_id"));
                role.Users.Add(user);
            }

            return role;
        }

        /// <summary>
        /// Получение роли пользователя системы из БД по наименованию
        /// </summary>
        /// <param name="name">Наименование роли</param>
        /// <returns>Объект типа Role</returns>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует роли с таким наименованием</exception>
        public Role GetRole(string name)
        {
            string query = string.Format("SELECT * " +
                                        "FROM [dbo].[Role] " +
                                        "WHERE name = '{0}'", name);
            DataTable dt = dbContext.LoadFromDatabase(query, CommandType.Text);
            DataRow queryResult = (dt.Rows.Count != 0) ? (dt.Rows[0]) : (null);

            Role role = GetRole(queryResult.Field<Guid>("id"));

            return role;
        }

        /// <summary>
        /// Добавление новой роли в таблицу
        /// </summary>
        /// <param name="role">Объект класса Role</param>
        /// <returns>ID новой записи</returns>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда параметр role равен null</exception>
        public Guid Add(Role role)
        {
            string recordId;

            if (role == null)
                throw new ArgumentNullException("role");

            string queryRole = String.Format("DECLARE @id UNIQUEIDENTIFIER " +
                                "SET @id = newid() " +
                                "INSERT INTO [dbo].[Role] " +
                                "([id] ,[name] ,[description] ,[creator_id], [create_date]) " +
                                "VALUES " +
                                "(@id ,@name ,@description, '{0}', GETDATE()) " +
                                "SELECT @id ", ((DBUser)CurrentUser.Instance).Id);
            Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
            role.toDictionary(paramsDictionary);

            recordId = dbContext.ExecuteScalar(queryRole, CommandType.Text, paramsDictionary).ToString();
            return Guid.Parse(recordId);
        }

        /// <summary>
        /// Удаление записи из таблицы
        /// </summary>
        /// <param name="ID">Глобальный уникальный идентификатор записи</param>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует пользователя с таким ID</exception>
        public void Delete(Guid ID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                string queryRole = string.Format("DELETE FROM [dbo].[Role] WHERE id = '{0}'", ID.ToString());
                int affectedRowCount = dbContext.ExecuteCommand(dbContext.CreateCommand(queryRole, CommandType.Text));
                if (affectedRowCount == 0) throw new ArgumentException("Role is not exist");

                scope.Complete();
            }
        }

        /// <summary>
        /// Сохранение состояния существующего объекта в БД.
        /// Суть - апдейт, при передаче объекта отсутствующего в базе апдейт ничего не изменит
        /// </summary>
        /// <param name="role">Объект класса Role</param>
        /// <exception cref="System.ArgumentNullException">Иcключение выбрасывается когда параметр role равен null</exception>
        public void Update(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            using (TransactionScope scope = new TransactionScope())
            {
                string queryUpdate = string.Format("UPDATE [dbo].[Role] " +
                                            "SET [name] = @name ,[description] = @description, [modifier_id] = '{1}' ,[modify_date] = GETDATE() " +
                                            "WHERE [id] = '{0}'", role.Id, ((DBUser)CurrentUser.Instance).Id);

                Dictionary<string, object> paramsDictionaryUpdate = new Dictionary<string, object>();
                role.toDictionary(paramsDictionaryUpdate);
                dbContext.ExecuteCommand(queryUpdate, CommandType.Text, paramsDictionaryUpdate);

                scope.Complete();
            }
        }

        /// <summary>
        /// Представление списка пользователей относящихся к указанной роли в виде таблицы
        /// </summary>
        /// <param name="role">Роль пользователей</param>
        /// <returns>Таблица с пользователями принадлежащими роли</returns>
        public DataTable GetUserView(Role role)
        {
            string query = string.Format("SELECT * FROM [dbo].[UserRole_view] WHERE role_id = '{0}' ORDER BY 'login'", role.Id.ToString());
            return dbContext.LoadFromDatabase(query, CommandType.Text);
        }
         * */
    }
}
