using _AM_ElectronicsStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AM_ElectronicsStore.BLL
{
    public class CategoriesBLL
    {
        private static CategoriesDAL Data = new CategoriesDAL();

        public static List<Entities.Category> GetAll()
        {
            return Data.GetAll();
        }

        public static Entities.Category GetById(int id)
        {
            return Data.GetById(id);
        }

        public static int AddOrUpdate(Entities.Category category)
        {
            return Data.AddOrUpdate(category);
        }

        public static void Delete(int id)
        {
            Data.Delete(id);
        }
    }
}
