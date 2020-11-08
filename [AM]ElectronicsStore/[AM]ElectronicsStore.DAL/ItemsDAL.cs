using _AM_ElectronicsStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AM_ElectronicsStore.DAL
{
    public class ItemsDAL : DALBase
    {
        //хранимые процедуры реализованы не все на данный момент из ниже объявленных (в разработке)
        //товары на данный момент добавлять в базу руками

        private const string procAdd = "addItem";
        private const string procUpdate = "updateItem";
        private const string procGetById = "getItemById";
        private const string procGetAll = "getItems";
        private const string procDelete = "delItem";

        private const string columnItemId = "Id";
        private const string columnCategoryId = "CategoryId";
        private const string columnName = "Name";
        private const string columnDescription = "Description";
        private const string columnPrice = "Price";
        private const string columnCount = "Count";
        private const string columnImagePath = "ImagePath";

        private const string argItemId = "@Id";
        private const string argName = "@Name";
        private const string argImagePath = "@ImagePath";
        private const string argCategoryId = "@CategoryId";
        private const string argDescription = "@Description";
        private const string argPrice = "@Price";
        private const string argCount = "@Count";

        public int AddOrUpdate(Item item)
        {
            var Id = item.Id;
            var isExist = false;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argItemId, item.Id));
            ExecuteStorProc(procGetById, parameters, (reader) => isExist = reader.HasRows);

            parameters = new List<SqlParameter>();
            if (item.Category.Id != 0)
                parameters.Add(new SqlParameter(argCategoryId, item.Category.Id));
            parameters.Add(new SqlParameter(argName, item.Name));
            parameters.Add(new SqlParameter(argDescription, item.Description));
            parameters.Add(new SqlParameter(argPrice, item.Price));
            parameters.Add(new SqlParameter(argCount, item.Count));
            parameters.Add(new SqlParameter(argImagePath, item.ImagePath));

            Action<SqlDataReader> action = (reader) =>
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    Id = fromDbNullable<int>(reader[0]);
                }
            };

            if (!isExist)
            {
                ExecuteStorProc(procAdd, parameters, action);
            }
            else
            {
                parameters.Add(new SqlParameter(argItemId, item.Id));
                ExecuteStorProc(procUpdate, parameters);
            }

            return Id;
        }

        public Item GetById(int id)
        {
            Item item = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argItemId, id));

            Action<SqlDataReader> action = (reader) =>
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    item = new Item()
                    {
                        Id = (int)reader[columnItemId],
                        Name = (string)reader[columnName],
                        Description = (string)reader[columnDescription],
                        Price = (decimal)reader[columnPrice],
                        Count = (int)reader[columnCount],
                        ImagePath = fromDbNullable<string>(reader[columnImagePath])
                    };
                    item.Category = new Category();
                    item.Category.Id = (int)reader[columnCategoryId];
                }
            };

            ExecuteStorProc(procGetById, parameters, action);

            if(item != null && item.Category.Id != 0)
            {
                item.Category = new CategoriesDAL().GetById(item.Category.Id);
            }

            return item;
        }

        public List<Item> GetAll(int? categoryId, string itemName)
        {
            List<Item> list = new List<Item>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argCategoryId, categoryId));
            parameters.Add(new SqlParameter(argName, itemName));

            Action<SqlDataReader> action = (reader) =>
            {
                while(reader.Read())
                {
                    Item item = new Item()
                    {
                        Id = (int)reader[columnItemId],
                        Name = (string)reader[columnName],
                        Description = (string)reader[columnDescription],
                        Count = (int)reader[columnCount],
                        Price = (decimal)reader[columnPrice],
                        ImagePath = (string)reader[columnImagePath],
                    };
                    item.Category = new Category();
                    item.Category.Id = (int)reader[columnCategoryId];
                    list.Add(item);
                }
            };

            ExecuteStorProc(procGetAll, parameters, action);

            return list;
        }

        public void Delete(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(argItemId, id));
            ExecuteStorProc(procDelete, parameters);
        }
    }
}
