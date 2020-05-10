using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ProjectKernel.Classes.Logger;

namespace ProjectKernel.Classes.User
{
    /// <summary>
    /// Пользователь системы из БД
    /// </summary>
    public class DBUser : User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Идетификатор создателя
        /// </summary>
        public Guid CreatorId { get; private set; }

        /// <summary>
        /// Идентификатор последнего модификатора
        /// </summary>
        public Guid? ModifierId { get; private set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime? ModifyDate { get; private set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public new string Name { get { return base.Name; } set { if (!string.IsNullOrWhiteSpace(value)) base.Name = value; else throw new ArgumentNullException("name"); } }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public new string Surname { get { return base.Surname; } set { if (!string.IsNullOrWhiteSpace(value)) base.Surname = value; else throw new ArgumentNullException("surname"); } }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public Password Password { get; protected internal set; }

        /// <summary>
        /// Необходимо сменить пароль
        /// </summary>
        public bool HaveToChangePassword
        {
            get
            {
                return Password.HaveToChange;
            }
            set
            {
                Password.HaveToChange = value;
            }
        }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public new string Login { get { return base.Login; } set { if (!string.IsNullOrWhiteSpace(value)) base.Login = value; else throw new ArgumentNullException("login"); } }

        /// <summary>
        /// Подразделение пользователя
        /// </summary>
        public new string Department { get { return base.Department; } set { if (!string.IsNullOrWhiteSpace(value)) base.Department = value; else throw new ArgumentNullException("department"); } }

        /// <summary>
        /// Состояние пользователя (разрешена работа в системе - не разрешена)
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Показывает был ли изменен список групп пользователя
        /// </summary>
        public virtual bool groupWasChanged { get; private set; }

        private DBUser() : base() 
        {
            groupWasChanged = false;
            Type = UserType.Database;
            IsActive = true;
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="surname">Фамилия пользователя</param>
        /// <param name="name">Имя пользователя</param>
        /// <param name="patronymic">Отчество пользователя</param>
        /// <param name="login">Логин пользователя</param>
        /// <param name="department">Подразделение пользователя</param>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда один из параметров surname, name, login или department равен null или пустой</exception>
        public DBUser(string surname, string name, string patronymic, string login, string department) : this() 
        {
            if (!string.IsNullOrWhiteSpace(surname)) Surname = surname; else throw new ArgumentNullException("surname");
            if (!string.IsNullOrWhiteSpace(name)) Name = name; else throw new ArgumentNullException("name");
            Patronymic = patronymic;
            if (!string.IsNullOrWhiteSpace(login)) Login = login; else throw new ArgumentNullException("login");
            if (!string.IsNullOrWhiteSpace(department)) Department = department; else throw new ArgumentNullException("department");
            Groups = new List<string>(0);
            string salt = Password.GenerateSalt();
            Password = new Password(salt, Password.GenerateHash(salt, "qwerty"), true);
        }

        /// <summary>
        /// Добавление групп в список групп пользователя
        /// </summary>
        /// <param name="groups">Список групп для добавления</param>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда список групп равен null, когда список содержит null или когда список содержит пустую запись</exception>
        public void addGroups(List<string> groups)
        {
            if (groups == null)
                throw new ArgumentNullException("groups");

            if(groups.Count!=0)
            { 
                foreach (string group in groups)
                    this.addGroup(group);
                groupWasChanged = true;
            }
        }

        /// <summary>
        /// Добавление группы в список групп пользователя
        /// </summary>
        /// <param name="groupName">Имя группы для добавления</param>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда значение имени группы равно null или пустой строке</exception>
        public void addGroup(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentNullException("group name");

            if (!Groups.Contains(groupName))
            {
                Groups.Add(groupName);
                groupWasChanged = true;
            }
        }

        /// <summary>
        /// Удаление всех групп у пользователя
        /// </summary>
        public void removeGroups()
        {
            Groups.Clear();
            groupWasChanged = true;
        }

        /// <summary>
        /// Удаление группы у пользователя системы
        /// </summary>
        /// <param name="groupName">Имя группы для удаления</param>
        public void removeGroup(string groupName)
        {
            //throw new NotImplementedException();
            if(Groups.Remove(groupName))
                groupWasChanged = true;
        }

        /// <summary>
        /// Создание объекта класса DBUser по строке данных.
        /// </summary>
        /// <param name="queryResult">Строка данных</param>
        /// <returns>Объект класса DBUser</returns>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда queryResult равен null</exception>
        public static DBUser ParseUser(DataRow queryResult)
        {
            DBUser user = new DBUser();

            if (queryResult == null)
                throw new ArgumentNullException("queryResult");

            Guid id;
            if (queryResult["id"] != null && Guid.TryParse(queryResult["id"].ToString(), out id))
                user.Id = id;
            else
                throw new ArgumentException("Can't parse BDUser. Invalid id parameter");
            //user.id = queryResult["id"].ToString();

            //if (queryResult["surname"] == null)
            //    throw new ArgumentException("Can't parse BDUser. Invalid surname parameter");
            user.Surname = queryResult["surname"].ToString();

            //if (queryResult["first_name"] == null)
            //    throw new ArgumentException("Can't parse BDUser. Invalid first_name parameter");
            user.Name = queryResult["first_name"].ToString();

            //if (queryResult["patronymic_name"] == null)
            //    throw new ArgumentException("Can't parse BDUser. Invalid patronymic_name parameter");
            user.Patronymic = queryResult["patronymic_name"].ToString();

            //if (queryResult["login"] == null)
            //    throw new ArgumentException("Can't parse BDUser. Invalid login parameter");
            user.Login = queryResult["login"].ToString();

            //if (queryResult["department"] == null)
            //    throw new ArgumentException("Can't parse BDUser. Invalid department parameter");
            user.Department = queryResult["department"].ToString();

            //user.HaveToChangePassword = bool.Parse(queryResult["change_password"].ToString());

            user.IsActive = bool.Parse(queryResult["is_active"].ToString());

            user.Password = new Password(queryResult["salt"].ToString(), queryResult["password"].ToString(), bool.Parse(queryResult["change_password"].ToString()));

            user.CreatorId = Guid.Parse(queryResult["creator_id"].ToString());
            user.CreateDate = DateTime.Parse(queryResult["create_date"].ToString());
            Guid modifierId;
            if (queryResult["modifier_id"] != null && Guid.TryParse(queryResult["modifier_id"].ToString(), out modifierId))
                user.ModifierId = modifierId;
            user.ModifyDate = queryResult.Field<DateTime?>("modify_date");

            return user;
        }

        internal static void ParseGroups(DataTable roles, ref DBUser user)
        {
            IEnumerable<DataRow> ie = roles.AsEnumerable();
            user.Groups = ie.Select<DataRow, string>(el => el[0].ToString()).Distinct().ToList();
        }

        internal void toDictionary(Dictionary<string, object> paramsDictionary)
        {
            paramsDictionary.Add("@surname", this.Surname);
            paramsDictionary.Add("@first_name", this.Name);
            paramsDictionary.Add("@patronymic_name", this.Patronymic);
            paramsDictionary.Add("@login", this.Login);
            paramsDictionary.Add("@department", this.Department);
            paramsDictionary.Add("@is_active", this.IsActive);
        }

        internal void resetGroupWasChanged()
        {
            this.groupWasChanged = false;
        }
    }

    /// <summary>
    /// Класс для работы с пользователями системы из БД
    /// </summary>
    public class DBUserStorage
    {
        /// <summary>
        /// Контекст для работы с БД
        /// </summary>
        private static DatabaseContext dbContext = DatabaseContext.Instance;

        /// <summary>
        /// Объект для работы с хранилищем
        /// </summary>
        private static DBUserStorage instance = new DBUserStorage();

        private DBUserStorage(DatabaseContext context) : base()
        {
            dbContext = context;
        }

        private DBUserStorage() { }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с хранилищем пользователей
        /// </summary>
        public static DBUserStorage Instance { get { return getInstance(); } }

        /// <summary>
        /// Метод для предоставления в окрущающую среду объекта для работы с хранилищем пользователей
        /// </summary>
        public static DBUserStorage getInstance()
        {
            return getInstance(dbContext);
        }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с хранилищем пользователей
        /// </summary>
        /// <param name="context">DatabaseContext объект</param>
        /// <returns>Объект для работы с хранилищем пользователей</returns>
        public static DBUserStorage getInstance(DatabaseContext context)
        {
            return new DBUserStorage(context);
        }

        /// <summary>
        /// Представление записей из БД в виде таблицы
        /// </summary>
        public DataTable getTable()
        {
            string query = "SELECT [surname] AS 'Фамилия' ,[first_name] AS 'Имя' ,[patronymic_name] AS 'Отчество' " +
                            ",[login] AS 'Логин' ,[department] AS 'Подразделение' " +
                            "FROM [dbo].[User] " +
                            "ORDER BY [surname] ,[first_name] ,[patronymic_name] ";
            return dbContext.LoadFromDatabase(query, CommandType.Text);
        }

        /// <summary>
        /// Представление записей из БД в виде таблицы
        /// </summary>
        public DataTable getView()
        {
            string query = "SELECT * FROM [dbo].[User_view] ORDER BY 'Логин'";
            return dbContext.LoadFromDatabase(query, CommandType.Text);
        }

        /// <summary>
        /// Получение пользователя системы из БД по логину
        /// </summary>
        /// <param name="login">Логин записи</param>
        /// <returns>Объект типа DBUser</returns>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует пользователя с таким логином</exception>
        public DBUser getUser(string login)
        {
            string query = string.Format("SELECT [id] FROM [dbo].[User] WHERE login = '{0}'", login);
            //return getUser(Guid.Parse(dbContext.ExecuteScalar(query, CommandType.Text).ToString()));

            object result = dbContext.ExecuteScalar(query, CommandType.Text);
            if (result != null)
                return getUser(Guid.Parse(dbContext.ExecuteScalar(query, CommandType.Text).ToString()));
            else
                throw new UserNotExistException(String.Format("User with login '{0}' is not exist", login));
        }

        /// <summary>
        /// Получение пользователя системы из БД по ID
        /// </summary>
        /// <param name="ID">Глобальный уникальный идентификатор записи</param>
        /// <returns>Объект типа DBUser</returns>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует пользователя с таким ID</exception>
        public DBUser getUser(Guid ID)
        {
            string query = string.Format("SELECT [id] ,[surname] ,[first_name] ,[patronymic_name] " +
                                        ",[login] ,[department] , [is_active], [salt] ,[password], [change_password], [creator_id], [create_date], [modifier_id], [modify_date] " +
                                        "FROM [dbo].[User] " +
                                        "WHERE id = '{0}'", ID.ToString());
            DataTable dt = dbContext.LoadFromDatabase(query, CommandType.Text);
            DataRow queryResult = (dt.Rows.Count != 0) ? (dt.Rows[0]) : (null);

            DBUser user;
            try
            {
                user = DBUser.ParseUser(queryResult);
            }
            catch (ArgumentNullException ex)
            {
                throw new UserNotExistException(String.Format("User with ID '{0}' is not exist", ID.ToString()), ex);
            }

            query = String.Format("SELECT name FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[Role] AS r ON ur.role_id = r.id WHERE user_id = '{0}' ORDER BY r.name ASC", ID.ToString());
            DataTable roles = dbContext.LoadFromDatabase(query, CommandType.Text);
            DBUser.ParseGroups(roles, ref user);
            return user;
        }

        /// <summary>
        /// Добавление новой записи в таблицу
        /// </summary>
        /// <param name="user">Объект класса DBUser</param>
        /// <returns>ID новой записи</returns>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда параметр user равен null</exception>
        /// <exception cref="ProjectKernel.Classes.User.UserAlreadyExistException">Исключение выбрасывается когда в базе уже существует пользователя с таким логином</exception>
        public Guid add(DBUser user)
        {
            string recordId;
            try
            {
                if (user == null)
                    throw new ArgumentNullException("user");

                List<System.Data.SqlClient.SqlCommand> comm = new List<System.Data.SqlClient.SqlCommand>();
                
                //Add command for insert user
                string queryUser = String.Format("DECLARE @id UNIQUEIDENTIFIER " +
                                    "SET @id = newid() " +
                                    "INSERT INTO [dbo].[User] " +
                                    "([id] ,[surname] ,[first_name] ,[patronymic_name] " +
                                    ",[login] ,[salt] ,[password], [change_password], [department], [is_active], [creator_id], [create_date]) " +
                                    "VALUES " +
                                    "(@id ,@surname ,@first_name ,@patronymic_name " +
                                    ",@login ,@salt ,@password, @change, @department, @is_active, '{0}', GETDATE()) " +
                                    "SELECT @id ", ((DBUser)CurrentUser.Instance).Id);
                Dictionary<string, object> paramsDictionary = new Dictionary<string, object>();
                user.toDictionary(paramsDictionary);

                paramsDictionary.Add("@salt", user.Password.salt);
                paramsDictionary.Add("@password", user.Password.hashPassword);
                paramsDictionary.Add("@change", user.Password.HaveToChange);
                
                comm.Add(dbContext.CreateCommand(queryUser, CommandType.Text, paramsDictionary)); //Add command for insert user
                
                //Add commands for insert user groups
                foreach (string role in user.Groups.Distinct())
                {
                    string queryRole = String.Format("DECLARE @user_id UNIQUEIDENTIFIER " + 
                                                    "DECLARE @role_id UNIQUEIDENTIFIER " +
                                                    "SELECT @user_id = id FROM [dbo].[User] WHERE login = '{0}' " +
                                                    "SELECT @role_id = id FROM [dbo].[Role] WHERE name = '{1}' " +
                                                    "INSERT INTO [dbo].[UserRole] " +
                                                    "([user_id] ,[role_id] ,[creator_id] ,[create_date]) " +
                                                    "VALUES " +
                                                    "(@user_id, @role_id, '{2}', GETDATE()) "
                                                    , user.Login, role, ((DBUser)CurrentUser.Instance).Id);
                    comm.Add(dbContext.CreateCommand(queryRole, CommandType.Text));
                }

                //Add command for insert activity record
                string queryActivity = String.Format("INSERT INTO [dbo].[Activity] " +
                                                    "([user_login], [state], [last_login_datetime], [last_login_workstation], [last_logout_datetime]) " +
                                                    "VALUES " +
                                                    "('{0}', 'log_out', GETDATE(), '{1}', GETDATE()) ", user.Login, Environment.MachineName);
                comm.Add(dbContext.CreateCommand(queryActivity, CommandType.Text));

                dbContext.ExecuteTransaction(comm);
                
                //Get user id
                string queryId = string.Format("SELECT id FROM [dbo].[User] WHERE login = '{0}'", user.Login);
                recordId = dbContext.ExecuteScalar(queryId, CommandType.Text).ToString();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if ((ex.Number == 2627) || (ex.Number == 2601))
                //if (ex.Message.Contains("\"CK_Users_login\"") && ex.Message.Contains("UNIQUE KEY") && ex.Message.Contains("\"dbo.Users\"") && ex.Message.Contains(user.Login))
                {
                    Exception exc = new UserAlreadyExistException(string.Format("User with login '{0}' is already exist in this system", user.Login), ex);
                    throw exc;
                }
                throw ex;
            }

            return Guid.Parse(recordId);
        }

        /// <summary>
        /// Удаление записи из таблицы
        /// </summary>
        /// <param name="ID">Глобальный уникальный идентификатор записи</param>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует пользователя с таким ID</exception>
        public void delete(Guid ID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                List<System.Data.SqlClient.SqlCommand> commands = new List<System.Data.SqlClient.SqlCommand>();

                string queryActivity = string.Format("DELETE FROM [dbo].[Activity] WHERE user_login in (SELECT login FROM [dbo].[User] WHERE id = '{0}')", ID.ToString());
                commands.Add(dbContext.CreateCommand(queryActivity, CommandType.Text));
                string queryRole = string.Format("DELETE FROM [dbo].[UserRole] WHERE user_id = '{0}'", ID.ToString());
                commands.Add(dbContext.CreateCommand(queryRole, CommandType.Text));
                string queryUser = string.Format("DELETE FROM [dbo].[User] WHERE id = '{0}'", ID.ToString());
                commands.Add(dbContext.CreateCommand(queryUser, CommandType.Text));
                //int affectedRowCount = dbContext.ExecuteCommand(dbContext.CreateCommand(queryUser, CommandType.Text));
                //if (affectedRowCount == 0) throw new ArgumentException("User is not exist");
                dbContext.ExecuteTransaction(commands);

                scope.Complete();
            }
        }

        /// <summary>
        /// Удаление записи из таблицы
        /// </summary>
        /// <param name="login">Логин записи</param>
        /// <exception cref="System.ArgumentException">Исключение выбрасывается когда в базе не существует пользователя с таким логином</exception>
        public void delete(string login)
        {
            string query = string.Format("SELECT [id] FROM [dbo].[User] WHERE login = '{0}'", login);
            object result = dbContext.ExecuteScalar(query, CommandType.Text);
            if (result != null)
                delete(Guid.Parse(dbContext.ExecuteScalar(query, CommandType.Text).ToString()));
            else
                throw new ArgumentException("User is not exist");
        }

        /// <summary>
        /// Сохранение состояния существующего объекта в БД.
        /// Суть - апдейт, при передаче объекта отсутствующего в базе апдейт ничего не изменит
        /// </summary>
        /// <param name="user">Объект класса DBUser</param>
        /// <exception cref="System.ArgumentNullException">Иcключение выбрасывается когда параметр user равен null</exception>
        public void update(DBUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            using (TransactionScope scope = new TransactionScope())
            {
                List<System.Data.SqlClient.SqlCommand> commands = new List<System.Data.SqlClient.SqlCommand>();

                string queryUpdate = string.Format("UPDATE [dbo].[User] " +
                                            "SET [surname] = @surname ,[first_name] = @first_name ,[patronymic_name] = @patronymic_name " +
                                            ",[login] = @login ,[change_password] = @change, [department] = @department, [is_active] = @is_active, [modifier_id] = '{1}' ,[modify_date] = GETDATE() " +
                                            "WHERE [id] = '{0}'", user.Id, ((DBUser)CurrentUser.Instance).Id);

                Dictionary<string, object> paramsDictionaryUpdate = new Dictionary<string, object>();
                user.toDictionary(paramsDictionaryUpdate);
                paramsDictionaryUpdate.Add("@change", user.Password.HaveToChange);
                commands.Add(dbContext.CreateCommand(queryUpdate, CommandType.Text, paramsDictionaryUpdate));

                if (user.groupWasChanged == true)
                {
                    string deleteRoleQuery = String.Format("DELETE FROM [dbo].[UserRole] WHERE [user_id] = '{0}'", user.Id);
                    commands.Add(dbContext.CreateCommand(deleteRoleQuery, CommandType.Text));

                    foreach (string role in user.Groups)
                    {
                        string queryRole = String.Format("DECLARE @role_id UNIQUEIDENTIFIER " +
                                    "SELECT @role_id = id FROM Role WHERE name = '{0}' " +
                                    "INSERT INTO [dbo].[UserRole] " +
                                    "([user_id] ,[role_id] ,[creator_id] ,[create_date]) " +
                                    "VALUES " +
                                    "('{1}' ,@role_id, '{2}', GETDATE()) ", role, user.Id, ((DBUser)CurrentUser.Instance).Id);
                        commands.Add(dbContext.CreateCommand(queryRole, CommandType.Text));
                    }
                }

                dbContext.ExecuteTransaction(commands);
                user.resetGroupWasChanged();
                scope.Complete();
            }
        }

        /// <summary>
        /// Изменение пароля пользователя в БД.
        /// </summary>
        /// <param name="user">Объект класса DBUser для которого необходимо сменить пароль</param>
        /// <param name="newPassword">Новый пароль пользователя</param>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда один из параметров user или newPassword равен null или newPassword является пустой строкой</exception>
        public void changePassword(DBUser user, string newPassword)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentNullException("new password");
            
            using (TransactionScope scope = new TransactionScope())
            {
                user.Password.salt = Password.GenerateSalt();
                user.Password.hashPassword = Password.GenerateHash(user.Password.salt, newPassword);
                dbContext.ExecuteScalar(String.Format("UPDATE [dbo].[User] SET salt = '{0}', password = '{1}', change_password = 0 WHERE login = '{2}'", user.Password.salt, user.Password.hashPassword, user.Login), CommandType.Text);
                scope.Complete();
            }
        }

        /// <summary>
        /// Изменение пароля пользователя в БД.
        /// </summary>
        /// <param name="user">Объект класса DBUser для которого необходимо сменить пароль</param>
        /// <param name="newPassword">Новый пароль пользователя</param>
        /// <param name="haveToChangePassword">Сменить пароль при первом входе в систему</param>
        /// <exception cref="System.ArgumentNullException">Исключение выбрасывается когда один из параметров user или newPassword равен null или newPassword является пустой строкой</exception>
        public void changePassword(DBUser user, string newPassword, bool haveToChangePassword)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentNullException("new password");

            using (TransactionScope scope = new TransactionScope())
            {
                user.Password.salt = Password.GenerateSalt();
                user.Password.hashPassword = Password.GenerateHash(user.Password.salt, newPassword);
                dbContext.ExecuteScalar(String.Format("UPDATE [dbo].[User] SET salt = '{0}', password = '{1}', change_password = '{2}' WHERE login = '{3}'", user.Password.salt, user.Password.hashPassword, haveToChangePassword, user.Login), CommandType.Text);
                scope.Complete();
            }
        }

