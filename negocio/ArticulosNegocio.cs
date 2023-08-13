using accesoriosSeguridad;
using dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticulosNegocio
    {
        public List<Articulos> listar(int id = 0, int IdUser = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (id != 0)
                {
                    //datos.setearProcedimiento("listarPorId");
                    datos.setearConsulta("select A.Id, Codigo, Nombre , A.Descripcion as DescripcionArt, ImagenUrl, Precio, A.IdMarca, M.Descripcion as DescripcionMar, A.IdCategoria,C.Descripcion as DescripcionCat from ARTICULOS as A, MARCAS as M, CATEGORIAS as C where IdMarca = M.Id and IdCategoria = C.Id and A.Id = @Id");
                    datos.setearParametros("@Id", id);
                }else if(IdUser != 0)
                {
                    //De esta forma podemos lista los articulos seteados como FAVORITOS por el usuario.
                    datos.setearConsulta("select A.Id, Codigo, A.Nombre , A.Descripcion as DescripcionArt, ImagenUrl, Precio,A.IdMarca, M.Descripcion as DescripcionMar,A.IdCategoria, C.Descripcion as DescripcionCat from ARTICULOS as A, MARCAS as M, CATEGORIAS as C, FAVORITOS as F, USERS as U where IdMarca = M.Id and IdCategoria = C.Id and A.Id = F.IdArticulo and U.Id = f.IdUser and U.Id = @IdUser");
                    datos.setearParametros("@IdUser",IdUser);
                }
                else
                    datos.setearConsulta("select A.Id, Codigo, Nombre , A.Descripcion as DescripcionArt, ImagenUrl, Precio, A.IdMarca, M.Descripcion as DescripcionMar, A.IdCategoria,C.Descripcion as DescripcionCat from ARTICULOS as A, MARCAS as M, CATEGORIAS as C where IdMarca = M.Id and IdCategoria = C.Id");
                    //datos.setearProcedimiento("listarArticulos");
                datos.ejecutarLectura();
                List<Articulos> lista = new List<Articulos>();
                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];
                    //if(datos.Lector["Nombre"] != DBNull.Value)
                    //    aux.Nombre = (string)datos.Lector["Nombre"];

                    aux.Nombre = (datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : null);

                    aux.Descripcion = (datos.Lector["DescripcionArt"] != DBNull.Value ? (string)datos.Lector["DescripcionArt"] : null);

                    aux.ImagenUrl = (datos.Lector["ImagenUrl"] != DBNull.Value ? (string)datos.Lector["ImagenUrl"] : null);

                    aux.Codigo = (datos.Lector["Codigo"] != DBNull.Value ? (string)datos.Lector["Codigo"] : null);

                    aux.IdMarca = new Marcas();
                    aux.IdMarca.Id = (int)datos.Lector["IdMarca"];
                    aux.IdMarca.Descripcion = (string)datos.Lector["DescripcionMar"];
                    aux.IdCategoria = new Categorias();
                    aux.IdCategoria.Id = (int)(datos.Lector["IdCategoria"]);
                    aux.IdCategoria.Descripcion = (string)datos.Lector["DescripcionCat"];
                    aux.Precio =Math.Truncate((Decimal)datos.Lector["Precio"]*100)/100;
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConn();

            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE from ARTICULOS where Id = @id");
                datos.setearParametros("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConn(); }
        }

        public void modificar (Articulos aux)
        {
            AccesoDatos datos = new AccesoDatos ();

            try
            {
                datos.setearProcedimiento("modificarArt");
                datos.setearParametros("@Id", aux.Id);
                datos.setearParametros("@Codigo", aux.Codigo);
                datos.setearParametros("@Nombre", aux.Nombre);
                datos.setearParametros("@Descripcion", aux.Descripcion);
                datos.setearParametros("@IdMarca",aux.IdMarca.Id);
                datos.setearParametros("@IdCategoria", aux.IdCategoria.Id);
                datos.setearParametros("@ImagenUrl",aux.ImagenUrl);
                datos.setearParametros("@Precio", aux.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }finally { datos.cerrarConn(); }
        }

        public int agregarArticulo(Articulos aux)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //datos.setearProcedimiento("agregarArticulo");
                datos.setearConsulta("INSERT into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) output inserted.Id values \r\n(@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@ImagenUrl,@Precio)");
                datos.setearParametros("@Codigo", aux.Codigo);
                datos.setearParametros("@Nombre", aux.Nombre);
                datos.setearParametros("@Descripcion", aux.Descripcion);
                datos.setearParametros("@IdMarca", aux.IdMarca.Id);
                datos.setearParametros("@IdCategoria", aux.IdCategoria.Id);
                datos.setearParametros("@ImagenUrl", aux.ImagenUrl);
                datos.setearParametros("@Precio", aux.Precio);
                return datos.ejecutarAccionInt();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConn(); }
            
        }
        
        public List<Articulos> listaFiltrada  (int IdCat = 0, int idMarca = 0)
        {
            List<Articulos> listaFiltrada = new List<Articulos> ();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if(IdCat > 0 && idMarca > 0)
                {
                    datos.setearConsulta("SELECT A.Id, Codigo, Nombre , A.Descripcion as DescripcionArt, ImagenUrl, Precio, A.IdMarca, M.Descripcion as DescripcionMar, A.IdCategoria,C.Descripcion as DescripcionCat from ARTICULOS as A, MARCAS as M, CATEGORIAS as C where IdMarca = M.Id and IdCategoria = C.Id and IdMarca = @idMarca and IdCategoria = @IdCategoria");
                    datos.setearParametros("@IdMarca",idMarca);            
                    datos.setearParametros("@IdCategoria", IdCat);
                }else if(idMarca > 0 && IdCat == 0)
                {
                    datos.setearConsulta("SELECT A.Id, Codigo, Nombre , A.Descripcion as DescripcionArt, ImagenUrl, Precio, A.IdMarca, M.Descripcion as DescripcionMar, A.IdCategoria,C.Descripcion as DescripcionCat from ARTICULOS as A, MARCAS as M, CATEGORIAS as C where IdMarca = M.Id and IdCategoria = C.Id and IdMarca = @idMarca ");
                    datos.setearParametros("@IdMarca", idMarca);
                }
                else
                {
                    datos.setearConsulta("SELECT A.Id, Codigo, Nombre , A.Descripcion as DescripcionArt, ImagenUrl, Precio, A.IdMarca, M.Descripcion as DescripcionMar, A.IdCategoria,C.Descripcion as DescripcionCat from ARTICULOS as A, MARCAS as M, CATEGORIAS as C where IdMarca = M.Id and IdCategoria = C.Id and IdCategoria = @IdCategoria ");
                    datos.setearParametros("@IdCategoria", IdCat);
                }
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Id = (int)datos.Lector["Id"];

                    aux.Nombre = (datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : null);

                    aux.Descripcion = (datos.Lector["DescripcionArt"] != DBNull.Value ? (string)datos.Lector["DescripcionArt"] : null);

                    aux.ImagenUrl = (datos.Lector["ImagenUrl"] != DBNull.Value ? (string)datos.Lector["ImagenUrl"] : null);

                    aux.Codigo = (datos.Lector["Codigo"] != DBNull.Value ? (string)datos.Lector["Codigo"] : null);

                    aux.IdMarca = new Marcas();
                    aux.IdMarca.Id = (int)datos.Lector["IdMarca"];
                    aux.IdMarca.Descripcion = (string)datos.Lector["DescripcionMar"];
                    aux.IdCategoria = new Categorias();
                    aux.IdCategoria.Id = (int)(datos.Lector["IdCategoria"]);
                    aux.IdCategoria.Descripcion = (string)datos.Lector["DescripcionCat"];
                    aux.Precio = Math.Truncate((Decimal)datos.Lector["Precio"] * 100) / 100;
                    listaFiltrada.Add(aux);

                }
                return listaFiltrada;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConn();
            }



        }
    }
}
