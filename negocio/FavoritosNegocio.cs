using accesoriosSeguridad;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class FavoritosNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        public void agregarFav(int idUser, int idArticulo)
        {
            try
            {
                datos.setearConsulta("INSERT into FAVORITOS (IdUser, IdArticulo) VALUES (@IdUser, @IdArticulo)");
                datos.setearParametros("@IdUser", idUser);
                datos.setearParametros("@IdArticulo",idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void quitarFav (int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE from FAVORITOS where IdUser = @IdUser AND IdArticulo = @IdArticulo");
                datos.setearParametros("@IdUser", idUser);
                datos.setearParametros("@IdArticulo", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool existeFav (int IdUser, int IdArticulo)
        {
            AccesoDatos datos = new AccesoDatos ();
            try
            {
                datos.setearConsulta("select Id from FAVORITOS where IdUser = @IdUser AND IdArticulo = @IdArticulo");
                datos.setearParametros("@IdUser", IdUser);
                datos.setearParametros("@IdArticulo", IdArticulo);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Favoritos aux = new Favoritos ();
                    aux.Id = (int)datos.Lector["Id"];
                    if (aux.Id > 0)
                        return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
    }
}
