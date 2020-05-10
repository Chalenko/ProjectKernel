using System;
using System.Collections.Generic;
using System.Linq;
//using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace ProjectKernel.Classes.User
{
    internal class SystemUser : User
    {
        //currentUser = new SystemUser();
        internal SystemUser() : base()
        {
            string username = Environment.UserName; //WindowsIdentity.GetCurrent().Name;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, username);
            DirectoryEntry directoryEntry = (DirectoryEntry)(user.GetUnderlyingObject());

            Name = user.GivenName ?? "";
            Surname = user.Surname ?? "";
            Login = user.SamAccountName ?? "";
            Department = directoryEntry.Properties["department"].Value != null ? directoryEntry.Properties["department"].Value.ToString() : "";
            Groups = user.GetGroups().Select(x => x.Name).Distinct().ToList();
            Type = UserType.System;
        }
    }
}
