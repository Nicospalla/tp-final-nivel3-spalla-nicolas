<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPFinalNivel3SpallaNicolas.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style>
        .valido {
            border-color: #198754;
        }
    </style>

    <div class="row" style="height: 100vh;">
        <div class="col-2 d-flex flex-column" style="border-right: 2px solid #ced4da; margin-top: -4px;">
            <div class="form-group" style="margin-top: 2em;">
                <label for="txtFiltroRap">Filtrar productos por:</label>
            </div>
            <div class="form-group" style="margin-top: 2em;">
                <label for="txtFiltroRap">Categoría:</label>
                <asp:DropDownList ID="ddlCat" CssClass="form-control valido" runat="server"></asp:DropDownList>
            </div>
            <div class="form-group" style="margin-top: 2em;">
                <label for="txtFiltroRap">Marca:</label>
                <asp:DropDownList ID="ddlMarca" CssClass="valido form-control valido" runat="server"></asp:DropDownList>
            </div>
            <div class="form-group" style="margin-top: 2em;">
                <asp:Button ID="btnAceptarFiltro" runat="server" OnClick="btnAceptarFiltro_Click" CssClass="btn btn-success" Text="Filtrar" Style="width: -webkit-fill-available;" />
            </div>
            <div class="form-group" style="margin-top: 2em;">
                <asp:Button ID="btnRestaurar" runat="server" Visible="true" OnClick="btnRestaurar_Click" CssClass="btn btn-warning" Text="Restaurar" Style="width: -webkit-fill-available;" />
            </div>
        </div>
        <div class="col">
            <div class="row row-cols-xs-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4">
                <%foreach (var aux in listaArticulos)
                    {%>
                <div class="card">
                    <img class="card-img-top" style="min-height: 15em; max-height: 15em;"
                        src="<%:aux.ImagenUrl != "" ? aux.ImagenUrl : "./images/noFoto2.jpg" %>" alt="Imagen Articulo">
                    <div class="card-body" style="display: flex; flex-direction: column; justify-content: space-between;">
                        <h5 class="card-title"><%:aux.Nombre %></h5>
                        <h6 class="card-title">Precio: <%:aux.Precio %></h6>
                        <p class="card-text"><%:aux.Descripcion %></p>
                        <hr />
                        <p class="card-text">Categoria: <%:aux.IdCategoria %></p>
                        <p class="card-text">Marca: <%:aux.IdMarca %></p>
                        <hr />
                    </div>
                    <div class="card-footer">
                        <a href="modificar.aspx?Id=+ <%:aux.Id %>" class="btn btn-outline-primary btnCard" style="width: -webkit-fill-available;">Ver detalles</a>
                    </div>
                </div>
                <% } %>
            </div>
        </div>
        <div class="col-1"></div>
    </div>



</asp:Content>
