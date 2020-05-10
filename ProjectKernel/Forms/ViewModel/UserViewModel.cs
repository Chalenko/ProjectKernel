using ProjectKernel.Classes;
using ProjectKernel.Classes.User;
using ProjectKernel.Forms.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectKernel.Forms.ViewModel
{
    public abstract class UserViewModel
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Login { get; set; }

        public string Department { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool IsActive { get; set; }

        public bool HaveToChangePassword { get; set; }

        public DataTable Roles { get; set; }

        public bool canEdit = true;

        public bool canDropPassword = false;

        public ActionResult OKResult = ActionResult.FAIL;

        public UserViewModel()
        {
            Surname = "";
            Name = "";
            Patronymic = "";
            Login = "";
            Department = "";
            Roles = new DataTable();
            Roles.Columns.Add("id", typeof(Guid));
            Roles.Columns.Add("name", typeof(string));
        }
        
        public void dropPassword(Form parent)
        {
            if (MessageBox.Show(parent, "Вы уверены что хотите сбросить пароль?", "Сброс пароля", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ProjectKernel.Forms.View.ResetPasswordForm rpf = new ProjectKernel.Forms.View.ResetPasswordForm(new ChangePasswordFormViewModel(DBUserStorage.Instance.getUser(Login), (a, b) => { return true; }, DBUserStorage.Instance.changePassword));
                if (rpf.ShowDialog(parent) == DialogResult.OK)
                {
                    HaveToChangePassword = true;
                    canDropPassword = false;
                    MessageBox.Show("Пароль сброшен");
                }
            }
        }
        
        public void addRole(Form parent)
        {
            SelectForm sf = new SelectForm(new SelectFormViewModel(DBRoleStorage.Instance.GetView()));
            sf.DGVData.Columns["creator"].Visible = sf.DGVData.Columns["create_date"].Visible = sf.DGVData.Columns["modifier"].Visible = sf.DGVData.Columns["modify_date"].Visible = false;
            sf.DGVData.Columns["name"].HeaderText = "Наименование";
            sf.DGVData.Columns["description"].HeaderText = "Описание";
            if (sf.ShowDialog(parent) == DialogResult.OK)
            {
                Role role = DBRoleStorage.Instance.GetRole(sf.SelectedId);
                DataRow row = Roles.NewRow();
                row["id"] = role.Id;
                row["name"] = role.Name;
                Roles.Rows.Add(row);
            }
        }

        public void removeRole(Form parent, int position)
        {
            DialogResult dr = MessageBox.Show(parent, "Хотите?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Roles.Rows.RemoveAt(position);
            }
        }

        public abstract void OKPerform();
    }

    public class AddUserViewModel : UserViewModel
    {

        public override void OKPerform()
        {
            DBUser user = new DBUser(Surname, Name, Patronymic, Login, Department);
            user.IsActive = IsActive;
            string salt = Password.GenerateSalt();
            string password = Password.GenerateHash(salt, "qwerty");
            user.Password = new Password(salt, password, HaveToChangePassword);
            foreach(DataRow row in Roles.Rows)
            {
                user.addGroup(row["name"].ToString());
            }

            try
            {
                DBUserStorage.Instance.add(user);
                ProjectKernel.Classes.Logger.DBLogger.Instance.info(String.Format("Пользователь {0} был добавлен", user.Login));
                OKResult = ActionResult.OK;
            }
            catch (UserAlreadyExistException ex)
            {
                MessageBox.Show("User already exist");
                ProjectKernel.Classes.Logger.DBLogger.Instance.warn("Пользователь уже существует", ex);
                OKResult = ActionResult.FAIL;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить пользователя");
                ProjectKernel.Classes.Logger.DBLogger.Instance.error(String.Format("Не удалось добавить пользователя {0}", user.ToString()), ex);
                OKResult = ActionResult.FAIL;
            }
        }
    }

    public class EditUserViewModel : UserViewModel
    {
        private DBUser user;

        public EditUserViewModel(DBUser user) : base()
        {
            this.user = user;
            Surname = user.Surname;
            Name = user.Name;
            Patronymic = user.Patronymic;
            Login = user.Login;
            Department = user.Department;
            HaveToChangePassword = user.Password.HaveToChange;
            IsActive = user.IsActive;

            foreach (string sRole in user.Groups)
            {
                Role role = DBRoleStorage.Instance.GetRole(sRole);
                DataRow row = Roles.NewRow();
                row["id"] = role.Id;
                row["name"] = role.Name;
                Roles.Rows.Add(row);
            }

            canDropPassword = CurrentUser.Instance.IsAdmin;
            canEdit = CurrentUser.Instance.IsAdmin;
        }

        public override void OKPerform()
        {
            user.Surname = Surname;
            user.Name = Name;
            user.Patronymic = Patronymic;
            user.Login = Login;
            user.Department = Department;
            user.IsActive = IsActive;
            user.Password.HaveToChange = HaveToChangePassword;
            user.removeGroups();
            //List<string> l = Roles.AsEnumerable().Select(x => x.Field<string>(0)).ToList<string>();
            user.addGroups(Roles.AsEnumerable().Select(x => x.Field<string>("name")).ToList<string>());

            try
            {
                DBUserStorage.Instance.update(user);
                ProjectKernel.Classes.Logger.DBLogger.Instance.info(String.Format("Пользователь {0} был изменен", user.Login));
                OKResult = ActionResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось изменить поьзователя");
                ProjectKernel.Classes.Logger.DBLogger.Instance.warn("Не удалось изменить пользователя", ex);
                OKResult = ActionResult.FAIL;
            }
        }
    }
}
