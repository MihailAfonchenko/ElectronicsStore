using _AM_ElectronicsStore.Entities;
using _AM_ElectronicsStore.Entities.RoleUser;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AM_ElectronicsStore.DAL
{
    public class UsersDAL : DALBase
    {
        private const string columnUserId = "Id";
        private const string columnLogin = "Login";
        private const string columnMail = "Mail";
        private const string columnPassword = "Password";
        private const string columnDateRegistration = "DateRegistration";
        private const string columnRoleId = "RoleId";

        private const string procAdd = "addUser";
        private const string procUpdate = "updateUser";
        private const string procPsw = "updateUserPsw";
        private const string procGetById = "getUserById";
        private const string procGetByLogin = "getUserByLogin";
        private const string procGetList = "getUsers";
        private const string procDelete = "delUser";

        private const string argUserId = "@Id";
        private const string argLogin = "@Login";
        private const string argMail = "@Mail";
        private const string argPassword = "@Password";
        private const string argDateRegistration = "@DateRegistration";
        private const string argRoleId = "@RoleId";
        private const string argStartIndex = "@StartIndex";
        private const string argCount = "@Count";

        public int AddOrUpdate(User user)
        {
            var isExist = false;
            var Id = user.Id;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argUserId, user.Id));
            ExecuteStorProc(procGetById, parameters, (reader) => isExist = reader.HasRows);

            parameters.Clear();
            parameters.Add(new SqlParameter(argRoleId, user.RoleId));

            Action<SqlDataReader> action = (reader) =>
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    Id = fromDbNullable<int>(reader[0]);
                }
            };

            if (!isExist)
            {
                parameters.Add(new SqlParameter(argLogin, user.Login));
                parameters.Add(new SqlParameter(argPassword, user.Password));
                parameters.Add(new SqlParameter(argMail, user.Mail));
                user.DateRegistration = DateTime.Now;
                parameters.Add(new SqlParameter(argDateRegistration, user.DateRegistration));
                ExecuteStorProc(procAdd, parameters, action);
            }
            else
            {
                parameters.Add(new SqlParameter(argUserId, user.Id));
                ExecuteStorProc(procUpdate, parameters, action);
            }
            return Id;
        }

        public void UpdatePassword(int id, string password)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argUserId, id));
            parameters.Add(new SqlParameter(argPassword, password));
            ExecuteStorProc(procPsw, parameters);
        }

        public User Get(string login)
        {
            User user = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argLogin, login));

            Action<SqlDataReader> action = (reader) =>
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        Id = (int)reader[columnUserId],
                        Login = (string)reader[columnLogin],
                        Password = (string)reader[columnPassword],
                        Mail = (string)reader[columnMail],
                        DateRegistration = (DateTime)reader[columnDateRegistration],
                        RoleId = (RoleType)reader[columnRoleId]
                    };
                }
                else
                {
                    user = new User();
                }
            };

            ExecuteStorProc(procGetByLogin, parameters, action);
            return user;
        }

        public User Get(int id)
        {
            User user = null;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argUserId, id));

            Action<SqlDataReader> success = (reader) =>
            {

                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        Id = (int)reader[columnUserId],
                        Login = (string)reader[columnLogin],
                        Password = (string)(reader[columnPassword]),
                        Mail = (string)(reader[columnMail]),
                        DateRegistration = (DateTime)reader[columnDateRegistration],
                        RoleId = (RoleType)reader[columnRoleId]
                    };
                }
                else
                {
                    user = new User();
                }
            };

            ExecuteStorProc(procGetById, parameters, success);

            return user;
        }

        /// <summary>
        /// Получение списка пользователей с указанной позиции и в указанном количестве
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count">0, если нужно показать всех пользователей</param>
        /// <returns></returns>
        public List<User> Get(int startIndex, int count)
        {
            List<User> list = new List<User>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argStartIndex, startIndex));
            parameters.Add(new SqlParameter(argCount, count));

            Action<SqlDataReader> success = (reader) =>
            {
                while (reader.Read())
                {
                    list.Add(new User
                    {
                        Id = (int)reader[columnUserId],
                        Login = (string)reader[columnLogin],
                        Password = (string)(reader[columnPassword]),
                        Mail = (string)(reader[columnMail]),
                        DateRegistration = (DateTime)reader[columnDateRegistration],
                        RoleId = (RoleType)reader[columnRoleId]
                    });
                }
            };

            ExecuteStorProc(procGetList, parameters, success);
            return list;
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = argUserId, Value = id });

            ExecuteStorProc(procDelete, parameters);
        }
    }
}
