using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class DBContextTest
    {
        internal void runTests()
        {
            CanCreateDefaultDBContext();
            CanCreateDBContext();
            CanCreateDBContextBySQLConnection();

            #region CreateCommand

            CanCreateCommand();
            CanCreateCommandWithParametrDictionary();
            CanCreateCommandWithParametrDictionaryWhenItIsNull();
            CanCreateCommandWithParametrDictionaryWhenItIsEmpty();
            CanCreateCommandWithSqlParametrList();
            CanCreateCommandWithSqlParametrListWhenItIsNull();
            CanCreateCommandWithSqlParametrListWhenItIsEmpty();

            #endregion
            
            #region ExecuteCommandTests

            ExecuteCommandWorked();
            ExecuteCommandThrowExceptionIfCommandIsNull();
            ExecuteCommandThrowExceptionIfConnectionIsNull();
            ExecuteCommandThrowExceptionIfConnectionIsEmpty();
            ExecuteCommandThrowExceptionIfCommandTextIsEmpty();
            ExecuteCommandThrowExceptionIfConnectToGarazhFromKernelInstance();
            ExecuteCommandThrowSqlExceptionIfIllegalCommandText();
            ExecuteCommandThrowSqlExceptionIfIllegalText();
            ExecuteCommandThrowSqlExceptionIfIllegalStored();

            //ExecuteCommandCommandTextWorked();
            //ExecuteCommandCommandStoredWorked();
            //ExecuteCommandCommandStoredParamsWorked();
            //ExecuteCommandCommandStoredParamsWithNullValueWorked();
            //ExecuteCommandCommandParamsWorked();
            //ExecuteCommandCommandStoredParamsThrowExceptionIfCommandIsNull();

            ExecuteCommandTextWorked();
            ExecuteCommandStoredWorked();
            ExecuteCommandTextWithDictionaryParamsWorked();
            ExecuteCommandTextWithListParamsWorked();
            ExecuteCommandStoredParamsWithNullValueWorked();
            //ExecuteCommandParamsWorked();
            #endregion

            #region ExecuteScalarTests

            ExecuteScalarCommandWorked();
            ExecuteScalarCommandThrowExceptionIfCommandIsNull();
            ExecuteScalarCommandThrowExceptionIfConnectionIsNull();
            ExecuteScalarCommandThrowExceptionIfConnectionIsEmpty();
            ExecuteScalarCommandThrowExceptionIfCommandTextIsEmpty();
            ExecuteScalarCommandThrowExceptionIfConnectToGarazhFromKernelInstance();
            ExecuteScalarThrowSqlExceptionIfIllegalCommandText();
            ExecuteScalarThrowSqlExceptionIfIllegalText();
            ExecuteScalarThrowSqlExceptionIfIllegalStored();

            //ExecuteScalarCommandTextWorked();
            //ExecuteScalarCommandStoredWorked();
            //ExecuteScalarCommandStoredParamsWorked();
            //ExecuteScalarCommandStoredParamsWithNullValueWorked();
            //ExecuteScalarCommandParamsWorked();
            //ExecuteScalarCommandStoredParamsThrowExceptionIfCommandIsNull();

            ExecuteScalarTextWorked();
            ExecuteScalarStoredWorked();
            ExecuteScalarStoredDictionaryParamsWorked();
            ExecuteScalarStoredListParamsWorked();
            ExecuteScalarStoredParamsWithNullValueWorked();
            ExecuteScalarParamsWorked();

            #endregion

            #region LoadFromDatabaseTests

            LoadFromDatabaseCommandWorked();
            LoadFromDatabaseCommandThrowExceptionIfCommandIsNull();
            LoadFromDatabaseCommandThrowExceptionIfConnectionIsNull();
            LoadFromDatabaseCommandThrowExceptionIfConnectionIsEmpty();
            LoadFromDatabaseCommandThrowExceptionIfCommandTextIsEmpty();
            LoadFromDatabaseCommandThrowExceptionIfConnectToGarazhFromKernelInstance();
            LoadFromDatabaseCommandThrowExceptionIfIllegalCommandText();

            LoadFromDatabaseTextWorked();
            LoadFromDatabaseStoredWorked();
            LoadFromDatabaseStoredDictionaryParamsWorked();
            LoadFromDatabaseStoredListParamsWorked();
            LoadFromDatabaseStoredParamsWorked();
            LoadFromDatabaseParamsWorked();

            #endregion

            #region ExecuteTransaction

            ExecuteTransactionCommitWhenCorrectCommands();
            ExecuteTransactionRollbackWhenIncorrectCommandsAndThrowException();
            ExecuteTransactionThrowArgumentNullExceptionWhenCommandsIsNull();
            ExecuteTransactionThrowArgumentNullExceptionWhenCommandIsNull();
            ExecuteTransactionThrowArgumentNullExceptionWhenConnectionIsNull();
            ExecuteTransactionThrowArgumentNullExceptionWhenConnectionIsEmpty();
            ExecuteTransactionThrowArgumentNullExceptionWhenCommandTextIsNull();
            ExecuteTransactionThrowArgumentNullExceptionWhenCommandTextIsEmpty();
            ExecuteTransactionThrowArgumentNullExceptionWhenIllegalConnection();
            ExecuteTransactionThrowExceptionWhenCanNotCommit();

            #endregion
        }

        [TestMethod]
        public void CanCreateDefaultDBContext()
        {
            DatabaseContext context = DatabaseContext.Instance;
            Assert.IsNotNull(context);
        }
        
        [TestMethod]
        public void CanCreateDBContext()
        {
            DatabaseContext context = DatabaseContext.getInstance("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''");
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void CanCreateDBContextBySQLConnection()
        {
            SqlConnection conn = new SqlConnection("Data Source=VS-NMZ-02;Initial Catalog=ceh34certificateOffice;User ID=1");
            DatabaseContext context = DatabaseContext.getInstance(conn);
            Assert.IsNotNull(context);
        }

        #region CreateCommand

        [TestMethod]
        public void CanCreateCommand()
        {
            string queryText = "SELECT * FROM Users";
            SqlCommand expComm = new SqlCommand(queryText, new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            SqlCommand comm = DatabaseContext.Instance.CreateCommand(queryText, CommandType.Text);

            Assert.IsNotNull(comm);
            Assert.ReferenceEquals(expComm, comm);
        }

        [TestMethod]
        public void CanCreateCommandWithParametrDictionary()
        {
            //Arrange
            string queryText = "SELECT * FROM Users WHERE login LIKE '@login' OR login LIKE '%part%'";
            SqlCommand expComm = new SqlCommand(queryText, new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            Dictionary<string, object> expPar = new Dictionary<string, object>();
            expPar.Add("@login", "p.v.chalenko");
            expPar.Add("@part", "ev");

            //Act
            SqlCommand comm = DatabaseContext.Instance.CreateCommand(queryText, CommandType.Text, expPar);

            //Assert
            Assert.IsNotNull(comm);
            Assert.ReferenceEquals(expComm, comm);
            Assert.AreEqual(2, comm.Parameters.Count);
            Assert.AreEqual("@login", comm.Parameters[0].ParameterName);
            Assert.AreEqual("@login", comm.Parameters["@login"].ParameterName);
            Assert.AreEqual("p.v.chalenko", comm.Parameters["@login"].Value);
            Assert.AreEqual(DbType.String, comm.Parameters["@login"].DbType);
        }

        [TestMethod]
        public void CanCreateCommandWithParametrDictionaryWhenItIsNull()
        {
            //Arrange
            string queryText = "SELECT * FROM Users WHERE login LIKE '@login' OR login LIKE '%part%'";
            SqlCommand expComm = new SqlCommand(queryText, new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            Dictionary<string, object> expPar = null;

            //Act
            SqlCommand comm = DatabaseContext.Instance.CreateCommand(queryText, CommandType.Text, expPar);

            //Assert
            Assert.IsNotNull(comm);
            Assert.ReferenceEquals(expComm, comm);
            Assert.AreEqual(0, comm.Parameters.Count);
        }

        [TestMethod]
        public void CanCreateCommandWithParametrDictionaryWhenItIsEmpty()
        {
            //Arrange
            string queryText = "SELECT * FROM Users WHERE login LIKE '@login' OR login LIKE '%part%'";
            SqlCommand expComm = new SqlCommand(queryText, new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            Dictionary<string, object> expPar = new Dictionary<string, object>();

            //Act
            SqlCommand comm = DatabaseContext.Instance.CreateCommand(queryText, CommandType.Text, expPar);

            //Assert
            Assert.IsNotNull(comm);
            Assert.ReferenceEquals(expComm, comm);
            Assert.AreEqual(0, comm.Parameters.Count);
        }

        [TestMethod]
        public void CanCreateCommandWithSqlParametrList()
        {
            //Arrange
            string queryText = "SELECT * FROM Users WHERE login LIKE '@login' OR login LIKE '%part%'";
            SqlCommand expComm = new SqlCommand(queryText, new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            List<SqlParameter> expPar = new List<SqlParameter>();
            expPar.Add(new SqlParameter("@login", "p.v.chalenko"));
            expPar.Add(new SqlParameter("@part", "ev"));

            //Act
            SqlCommand comm = DatabaseContext.Instance.CreateCommand(queryText, CommandType.Text, expPar);

            //Assert
            Assert.IsNotNull(comm);
            Assert.ReferenceEquals(expComm, comm);
            Assert.AreEqual(2, comm.Parameters.Count);
            Assert.AreEqual("@login", comm.Parameters[0].ParameterName);
            Assert.AreEqual("@login", comm.Parameters["@login"].ParameterName);
            Assert.AreEqual("p.v.chalenko", comm.Parameters["@login"].Value);
            Assert.AreEqual(DbType.String, comm.Parameters["@login"].DbType);
        }

        [TestMethod]
        public void CanCreateCommandWithSqlParametrListWhenItIsNull()
        {
            //Arrange
            string queryText = "SELECT * FROM Users WHERE login LIKE '@login' OR login LIKE '%part%'";
            SqlCommand expComm = new SqlCommand(queryText, new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            List<SqlParameter> expPar = null;

            //Act
            SqlCommand comm = DatabaseContext.Instance.CreateCommand(queryText, CommandType.Text, expPar);

            //Assert
            Assert.IsNotNull(comm);
            Assert.ReferenceEquals(expComm, comm);
            Assert.AreEqual(0, comm.Parameters.Count);
        }

        [TestMethod]
        public void CanCreateCommandWithSqlParametrListWhenItIsEmpty()
        {
            //Arrange
            string queryText = "SELECT * FROM Users WHERE login LIKE '@login' OR login LIKE '%part%'";
            SqlCommand expComm = new SqlCommand(queryText, new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            List<SqlParameter> expPar = new List<SqlParameter>();

            //Act
            SqlCommand comm = DatabaseContext.Instance.CreateCommand(queryText, CommandType.Text, expPar);

            //Assert
            Assert.IsNotNull(comm);
            Assert.ReferenceEquals(expComm, comm);
            Assert.AreEqual(0, comm.Parameters.Count);
        }

        #endregion

        #region ExecuteCommand

        [TestMethod]
        public void ExecuteCommandWorked()
        {
            string expectedDep = "ОРПОиТП";
            SqlCommand com = new SqlCommand(String.Format("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", expectedDep), new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));
            com.CommandType = CommandType.Text;
            
            int affectedRow = DatabaseContext.Instance.ExecuteCommand(com);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteCommandThrowExceptionIfCommandIsNull()
        {
            SqlCommand com = null;
            try
            {
                int affectedRow = DatabaseContext.Instance.ExecuteCommand(com);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command", ex.Message);
                return;
            }

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\p.v.chalenko\\Desktop\\ProjectKernel\\ProjectKernel\\dbTest.mdf;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteCommandThrowExceptionIfConnectionIsNull()
        {
            SqlCommand com = new SqlCommand("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", null);
            try
            {
                int affectedRow = DatabaseContext.Instance.ExecuteCommand(com);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: connection", ex.Message);
                return;
            }

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\p.v.chalenko\\Desktop\\ProjectKernel\\ProjectKernel\\dbTest.mdf;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteCommandThrowExceptionIfConnectionIsEmpty()
        {
            SqlCommand com = new SqlCommand("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", new SqlConnection(""));
            try
            {
                int affectedRow = DatabaseContext.Instance.ExecuteCommand(com);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: connection", ex.Message);
                return;
            }

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\p.v.chalenko\\Desktop\\ProjectKernel\\ProjectKernel\\dbTest.mdf;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteCommandThrowExceptionIfCommandTextIsEmpty()
        {
            SqlCommand com = new SqlCommand("", new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\p.v.chalenko\\Desktop\\ProjectKernel\\ProjectKernel\\dbTest.mdf;Integrated Security=True"));
            try
            {
                int affectedRow = DatabaseContext.Instance.ExecuteCommand(com);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command text", ex.Message);
                return;
            }

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\p.v.chalenko\\Desktop\\ProjectKernel\\ProjectKernel\\dbTest.mdf;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteCommandThrowExceptionIfConnectToGarazhFromKernelInstance()
        {
            SqlCommand com = new SqlCommand("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            try
            {
                int affectedRow = DatabaseContext.Instance.ExecuteCommand(com);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Illegal command connection", ex.Message);
                return;
            }

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\p.v.chalenko\\Desktop\\ProjectKernel\\ProjectKernel\\dbTest.mdf;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void ExecuteCommandThrowSqlExceptionIfIllegalCommandText()
        {
            SqlCommand com = new SqlCommand("Some illegal query", new SqlConnection(ProjectKernel.Properties.Settings.Default.dbConnection));
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteCommand(com).ToString();
            }
            catch (SqlException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SqlException));
                Assert.AreEqual(156, ex.Number);
                Assert.AreEqual("Неправильный синтаксис около ключевого слова \"Some\".", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteCommandThrowSqlExceptionIfIllegalText()
        {
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteCommand("Some illegal query", CommandType.Text).ToString();
            }
            catch (SqlException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SqlException));
                Assert.AreEqual(156, ex.Number);
                Assert.AreEqual("Неправильный синтаксис около ключевого слова \"Some\".", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteCommandThrowSqlExceptionIfIllegalStored()
        {
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteCommand("Some illegal procedure", CommandType.StoredProcedure).ToString();
            }
            catch (SqlException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SqlException));
                Assert.AreEqual(2812, ex.Number);
                Assert.AreEqual("Не удалось найти хранимую процедуру \"Some illegal procedure\".", ex.Message);
                return;
            }
            Assert.Fail();
        }

        /*
        [TestMethod]
        public void ExecuteCommandCommandTextWorked()
        {
            string expectedDep = "ОРПОиТП";
            SqlCommand com = new SqlCommand(String.Format("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", expectedDep), new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));
            
            int affectedRow = DatabaseContext.Instance.ExecuteCommand(com, CommandType.Text); DatabaseContext.Instance.ExecuteScalar(com, CommandType.Text);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }

        [TestMethod]
        public void ExecuteCommandCommandStoredWorked()
        {
            string expectedDep = "NNSU";
            SqlCommand com = new SqlCommand("set_Azar_department", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));

            int affectedRow = DatabaseContext.Instance.ExecuteCommand(com, CommandType.StoredProcedure);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Азар'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = N'ННГУ' WHERE surname LIKE N'Азар'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }

        [TestMethod]
        public void ExecuteCommandCommandStoredParamsWorked()
        {
            string expectedDep = "ORPOiTP";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@id", 2);
            par.Add("@value", expectedDep);
            SqlCommand com = new SqlCommand("set_department", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));

            int affectedRow = DatabaseContext.Instance.ExecuteCommand(com, CommandType.StoredProcedure, par);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = N'ОРПОиТП' WHERE surname LIKE N'Ильичев'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }

        [TestMethod]
        public void ExecuteCommandCommandStoredParamsWithNullValueWorked()
        {
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@patronymic", null);
            par.Add("@department", "ННГУ");
            SqlCommand com = new SqlCommand("set_patronymic_by_department", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));

            int affectedRow = DatabaseContext.Instance.ExecuteCommand(com, CommandType.StoredProcedure, par);

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET patronymic_name = N'Львовна' WHERE surname LIKE N'Крылова' UPDATE Users SET patronymic_name = N'Александровна' WHERE surname LIKE N'Леванова'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(3, affectedRow);
        }

        [TestMethod]
        public void ExecuteCommandCommandParamsWorked()
        {
            string expectedDep = "ORPOiTP";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@id", 2);
            par.Add("@value", expectedDep);
            SqlCommand com = new SqlCommand("set_department", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));

            int affectedRow = DatabaseContext.Instance.ExecuteCommand(com, CommandType.StoredProcedure, par);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = N'ОРПОиТП' WHERE surname LIKE N'Ильичев'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteCommandCommandStoredParamsThrowExceptionIfCommandIsNull()
        {
            SqlCommand com = null;
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@id", 2);

            try
            {
                int affectedRow = DatabaseContext.Instance.ExecuteCommand(com, CommandType.StoredProcedure, par);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command", ex.Message);
                return;
            }
            Assert.Fail();
        }
        */

        [TestMethod]
        public void ExecuteCommandTextWorked()
        {
            string expectedDep = "ОРПОиТП";
            
            int affectedRow = DatabaseContext.Instance.ExecuteCommand(String.Format("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", expectedDep), CommandType.Text);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }

        [TestMethod]
        public void ExecuteCommandStoredWorked()
        {
            string expectedDep = "NNSU";
            
            int affectedRow = DatabaseContext.Instance.ExecuteCommand("set_Azar_department", CommandType.StoredProcedure);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Азар'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = N'ННГУ' WHERE surname LIKE N'Азар'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }

        [TestMethod]
        public void ExecuteCommandTextWithDictionaryParamsWorked()
        {
            string expectedDep = "ORPOiTP";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@login", "a.s.ilichev");
            par.Add("@value", expectedDep);

            int affectedRow = DatabaseContext.Instance.ExecuteCommand("UPDATE [Users] SET [department] = @value WHERE login LIKE @login", CommandType.Text, par);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = N'ОРПОиТП' WHERE surname LIKE N'Ильичев'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }

        [TestMethod]
        public void ExecuteCommandTextWithListParamsWorked()
        {
            string expectedDep = "ORPOiTP";
            List<SqlParameter> par = new List<SqlParameter>();
            par.Add(new SqlParameter("@login", "a.s.ilichev"));
            par.Add(new SqlParameter("@value", expectedDep));

            int affectedRow = DatabaseContext.Instance.ExecuteCommand("UPDATE [Users] SET [department] = @value WHERE login LIKE @login", CommandType.Text, par);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = N'ОРПОиТП' WHERE surname LIKE N'Ильичев'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }

        [TestMethod]
        public void ExecuteCommandStoredParamsWithNullValueWorked()
        {
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@patronymic", null);
            par.Add("@department", "ННГУ");
            
            int affectedRow = DatabaseContext.Instance.ExecuteCommand("set_patronymic_by_department", CommandType.StoredProcedure, par);

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET patronymic_name = N'Львовна' WHERE surname LIKE N'Крылова' UPDATE Users SET patronymic_name = N'Александровна' WHERE surname LIKE N'Леванова'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(3, affectedRow);
        }

        /*
        [TestMethod]
        public void ExecuteCommandParamsWorked()
        {
            string expectedDep = "ORPOiTP";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@id", 2);
            par.Add("@value", expectedDep);

            int affectedRow = DatabaseContext.Instance.ExecuteCommand("set_department", CommandType.StoredProcedure, par);
            string actualDep = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = N'ОРПОиТП' WHERE surname LIKE N'Ильичев'";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.AreEqual(expectedDep, actualDep);
            Assert.AreEqual(1, affectedRow);
        }
        */

        #endregion
        
        #region ExecuteScalar

        [TestMethod]
        public void ExecuteScalarCommandWorked()
        {
            string expectedName = "Pavel";

            SqlCommand com = new SqlCommand("SELECT first_name, department FROM Users WHERE surname LIKE 'Chalenko'", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));
            com.CommandType = CommandType.Text;
            string actualName = DatabaseContext.Instance.ExecuteScalar(com).ToString();

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteScalarCommandThrowExceptionIfCommandIsNull()
        {
            SqlCommand com = null;
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteScalar(com).ToString();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteScalarCommandThrowExceptionIfConnectionIsNull()
        {
            SqlCommand com = new SqlCommand("SELECT first_name, department FROM Users WHERE surname LIKE 'Chalenko'", null);
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteScalar(com).ToString();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: connection", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteScalarCommandThrowExceptionIfConnectionIsEmpty()
        {
            SqlCommand com = new SqlCommand("SELECT first_name, department FROM Users WHERE surname LIKE 'Chalenko'", new SqlConnection(""));
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteScalar(com).ToString();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: connection", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteScalarCommandThrowExceptionIfCommandTextIsEmpty()
        {
            SqlCommand com = new SqlCommand("", new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\p.v.chalenko\\Desktop\\ProjectKernel\\ProjectKernel\\dbTest.mdf;Integrated Security=True"));
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteScalar(com).ToString();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command text", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteScalarCommandThrowExceptionIfConnectToGarazhFromKernelInstance()
        {
            SqlCommand com = new SqlCommand("SELECT first_name, department FROM Users WHERE surname LIKE 'Chalenko'", new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteScalar(com).ToString();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Illegal command connection", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteScalarThrowSqlExceptionIfIllegalCommandText()
        {
            SqlCommand com = new SqlCommand("Some illegal query", new SqlConnection(ProjectKernel.Properties.Settings.Default.dbConnection));
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteScalar(com).ToString();
            }
            catch (SqlException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SqlException));
                Assert.AreEqual(156, ex.Number);
                Assert.AreEqual("Неправильный синтаксис около ключевого слова \"Some\".", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteScalarThrowSqlExceptionIfIllegalText()
        {
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteScalar("Some illegal query", CommandType.Text).ToString();
            }
            catch (SqlException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SqlException));
                Assert.AreEqual(156, ex.Number);
                Assert.AreEqual("Неправильный синтаксис около ключевого слова \"Some\".", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteScalarThrowSqlExceptionIfIllegalStored()
        {
            try
            {
                string actualName = DatabaseContext.Instance.ExecuteScalar("Some illegal procedure", CommandType.StoredProcedure).ToString();
            }
            catch (SqlException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SqlException));
                Assert.AreEqual(2812, ex.Number);
                Assert.AreEqual("Не удалось найти хранимую процедуру \"Some illegal procedure\".", ex.Message);
                return;
            }
            Assert.Fail();
        }

        /*
        [TestMethod]
        public void ExecuteScalarCommandTextWorked()
        {
            string expectedName = "Pavel";

            SqlCommand com = new SqlCommand("SELECT first_name, department FROM Users WHERE surname LIKE 'Chalenko'", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));
            string actualName = DatabaseContext.Instance.ExecuteScalar(com, CommandType.Text).ToString();

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void ExecuteScalarCommandStoredWorked()
        {
            string expectedName = "Джон";
            SqlCommand com = new SqlCommand("get_Azar_name", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));
            
            string actualName = DatabaseContext.Instance.ExecuteScalar(com, CommandType.StoredProcedure).ToString();
            
            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void ExecuteScalarCommandStoredParamsWorked()
        {
            string expectedName = "Андрей";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@surname", "Ильичев");
            SqlCommand com = new SqlCommand("get_name", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));
            
            string actualName = DatabaseContext.Instance.ExecuteScalar(com, CommandType.StoredProcedure, par).ToString();
            
            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void ExecuteScalarCommandStoredParamsWithNullValueWorked()
        {
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@patronymic", null);
            SqlCommand com = new SqlCommand("get_name_by_patronymic", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));
            
            object actualValue = DatabaseContext.Instance.ExecuteScalar(com, CommandType.StoredProcedure, par);

            Assert.IsNull(actualValue);
        }

        [TestMethod]
        public void ExecuteScalarCommandParamsWorked()
        {
            string expectedName = "Андрей";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@surname", "Ильичев");
            SqlCommand com = new SqlCommand("get_name", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));
            
            string actualName = DatabaseContext.Instance.ExecuteScalar(com, CommandType.StoredProcedure, par).ToString();

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void ExecuteScalarCommandStoredParamsThrowExceptionIfCommandIsNull()
        {
            SqlCommand com = null;
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@id", 2);

            try
            {
                string actualName = DatabaseContext.Instance.ExecuteScalar(com, CommandType.StoredProcedure, par).ToString();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command", ex.Message);
                return;
            }
            Assert.Fail();
        }
        */

        [TestMethod]
        public void ExecuteScalarTextWorked()
        {
            string expectedName = "Pavel";

            string actualName = DatabaseContext.Instance.ExecuteScalar("SELECT first_name, department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void ExecuteScalarStoredWorked()
        {
            string expectedName = "Джон";
            
            string actualName = DatabaseContext.Instance.ExecuteScalar("get_Azar_name", CommandType.StoredProcedure).ToString();

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void ExecuteScalarStoredDictionaryParamsWorked()
        {
            string expectedName = "Андрей";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@surname", "Ильичев");
            
            string actualName = DatabaseContext.Instance.ExecuteScalar("get_name", CommandType.StoredProcedure, par).ToString();

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void ExecuteScalarStoredListParamsWorked()
        {
            string expectedName = "Андрей";
            List<SqlParameter> par = new List<SqlParameter>();
            par.Add(new SqlParameter("@surname", "Ильичев"));

            string actualName = DatabaseContext.Instance.ExecuteScalar("get_name", CommandType.StoredProcedure, par).ToString();

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void ExecuteScalarStoredParamsWithNullValueWorked()
        {
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@patronymic", null);
            
            object actualValue = DatabaseContext.Instance.ExecuteScalar("get_name_by_patronymic", CommandType.StoredProcedure, par);

            Assert.IsNull(actualValue);
        }

        [TestMethod]
        public void ExecuteScalarParamsWorked()
        {
            string expectedName = "Андрей";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@surname", "Ильичев");

            string actualName = DatabaseContext.Instance.ExecuteScalar("get_name", CommandType.StoredProcedure, par).ToString();

            Assert.AreEqual(expectedName, actualName);
        }

        #endregion

        #region LoadFromDatebase

        [TestMethod]
        public void LoadFromDatabaseCommandWorked()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Users WHERE department LIKE N'ННГУ' ORDER BY login", new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True"));
            com.CommandType = CommandType.Text;

            DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase(com);
            
            Assert.AreEqual("ННГУ", actualTable.Rows[0]["department"]);
            Assert.AreEqual("Азар", actualTable.Rows[0]["surname"]);
            Assert.AreEqual("Крылова", actualTable.Rows[1]["surname"]);
            Assert.AreEqual("Татьяна", actualTable.Rows[2]["first_name"]);
            Assert.AreEqual(3, actualTable.Rows.Count);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void LoadFromDatabaseCommandThrowExceptionIfCommandIsNull()
        {
            SqlCommand com = null;
            try
            {
                DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase(com);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void LoadFromDatabaseCommandThrowExceptionIfConnectionIsNull()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Users WHERE department LIKE N'ННГУ'", null);

            try
            {
                DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase(com);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: connection", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void LoadFromDatabaseCommandThrowExceptionIfConnectionIsEmpty()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Users WHERE department LIKE N'ННГУ'", new SqlConnection(""));
            try
            {
                DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase(com);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: connection", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void LoadFromDatabaseCommandThrowExceptionIfCommandTextIsEmpty()
        {
            SqlCommand com = new SqlCommand("", new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\p.v.chalenko\\Desktop\\ProjectKernel\\ProjectKernel\\dbTest.mdf;Integrated Security=True"));
            try
            {
                DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase(com);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command text", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void LoadFromDatabaseCommandThrowExceptionIfConnectToGarazhFromKernelInstance()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Users WHERE department LIKE N'ННГУ'", new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''"));
            try
            {
                DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase(com);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Illegal command connection", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void LoadFromDatabaseCommandThrowExceptionIfIllegalCommandText()
        {
            SqlCommand com = new SqlCommand("Some illegal query", new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\p.v.chalenko\\Desktop\\ProjectKernel\\ProjectKernel\\dbTest.mdf;Integrated Security=True"));
            try
            {
                DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase(com);
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void LoadFromDatabaseTextWorked()
        {
            DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM Users WHERE department LIKE N'ННГУ' ORDER BY login", CommandType.Text);
            
            Assert.AreEqual("ННГУ", actualTable.Rows[0]["department"]);
            Assert.AreEqual("Азар", actualTable.Rows[0]["surname"]);
            Assert.AreEqual("Крылова", actualTable.Rows[1]["surname"]);
            Assert.AreEqual("Татьяна", actualTable.Rows[2]["first_name"]);
            Assert.AreEqual(3, actualTable.Rows.Count);
        }

        [TestMethod]
        public void LoadFromDatabaseStoredWorked()
        {
            string expectedName = "Джон";
            
            DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase("get_Azar_name", CommandType.StoredProcedure);

            Assert.AreEqual(expectedName, actualTable.Rows[0]["first_name"]);
            Assert.AreEqual(1, actualTable.Rows.Count);
        }

        [TestMethod]
        public void LoadFromDatabaseStoredDictionaryParamsWorked()
        {
            string expectedName = "Андрей";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@surname", "Ильичев");
            
            DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase("get_name", CommandType.StoredProcedure, par);

            Assert.AreEqual(expectedName, actualTable.Rows[0]["first_name"]);
            Assert.AreEqual(1, actualTable.Rows.Count);
        }

        [TestMethod]
        public void LoadFromDatabaseStoredListParamsWorked()
        {
            string expectedName = "Андрей";
            List<SqlParameter> par = new List<SqlParameter>();
            par.Add(new SqlParameter("@surname", "Ильичев"));

            DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase("get_name", CommandType.StoredProcedure, par);

            Assert.AreEqual(expectedName, actualTable.Rows[0]["first_name"]);
            Assert.AreEqual(1, actualTable.Rows.Count);
        }

        [TestMethod]
        public void LoadFromDatabaseStoredParamsWorked()
        {
            string expectedName = "Андрей";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@surname", "Ильичев");
            
            DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase("get_name", CommandType.StoredProcedure, par);

            Assert.AreEqual(expectedName, actualTable.Rows[0]["first_name"]);
            Assert.AreEqual(1, actualTable.Rows.Count);
        }

        [TestMethod]
        public void LoadFromDatabaseParamsWorked()
        {
            string expectedName = "Андрей";
            System.Collections.Generic.Dictionary<string, object> par = new System.Collections.Generic.Dictionary<string, object>();
            par.Add("@surname", "Ильичев");

            DataTable actualTable = DatabaseContext.Instance.LoadFromDatabase("get_name", CommandType.StoredProcedure, par);

            Assert.AreEqual(expectedName, actualTable.Rows[0]["first_name"]);
            Assert.AreEqual(1, actualTable.Rows.Count);
        }

        #endregion

        #region ExecuteTransaction

        [TestMethod]
        public void ExecuteTransactionCommitWhenCorrectCommands()
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            string expectedDepForChalenko = "ОРПОиТП";
            commands.Add(DatabaseContext.Instance.CreateCommand(String.Format("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", expectedDepForChalenko), CommandType.Text));
            string expectedDepForIlichev = "ORPOiTP";
            System.Collections.Generic.Dictionary<string, object> parForIlichev = new System.Collections.Generic.Dictionary<string, object>();
            parForIlichev.Add("@login", "a.s.ilichev");
            parForIlichev.Add("@value", expectedDepForIlichev);
            commands.Add(DatabaseContext.Instance.CreateCommand(String.Format("set_department", expectedDepForIlichev), CommandType.StoredProcedure, parForIlichev));

            DatabaseContext.Instance.ExecuteTransaction(commands);

            string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
            string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();
            
            #region undo

            using (SqlCommand command = DatabaseContext.Instance.CreateCommand("", CommandType.Text))
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko' ";
                command.CommandText += "UPDATE Users SET department = N'ОРПОиТП' WHERE surname LIKE N'Ильичев' ";
                DatabaseContext.Instance.ExecuteCommand(command);
            }

            #endregion

            Assert.AreEqual(expectedDepForChalenko, actualDepForChalenko);
            Assert.AreEqual(expectedDepForIlichev, actualDepForIlichev);
        }

        [TestMethod]
        public void ExecuteTransactionRollbackWhenIncorrectCommandsAndThrowException()
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            string expectedDepForChalenko = "ОРПОиТП";
            commands.Add(DatabaseContext.Instance.CreateCommand(String.Format("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", expectedDepForChalenko), CommandType.Text));
            string expectedDepForIlichev = "ORPOiTP";
            System.Collections.Generic.Dictionary<string, object> parForIlichev = new System.Collections.Generic.Dictionary<string, object>();
            parForIlichev.Add("@id", 2);
            parForIlichev.Add("@value", expectedDepForIlichev);
            commands.Add(DatabaseContext.Instance.CreateCommand(String.Format("set_department", expectedDepForIlichev), CommandType.StoredProcedure, parForIlichev));
            commands.Add(DatabaseContext.Instance.CreateCommand("Some iilegal command", CommandType.Text));

            try
            {
                DatabaseContext.Instance.ExecuteTransaction(commands);
            }
            catch 
            {
                string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
                string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

                //Assert.IsInstanceOfType(ex, typeof(SqlException));
                //Assert.AreEqual("Incorrect syntax near the keyword 'Some'.", ex.Message);
                Assert.AreEqual("ORPOiTP", actualDepForChalenko);
                Assert.AreEqual("ОРПОиТП", actualDepForIlichev);
                return;
            }
            
            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko' ";
                command.CommandText += "UPDATE Users SET department = N'ОРПОиТП' WHERE surname LIKE N'Ильичев' ";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.Fail();    
        }

        [TestMethod]
        public void ExecuteTransactionThrowArgumentNullExceptionWhenCommandsIsNull()
        {
            try
            {
                DatabaseContext.Instance.ExecuteTransaction(null);
            }
            catch (Exception ex)
            {
                string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
                string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: commands", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteTransactionThrowArgumentNullExceptionWhenCommandIsNull()
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            commands.Add(null);

            try
            {
                DatabaseContext.Instance.ExecuteTransaction(commands);
            }
            catch (Exception ex)
            {
                string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
                string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteTransactionThrowArgumentNullExceptionWhenConnectionIsNull()
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            string expectedDepForChalenko = "ОРПОиТП";
            commands.Add(new SqlCommand(String.Format("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", expectedDepForChalenko), null));

            try
            {
                DatabaseContext.Instance.ExecuteTransaction(commands);
            }
            catch (Exception ex)
            {
                string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
                string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: connection", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteTransactionThrowArgumentNullExceptionWhenConnectionIsEmpty()
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            string expectedDepForChalenko = "ОРПОиТП";
            commands.Add(new SqlCommand(String.Format("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", expectedDepForChalenko), new SqlConnection("")));

            try
            {
                DatabaseContext.Instance.ExecuteTransaction(commands);
            }
            catch (Exception ex)
            {
                string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
                string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: connection", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteTransactionThrowArgumentNullExceptionWhenCommandTextIsNull()
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            commands.Add(new SqlCommand(null, new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''")));

            try
            {
                DatabaseContext.Instance.ExecuteTransaction(commands);
            }
            catch (Exception ex)
            {
                string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
                string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command text", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteTransactionThrowArgumentNullExceptionWhenCommandTextIsEmpty()
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            commands.Add(DatabaseContext.Instance.CreateCommand("", CommandType.Text));

            try
            {
                DatabaseContext.Instance.ExecuteTransaction(commands);
            }
            catch (Exception ex)
            {
                string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
                string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: command text", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteTransactionThrowArgumentNullExceptionWhenIllegalConnection()
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            string expectedDepForChalenko = "ОРПОиТП";
            commands.Add(new SqlCommand(String.Format("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", expectedDepForChalenko), new SqlConnection("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''")));

            try
            {
                DatabaseContext.Instance.ExecuteTransaction(commands);
            }
            catch (Exception ex)
            {
                string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
                string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
                Assert.AreEqual("Illegal command connection", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteTransactionThrowExceptionWhenCanNotCommit()
        {
            List<SqlCommand> commands = new List<SqlCommand>();
            string expectedDepForChalenko = "ОРПОиТП";
            commands.Add(DatabaseContext.Instance.CreateCommand(String.Format("UPDATE Users SET department = N'{0}' WHERE surname LIKE 'Chalenko'", expectedDepForChalenko), CommandType.Text));
            string expectedDepForIlichev = "ORPOiTP";
            System.Collections.Generic.Dictionary<string, object> parForIlichev = new System.Collections.Generic.Dictionary<string, object>();
            parForIlichev.Add("@login", "a.s.ilichev");
            parForIlichev.Add("@value", expectedDepForIlichev);
            commands.Add(DatabaseContext.Instance.CreateCommand(String.Format("set_department", expectedDepForIlichev), CommandType.StoredProcedure, parForIlichev));
            commands.Add(DatabaseContext.Instance.CreateCommand("ROLLBACK TRANSACTION", CommandType.Text));
            
            try
            {
                DatabaseContext.Instance.ExecuteTransaction(commands);
            }
            catch (Exception ex)
            {
                string actualDepForChalenko = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE 'Chalenko'", CommandType.Text).ToString();
                string actualDepForIlichev = DatabaseContext.Instance.ExecuteScalar("SELECT department FROM Users WHERE surname LIKE N'Ильичев'", CommandType.Text).ToString();

                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
                Assert.AreEqual("Данный SqlTransaction завершен; его повторное использование невозможно.", ex.Message);
                Assert.AreEqual("ORPOiTP", actualDepForChalenko);
                Assert.AreEqual("ОРПОиТП", actualDepForIlichev);
                return;
            }

            #region undo

            SqlConnection _connection = new SqlConnection("Data Source=PLAST-7;Initial Catalog=DBTEST;Integrated Security=True");
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET department = 'ORPOiTP' WHERE surname LIKE 'Chalenko' ";
                command.CommandText += "UPDATE Users SET department = N'ОРПОиТП' WHERE surname LIKE N'Ильичев' ";
                command.CommandType = CommandType.Text;
                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }

            #endregion

            Assert.Fail();
        }

        [TestMethod]
        public void ExecuteTransactionWorkOnVS()
        {
            //Arrange
            DatabaseContext context = DatabaseContext.getInstance("Persist Security Info=False; Data Source=vs-nmz-02; Initial Catalog=garazh; User ID=1; Password=''");
            List<SqlCommand> list = new List<SqlCommand>();
            list.Add(context.CreateCommand("select 4", CommandType.Text));
            list.Add(context.CreateCommand("select 5", CommandType.Text));

            //Act
            context.ExecuteTransaction(list);

            //Assert

        }


        #endregion
    }
}
