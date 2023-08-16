using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3SpallaNicolas
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public bool userActivo { get; set; }
        public bool userAdmin { get; set; }
        public string auxPic { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersNegocio usersNegocio = new UsersNegocio();
            auxPic = "https://img.freepik.com/iconos-gratis/hombre_318-677829.jpg";

            if (Session["userActivo"] != null && ((Users)Session["userActivo"]).Admin == true)
            {
                userActivo = true;
                userAdmin = true;
            }else if(Session["userActivo"] != null)
            {
                userActivo = true;
            }
            if(Session["userActivo"] != null)
                imgAvatar.ImageUrl = usersNegocio.validaImg((Users)Session["userActivo"])  ? "~/Images/" + ((Users)Session["userActivo"]).UrlImagenPerfil : auxPic;
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            //Session.Remove("userActivo");
            Session.Clear();
            Response.Redirect("Default.aspx", false);
        }
    }
}