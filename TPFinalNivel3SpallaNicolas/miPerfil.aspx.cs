using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace TPFinalNivel3SpallaNicolas
{
    public partial class miPerfil : System.Web.UI.Page
    {
        UsersNegocio usersNegocio = new UsersNegocio();
        public bool confirmaEliminar { get; set; }
        public bool admin { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string noFoto = "./images/perfilNoFoto.jpg";
            if (Session["userActivo"] == null)
            {
                string error = "Para poder ingresar a éste sector, debe estar logueado.";
                Session.Add("error", error);
                Response.Redirect("error.aspx", false);
            }
            else
            {
                admin = ((Users)Session["userActivo"]).Admin == true ? true : false;
                try
                {
                    gridUsers.DataSource = usersNegocio.listaUser();
                    gridUsers.DataBind();

                }
                catch (Exception ex)
                {

                    Session.Add("error", ex.ToString());
                    Response.Redirect("error.aspx", false);
                }
                if (!IsPostBack)
                {
                    Users activo = (Users)Session["userActivo"];
                    txtNombre.Text = activo.Nombre != null ? activo.Nombre : "";
                    txtApellido.Text = activo.Apellido != null ? activo.Apellido : "";
                    txtUrlImagen.Text = activo.UrlImagenPerfil != null && activo.UrlImagenPerfil != noFoto ? activo.UrlImagenPerfil : "";
                    imagenPerfil.ImageUrl = activo.UrlImagenPerfil != null ? activo.UrlImagenPerfil : noFoto;
                    txtEmail.Text = activo.Email;

                }
                
            }
        }

        protected void gridUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int aux = int.Parse(gridUsers.SelectedRow.Cells[0].Text);
                bool status = bool.Parse(gridUsers.SelectedDataKey.Value.ToString());
                usersNegocio.changeStatusAdmin(aux, status);
                Response.Redirect("miPerfil.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }
        }

        protected void gridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUsers.PageIndex = e.NewPageIndex;
            gridUsers.DataBind();
        }

        protected void gridUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            confirmaEliminar = true;
            int aux = int.Parse(gridUsers.Rows[int.Parse(e.RowIndex.ToString())].Cells[0].Text);
            Session.Add("idTemp", aux);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            return;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                usersNegocio.eliminarUser(int.Parse(Session["idTemp"].ToString()));
                Session.Remove("idTemp");
                Response.Redirect("miPerfil.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }
        }


        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            Users aux = (Users)Session["userActivo"];
            try
            {
                aux.Nombre = txtNombre.Text.ToString();
                aux.Apellido = txtApellido.Text.ToString();
                aux.Pass = (!string.IsNullOrEmpty(txtPass.Text) && txtPass.Text == txtPass2.Text ? txtPass.Text.ToString() : aux.Pass);
                aux.UrlImagenPerfil = txtUrlImagen.Text;
                usersNegocio.modifUser(aux);
                Response.Redirect("Default.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }


        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            if (txtUrlImagen.Text != "")
                imagenPerfil.ImageUrl = txtUrlImagen.Text;
            else
                imagenPerfil.ImageUrl = "./images/perfilNoFoto.jpg";
            
            //ViewState["PasswordValue"] = txtPass.Text;
        }
    }
}