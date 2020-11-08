using _AM_ElectronicsStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AM_ElectronicsStore.DAL
{
    public class CategoriesDAL : DALBase
    {
        //хранимые процедуры не реализованы на данный момент (в разработке)
        //категории на данный момент добавляются в базу скриптом

        private const string procAdd = "addCategory";
        private const string procUpdate = "updateCategory";
        private const string procGetById = "getCategoryById";
        private const string procGetAllCategories = "getCategories";
        private const string procDelete = "deleteCategory";

        private const string argId = "@Id";
        private const string argName = "@Name";

        private const string columnCategoryIdDb = "Id";
        private const string columnCategoryNameDb = "Name";

        public int AddOrUpdate(Category category)
        {
            var Id = category.Id;
            var isExist = false;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(procGetById, category.Id));
            ExecuteStorProc(procGetById, parameters, (reader) => { isExist = reader.HasRows; });

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argName, category.Name));

            if(!isExist)
            {
                Action<SqlDataReader> action = (reader) =>
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Id = fromDbNullable<int>(reader[0]);
                    }
                };

                ExecuteStorProc(procAdd, parameters, action);
            }
            else
            {
                parameters.Add(new SqlParameter(argId, category.Id));
                ExecuteStorProc(procUpdate, parameters);
            }

            return Id;
        }

        public Category GetById(int Id)
        {
            Category category = null;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argId, Id));

            Action<SqlDataReader> action = (reader) =>
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    category = new Category()
                    {
                        Id = (int)reader[columnCategoryIdDb],
                        Name = (string)reader[columnCategoryNameDb]
                    };
                }
                else
                {
                    category = new Category();
                }
            };

            ExecuteStorProc(procGetById, parameters, action);

            return category;
        }
       

        public List<Category> GetAll()
        {
            List<Category> listCategories = new List<Category>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            Action<SqlDataReader> action = (reader) =>
            {
                while (reader.Read())
                {
                    listCategories.Add(new Category()
                    {
                        Id = (int)reader[columnCategoryIdDb],
                        Name = (string)reader[columnCategoryNameDb]
                    });
                }
            };

            ExecuteStorProc(procGetAllCategories, parameters, action);
            return listCategories;
        }

        public void Delete(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argId, Id));

            ExecuteStorProc(procDelete, parameters);
        }
    }
}
