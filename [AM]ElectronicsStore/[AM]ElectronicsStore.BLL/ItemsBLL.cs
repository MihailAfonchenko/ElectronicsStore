using _AM_ElectronicsStore.DAL;
using _AM_ElectronicsStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AM_ElectronicsStore.BLL
{
    public static class ItemsBLL
    {
        private static ItemsDAL Data = new ItemsDAL();

        public static int AddOrUpdate(Item item)
        {
            return Data.AddOrUpdate(item);
        }

        public static List<Item> GetAll(int? categoryId, string itemName)
        {
            return Data.GetAll(categoryId, itemName);
        }

        public static Item GetById(int id)
        {
            return Data.GetById(id);
        }

        public static void Delete(int id)
        {
            Data.Delete(id);
        }
    }
}
