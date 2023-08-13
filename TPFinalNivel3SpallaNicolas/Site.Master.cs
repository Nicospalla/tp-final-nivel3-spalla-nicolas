using dominio;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userActivo"] != null && ((Users)Session["userActivo"]).Admin == true)
            {
                userActivo = true;
                userAdmin = true;
            }else if(Session["userActivo"] != null)
            {
                userActivo = true;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            //Session.Remove("userActivo");
            Session.Clear();
            Response.Redirect("Default.aspx", false);
        }
    }
}