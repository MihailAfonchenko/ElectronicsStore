using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AM_ElectronicsStore.DAL
{
    public abstract class DALBase
    {
        /// <summary>
        /// Выполнение хранимой процедуры с параметрами
        /// </summary>
        /// <param name="storProcName"></param>
        /// <param name="param"></param>
        /// <param name="action"></param>
        protected void ExecuteStorProc(string storProcName, List<SqlParameter> param, Action<SqlDataReader> action = null)
        {
            if (param != null)
            {
                param.ForEach(p => p.Value = toDbNullable(p.Value));
            }

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ElectronicsStore"].ConnectionString))
            using (var command = new SqlCommand(storProcName, conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                if (param != null)
                {
                    command.Parameters.AddRange(param.ToArray());
                }
                if (action != null)
                {
                    SqlDataReader reader = command.ExecuteReader();
                    action(reader);
                }
                else
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        protected static object toDbNullable(object o)
        {
            return o ?? DBNull.Value;
        }

        protected static T fromDbNullable<T>(object o)
        {
            if (o != DBNull.Value)
            {
                return (T)o;
            }
            else
            {
                return default(T);
            }
        }

    }
}
