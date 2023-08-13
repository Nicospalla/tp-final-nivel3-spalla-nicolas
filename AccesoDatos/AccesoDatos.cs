using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace accesoriosSeguridad
{
    public class AccesoDatos
    {
        SqlConnection conn;
        SqlCommand comando;
        SqlDataReader lector;
        
        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public AccesoDatos() 
        {
            try
            {
                conn = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
                //conn = new SqlConnection("server = .\\SQLEXPRESS ; database = CATALOGO_WEB_DB ; integrated security = true");
                comando = new SqlCommand();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void setearProcedimiento(string sp)
        {
            try
            {
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = sp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void setearConsulta(string consulta)
        {
            try
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ejecutarLectura()
        {
            try
            {
                comando.Connection = conn;
                conn.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        public int ejecutarAccionInt()
        {
            try
            {
                comando.Connection = conn;
                conn.Open();
                return int.Parse(comando.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 
        public void ejecutarAccion()
        {
            try
            {
                comando.Connection = conn;
                conn.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setearParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        public void limpiarParametros()
        {
            comando.Parameters.Clear();
        }
        public void cerrarConn()
        {
            try
            {
                //lector.Close();
                conn.Close();
                comando.Parameters.Clear();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
