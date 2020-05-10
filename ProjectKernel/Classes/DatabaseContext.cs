using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace ProjectKernel.Classes
{
    /// <summary>
    /// Класс для работы с запросами в БД
    /// </summary>
    public sealed class DatabaseContext
    {

        /// <summary>
        /// Соединение с БД
        /// </summary>
        private SqlConnection connection = new SqlConnection(ProjectKernel.Properties.Settings.Default.dbConnection);

        /// <summary>
        /// Хранилище контекстов БД которые и взаимодействует с БД, с которыми в свою очередь работают все остальные классы
        /// </summary>
        private static Dictionary<string, DatabaseContext> contexts = new Dictionary<string, DatabaseContext>();

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с БД используещее дефолтное соединение из параметров проекта
        /// </summary>
        public static DatabaseContext Instance { get {return getInstance(); } }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с БД используещее connection в качестве строки подключения
        /// </summary>
        /// <param name="connection"> Строка подключения</param>
        /// <returns>Объект для работы с БД</returns>
        public static DatabaseContext getInstance(string connection)
        {
            if (!contexts.ContainsKey(connection)) contexts.Add(connection, new DatabaseContext(connection));
            return contexts[connection];
        }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с БД используещее объект connection в качестве подключения
        /// </summary>
        /// <param name="connection">SqlConnection объект</param>
        /// <returns>Объект для работы с БД</returns>
        public static DatabaseContext getInstance(SqlConnection connection)
        {
            return getInstance(connection.ConnectionString);
        }

        /// <summary>
        /// Свойство для предоставления в окрущающую среду объекта для работы с БД используещее дефолтное соединение из параметров проекта
        /// </summary>
        public static DatabaseContext getInstance()
        {
            return getInstance(ProjectKernel.Properties.Settings.Default.dbConnection);
        }

        private DatabaseContext() { }

        private DatabaseContext(string connectionString) : this()
        {
            this.connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Создание SqlCommand по параметрам
        /// </summary>
        /// <param name="commandText">Текст запроса</param>
        /// <param name="commandType">Тип запроса</param>
        /// <param name="parameters">Параметры запроса</param>
        /// <returns>Команда, готовая к выполнению</returns>
        public SqlCommand CreateCommand(string commandText, CommandType commandType, Dictionary<string, object> parameters = null)
        {
            SqlCommand command = createCommand(commandText, commandType);
            List<SqlParameter> par = createParametersFromDictionary(parameters);
            includeParameters(command, par);
            return command;
        }

        /// <summary>
        /// Создание SqlCommand по параметрам
        /// </summary>
        /// <param name="commandText">Текст запроса</param>
        /// <param name="commandType">Тип запроса</param>
        /// <param name="parameters">Параметры запроса</param>
        /// <returns>Команда, готовая к выполнению</returns>
        public SqlCommand CreateCommand(string commandText, CommandType commandType, List<SqlParameter> parameters)
        {
            SqlCommand command = createCommand(commandText, commandType);
            includeParameters(command, parameters);
            return command;
        }

        /// <summary>
        /// Выполняет команду и возвращает количество задействованных строк
        /// </summary>
        /// <param name="command">SqlCommand команда</param>
        /// <returns>Возвращает количество задействованных строк</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException">Невозможно открыть подключение без указания сервера или источника данных.or
        /// Подключение уже открыто.</exception>
        /// <exception cref="System.Data.SqlClient.SqlException">При открытии подключения возникла ошибка на уровне соединения.Если свойство System.Data.SqlClient.SqlException.Number содержит значение 18487 или 18488, значит заданный пароль устарел или должен быть сброшен.Для получения дополнительных сведений см. описание метода System.Data.SqlClient.SqlConnection.ChangePassword(System.String,System.String).</exception>
        /// <exception cref="System.Data.SqlClient.SqlException">Исключение генерируется при выполнении команды над заблокированной строкой.Это исключение не генерируется при использовании Microsoft .NET Framework версии 1.0.</exception>
        public int ExecuteCommand(SqlCommand command)
        {
            int rowCount;
            if (command == null)
                throw new ArgumentNullException("command");
            if (command.Connection == null || string.IsNullOrWhiteSpace(command.Connection.ConnectionString))
                throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(command.CommandText))
                throw new ArgumentNullException("command text");
            if (!command.Connection.ConnectionString.Equals(connection.ConnectionString))
                throw new ArgumentException("Illegal command connection");
            try
            {
                command.Connection.Open();
                rowCount = command.ExecuteNonQuery();
            }
            finally
            {
                command.Connection.Close();
            }
            return rowCount;
        }

        /// <summary>
        /// Выполняет команду и возвращает количество задействованных строк
        /// </summary>
        /// <param name="commandText">Текст команды</param>
        /// <param name="type">Тип команды System.Data.CommandType</param>
        /// <param name="parameters">Набор параметров с именами и значениями</param>
        /// <returns>Возвращает количество задействованных строк</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Data.SqlClient.SqlException">При открытии подключения возникла ошибка на уровне соединения.Если свойство System.Data.SqlClient.SqlException.Number содержит значение 18487 или 18488, значит заданный пароль устарел или должен быть сброшен.Для получения дополнительных сведений см. описание метода System.Data.SqlClient.SqlConnection.ChangePassword(System.String,System.String).</exception>
        /// <exception cref="System.Data.SqlClient.SqlException">Исключение генерируется при выполнении команды над заблокированной строкой.Это исключение не генерируется при использовании Microsoft .NET Framework версии 1.0.</exception>
        public int ExecuteCommand(string commandText, CommandType type, Dictionary<string, object> parameters = null)
        {
            using (SqlCommand command = CreateCommand(commandText, type, parameters))
            {
                return this.ExecuteCommand(command);
            }
        }

        /// <summary>
        /// Выполняет команду и возвращает количество задействованных строк
        /// </summary>
        /// <param name="commandText">Текст команды</param>
        /// <param name="type">Тип команды System.Data.CommandType</param>
        /// <param name="parameters">Набор параметров с именами и значениями</param>
        /// <returns>Возвращает количество задействованных строк</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Data.SqlClient.SqlException">При открытии подключения возникла ошибка на уровне соединения.Если свойство System.Data.SqlClient.SqlException.Number содержит значение 18487 или 18488, значит заданный пароль устарел или должен быть сброшен.Для получения дополнительных сведений см. описание метода System.Data.SqlClient.SqlConnection.ChangePassword(System.String,System.String).</exception>
        /// <exception cref="System.Data.SqlClient.SqlException">Исключение генерируется при выполнении команды над заблокированной строкой.Это исключение не генерируется при использовании Microsoft .NET Framework версии 1.0.</exception>
        public int ExecuteCommand(string commandText, CommandType type, List<SqlParameter> parameters)
        {
            using (SqlCommand command = CreateCommand(commandText, type, parameters))
            {
                return this.ExecuteCommand(command);
            }
        }

        /// <summary>
        /// Выполняет команду и возвращает скаляр
        /// </summary>
        /// <param name="command">SqlCommand команда</param>
        /// <returns>Возвращает значение первого столбца первой строки из результирующего набора данных</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Data.SqlClient.SqlException"></exception>
        public object ExecuteScalar(SqlCommand command)
        {
            object result = null;
            if (command == null)
                throw new ArgumentNullException("command");
            if (command.Connection == null || string.IsNullOrWhiteSpace(command.Connection.ConnectionString))
                throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(command.CommandText))
                throw new ArgumentNullException("command text");
            if (!command.Connection.ConnectionString.Equals(connection.ConnectionString)) 
                throw new ArgumentException("Illegal command connection");
            try
            {
                command.Connection.Open();
                result = command.ExecuteScalar();
            }
            finally
            {
                command.Connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Выполняет команду и возвращает скаляр
        /// </summary>
        /// <param name="commandText">Текст команды</param>
        /// <param name="type">Тип команды System.Data.CommandType</param>
        /// <param name="parameters">Набор параметров с именами и значениями</param>
        /// <returns>Возвращает значение первого столбца первой строки из результирующего набора данных</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Data.SqlClient.SqlException"></exception>
        public object ExecuteScalar(string commandText, CommandType type, Dictionary<string, object> parameters = null)
        {
            using (SqlCommand command = CreateCommand(commandText, type, parameters))
            {
                return this.ExecuteScalar(command);
            }
        }

        /// <summary>
        /// Выполняет команду и возвращает скаляр
        /// </summary>
        /// <param name="commandText">Текст команды</param>
        /// <param name="type">Тип команды System.Data.CommandType</param>
        /// <param name="parameters">Набор параметров с именами и значениями</param>
        /// <returns>Возвращает значение первого столбца первой строки из результирующего набора данных</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Data.SqlClient.SqlException"></exception>
        public object ExecuteScalar(string commandText, CommandType type, List<SqlParameter> parameters)
        {
            using (SqlCommand command = CreateCommand(commandText, type, parameters))
            {
                return this.ExecuteScalar(command);
            }
        }

        /*
        /// <summary>
        /// Execute reader in database and return result as a reader
        /// </summary>
        /// <param name="command">SqlComm</param>
        /// <returns>Returns result set>SqlDataReader</returns>
        private SqlDataReader ExecuteReader(SqlCommand command)
        {
            SqlDataReader reader;

            if (command == null || string.IsNullOrWhiteSpace(command.CommandText))
                throw new ArgumentException("InvalidCommand");
            try
            {
                command.Connection.Open();
                //_connection.Open();
                reader = command.ExecuteReader();
            }
            catch
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }

            return reader;
        }

        /// <summary>
        /// Execute reader in database and return result as a reader
        /// </summary>
        /// <param name="command">SqlCommand command</param>
        /// <param name="parameters">Set of parameters names with values</param>
        /// <returns>Returns result set>SqlDataReader</returns>
        private SqlDataReader ExecuteReader(SqlCommand command, Dictionary<string, object> parameters)
        {
            if (command == null || string.IsNullOrWhiteSpace(command.CommandText))
                throw new ArgumentException("Invalid command");
            if (parameters != null && parameters.Count != 0)
            {
                command.Parameters.Clear();
                foreach (var parameter in parameters)
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value == null ? DBNull.Value : parameter.Value);
            }
            return this.ExecuteReader(command);
        }

        /// <summary>
        /// Execute reader in database and return result as a reader
        /// </summary>
        /// <param name="commandText">Command text</param>
        /// <param name="type">Command type System.Data.CommandType</param>
        /// <returns>Returns result set>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string commandText, CommandType type = CommandType.StoredProcedure)
        {
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentException("Invalid command text");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = type;
                return this.ExecuteReader(command);
            }
        }

        /// <summary>
        /// Execute reader in database and return result as a reader
        /// </summary>
        /// <param name="commandText">Command text</param>
        /// <param name="type">Command type System.Data.CommandType</param>
        /// <param name="parameters">Set of parameters names with values</param>
        /// <returns>Returns result set>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string commandText, Dictionary<string, object> parameters, CommandType type = CommandType.StoredProcedure)
        {
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentException("Invalid command text");
            SqlDataReader result = null;

            if (parameters == null || parameters.Count == 0)
                result = this.ExecuteReader(commandText, type);
            else
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = type;
                    result = this.ExecuteReader(command, parameters);
                }
            return result;
        }
        */

        /// <summary>
        /// Выгрузка таблицы из БД
        /// </summary>
        /// <param name="command">SqlCommand команда</param>
        /// <returns>DataTable объект</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException">Невозможно открыть подключение без указания сервера или источника данных.or\r\nПодключение уже открыто.</exception>
        /// <exception cref="System.Data.SqlClient.SqlException"></exception>
        public DataTable LoadFromDatabase(SqlCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            if (command.Connection == null || string.IsNullOrWhiteSpace(command.Connection.ConnectionString))
                throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(command.CommandText))
                throw new ArgumentNullException("command text");
            if (!command.Connection.ConnectionString.Equals(connection.ConnectionString))
                throw new ArgumentException("Illegal command connection");

            DataTable table = new DataTable();
            try
            {
                command.Connection.Open();
                table.Load(command.ExecuteReader());
            }
            finally
            {
                command.Connection.Close();
            }
            return table;
        }

        /// <summary>
        /// Выгрузка таблицы из БД
        /// </summary>
        /// <param name="commandText">Текст команды</param>
        /// <param name="type">Тип команды System.Data.CommandType</param>
        /// <param name="parameters">Набор параметров с именами и значениями</param>
        /// <returns>DataTable объект</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Data.SqlClient.SqlException"></exception>
        public DataTable LoadFromDatabase(string commandText, CommandType type, Dictionary<string, object> parameters = null)
        {
            using (SqlCommand command = CreateCommand(commandText, type, parameters))
            {
                return this.LoadFromDatabase(command);
            }
        }

        /// <summary>
        /// Выгрузка таблицы из БД
        /// </summary>
        /// <param name="commandText">Текст команды</param>
        /// <param name="type">Тип команды System.Data.CommandType</param>
        /// <param name="parameters">Набор параметров с именами и значениями</param>
        /// <returns>DataTable объект</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Data.SqlClient.SqlException"></exception>
        public DataTable LoadFromDatabase(string commandText, CommandType type, List<SqlParameter> parameters)
        {
            using (SqlCommand command = CreateCommand(commandText, type, parameters))
            {
                return this.LoadFromDatabase(command);
            }
        }

        /// <summary>
        /// Выполнение набора команд в одной транзакции
        /// </summary>
        /// <param name="commands">Список команд для выполнения</param>
        /// <param name="level">Уровень изоляции транзакции</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Data.SqlClient.SqlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public void ExecuteTransaction(List<SqlCommand> commands, System.Data.IsolationLevel level = System.Data.IsolationLevel.RepeatableRead)
        {
            if (commands == null)
                throw new ArgumentNullException("commands");

            System.Data.SqlClient.SqlTransaction transaction = null;
            try
            {
                connection.Open();
                // Start a local transaction.
                transaction = connection.BeginTransaction(level);
                foreach (SqlCommand command in commands)
                {
                    //command.Transaction = transaction;
                    ExecuteTransaction(command, transaction);
                }
                // Attempt to commit the transaction.
                // connection.Close();
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                // Attempt to roll back the transaction.
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Выполнение набора команд в одной транзакции
        /// </summary>
        /// <param name="command">SqlCommand команда</param>
        /// <param name="transaction"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Data.SqlClient.SqlException"></exception>
        private int ExecuteTransaction(SqlCommand command, SqlTransaction transaction)
        {
            int rowCount;
            if (command == null)
                throw new ArgumentNullException("command");
            if (command.Connection == null || string.IsNullOrWhiteSpace(command.Connection.ConnectionString))
                throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(command.CommandText))
                throw new ArgumentNullException("command text");
            if (!command.Connection.ConnectionString.Equals(connection.ConnectionString))
                throw new ArgumentException("Illegal command connection");
                
            command.Transaction = transaction;
            rowCount = command.ExecuteNonQuery();

            return rowCount;
        }

        private SqlCommand createCommand(string commandText, CommandType commandType)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandTimeout = 60; //In seconds
            command.CommandText = commandText;
            command.CommandType = commandType;
            return command;
        }

        private List<SqlParameter> createParametersFromDictionary(Dictionary<string, object> parameters)
        {
            List<SqlParameter> outPar = new List<SqlParameter>();
            if (!parameters.IsNullOrEmpty())
            {
                foreach (var parametr in parameters)
                {
                    outPar.Add(new SqlParameter(parametr.Key, parametr.Value ?? DBNull.Value));
                }
            }
            return outPar;
        }

        private void includeParameters(SqlCommand command, Dictionary<string, object> parameters)
        {
            command.Parameters.Clear();
            if (!parameters.IsNullOrEmpty())
            {
                foreach (var parameter in parameters)
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
            }
        }

        private void includeParameters(SqlCommand command, List<SqlParameter> parameters)
        {
            command.Parameters.Clear();
            if (!parameters.IsNullOrEmpty())
            {
                command.Parameters.AddRange(parameters.ToArray());
            }
        }
    }
}
