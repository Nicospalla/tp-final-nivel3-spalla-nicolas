<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="listado.aspx.cs" Inherits="TPFinalNivel3SpallaNicolas.listado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btnW {
            width: inherit;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container text-center" style="margin-top: 3%;">
        <asp:GridView ID="gridView" CssClass="table table-hover" runat="server" OnRowDeleting="gridView_RowDeleting" OnSelectedIndexChanged="gridView_SelectedIndexChanged" DataKeyNames="Id" AutoGenerateColumns="false">
            <Columns>

                <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                <asp:BoundField HeaderText="Categoria" DataField="IdCategoria.Descripcion" />
                <asp:BoundField HeaderText="Marca" DataField="IdMarca.Descripcion" />
                <asp:CommandField HeaderText="Modificar" ControlStyle-CssClass="btn btn-outline-primary" ShowSelectButton="true"  />

                <asp:CommandField HeaderText="Eliminar" ControlStyle-CssClass="btn btn-outline-danger" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <% if (confirmaEliminar == true)
                {  %>
            <div class="row">
                <div class="col-2"></div>
                <div class="col-1">
                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-success" OnClick="btnCancelar_Click" Text="Cancelar" />
                </div>
                <div class="col">
                    <asp:Button ID="btnElimniar" OnClick="btnElimniar_Click" PostBackUrl="~/listado.aspx" CssClass="btn btn-danger btnW" runat="server" Text="Seguro desea eliminar?" />
                </div>
                <div class="col-2"></div>
            </div>
            <%} %>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="row">
        <div class="col-1"></div>
        <div class="col">
            <a href="modificar.aspx" class="btn btn-primary">Agregar nuevo</a>
            
        </div>
    </div>


</asp:Content>
