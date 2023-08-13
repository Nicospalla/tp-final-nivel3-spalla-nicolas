using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3SpallaNicolas
{
    public partial class listado : System.Web.UI.Page
    {
        public bool confirmaEliminar { get; set; }
        ArticulosNegocio negocio = new ArticulosNegocio();
        private int auxiliar { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userActivo"] == null || ((Users)Session["userActivo"]).Admin == false)
            {
                string error = "Para poder ingresar a éste sector, debes tener permisos de administrador";
                Session.Add("error", error);
                Response.Redirect("error.aspx", false);
            }
            confirmaEliminar = false;
            listarGrid();
        }

        private void listarGrid()
        {
            gridView.DataSource = negocio.listar();
            gridView.DataBind();
        }
        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {

            int aux = int.Parse(gridView.SelectedDataKey.Value.ToString());
            Response.Redirect("modificar.aspx?Id=" + aux, false);

        }

        
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            auxiliar = Convert.ToInt32(gridView.DataKeys[e.RowIndex].Values["Id"]);
            confirmaEliminar = true;
            Session.Add("id", auxiliar);
        }
        private void elim(int aux)
        {
            try
            {
                negocio.eliminar(aux);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("error.aspx", false);
                throw;
            }finally { 
                confirmaEliminar = false;
                listarGrid();
            }
        }
        protected void btnElimniar_Click(object sender, EventArgs e)
        {
            try
            {
                elim(int.Parse(Session["id"].ToString()));
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("error.aspx", false);

            }
            finally
            {
                Session.Remove("id");
            }        
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            confirmaEliminar = false;
            return;
        }
    }
}