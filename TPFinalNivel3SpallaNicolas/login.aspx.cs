using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using accesoriosSeguridad;

namespace TPFinalNivel3SpallaNicolas
{
    public partial class login : System.Web.UI.Page
    {

        UsersNegocio usersNegocio = new UsersNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string pass = txtPass.Text;
            if (Session["recuPass"] != null)
            {
                if (usersNegocio.existeUser(email) && Session["recuPassEmail"].ToString() == email && pass == Session["recuPass"].ToString())
                {
                    usersNegocio.recuperaPass(email, pass);
                    funcLogin(email, pass);

                }
                else
                {
                    funcLogin(email, pass);
                }

            }
            else
            {
                funcLogin(email, pass);
            }

        }
        private void funcLogin(string email, string pass)
        {
            try
            {
                Users aux = usersNegocio.buscaUser(email, pass);
                if(aux.Id != 0 && Session["recuPass"] != null)
                {
                    Session.Add("userActivo", aux);
                    if(pass == Session["recuPass"].ToString())
                        Response.Redirect("miPerfil.aspx", false);
                    else
                    {
                        Response.Redirect("Default.aspx", false);
                    }
                }
                else if (aux.Id != 0)
                {
                    Session.Add("userActivo", aux);
                    Response.Redirect("Default.aspx", false);
                }
                else
                    lblTest.Text = "El email o la contraseña son incorrectos...";
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }

        }
        protected void recuPass_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            Random rand = new Random();
            int passProv = rand.Next(1000, 10000);
            try
            {
                EmailService emailService = new EmailService();
                if (usersNegocio.existeUser(email))
                {

                    emailService.cuerpoMail(email, passProv);
                    emailService.enviarMail();
                    Session.Add("recuPassEmail", email);
                    Session.Add("recuPass", passProv);
                    lblTxtPass.ForeColor = System.Drawing.Color.Red;
                    lblTxtPass.Text = "Ingresa la contraseña que ha sido enviada al tu email, luego, cambiala desde tu perfil.";
                    lblRecuPass.Text = "Se ha enviado un mail a su casilla de correo";
                }
                else
                {
                    lblRecuPass.Text = "Ese correo no existe";
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }


        }
    }
}