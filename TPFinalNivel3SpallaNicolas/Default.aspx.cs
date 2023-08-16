using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using accesoriosSeguridad;
using dominio;
using negocio;

namespace TPFinalNivel3SpallaNicolas
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulos> listaArticulos { get; set; }
        ArticulosNegocio negocio = new ArticulosNegocio();
        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        MarcasNegocio marcasNegocio = new MarcasNegocio();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["listaFiltrada"] == null)
            {
                listaArticulos = negocio.listar();
                btnRestaurar.Visible = false;
            }
            else
            {
                listaArticulos = (List<Articulos>)Session["listaFiltrada"];
                btnRestaurar.Visible=true;
            }

            if (!IsPostBack)
            {
                ddlCat.Items.Clear();
                ddlCat.DataSource = categoriaNegocio.listarCat();
                ddlCat.DataValueField = "Id";
                ddlCat.DataTextField = "Descripcion";
                ddlCat.DataBind();
                ddlCat.Items.Insert(0, new ListItem("Seleccione una Categoria", "0"));

                ddlMarca.DataSource = marcasNegocio.listaMar();
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Seleccione una Marca"));
                
            }
            //repeaterCard.DataSource = negocio.listar();
            //repeaterCard.DataBind();
        }

        protected void btnDetalles_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
            Response.Redirect("modificar.aspx?Id=" + valor, false);

        }

        protected void btnAceptarFiltro_Click(object sender, EventArgs e)
        {
            bool done = false;
            List<Articulos> listaFiltrada = new List<Articulos>() ;
            if (ddlCat.SelectedIndex > 0 && ddlMarca.SelectedIndex > 0)
            {
                int idCat = int.Parse(ddlCat.SelectedValue);
                int idMarca = int.Parse(ddlMarca.SelectedValue);
                listaFiltrada = negocio.listaFiltrada(idCat, idMarca);
                done = true;
            }
            else if (ddlCat.SelectedIndex > 0)
            {
                int idCat = int.Parse(ddlCat.SelectedValue);
                listaFiltrada = negocio.listaFiltrada(idCat,0);
                done = true;
            }
            else if (ddlMarca.SelectedIndex > 0)
            {
                int idMarca = int.Parse(ddlMarca.SelectedValue);
                listaFiltrada = negocio.listaFiltrada(0,idMarca);
                done = true;
            }
            if (done)
            {
                Session.Add("listaFiltrada", listaFiltrada);
                btnRestaurar.Visible = true;
                Response.Redirect("Default.aspx", false);
            }
            else
            {
                Response.Redirect("Default.aspx", false);
                btnRestaurar.Visible = false;
                Session.Remove("listaFiltrada");
            }
        }

        protected void btnRestaurar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
            btnRestaurar.Visible = false;
            Session.Remove("listaFiltrada");
        }
    }
}