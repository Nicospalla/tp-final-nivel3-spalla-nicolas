using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace TPFinalNivel3SpallaNicolas
{
    public partial class modificar : System.Web.UI.Page
    {
        CategoriaNegocio catNegocio = new CategoriaNegocio();
        MarcasNegocio marNegocio = new MarcasNegocio();
        ArticulosNegocio negocio = new ArticulosNegocio();
        FavoritosNegocio FavoritosNegocio = new FavoritosNegocio();
        public bool confirmaEliminar { get; set; }
        public bool confirmaModificar { get; set; }
        public bool confirmaAgregar { get; set; }
        public bool esFav { get; set; }

        public bool admin { get; set; }
        private string ruta { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ruta = Server.MapPath("./ProductsImages/");
            string rutaTemp = Server.MapPath("./TempFiles/");
            if (Session["userActivo"] == null)
            {
                string error = "Para poder ingresar a éste sector, debe estar logueado.";
                Session.Add("error", error);
                Response.Redirect("error.aspx", true);
            }

            List<Articulos> listaAux = (Request.QueryString["Id"] != null ? negocio.listar(int.Parse(Request.QueryString["Id"])) : null);
            confirmaEliminar = false;
            confirmaModificar = false;
            confirmaModificar = false;
            txtId.Enabled = false;
            admin = Session["userActivo"] != null && ((Users)Session["userActivo"]).Admin == true ? true : false;

            if (inputImgPerfil.PostedFile != null)
            {
                if (inputImgPerfil.PostedFile.FileName != "")
                {
                    string rutaCompleta = inputImgPerfil.PostedFile != null ? "~/ProductsImages/" + inputImgPerfil.PostedFile.FileName : null;
                    string fileName = Path.GetFileName(inputImgPerfil.PostedFile.FileName);
                    string filePath = Server.MapPath("~/TempFiles/" + fileName);

                    System.IO.DirectoryInfo di = new DirectoryInfo(rutaTemp);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }

                    inputImgPerfil.PostedFile.SaveAs(filePath);
                    Session.Remove("filePath");
                    Session.Remove("fileName");
                    Session.Add("filePath", filePath);
                    Session.Add("fileName", fileName);

                }
            }

            if (Session["filePath"] != null)
                imagenPerfil.ImageUrl = "~/TempFiles/" + Session["fileName"].ToString();

            if (!IsPostBack)
            {
                ddlCategoria.Items.Clear();
                ddlCategoria.DataSource = catNegocio.listarCat();
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Seleccione una Categoría", "0"));
                ddlCategoria.Items[0].Value = "0";
                ddlCategoria.Items[0].Selected = true;
                ddlCategoria.SelectedIndex = 0;

                ddlMarca.DataSource = marNegocio.listaMar();
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Seleccione una Marca"));

                if (listaAux != null)
                {

                    Articulos aux = listaAux[0];
                    Session.Add("Articulo", aux);
                    txtId.Text = aux.Id.ToString();
                    txtCodigo.Text = (aux.Codigo != null ? aux.Codigo.ToString() : null);
                    txtPrecio.Text = aux.Precio.ToString();

                    txtDescripcion.Text = (aux.Descripcion != null ? aux.Descripcion.ToString() : null);

                    txtNombre.Text = (aux.Nombre != null ? aux.Nombre.ToString() : null);
                    ddlCategoria.SelectedValue = aux.IdCategoria.Id.ToString();
                    ddlMarca.SelectedValue = aux.IdMarca.Id.ToString();
                    imagenPerfil.ImageUrl = aux.ImagenUrl != null ? "~/ProductsImages/" + aux.ImagenUrl.ToString() : "~/Images/NoFoto.jpg";
                    if (admin == false)
                    {
                        txtNombre.ReadOnly = true;
                        txtDescripcion.ReadOnly = true;
                        txtCodigo.ReadOnly = true;
                        txtPrecio.ReadOnly = true;
                    }
                }
            }
            if (Session["userActivo"] != null && txtId.Text != "")
                esFav = FavoritosNegocio.existeFav(((Users)Session["userActivo"]).Id, int.Parse(txtId.Text));

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            confirmaModificar = true;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            confirmaEliminar = true;
            return;
        }


        protected void btnConfirmaModificar_Click(object sender, EventArgs e)
        {
            if (!PageValid())
                return;

            Articulos aux = (Articulos)Session["Articulo"];
            try
            {
                aux.Id = int.Parse(txtId.Text);
                aux.Nombre = txtNombre.Text;
                aux.Descripcion = txtDescripcion.Text;

                aux.Precio = Math.Truncate((Decimal.Parse(txtPrecio.Text.Replace(".", ","))) * 100) / 100;
                aux.IdMarca = new Marcas();
                aux.IdMarca.Id = int.Parse(ddlMarca.SelectedItem.Value);
                aux.IdCategoria = new Categorias();
                aux.IdCategoria.Id = int.Parse(ddlCategoria.SelectedItem.Value);

                if (Session["filePath"] != null)
                {
                    System.IO.File.Copy(Session["filePath"].ToString(), ruta + "Producto-" + aux.Id + ".jpg", true);
                    System.IO.File.Delete(Session["filePath"].ToString());
                    aux.ImagenUrl = "Producto-" + aux.Id + ".jpg";
                    Session.Remove("filePath");
                }
                aux.Codigo = txtCodigo.Text;
                negocio.modificar(aux);
                
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }
            finally
            {
                Response.Redirect("modificar.aspx?Id= " + aux.Id, false);
            }
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                negocio.eliminar(int.Parse(txtId.Text));
                Response.Redirect("listado.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }

        }

        private bool PageValid()
        {

            if (!Page.IsValid)
            {

                return false;
            }
            return true;

        }

        protected void btnConfirmaAgregar_Click(object sender, EventArgs e)
        {
            ;
            if (!PageValid())
                return;
            try
            {

                Articulos aux = new Articulos();
                aux.Nombre = txtNombre.Text;
                aux.Descripcion = txtDescripcion.Text;
                aux.Precio = Math.Truncate((Decimal.Parse(txtPrecio.Text.Replace(".", ","))) * 100) / 100;
                aux.IdMarca = new Marcas();
                aux.IdMarca.Id = int.Parse(ddlMarca.SelectedItem.Value);
                aux.IdCategoria = new Categorias();
                aux.IdCategoria.Id = int.Parse(ddlCategoria.SelectedItem.Value);
                aux.Codigo = txtCodigo.Text;
                aux.ImagenUrl = null;
                int id = negocio.agregarArticulo(aux);
                if (Session["filePath"] != null)
                {
                    System.IO.File.Move(Session["filePath"].ToString(), ruta + "Producto-" + id + ".jpg");
                    negocio.actualizaImg(id);
                    //aux.ImagenUrl = "Producto-" + id + ".jpg";
                    Session.Remove("filePath");
                }
                Response.Redirect("listado.aspx", false);
                //negocio.actualizaImg(id);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }
 
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!PageValid())
                return;
            else
            {
                txtId.Text = null;
                confirmaAgregar = true;
            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

            txtCodigo.Text = null;
            txtDescripcion.Text = null;
            txtPrecio.Text = null;
            txtId.Text = null;
            txtNombre.Text = null;
            ddlCategoria.SelectedIndex = 0;
            ddlMarca.SelectedIndex = 0;
            Response.Redirect("modificar.aspx");
        }

        protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (confirmaEliminar == true)
                return;

            args.IsValid = true;
            if (ddlCategoria.SelectedIndex == 0)
            {
                ddlCategoria.CssClass = "form-control is-invalid";
                CustomValidator1.ErrorMessage = "Debe seleccionar una Categoria";
                args.IsValid = false;
            }
            if (ddlMarca.SelectedIndex == 0)
            {
                ddlMarca.CssClass = "form-control is-invalid";
                lblddlMarcaError.Text = "Debe seleccionar una Marca";
                args.IsValid = false;
            }
            if ((!validaPrecio(txtPrecio.Text)) || string.IsNullOrEmpty(txtPrecio.Text))
            {
                txtPrecio.CssClass = "form-control is-invalid";
                LabellblTxtPrecio.Text = "Debe ingresar un precio con formato 123.45 o 123,45";
                args.IsValid = false;
            }
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                txtCodigo.CssClass = "form-control is-invalid";
                lblCodigoError.Text = "Debe incluir un Codigo de Articulo";
                lblCodigoError.ForeColor = System.Drawing.Color.Red;
                args.IsValid = false;
            }


        }

        private bool validaPrecio(string precio)
        {
            string regexAux = @"^\d+(\.|,)?\d{0,2}$";
            Regex aux = new Regex(regexAux);

            if (aux.IsMatch(precio))
            {
                return true;
            }
            else
                return false;
        }
        protected void Custom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoria.SelectedIndex == 0)
            {
                ddlCategoria.CssClass = "form-control is-invalid";
                CustomValidator1.ErrorMessage = "Debe seleccionar una Categoria";
            }
            else
            {
                ddlCategoria.CssClass = "form-control is-valid";
                CustomValidator1.ErrorMessage = "";
            }

            if (ddlMarca.SelectedIndex == 0)
            {
                ddlMarca.CssClass = "form-control is-invalid";
                lblddlMarcaError.Text = "Debe seleccionar una Marca";
            }
            else
            {
                ddlMarca.CssClass = "form-control is-valid";
                lblddlMarcaError.Text = "";
            }
            if (!validaPrecio(txtPrecio.Text) || string.IsNullOrEmpty(txtPrecio.Text))
            {
                txtPrecio.CssClass = "form-control is-invalid";
                LabellblTxtPrecio.Text = "Debe ingresar un precio con formato 123.45 o 123,45";
            }
            else
            {
                txtPrecio.CssClass = "form-control is-valid";
                LabellblTxtPrecio.Text = "";
            }
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                txtCodigo.CssClass = "form-control is-valid";
                lblCodigoError.Text = "";
            }
            else
            {
                txtCodigo.CssClass = "form-control is-invalid";
                lblCodigoError.Text = "Debe incluir un Codigo de Articulo";
            }
        }

        protected void btnAgregarFav_Click(object sender, EventArgs e)
        {
            if (esFav == false)
            {
                Users aux = (Users)Session["userActivo"];
                try
                {
                    FavoritosNegocio.agregarFav(aux.Id, int.Parse(txtId.Text));
                    Response.Redirect("modificar.aspx?Id=" + int.Parse(txtId.Text), false);
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("error.aspx", false);
                }
            }
            else
                return;
        }

        protected void btnQuitarFavoritos_Click(object sender, EventArgs e)
        {
            try
            {
                FavoritosNegocio.quitarFav(((Users)Session["userActivo"]).Id, int.Parse(txtId.Text));
                Response.Redirect("modificar.aspx?Id=" + int.Parse(txtId.Text), false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }
        }
    }
}
