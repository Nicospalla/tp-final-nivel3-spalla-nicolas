using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3SpallaNicolas
{
    public partial class registro : System.Web.UI.Page
    {
        UsersNegocio usersNegocio = new UsersNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool validaEmail(string email)
        {
            string regex = "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
            Regex regexAux = new Regex(regex);
            if(regexAux.IsMatch(email))
                return true;
            else
                return false;
        }
        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            if (usersNegocio.existeUser(txtEmail.Text))
            {
                lblTxtEmail.Text = "Este email ya se encuentra registrado.";
                lblTxtEmail.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if(!string.IsNullOrEmpty(txtEmail.Text) && validaEmail(txtEmail.Text) && !string.IsNullOrEmpty(txtPass1.Text) && txtPass1.Text == txtPass2.Text)
            {
                try
                {
                    int userId =  usersNegocio.nuevoUser(txtEmail.Text, txtPass1.Text);
                    Users aux = new Users() { Id = userId, Email = txtEmail.Text, Pass = txtPass1.Text };
                    Session.Add("userActivo", aux);
                    Response.Redirect("miPerfil.aspx" , false);
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("error.aspx", false);
                }
            }
            else
            {
                
                validateCampoEmail();
                validateCampoPass();
                return;
            }
        }
        private void validateCampoEmail()
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || !validaEmail(txtEmail.Text))
            {
                txtEmail.CssClass = "form-control is-invalid";
                lblTxtEmail.Text = "El email ingresado es incorrecto";
            }
            else
            {
                txtEmail.CssClass = "form-control is-valid";
                lblTxtEmail.Text = "";
            }
        }
        private void validateCampoPass() { 
            if(string.IsNullOrEmpty(txtPass1.Text) || txtPass1.Text != txtPass2.Text)
            {
                txtPass1.CssClass = "form-control is-invalid";
                txtPass2.CssClass = "form-control is-invalid";
                lblPass1.Text = "Las contraseñas deben coincidir";
            }
            else
            {
                txtPass1.CssClass = "form-control is-valid";
                txtPass2.CssClass = "form-control is-valid";
                lblPass1.Text = "";
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            validaEmail(txtEmail.Text);
            validateCampoEmail();
        }
    }
}