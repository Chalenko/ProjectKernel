using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes;
using ProjectKernel.Classes.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelTest.ClassesTest
{
    [TestClass]
    public class DBUserTest
    {
        DBUser user;

        public DBUserTest()
        {
            //UserCreator.create(UserType.DataBase, "l.l.krylova");
            //user = CurrentUser.Instance;
        }

        internal void runTests()
        {
            UserByConstructorWithParametrsIsNotNull();
            UserByConstructorWithParametrsIsNotEmpty();
            userConstructorThrowArgumentNullExceptionWhenSurnameIsNull();
            userConstructorThrowArgumentNullExceptionWhenNameIsWhitespace();
            userConstructorDoesNotThrowArgumentNullExceptionWhenPatronymicIsWhitespace();
            userConstructorThrowArgumentNullExceptionWhenLoginIsNull();
            userConstructorThrowArgumentNullExceptionWhenDepartmentIsWhitespace();
            setSurnameThrowArgumentNullExceptionWhenValueIsNull();
            setNameThrowArgumentNullExceptionWhenValueIsWhitespace();
            setPatronymicDoesNotThrowExceptionWhenValueIsNull();
            setLoginThrowArgumentNullExceptionWhenValueIsWhitespace();
            setDepartmentThrowArgumentNullExceptionWhenValueIsNull();
            addRoleToUserIsWorked();
            addRolesToUserIsWorked();
            addRolesToNullUserThrowArgumentNullException();
            addNullRolesToUserThrowArgumentNullException();
            addNullRoleToUserThrowArgumentNullException();
            addExistingRoleToUserDoesNotThrowException();
            removeRoleFromUserIsWorked();
            removeRolesFromUserIsWorked();
            removeRoleFromNullUserThrowArgumentNullException();
            removeNullRoleFromUserDoesNotThrowException();
            removeNotExistingRoleFromUserDoesNotThrowException();
        }

        [TestMethod]
        public void UserByConstructorWithParametrsIsNotNull()
        {
            user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void UserByConstructorWithParametrsIsNotEmpty()
        {
            user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            Assert.AreEqual("Bolotov", user.Surname);
            Assert.AreEqual("Max", user.Name);
            Assert.AreEqual("Il'ich", user.Patronymic);
            Assert.AreEqual("Bolotov Max", user.FullName);
            Assert.AreEqual("m.i.bolotov", user.Login);
            Assert.AreEqual("NNSU", user.Department);
            Assert.IsFalse(user.IsAdmin);
            Assert.AreEqual(0, user.Groups.Count);
            Assert.AreEqual(UserType.Database, user.Type);
        }

        [TestMethod]
        public void userConstructorThrowArgumentNullExceptionWhenSurnameIsNull()
        {
            try
            {
                user = new DBUser(null, "Max", "Il'ich", "m.i.bolotov", "NNSU");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: surname", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void userConstructorThrowArgumentNullExceptionWhenNameIsWhitespace()
        {
            try
            {
                user = new DBUser("Bolotov", "", "Il'ich", "m.i.bolotov", "NNSU");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: name", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void userConstructorDoesNotThrowArgumentNullExceptionWhenPatronymicIsWhitespace()
        {
            try
            {
                user = new DBUser("Bolotov", "Max", "", "m.i.bolotov", "NNSU");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void userConstructorThrowArgumentNullExceptionWhenLoginIsNull()
        {
            try
            {
                user = new DBUser("Bolotov", "Max", "Il'ich", null, "NNSU");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: login", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void userConstructorThrowArgumentNullExceptionWhenDepartmentIsWhitespace()
        {
            try
            {
                user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: department", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void setSurnameThrowArgumentNullExceptionWhenValueIsNull()
        {
            user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            try
            {
                user.Surname = null;
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: surname", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void setNameThrowArgumentNullExceptionWhenValueIsWhitespace()
        {
            user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            try
            {
                user.Name = "";
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: name", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void setPatronymicDoesNotThrowExceptionWhenValueIsNull()
        {
            user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            try
            {
                user.Patronymic = null;
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void setLoginThrowArgumentNullExceptionWhenValueIsWhitespace()
        {
            user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            try
            {
                user.Login = "";
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: login", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void setDepartmentThrowArgumentNullExceptionWhenValueIsNull()
        {
            user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            try
            {
                user.Department = null;
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: department", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addRoleToUserIsWorked()
        {
            user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            user.addGroup("Friend");
            user.addGroup("NNSU");
            user.addGroup("IAP");

            Assert.AreEqual("Khilov", user.Surname);
            Assert.AreEqual(3, user.Groups.Count);
            Assert.AreEqual("Friend", user.Groups[0]);
            Assert.AreEqual("NNSU", user.Groups[1]);
            Assert.AreEqual("IAP", user.Groups[2]);
        }

        [TestMethod]
        public void addRolesToUserIsWorked()
        {
            user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            List<string> roles = new List<string>();
            roles.Add("Friends");
            roles.Add("IAP RAS Employees");
            user.addGroups(roles);

            Assert.AreEqual("Khilov", user.Surname);
            Assert.AreEqual(2, user.Groups.Count);
            Assert.AreEqual("Friends", user.Groups[0]);
            Assert.AreEqual("IAP RAS Employees", user.Groups[1]);
        }

        [TestMethod]
        public void addRolesToNullUserThrowArgumentNullException()
        {
            user = null;
            try
            {
                List<string> roles = new List<string>();
                roles.Add("Friends");
                user.addGroups(roles);
            }
            catch (NullReferenceException ex)
            {
                Assert.AreEqual("Ссылка на объект не указывает на экземпляр объекта.", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addNullRolesToUserThrowArgumentNullException()
        {
            user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            try
            {
                user.addGroups(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: groups", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addNullRoleToUserThrowArgumentNullException()
        {
            user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            try
            {
                List<string> roles = new List<string>();
                roles.Add("");
                user.addGroups(roles);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: group name", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addExistingRoleToUserDoesNotThrowException()
        {
            user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            List<string> roles = new List<string>();
            user.addGroup("Friend");
            user.addGroup("NNSU");
            user.addGroup("IAP");
            roles.Add("IAP");
            try
            {
                user.addGroups(roles);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.AreEqual(3, user.Groups.Count);
            Assert.AreEqual("Friend", user.Groups[0]);
            Assert.AreEqual("NNSU", user.Groups[1]);
            Assert.AreEqual("IAP", user.Groups[2]);
        }

        [TestMethod]
        public void removeRoleFromUserIsWorked()
        {
            user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            user.addGroup("Friend");
            user.addGroup("NNSU");
            user.addGroup("IAP");
            try
            {
                user.removeGroup("NNSU");
            }
            catch
            {
                Assert.Fail();
            }
            Assert.AreEqual(2, user.Groups.Count);
            Assert.AreEqual("Friend", user.Groups[0]);
            Assert.AreEqual("IAP", user.Groups[1]);
        }

        [TestMethod]
        public void removeRolesFromUserIsWorked()
        {
            user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            user.addGroup("Friend");
            user.addGroup("NNSU");
            user.addGroup("IAP");
            try
            {
                user.removeGroups();
            }
            catch
            {
                Assert.Fail();
            }
            Assert.AreEqual(0, user.Groups.Count);
            Assert.IsNotNull(user.Groups);
        }

        [TestMethod]
        public void removeRoleFromNullUserThrowArgumentNullException()
        {
            user = null;
            try
            {
                user.removeGroup("NNSU");
            }
            catch (NullReferenceException ex)
            {
                Assert.AreEqual("Ссылка на объект не указывает на экземпляр объекта.", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void removeNullRoleFromUserDoesNotThrowException()
        {
            user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            user.addGroup("Friend");
            user.addGroup("NNSU");
            user.addGroup("IAP");
            try
            {
                user.removeGroup(null);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.AreEqual(3, user.Groups.Count);
            Assert.AreEqual("Friend", user.Groups[0]);
            Assert.AreEqual("NNSU", user.Groups[1]);
            Assert.AreEqual("IAP", user.Groups[2]);
        }

        [TestMethod]
        public void removeNotExistingRoleFromUserDoesNotThrowException()
        {
            user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            user.addGroup("Friend");
            user.addGroup("NNSU");
            user.addGroup("IAP");
            try
            {
                user.removeGroup("IAP RAS");
            }
            catch
            {
                Assert.Fail();
            }
            Assert.AreEqual(3, user.Groups.Count);
            Assert.AreEqual("Friend", user.Groups[0]);
            Assert.AreEqual("NNSU", user.Groups[1]);
            Assert.AreEqual("IAP", user.Groups[2]);
        }

        [TestMethod]
        public void parseUserThrowArgumentExceptionIfIdInQueryResultIsNull()
        {
            //arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(object));
            DataRow dr = dt.NewRow();
            dr["id"] = null;

            try
            {
                //act
                user = DBUser.ParseUser(dr);
            }
            catch (ArgumentException ae)
            {
                //assert
                Assert.AreEqual("Can't parse BDUser. Invalid id parameter", ae.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void parseUserThrowArgumentExceptionIfCantParseIdAsGuid()
        {
            //arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(object));
            DataRow dr = dt.NewRow();
            dr["id"] = "it's not guid";

            try
            {
                //act
                user = DBUser.ParseUser(dr);
            }
            catch (ArgumentException ae)
            {
                //assert
                Assert.AreEqual("Can't parse BDUser. Invalid id parameter", ae.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void newUserByDefaultIsActive()
        {
            //act
            user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            //assert
            Assert.IsTrue(user.IsActive);
        }

        [TestMethod]
        public void canChangeIsActiveProperty()
        {
            //arrange
            user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            //act
            user.IsActive = false;
            //assert
            Assert.IsFalse(user.IsActive);
        }
    }

    [TestClass]
    public class DBUserStorageTest
    {
        public DBUserStorageTest()
        {
            //UserCreator.create(UserType.DataBase, "a.s.ilichev");
            //DBUserStorage.logger = ProjectKernel.Classes.Logger.DBLogger.Instance;
        }

        internal void runTests()
        {
            DBUserViewIsNotNull();
            DBUserViewIsNotEmpty();
            canViewDBUserList();

            getUserResultByLoginIsNotNullForSESergienko();
            getUserResultByLoginIsCorrectForSESergienko();
            getUserResultByIDIsCorrectForNikolaev();
            getUserByIdThrowUserNotExistExceptionForNotExistingGUID();
            getUserByLoginThrowUserNotExistExceptionWhenUserWithThisLoginIsNotExists();

            addUserIsWorked();
            addUserWithRolesIsWorked();
            addNullUserThrowArgumentNullException();
            addExistingUserThrowUserAlreadyExistException();
            addUserThrowExceptionWhenCanNotFindGroup();
            addUserBreakAfterExceptionInTransaction();
            addUserRollbackActionWhenThrowsException();

            removeUserByIDIsWorked();
            removeUserByLoginIsWorked();
            removeUserWithRolesIsWorked();
            removeNotExistingUserByLoginThrowArgumentException();
            removeNotExistingUserByIDThrowArgumentException();

            canChangeDepartmentForUser();
            updateForNullUserThrowArgumentNullException();
            addGroupForUserIsWorked();
            changeGroupForUserIsWorked();
            removeGroupForUserIsWorked();
            dropGroupsForUserIsWorked();
            addExistingGroupDoesNotThrowException();

            updateUserThrowExceptionWhenCanNotFindGroup();
            updateUserBreakAfterExceptionInTransaction();
            updateUserRollbackActionWhenThrowsException();
            changePasswordForUserIsWorked();
            changePasswordThrowArgumentNullExceptionWhenUserIsNull();
            changePasswordThrowArgumentNullExceptionWhenNEwPasswordIsWhitespace();
            checkChalenkoWithCorrectPasswordIsTrue();
            checkChalenkoWithUncorrectPasswordIsFalse();
            checkBelozerovThrowUserNotExistException();
            resetPasswordIsWorked();
        }

        [ClassInitialize()]
        public static void InitClass(TestContext context)
        {
            DatabaseContext.Instance.ExecuteCommand("prepare_user_tables", CommandType.StoredProcedure);
            CurrentUser.getDBInstance("a.s.ilichev");
            //DBUserStorage.logger = ProjectKernel.Classes.Logger.DBLogger.Instance;
        }

        [TestInitialize()]
        public void InitTest()
        {
            //DatabaseContext.Instance.ExecuteCommand("prepare_user_tables", CommandType.StoredProcedure);
            CurrentUser.getDBInstance("a.s.ilichev");
            //DBUserStorage.logger = ProjectKernel.Classes.Logger.DBLogger.Instance;
        }

        [TestMethod]
        public void DBUserViewIsNotNull()
        {
            System.Data.DataTable dt = DBUserStorage.Instance.getView();
            
            Assert.IsNotNull(dt);
        }

        [TestMethod]
        public void DBUserViewIsNotEmpty()
        {
            System.Data.DataTable dt = DBUserStorage.Instance.getView();
            
            Assert.AreEqual(7, dt.Rows.Count);
        }

        [TestMethod]
        public void canViewDBUserList()
        {
            System.Data.DataTable dt = DBUserStorage.Instance.getView();
            
            Assert.AreEqual("Леванова", dt.Rows[6]["Фамилия"]);
            Assert.AreEqual("Татьяна", dt.Rows[6]["Имя"]);
            Assert.AreEqual("Александровна", dt.Rows[6]["Отчество"]);
            Assert.AreEqual("t.a.levanova", dt.Rows[6]["Логин"]);
            Assert.AreEqual("ННГУ", dt.Rows[6]["Подразделение"]);
        }

        [TestMethod]
        public void getUserResultByLoginIsNotNullForSESergienko()
        {
            DBUser user = DBUserStorage.Instance.getUser("s.e.sergienko");
            
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void getUserResultByLoginIsCorrectForSESergienko()
        {
            DBUser user = DBUserStorage.Instance.getUser("s.e.sergienko");
            
            Assert.AreEqual("Сергиенко", user.Surname);
            Assert.AreEqual("Сергей", user.Name);
            Assert.AreEqual("Сергиенко Сергей", user.FullName);
            Assert.AreEqual("Эдуардович", user.Patronymic);
            Assert.AreEqual("s.e.sergienko", user.Login);
            Assert.AreEqual("Freemake", user.Department);
            Assert.IsFalse(user.IsAdmin);
            Assert.AreEqual(2, user.Groups.Count);
            Assert.AreEqual("Friends", user.Groups[0]);
            Assert.AreEqual("NNSU", user.Groups[1]);
        }

        [TestMethod]
        public void getUserResultByIDIsCorrectForNikolaev()
        {
            string id = DatabaseContext.Instance.ExecuteScalar("SELECT [id] FROM [dbo].[User] WHERE login = 'a.o.nikolaev'", CommandType.Text).ToString();
            DBUser user = DBUserStorage.Instance.getUser(Guid.Parse(id));

            Assert.AreEqual("Николаев", user.Surname);
            Assert.AreEqual("Артем", user.Name);
            Assert.AreEqual("Николаев Артем", user.FullName);
            Assert.AreEqual("a.o.nikolaev", user.Login);
            Assert.AreEqual("HSE", user.Department);
            Assert.IsFalse(user.IsAdmin);
            Assert.AreEqual(0, user.Groups.Count);
        }

        [TestMethod]
        public void getUserByIdThrowUserNotExistExceptionForNotExistingGUID()
        {
            try
            {
                DBUser user = DBUserStorage.Instance.getUser(Guid.Parse("55b882fd-349a-439f-8155-12f592f01363"));
            }
            catch (UserNotExistException ex)
            {
                Assert.AreEqual("User with ID '55b882fd-349a-439f-8155-12f592f01363' is not exist", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void getUserByLoginThrowUserNotExistExceptionWhenUserWithThisLoginIsNotExists()
        {
            try
            {
                DBUser user = DBUserStorage.Instance.getUser("r.s.nagornov");
            }
            catch (UserNotExistException ex)
            {
                Assert.AreEqual("User with login 'r.s.nagornov' is not exist", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addUserIsWorked()
        {
            CurrentUser.getDBInstance("a.s.ilichev");
            DataTable dtBefore = DBUserStorage.Instance.getView();
            DBUser user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            Guid returnedId = DBUserStorage.Instance.add(user);
            DataTable dtAfter = DBUserStorage.Instance.getView();
            DBUser actualUser = DBUserStorage.Instance.getUser("m.i.bolotov");

            #region undo

            DBUserStorage.Instance.delete(returnedId);

            #endregion

            Assert.IsNotNull(returnedId);
            Assert.ReferenceEquals(user, actualUser);
            Assert.AreEqual(0, dtBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
            Assert.AreEqual(1, dtAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
        }

        [TestMethod]
        public void addUserWithRolesIsWorked()
        {
            DataTable dtBefore = DBUserStorage.Instance.getView();
            DBUser user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            List<string> roles = new List<string>();
            roles.Add("Friends");
            roles.Add("NNSU");
            user.addGroups(roles);
            Guid returnedId = DBUserStorage.Instance.add(user);
            DataTable dtAfter = DBUserStorage.Instance.getView();
            DataTable dtRoles = DatabaseContext.Instance.LoadFromDatabase(string.Format("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[Role] AS r ON ur.role_id = r.id WHERE user_id = '{0}' ORDER BY r.name ASC", returnedId), CommandType.Text);
            DBUser actualUser = DBUserStorage.Instance.getUser("a.v.khilov");

            #region undo

            DBUserStorage.Instance.delete(returnedId);

            #endregion

            Assert.IsNotNull(returnedId);
            Assert.ReferenceEquals(user, actualUser);
            Assert.AreEqual(2, actualUser.Groups.Count);
            Assert.AreEqual(0, dtBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "a.v.khilov"));
            Assert.AreEqual(1, dtAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "a.v.khilov"));
            Assert.AreEqual(2, dtRoles.AsEnumerable().Count());
            Assert.AreEqual("Friends", dtRoles.Rows[0]["name"]);
            Assert.AreEqual("NNSU", dtRoles.Rows[1]["name"]);
        }

        [TestMethod]
        public void addNullUserThrowArgumentNullException()
        {
            DataTable dtBefore = DBUserStorage.Instance.getView();
            try
            {
                DBUserStorage.Instance.add(null);
            }
            catch (ArgumentNullException ex)
            {
                DataTable dtAfter = DBUserStorage.Instance.getView();

                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: user", ex.Message);
                Assert.IsTrue(dtAfter.AsEnumerable().Count() - dtBefore.AsEnumerable().Count() == 0);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addExistingUserThrowUserAlreadyExistException()
        {
            DataTable dtBefore = DBUserStorage.Instance.getView();
            DBUser user = DBUserStorage.Instance.getUser("s.e.sergienko");
            try
            {
                DBUserStorage.Instance.add(user);
            }
            catch (ProjectKernel.Classes.User.UserAlreadyExistException ex)
            {
                DataTable dtAfter = DBUserStorage.Instance.getView();
                
                Assert.AreEqual("User with login 's.e.sergienko' is already exist in this system", ex.Message);
                Assert.AreEqual(1, dtBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "s.e.sergienko"));
                Assert.AreEqual(1, dtAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "s.e.sergienko"));
                Assert.IsTrue(dtAfter.AsEnumerable().Count() - dtBefore.AsEnumerable().Count() == 0);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addUserThrowExceptionWhenCanNotFindGroup()
        {
            // Add user work like transaction
            DataTable dtBefore = DBUserStorage.Instance.getView();
            DBUser user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            user.addGroup("Murom citizen");
            try
            {
                Guid returnedId = DBUserStorage.Instance.add(user);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                DataTable dtAfter = DBUserStorage.Instance.getView();

                Assert.AreEqual(515, ex.Number);
                Assert.AreEqual("Не удалось вставить значение NULL в столбец \"role_id\", таблицы \"DBTEST.dbo.UserRole\"; в столбце запрещены значения NULL. Ошибка в INSERT.\r\nВыполнение данной инструкции было прервано.", ex.Message);
                Assert.AreEqual(0, dtBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
                Assert.AreEqual(0, dtAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
                Assert.IsTrue(dtAfter.AsEnumerable().Count() - dtBefore.AsEnumerable().Count() == 0);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addUserBreakAfterExceptionInTransaction()
        {
            // Add user work like transaction
            DataTable dtBefore = DBUserStorage.Instance.getView();
            DataTable dtRolesBefore = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM UserRole", CommandType.Text);
            DBUser user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            user.addGroup("Murom citizen");
            user.addGroup("Postgraduate");
            try
            {
                Guid returnedId = DBUserStorage.Instance.add(user);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                DataTable dtAfter = DBUserStorage.Instance.getView();
                DataTable dtRolesAfter = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM UserRole", CommandType.Text);

                //Assert.AreEqual("Cannot insert the value NULL into column 'role_id', table 'C:\\USERS\\P.V.CHALENKO\\DESKTOP\\PROJECTKERNEL\\PROJECTKERNEL\\DBTEST.MDF.dbo.UserRole'; column does not allow nulls. INSERT fails.\r\nThe statement has been terminated.", ex.Message);
                Assert.AreEqual(515, ex.Number);
                Assert.AreEqual(0, dtBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
                Assert.AreEqual(0, dtAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
                Assert.IsTrue(dtRolesAfter.AsEnumerable().Count() - dtRolesBefore.AsEnumerable().Count() == 0);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addUserRollbackActionWhenThrowsException()
        {
            // Add user work like transaction
            DataTable dtUserBefore = DBUserStorage.Instance.getView();
            DataTable dtRolesBefore = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id", CommandType.Text);
            DBUser user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            user.addGroup("NNSU");
            user.addGroup("Postgraduate");
            try
            {
                Guid returnedId = DBUserStorage.Instance.add(user);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                DataTable dtUserAfter = DBUserStorage.Instance.getView();
                DataTable dtRolesAfter = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id", CommandType.Text);
                
                //Assert.AreEqual("Cannot insert the value NULL into column 'role_id', table 'C:\\USERS\\P.V.CHALENKO\\DESKTOP\\PROJECTKERNEL\\PROJECTKERNEL\\DBTEST.MDF.dbo.UserRole'; column does not allow nulls. INSERT fails.\r\nThe statement has been terminated.", ex.Message);
                Assert.AreEqual(515, ex.Number);
                Assert.IsTrue(dtUserBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov") == dtUserAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
                Assert.AreEqual(0, dtUserBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
                Assert.AreEqual(0, dtUserAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
                Assert.IsTrue(dtRolesBefore.AsEnumerable().Count(el => el["login"].ToString() == "m.i.bolotov") == dtRolesAfter.AsEnumerable().Count(el => el["login"].ToString() == "m.i.bolotov"));
                Assert.AreEqual(0, dtRolesBefore.AsEnumerable().Count(el => el["login"].ToString() == "m.i.bolotov"));
                Assert.AreEqual(0, dtRolesAfter.AsEnumerable().Count(el => el["login"].ToString() == "m.i.bolotov"));
                Assert.IsTrue(dtRolesAfter.AsEnumerable().Count() - dtRolesBefore.AsEnumerable().Count() == 0);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void removeUserByIDIsWorked()
        {
            #region prepare

            DBUser user = new DBUser("Bolotov", "Max", "Il'ich", "m.i.bolotov", "NNSU");
            Guid returnedId = DBUserStorage.Instance.add(user);

            #endregion

            DataTable dtBefore = DBUserStorage.Instance.getView();
            DBUserStorage.Instance.delete(returnedId);
            DataTable dtAfter = DBUserStorage.Instance.getView();

            Assert.AreEqual(1, dtBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
            Assert.AreEqual(0, dtAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "m.i.bolotov"));
        }

        [TestMethod]
        public void removeUserByLoginIsWorked()
        {
            #region prepare

            DBUser user = new DBUser("Lavrenov", "Ilya", "Pavlovich", "i.p.lavrenov", "NNSU");
            DBUserStorage.Instance.add(user);

            #endregion

            DataTable dtBefore = DBUserStorage.Instance.getView();
            DBUserStorage.Instance.delete("i.p.lavrenov");
            DataTable dtAfter = DBUserStorage.Instance.getView();

            Assert.AreEqual(1, dtBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "i.p.lavrenov"));
            Assert.AreEqual(0, dtAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "i.p.lavrenov"));
        }

        [TestMethod]
        public void removeUserWithRolesIsWorked()
        {
            #region prepare

            DataTable dtBefore = DBUserStorage.Instance.getView();
            DBUser user = new DBUser("Khilov", "Alex", "Vladimirovich", "a.v.khilov", "IAP RAS");
            List<string> roles = new List<string>();
            roles.Add("Friends");
            roles.Add("NNSU");
            user.addGroups(roles);
            Guid returnedId = DBUserStorage.Instance.add(user);

            #endregion

            DataTable dtUserBefore = DBUserStorage.Instance.getView();
            DataTable dtUserRolesBefore = DatabaseContext.Instance.LoadFromDatabase(string.Format("SELECT * FROM UserRole WHERE user_id = '{0}'", returnedId), CommandType.Text);
            DBUserStorage.Instance.delete("a.v.khilov");
            DataTable dtUserAfter = DBUserStorage.Instance.getView();
            DataTable dtUserRolesAfter = DatabaseContext.Instance.LoadFromDatabase(string.Format("SELECT * FROM UserRole WHERE user_id = '{0}'", returnedId), CommandType.Text);

            Assert.AreEqual(1, dtUserBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "a.v.khilov"));
            Assert.AreEqual(0, dtUserAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "a.v.khilov"));
            Assert.AreEqual(2, dtUserRolesBefore.AsEnumerable().Count());
            Assert.AreEqual(0, dtUserRolesAfter.AsEnumerable().Count());
        }

        [TestMethod]
        public void removeNotExistingUserByLoginThrowArgumentException()
        {
            try
            {
                DBUserStorage.Instance.delete("i.p.lavrenov");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("User is not exist", ex.Message);
            }
        }

        [TestMethod]
        public void removeNotExistingUserByIDThrowArgumentException()
        {
            try
            {
                DBUserStorage.Instance.delete(Guid.Parse("55b882fd-349a-439f-8155-12f592f01363"));
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("User is not exist", ex.Message);
            }
        }

        [TestMethod]
        public void canChangeDepartmentForUser()
        {
            DBUser user = DBUserStorage.Instance.getUser("p.v.chalenko");
            user.Department = "ОРПОиТП";
            DBUserStorage.Instance.update(user);
            DBUser actualUser = DBUserStorage.Instance.getUser("p.v.chalenko");

            #region undo

            user.Department = "ORPOiTP";
            DBUserStorage.Instance.update(user);

            #endregion

            Assert.AreEqual("ОРПОиТП", actualUser.Department);
        }

        [TestMethod]
        public void updateForNullUserThrowArgumentNullException()
        {
            try
            {
                DBUserStorage.Instance.update(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: user", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void addGroupForUserIsWorked()
        {
            DataTable dtBefore = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 's.e.sergienko'", CommandType.Text);
            DBUser user = DBUserStorage.Instance.getUser("s.e.sergienko");
            user.addGroup("Admin");
            DBUserStorage.Instance.update(user);
            DBUser actualUser = DBUserStorage.Instance.getUser("s.e.sergienko");
            DataTable dtAfter = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 's.e.sergienko'", CommandType.Text);

            #region undo

            user.removeGroup("Admin");
            DBUserStorage.Instance.update(user);

            #endregion

            Assert.IsTrue(actualUser.IsAdmin);
            Assert.IsTrue(actualUser.Groups.Contains("Admin"));
            Assert.AreEqual(3, actualUser.Groups.Count);
            Assert.AreEqual(2, dtBefore.AsEnumerable().Count());
            Assert.AreEqual(3, dtAfter.AsEnumerable().Count());
        }

        [TestMethod]
        public void changeGroupForUserIsWorked()
        {
            DataTable dtBefore = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 's.e.sergienko'", CommandType.Text);
            DBUser user = DBUserStorage.Instance.getUser("s.e.sergienko");
            user.removeGroup("NNSU");
            user.addGroup("Admin");
            DBUserStorage.Instance.update(user);
            DBUser actualUser = DBUserStorage.Instance.getUser("s.e.sergienko");
            DataTable dtAfter = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 's.e.sergienko'", CommandType.Text);

            #region undo

            user.removeGroup("Admin");
            user.addGroup("NNSU");
            DBUserStorage.Instance.update(user);

            #endregion

            Assert.IsTrue(actualUser.IsAdmin);
            Assert.IsTrue(actualUser.Groups.Contains("Admin"));
            Assert.AreEqual(2, actualUser.Groups.Count);
            Assert.AreEqual(2, dtBefore.AsEnumerable().Count());
            Assert.AreEqual(2, dtAfter.AsEnumerable().Count());
        }

        [TestMethod]
        public void removeGroupForUserIsWorked()
        {
            DataTable dtBefore = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 'p.v.chalenko'", CommandType.Text);
            DBUser user = DBUserStorage.Instance.getUser("p.v.chalenko");
            user.removeGroup("Admin");
            DBUserStorage.Instance.update(user);
            DBUser actualUser = DBUserStorage.Instance.getUser("p.v.chalenko");
            DataTable dtAfter = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 'p.v.chalenko'", CommandType.Text);

            #region undo

            user.addGroup("Admin");
            DBUserStorage.Instance.update(user);

            #endregion

            Assert.IsFalse(actualUser.IsAdmin);
            Assert.IsFalse(actualUser.Groups.Contains("Admin"));
            Assert.AreEqual(0, actualUser.Groups.Count);
            Assert.AreEqual(1, dtBefore.AsEnumerable().Count());
            Assert.AreEqual(0, dtAfter.AsEnumerable().Count());
        }

        [TestMethod]
        public void dropGroupsForUserIsWorked()
        {
            DataTable dtBefore = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 's.e.sergienko'", CommandType.Text);
            DBUser user = DBUserStorage.Instance.getUser("s.e.sergienko");
            user.removeGroups();
            DBUserStorage.Instance.update(user);
            DBUser actualUser = DBUserStorage.Instance.getUser("s.e.sergienko");
            DataTable dtAfter = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 's.e.sergienko'", CommandType.Text);

            #region undo

            user.addGroup("Friends");
            user.addGroup("NNSU");
            DBUserStorage.Instance.update(user);

            #endregion

            Assert.IsFalse(actualUser.IsAdmin);
            Assert.AreEqual(0, actualUser.Groups.Count);
            Assert.AreEqual(2, dtBefore.AsEnumerable().Count());
            Assert.AreEqual(0, dtAfter.AsEnumerable().Count());
        }

        [TestMethod]
        public void addExistingGroupDoesNotThrowException()
        {
            DBUser user = DBUserStorage.Instance.getUser("p.v.chalenko");
            user.addGroup("Admin");
            try
            {
                DBUserStorage.Instance.update(user);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void updateUserThrowExceptionWhenCanNotFindGroup()
        {
            //Update user work like transaction
            DataTable dtBefore = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 'a.s.ilichev'", CommandType.Text);
            DBUser user = DBUserStorage.Instance.getUser("a.s.ilichev");
            user.addGroup("Dzerzhinsk citizen");
            try
            {
                DBUserStorage.Instance.update(user);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                DataTable dtAfter = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 'a.s.ilichev'", CommandType.Text);
                
                //Assert.AreEqual("Cannot insert the value NULL into column 'role_id', table 'C:\\USERS\\P.V.CHALENKO\\DESKTOP\\PROJECTKERNEL\\PROJECTKERNEL\\DBTEST.MDF.dbo.UserRole'; column does not allow nulls. INSERT fails.\r\nThe statement has been terminated.", ex.Message);
                Assert.AreEqual(515, ex.Number);
                Assert.AreEqual(2, dtBefore.AsEnumerable().Count());
                Assert.AreEqual(2, dtAfter.AsEnumerable().Count());
                Assert.IsTrue(dtAfter.AsEnumerable().Count() - dtBefore.AsEnumerable().Count() == 0);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void updateUserBreakAfterExceptionInTransaction()
        {
            // Update user work like transaction
            DataTable dtRolesBefore = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 'a.s.ilichev'", CommandType.Text);
            DBUser user = DBUserStorage.Instance.getUser("a.s.ilichev");
            user.addGroup("Dzerzhinsk citizen");
            user.addGroup("Photograph");
            try
            {
                DBUserStorage.Instance.update(user);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                DataTable dtRolesAfter = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 'a.s.ilichev'", CommandType.Text);

                //Assert.AreEqual("Cannot insert the value NULL into column 'role_id', table 'C:\\USERS\\P.V.CHALENKO\\DESKTOP\\PROJECTKERNEL\\PROJECTKERNEL\\DBTEST.MDF.dbo.UserRole'; column does not allow nulls. INSERT fails.\r\nThe statement has been terminated.", ex.Message);
                Assert.AreEqual(515, ex.Number);
                Assert.AreEqual(2, dtRolesBefore.AsEnumerable().Count());
                Assert.AreEqual(2, dtRolesAfter.AsEnumerable().Count());
                Assert.IsTrue(dtRolesAfter.AsEnumerable().Count() - dtRolesBefore.AsEnumerable().Count() == 0);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void updateUserRollbackActionWhenThrowsException()
        {
            // Update user work like transaction
            DataTable dtUserBefore = DBUserStorage.Instance.getView();
            DataTable dtRolesBefore = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 'a.s.ilichev'", CommandType.Text);
            DBUser user = DBUserStorage.Instance.getUser("a.s.ilichev");
            user.addGroup("Friends");
            user.addGroup("Photograph");
            try
            {
                DBUserStorage.Instance.update(user);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                DataTable dtUserAfter = DBUserStorage.Instance.getView();
                DataTable dtRolesAfter = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[UserRole] AS ur LEFT JOIN [dbo].[User] AS u ON ur.user_id = u.id WHERE login LIKE 'a.s.ilichev'", CommandType.Text);

                //Assert.AreEqual("Cannot insert the value NULL into column 'role_id', table 'C:\\USERS\\P.V.CHALENKO\\DESKTOP\\PROJECTKERNEL\\PROJECTKERNEL\\DBTEST.MDF.dbo.UserRole'; column does not allow nulls. INSERT fails.\r\nThe statement has been terminated.", ex.Message);
                int codeOfCannotInsertNull = 515;
                Assert.AreEqual(codeOfCannotInsertNull, ex.Number);
                Assert.IsTrue(dtUserBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "a.s.ilichev") == dtUserAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "a.s.ilichev"));
                Assert.AreEqual(1, dtUserBefore.AsEnumerable().Count(el => el["Логин"].ToString() == "a.s.ilichev"));
                Assert.AreEqual(1, dtUserAfter.AsEnumerable().Count(el => el["Логин"].ToString() == "a.s.ilichev"));
                Assert.IsTrue(dtRolesBefore.AsEnumerable().Count() == dtRolesAfter.AsEnumerable().Count());
                Assert.AreEqual(2, dtRolesBefore.AsEnumerable().Count());
                Assert.AreEqual(2, dtRolesAfter.AsEnumerable().Count());
                Assert.IsTrue(dtRolesAfter.AsEnumerable().Count() - dtRolesBefore.AsEnumerable().Count() == 0);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void changePasswordForUserIsWorked()
        {
            DBUserStorage.Instance.changePassword(DBUserStorage.Instance.getUser("p.v.chalenko"), "chaly chiter");
            bool checkResult = DBUserStorage.Instance.checkUser("p.v.chalenko", "chaly chiter");
            DataTable dt = DatabaseContext.Instance.LoadFromDatabase("SELECT * FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko'", CommandType.Text);
            
            #region undo

            DBUserStorage.Instance.changePassword(DBUserStorage.Instance.getUser("p.v.chalenko"), "hohol");

            #endregion

            Assert.IsNotNull(dt.Rows[0]["password"]);
            Assert.IsNotNull(dt.Rows[0]["salt"]);
            Assert.AreEqual(Password.GenerateHash(dt.Rows[0]["salt"].ToString(), "chaly chiter"), dt.Rows[0]["password"].ToString());
            Assert.IsTrue(checkResult);
        }

        [TestMethod]
        public void changePasswordThrowArgumentNullExceptionWhenUserIsNull()
        {
            try
            {
                DBUserStorage.Instance.changePassword(null, "chaly chiter");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: user", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void changePasswordThrowArgumentNullExceptionWhenNEwPasswordIsWhitespace()
        {
            try
            {
                DBUserStorage.Instance.changePassword(DBUserStorage.Instance.getUser("p.v.chalenko"), "");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Значение не может быть неопределенным.\r\nИмя параметра: new password", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void checkChalenkoWithCorrectPasswordIsTrue()
        {
            DBUserStorage.Instance.changePassword(DBUserStorage.Instance.getUser("p.v.chalenko"), "hohol");
            bool checkResult = DBUserStorage.Instance.checkUser("p.v.chalenko", "hohol");
            
            Assert.IsTrue(checkResult);
        }

        [TestMethod]
        public void checkChalenkoWithUncorrectPasswordIsFalse()
        {
            bool checkResult = DBUserStorage.Instance.checkUser("p.v.chalenko", "lohoh");

            Assert.IsFalse(checkResult);
        }

        [TestMethod]
        public void checkBelozerovThrowUserNotExistException()
        {
            try
            {
                bool checkResult = DBUserStorage.Instance.checkUser("yu.s.belozerov", "Yuran");
            }
            catch (UserNotExistException ex)
            {
                Assert.AreEqual("User with login 'yu.s.belozerov' is not exist", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void resetPasswordIsWorked()
        {
            //arrange
            DBUser user = DBUserStorage.Instance.getUser("a.o.nikolaev");
            DBUserStorage.Instance.changePassword(user, "I'm best of the best of the best");
            //act
            DBUserStorage.Instance.resetPassword(user);
            //assert
            Assert.IsTrue(DBUserStorage.Instance.checkUser("a.o.nikolaev", "qwerty"));
        }

        [TestMethod]
        public void isActivePropertyForSergienkoIsTrue()
        {
            //act
            DBUser user = DBUserStorage.Instance.getUser("s.e.sergienko");
            //assert
            Assert.IsTrue(user.IsActive);
        }

        [TestMethod]
        public void isActivePropertyForAzarIsFalse()
        {
            //act
            DBUser user = DBUserStorage.Instance.getUser("j.azar");
            //assert
            Assert.IsFalse(user.IsActive);
        }
    }
}
