using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3SpallaNicolas
{
    public partial class error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["errorGenerico"] != null)
            {
                lblError.Text = "Se ha producido un error fuera de lo habitual, contacte con el desarrollador.";
                lblErrorDetallado.Text = Session["errorGenerico"].ToString();
            }
            else if (Session["error"] != null || Session["userActivo"] == null)
            {

                lblError.Text = Session["error"] != null ? Session["error"].ToString() : "Debes estar logueado para ingresar en este sector.";


            }

        }
    }
}