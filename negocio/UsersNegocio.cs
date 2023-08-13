using accesoriosSeguridad;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class UsersNegocio
    {
        AccesoDatos datos = new AccesoDatos();

        public bool existeUser (string mail)
        {
            try
            {
                datos.setearConsulta("select Id from USERS where email = @email");
                datos.setearParametros("@email", mail);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConn(); }
            return false;
        }
        public Users buscaUser(string email = "", string pass = "")
        {
            Users aux = new Users() { Email=email, Pass = pass };

            try
            {
                datos.setearConsulta("SELECT Id, email, pass, nombre, apellido, urlImagenPerfil, admin FROM USERS where email = @email and pass = @pass");
                datos.setearParametros("@email", aux.Email);
                datos.setearParametros("@pass", aux.Pass);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    aux.Id =(int)datos.Lector["Id"];
                    aux.Nombre = (datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : null) ;
                    aux.Apellido =(datos.Lector["apellido"] != DBNull.Value ?  (string)datos.Lector["apellido"] : null) ;
                    aux.UrlImagenPerfil = (datos.Lector["urlImagenPerfil"] != DBNull.Value ? (string)datos.Lector["urlImagenPerfil"] : null);
                    aux.Admin = (bool)datos.Lector["admin"];
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConn();
            }

            return aux;
        }

        public List<Users> listaUser()
        {
            List<Users> lista = new List<Users>();
            try
            {
                datos.setearConsulta("SELECT Id, email, pass, nombre, apellido, urlImagenPerfil, admin FROM USERS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Users aux = new Users();

                    aux.Id = (int)datos.Lector["Id"];

                    aux.Nombre = (datos.Lector["nombre"] != DBNull.Value ? (string)datos.Lector["nombre"] : null);

                    aux.Apellido = (datos.Lector["apellido"] != DBNull.Value ? (string)datos.Lector["apellido"] : null);

                    aux.Email = (string)datos.Lector["email"];

                    aux.UrlImagenPerfil = (datos.Lector["urlImagenPerfil"] != DBNull.Value ? (string)datos.Lector["urlImagenPerfil"] : null) ;

                    aux.Admin = (bool)datos.Lector["admin"];
                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConn(); }

            return lista;
        }

        public void changeStatusAdmin (int user, bool status)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (status == true)
                    datos.setearConsulta("UPDATE USERS set admin = 0 where Id = @Id");
                else
                    datos.setearConsulta("UPDATE USERS set admin = 1 where Id = @Id");

                datos.setearParametros("@Id", user);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConn();
            }
        }
        public void eliminarUser(int user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE from USERS where Id = @Id");
                datos.setearParametros("@Id", user);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConn();
            }
        }

        public int nuevoUser (string mail, string pass)
        {
            AccesoDatos datos = new AccesoDatos ();
            try
            {
                datos.setearConsulta("INSERT into USERS (email, pass, admin) values  (@email, @pass, 0)");
                datos.setearParametros("@email", mail);
                datos.setearParametros("@pass", pass);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConn();
            }


            return 1;
        }

        public void modifUser (Users aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE USERS set nombre = @nombre, apellido = @apellido, pass = @pass, urlImagenPerfil = @urlImagenPerfil where id = @id");
                datos.setearParametros("@nombre", aux.Nombre != "" ? aux.Nombre : (object)DBNull.Value);
                datos.setearParametros("@apellido", aux.Apellido != "" ? aux.Apellido : (object)DBNull.Value);
                datos.setearParametros("@pass",aux.Pass);
                datos.setearParametros("@urlImagenPerfil", aux.UrlImagenPerfil != "" ? aux.UrlImagenPerfil : (object) DBNull.Value);
                datos.setearParametros("@Id", aux.Id);
                datos.ejecutarAccion();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally {
                datos.cerrarConn();
            }
        }

        public void recuperaPass(string email, string pass)
        {
            try
            {
                
                datos.setearConsulta("Update USERS set pass = @pass where email = @email");
                datos.setearParametros("@pass", pass);
                datos.setearParametros("@email", email);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConn();
                
            }
        }
    }


}