        /// <summary>
        /// Аутентификация пользователя.
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль для проверки</param>
        /// <returns>true в случае удачной аутентификации, false - иначе</returns>
        public bool checkUser(string login, string password)
        {
            DBUser user = DBUserStorage.Instance.getUser(login);
            string salt = Convert.ToString(dbContext.ExecuteScalar(String.Format("SELECT salt FROM [dbo].[User] WHERE login = '{0}'", user.Login), System.Data.CommandType.Text));
            string goodPassword = Convert.ToString(dbContext.ExecuteScalar(String.Format("SELECT password FROM [dbo].[User] WHERE login = '{0}'", user.Login), System.Data.CommandType.Text));
            return PasswordVerificator.VerifyPassword(salt, password, goodPassword);
        }

        /// <summary>
        /// Сброс пароля на стандартный 'qwerty'
        /// </summary>
        /// <param name="user">Объект класса DBUser</param>
        /// <returns>true в случае удачного сброса, false - иначе</returns>
        public void resetPassword(DBUser user)
        {
            changePassword(user, "qwerty");
        }
    }

    /// <summary>
    /// Это исключение выбрасывается, если пользователь уже существует в системе
    /// </summary>
    public class UserAlreadyExistException : ArgumentException
    {
        /// <summary>
        /// Выполняет инициализацию нового экземпляра класса ProjectKernel.Classes.User.UserAlreadyExistException
        /// с указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое
        /// стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке с объяснением причины исключения.</param>
        /// <param name="innerException">Исключение, являющееся причиной текущего исключения.Если параметр innerException
        /// не является указателем NULL, текущее исключение вызывается в блоке catch,
        /// обрабатывающем внутренние исключения.</param>
        public UserAlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Это исключение выбрасывается, если пользователь не существует в системе
    /// </summary>
    public class UserNotExistException : ObjectNotExistException
    {
        /// <summary>
        /// Выполняет инициализацию нового экземпляра класса ProjectKernel.Classes.User.UserNotExistException с указанным сообщением об ошибке.
        /// </summary>
        /// <param name="message">Сообщение об ошибке с объяснением причины исключения.</param>
        public UserNotExistException(string message) : base(message) { }

        /// <summary>
        /// Выполняет инициализацию нового экземпляра класса ProjectKernel.Classes.User.UserNotExistException с указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке с объяснением причины исключения.</param>
        /// <param name="innerException">Исключение, являющееся причиной текущего исключения.Если параметр innerException не является указателем NULL, текущее исключение вызывается в блоке catch, обрабатывающем внутренние исключения.</param>
        public UserNotExistException(string message, Exception innerException) : base(message, innerException) { }
    }
}
