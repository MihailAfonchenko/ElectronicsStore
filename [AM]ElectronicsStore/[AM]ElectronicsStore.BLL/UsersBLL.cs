using _AM_ElectronicsStore.DAL;
using _AM_ElectronicsStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AM_ElectronicsStore.BLL
{
    public class UsersBLL
    {
        private static UsersDAL Data = new UsersDAL();

        public static int AddOrUpdate(User user)
        {
            return Data.AddOrUpdate(user);
        }

        public static void Delete(int id)
        {
            Data.Delete(id);
        }

        public static void UpdatePassword(int id, string password)
        {
            Data.UpdatePassword(id, password);
        }

        public static User GetByLogin(string login)
        {
            return Data.Get(login);
        }

        public static User GetById(int id)
        {
            return Data.Get(id);
        }

        public static List<User> GetAll()
        {
            return Data.Get(0, 0);
        }
    }
}
