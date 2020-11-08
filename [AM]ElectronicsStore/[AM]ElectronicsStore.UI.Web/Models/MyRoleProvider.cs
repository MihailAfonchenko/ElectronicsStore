using _AM_ElectronicsStore.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace _AM_ElectronicsStore.UI.Web.Models
{
    public class MyRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            return new[] { UsersBLL.GetByLogin(username).RoleId == Entities.RoleUser.RoleType.Admin ? "Admin" : "User"};
        }

        public override string[] GetAllRoles()
        {
            return new[] { "Admin", "User" }; 
        }

        #region not implemented section
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}