using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Datos.Utils
{
    public class DataHelper
    {
        private static DataHelper _instancia;
        private SqlConnection _connection;

        
        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.StringCnn);
        }

        public static DataHelper GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DataHelper();
            } 

            return _instancia;
        }

        public DataTable ExecuteSPQuery(string sp, List<ParameterSQL>? parametros)
        {
            DataTable dt = new DataTable();

            try
            {
                _connection.Open();
                var comando = new SqlCommand(sp, _connection);
                comando.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var param in parametros)
                        comando.Parameters.AddWithValue(param.Name, param.Value);
                };

                dt.Load(comando.ExecuteReader());
                _connection.Close();
            }
            catch (SqlException) { dt = null; }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return dt;
        }

        public int ExecuteSPDML(string sp, List<ParameterSQL>? parameters)
        {
            int rows;
            try
            {
                _connection.Open();
                var comando = new SqlCommand(sp, _connection);
                comando.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                        comando.Parameters.AddWithValue(param.Name, param.Value);
                };

                rows = comando.ExecuteNonQuery();
                _connection.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return rows;
        }

        internal SqlConnection? GetConnection()
        {
            return _connection;
        }
    }
}
