using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using dominio;
using negocio;

namespace TPFinalNivel3SpallaNicolas
{

    public partial class favoritos : System.Web.UI.Page
    {
        ArticulosNegocio negocio = new ArticulosNegocio();
        FavoritosNegocio FavoritosNegocio = new FavoritosNegocio();
        public List<Articulos> listaArticulos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userActivo"] == null)
            {
                string error = "Para poder ingresar a éste sector, debe estar logueado.";
                Session.Add("error", error);
                Response.Redirect("error.aspx", true);
               
            }
            else
            {
                try
                {
                    repeaterArticulos.DataSource = negocio.listar(0, (((Users)Session["userActivo"]).Id));
                    repeaterArticulos.DataBind();
                    //listaArticulos = negocio.listar(0, (((Users)Session["userActivo"]).Id));
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("error.aspx");
                }
            }
        }

        protected void btnQuitarFav_Click(object sender, EventArgs e)
        {
            int auxId = Convert.ToInt32(((Button)sender).CommandArgument);
            try
            {
                FavoritosNegocio.quitarFav(((Users)Session["userActivo"]).Id, auxId);
                Response.Redirect("favoritos.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx");
            }
            
        }
    }
}