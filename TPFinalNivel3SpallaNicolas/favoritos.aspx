<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="favoritos.aspx.cs" Inherits="TPFinalNivel3SpallaNicolas.favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="display: flex; flex-direction: column; min-height: 100vh;">
        <div class="row">
            <div class="col-2"></div>
            <div class="col-8">
                <div class="row row-cols-1 row-cols-md-4 g-4" style="justify-content: space-evenly;">

                    <asp:Repeater ID="repeaterArticulos" runat="server">
                        <ItemTemplate>
                            <div class="card" style="width: 18rem;">
                                <div class="card-body">
                                    <a href="modificar.aspx?Id=<%# Eval("Id") %>">
                                    <img src="<%#"./ProductsImages/"+ Eval("ImagenUrl") %>" style="max-width: -webkit-fill-available; max-height: -webkit-fill-available;" alt="Imagen de Producto" />
                                    </a>
                                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    <h6 class="card-subtitle mb-2 text-muted" style="margin-top: 1px;">U$D<%# Eval("Precio") %></h6>
                                    <p class="card-text"><%# Eval("Descripcion") %></p>
                                    <ul class="list-group list-group-flush">
                                        <li class="list-group-item">Marca: <%# Eval("IdMarca.Descripcion") %></li>
                                        <li class="list-group-item">Categoria: <%# Eval("IdCategoria.Descripcion") %></li>
                                    </ul>
                                </div>
                                <div class="card-footer" style="width: max-content;">
                                    <asp:Button ID="btnQuitarFav" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger" OnClick="btnQuitarFav_Click" Text="Quitar de Favs" />
                                    <a href='modificar.aspx?Id=<%# Eval("Id") %>&fav=1' class="btn btn-primary">Ver detalles</a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <asp:Label ID="lblVacio" Font-Size="Large" Font-Italic="true" style="text-align-last: center; margin-top:3em" runat="server" Text=""></asp:Label>
            <div class="col-2"></div>
        </div>
    </div>
</asp:Content>
