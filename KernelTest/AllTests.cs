using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectKernel.Classes;
using ProjectKernel.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelTest
{
    public class TemplateDelegate 
    {
        public static bool LoginCheck(string login, string password)
        {
            //Go to database and check
            //or to do somthing enother
            return Convert.ToInt32(DatabaseContext.Instance.ExecuteScalar(String.Format("SELECT password FROM Users WHERE login = '{0}'", login), System.Data.CommandType.Text)) == password.GetHashCode();
        }

        public static void FiveSecondsWaitMarque(System.Windows.Forms.ProgressBar pb, System.Windows.Forms.Label lbl)
        {
            for (int i = 1; i < 6; i++)
            {
                lbl.Text = i.ToString();
                System.Threading.Thread.Sleep(1000);
            }
            System.Windows.Forms.MessageBox.Show("Your time is out");
        }

        public static void TenSecondsWaitCont(System.Windows.Forms.ProgressBar pb, System.Windows.Forms.Label lbl)
        {
            pb.Step = (pb.Maximum - pb.Minimum) / 10;
            for (int i = 0; i < 10; i++)
            {
                lbl.Text = string.Format("{0}%", i * 10);
                System.Threading.Thread.Sleep(500);
                pb.PerformStep();
            }
        }

        public static void TwentySecondsWaitCont(System.ComponentModel.BackgroundWorker bw)
        {
            for (int i = 0; i < 20; i++)
            {
                if (bw.CancellationPending) break;
                bw.ReportProgress(i * 5, string.Format("{0}%", i * 5));
                System.Threading.Thread.Sleep(500);
            }
        }
    }

    class AllTests
    {
        public static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();

            ProjectKernel.Classes.User.CurrentUser.getDBInstance("p.v.chalenko");

            new Form2().ShowDialog();
            //new UserForm(new ProjectKernel.Forms.ViewModel.AddUserViewModel()).ShowDialog();
            //new UserForm(new ProjectKernel.Forms.ViewModel.EditUserViewModel(ProjectKernel.Classes.User.DBUserStorage.Instance.getUser("a.s.ilichev"))).ShowDialog();

            //new ProgressForm(TemplateDelegate.TwentySecondsWaitCont).ShowDialog();

            //ProjectKernel.Classes.User.DBUserStorage.changePassword(ProjectKernel.Classes.User.DBUserStorage.getUser("p.v.chalenko"), "qwerty");
            //System.Windows.Forms.MessageBox.Show(ProjectKernel.Classes.User.DBUserStorage.checkUser("p.v.chalenko", "qwerty").ToString());
            ProjectKernel.Forms.View.LoginForm lf = new ProjectKernel.Forms.View.LoginForm(new ProjectKernel.Forms.ViewModel.LoginFormViewModel(ProjectKernel.Classes.User.DBUserStorage.Instance.checkUser));
            lf.ShowDialog();
            if (lf.EnterResult == ProjectKernel.Forms.ViewModel.ActionResult.OK) 
            {
                Activity.logIn();
            }
            Activity.logOut();
            //new ProjectKernel.Forms.View.LoginForm(new ProjectKernel.Forms.ViewModel.LoginFormViewModel(ProjectKernel.Classes.User.DBUserStorage.checkUser)).ShowDialog();
            System.Windows.Forms.MessageBox.Show(new ProjectKernel.Forms.View.LoginForm(new ProjectKernel.Forms.ViewModel.LoginFormViewModel(ProjectKernel.Classes.User.DBUserStorage.Instance.checkUser)).ShowDialog().ToString());
            ProjectKernel.Classes.User.CurrentUser.getDBInstance("p.v.chalenko");
            new UserForm(new ProjectKernel.Forms.ViewModel.AddUserViewModel()).ShowDialog();
            new UserForm(new ProjectKernel.Forms.ViewModel.EditUserViewModel(ProjectKernel.Classes.User.DBUserStorage.Instance.getUser("a.s.ilichev"))).ShowDialog();
            //System.Windows.Forms.DialogResult dr = new WaitForm(TemplateDelegate.TwentySecondsWaitCont).ShowDialog();
            //System.Windows.Forms.MessageBox.Show(dr.ToString());
            //new Form2().ShowDialog();
            //new Form1().ShowDialog();
            //ProjectKernel.Classes.User.UserCreator.create(ProjectKernel.Classes.User.UserType.System);
            //ProjectKernel.Classes.User.CurrentUser.getDBInstance("p.v.chalenko");
            //ProjectKernel.Classes.User.DBUserStorage.resetPassword((ProjectKernel.Classes.User.DBUser)ProjectKernel.Classes.User.CurrentUser.Instance);
            //new WaitForm(TemplateDelegate.FiveSecondsWaitMarque).ShowDialog();
            //new ProgressForm(TemplateDelegate.TenSecondsWaitCont).ShowDialog();
            //new ChangePasswordForm(ProjectKernel.Classes.User.DBUserStorage.checkUser, ProjectKernel.Classes.User.DBUserStorage.changePassword).ShowDialog();
            //new LoginForm(delegate() { Console.WriteLine("Hello!"); }, ProjectKernel.Classes.User.DBUserStorage.checkUser).ShowDialog();
            //new UserCreatorTest().runTests();
            //new SystemUserTest().runTests();
            //new DBUserTest().runTests();
            //new DBUserStorageTest().runTests();
            //new FormatTest().runTests();
            //new TextFileLoggerTest().runTests();
            //new XMLFileLoggerTest().runTests();
            //new DBLoggerTest().runTests();
            //new ConsoleLoggerTest().runTests();
            //new DBContextTest().runTests();
            //new ComparerTest().runTests();
            //new PasswordTest().runTests();
        }
    }
}
