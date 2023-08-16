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
                    if(activo.UrlImagenPerfil.StartsWith("http"))
                        imagenPerfil.ImageUrl = activo.UrlImagenPerfil;
                    else if(usersNegocio.validaImg(activo))
                        imagenPerfil.ImageUrl = "~/Images/" + activo.UrlImagenPerfil != "" ? "~/Images/" + activo.UrlImagenPerfil : noFoto;
                    //txtUrlImagen.Text = activo.UrlImagenPerfil != null && activo.UrlImagenPerfil != noFoto ? activo.UrlImagenPerfil : "";
                    //imagenPerfil.ImageUrl = activo.UrlImagenPerfil != null ? activo.UrlImagenPerfil : noFoto;
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

        private bool validaPass(string pass1, string pass2)
        {
            if (!string.IsNullOrEmpty(pass1) && !string.IsNullOrEmpty(pass2) && pass1 == pass2)
                return true;
            else
                return false;
        }
        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPass.Text))
            {
                if (!validaPass(txtPass.Text, txtPass2.Text))
                {
                    lblPass.Text = "Las contraseñas no coinciden!";
                    return;
                }
                else
                    lblPass.Text = "";
            }
            else
            {
                lblPass.Text = "";
            }
            string ruta = Server.MapPath("./Images/");
            Users aux = (Users)Session["userActivo"];
            try
            {
                aux.Nombre = txtNombre.Text.ToString();
                aux.Apellido = txtApellido.Text.ToString();
                aux.Pass = validaPass(txtPass.Text,txtPass2.Text) ? txtPass.Text  : aux.Pass;
                if (inputPerfil.PostedFile.FileName != "")
                {
                    inputPerfil.PostedFile.SaveAs(ruta + "Perfil-" + aux.Id + ".jpg");
                    aux.UrlImagenPerfil = "Perfil-" + aux.Id + ".jpg";
                    
                }
                usersNegocio.modifUser(aux);

                if (usersNegocio.validaImg(aux))
                {
                    Image img = (Image)Master.FindControl("imgAvatar");
                    img.ImageUrl = "~/Images/" + aux.UrlImagenPerfil;
                    imagenPerfil.ImageUrl = "~/Images/" + aux.UrlImagenPerfil != "" ? aux.UrlImagenPerfil : "~/Images/perfilNoFoto.jpg";
                }
                else
                {
                    imagenPerfil.ImageUrl = "~/Images/perfilNoFoto.jpg";
                }

                Response.Redirect("Default.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }


        }

    }
}