<%@ Page Title="Detalle de producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="modificar.aspx.cs" Inherits="TPFinalNivel3SpallaNicolas.modificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="row" style="margin-top: 3%;">
    
        <div class="col-2"></div>
        <div class="col">
            <%if (esFav == true && txtId.Text != "")
                {%>
            <asp:Button ID="btnQuitarFavoritos" OnClick="btnQuitarFavoritos_Click" CssClass="btn btn-outline-danger" runat="server" Text="Quitar de Favoritos" />
           <% }%>
                <%else if(esFav == false && txtId.Text != "") {%>
                   <asp:Button ID="btnAgregarFav" OnClick="btnAgregarFav_Click" CssClass="btn btn-outline-success" runat="server" Text="Agregar a Favoritos" />
                <%} %>
            <%if (admin == true)
                {  %>
            <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" CausesValidation="false" CssClass="btn btn-outline-info"   Text="Limpiar planilla" />
            <%} %>

            <%if (admin == true && txtId.Text != "")
                {  %>
            <asp:Button ID="btnModificar" CssClass="btn btn-outline-warning"  OnClick="btnModificar_Click" runat="server" Text="Modificar Articulo" />
            <asp:Button ID="btnEliminar" OnClick="btnEliminar_Click" CausesValidation="false" runat="server" CssClass="btn btn-outline-danger" Text="Eliminar articulo" />
            <asp:Button ID="btnAgregarFalse" Visible="false"  CssClass="btn btn-outline-success" runat="server" Text="Agregar articulo" />
            <%}
                else if (admin == true && txtId.Text == "")
                {  %>
                    
                        <asp:Button ID="btnModificarFalse" CssClass="btn btn-outline-warning" Visible="false" runat="server" Text="Modificar Articulo" />
                        <asp:Button ID="btnEliminarFalse" Visible="false" runat="server" CssClass="btn btn-outline-danger" Text="Eliminar articulo" />
                        <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" CausesValidation="true"  CssClass="btn btn-outline-success" runat="server" Text="Agregar articulo" />
            <%} %>
        </div>
    </div>
    <% if (confirmaEliminar == true)
        {
    %><div class="row" style="margin-top: 1%;">
        <div class="col-2"></div>
        <div class="col-8">
            <asp:Button ID="btnConfirmaEliminar" CssClass="btn btn-danger" OnClick="btnConfirmaEliminar_Click" Style="width: inherit" runat="server" Text="Confirma que desea eliminar?" />
            <asp:Button ID="btnCancelarElim" CssClass="btn btn-success" runat="server" Text="Cancelar" PostBackUrl="~/modificar.aspx" />
        </div>
        <div class="col-2"></div>
    </div>
    <% }%>
    <% if (confirmaModificar == true)
        {
    %><div class="row" style="margin-top: 1%;">
        <div class="col-2"></div>
        <div class="col-8">
            <asp:Button ID="btnConfirmaModificar" CssClass="btn btn-warning" OnClick="btnConfirmaModificar_Click" Style="width: inherit" runat="server" Text="Confirma que desea modificar?" />
            <asp:Button ID="btnCancelarModif" CssClass="btn btn-success" runat="server" Text="Cancelar" PostBackUrl="~/modificar.aspx" />
        </div>
        <div class="col-2"></div>
    </div>
    <% }%>
    <% if (confirmaAgregar == true)
        {
    %><div class="row" style="margin-top: 1%;">
        <div class="col-1"></div>
        <div class="col-8">
            <asp:Button ID="btnConfirmaAgregar" CssClass="btn btn-success" OnClick="btnConfirmaAgregar_Click" Style="width: inherit" runat="server" Text="Confirma que desea agregar el articulo?" />
            <asp:Button ID="btnCancelarAgregar" CssClass="btn btn-danger" runat="server" Text="Cancelar" PostBackUrl="~/modificar.aspx" />
        </div>
        <div class="col-2"></div>
    </div>
    <% }%>



    <div class="row" style="margin-top: 1%;">
        <div class="col-2"></div>
        <div class="col-3">
            <%if (admin == true)
                {  %>
            <div class="mb-3">
                <label for="txtId" class="form-label">ID de Articulo en BBDD</label>
                <asp:TextBox ID="txtId" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <%} %>
            <div class="mb-3">
                <label for="txtCodigo" class="form-label">Codigo de Articulo</label>
                <asp:TextBox ID="txtCodigo" OnTextChanged="Custom_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Label ID="lblCodigoError" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripcion</label>
                <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="ddlCategoria" class="form-label">Categoria</label>
                <asp:DropDownList ID="ddlCategoria" CssClass="form-control "  OnSelectedIndexChanged="Custom_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                <asp:CustomValidator ID="CustomValidator1" OnServerValidate="CustomValidator_ServerValidate"
                    runat="server" ControlToValidate="ddlCategoria" Display="Dynamic" ErrorMessage=""
                    ForeColor="Red"></asp:CustomValidator>
                </div>
        </div>
    <div class="col-3">
        <div class="mb-3" >
            <label for="ddlMarca" class="form-label">Marca</label>
            <asp:DropDownList ID="ddlMarca" class="form-control"  OnSelectedIndexChanged="Custom_SelectedIndexChanged" AutoPostBack="true"  runat="server"></asp:DropDownList> 
            <asp:Label ID="lblddlMarcaError" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:CustomValidator ID="ddlMarcaValidator" runat="server" Display="None"
                ControlToValidate="ddlMarca" ForeColor="Red"></asp:CustomValidator>
        </div>

        <div class="mb-3">
            <label for="txtPrecio" class="form-label">Precio</label>
            <asp:TextBox ID="txtPrecio" CssClass="form-control"  OnTextChanged="Custom_SelectedIndexChanged" AutoPostBack="true"  runat="server"></asp:TextBox>
            <asp:Label ID="LabellblTxtPrecio" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        <div class="mb-3">
            <label for="txtUrlImagen" class="form-label">Url Imagen</label>
            <input id="inputImgPerfil" type="file" runat="server" class="form-control"/>
<%--            <asp:TextBox ID="txtUrlImagen" OnTextChanged="txtUrlImagen_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>--%>
        </div>
        <div class="mb-3">
            <asp:Image ID="imagenPerfil" Style="max-width: 300px; max-height: 300px;"
                ImageUrl="./images/noFoto2.jpg" runat="server" />
        </div>

    </div>
    </div>
</asp:Content>
