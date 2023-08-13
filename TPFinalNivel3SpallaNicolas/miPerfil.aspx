<%@ Page Title="Mi perfil" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="miPerfil.aspx.cs" Inherits="TPFinalNivel3SpallaNicolas.miPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-2"></div>
        <div class="col-4">
            <h2>Mi perfil</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-2"></div>
        <div class="col-4">
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" TextMode="Email" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group" style="margin-top: 5%;">
                <label for="txtNombre">Nombre</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group" style="margin-top: 5%;">
                <label for="txtApellido">Apellido</label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="row" style="justify-content: space-around;">
                <div class="col-2" style="width: auto;">
                    <div class="form-group" style="margin-top: 5%; width: auto;">
                        <label for="txtPass">Password</label>
                        <asp:TextBox ID="txtPass" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-2" style="width: auto;">
                    <div class="form-group" style="margin-top: 5%;">
                        <label for="txtPass2">Repita Password</label>
                        <asp:TextBox ID="txtPass2" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div style="margin-top: 3%;">
                <asp:Button ID="btnGuardarPerfil" CssClass="btn btn-success" OnClick="btnGuardarPerfil_Click" runat="server" Text="Guardar" />
                <a href="Default.aspx" class="btn btn-warning">Cancelar</a>
            </div>
        </div>

        <div class="col-4" style="text-align: center;">
            <div class="mb-3">
                <label for="txtUrlImagen" class="form-label">Imagen Perfil</label>
                <asp:TextBox ID="txtUrlImagen" OnTextChanged="txtUrlImagen_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Image ID="imagenPerfil" Style="max-width: 300px; max-height: 300px;"
                    ImageUrl="./images/noFoto2.jpg" runat="server" />
            </div>
        </div>
    </div>
    <%if (admin == true)
        { %>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <%if (confirmaEliminar == true)
                {  %>
            <div class="row" style="margin-top: 1%;">
                <div class="col-2"></div>
                <div class="col-8" style="display: flex;">
                    <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Seguro desea eliminar de forma definitiva a éste usuario?" Style="width: -webkit-fill-available; margin-right: 10px;"
                        CssClass="btn btn-danger" />
                    <asp:Button ID="btnCancelar" Text="Cancelar" CssClass="btn  btn-primary" OnClick="btnCancelar_Click" runat="server" Style="height: fit-content;" />
                </div>
            </div>
            <%} %>
            <div class="row" style="margin-top: 1%;">
                <div class="col-2"></div>
                <div class="col-8" style="text-align: center;">

                    <asp:GridView ID="gridUsers" runat="server" CssClass="table table-hover" AutoGenerateColumns="false"
                        AllowPaging="true" PageSize="5" OnPageIndexChanging="gridUsers_PageIndexChanging" OnRowDeleting="gridUsers_RowDeleting"
                        OnSelectedIndexChanged="gridUsers_SelectedIndexChanged" DataKeyNames="Admin">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                            <asp:CheckBoxField DataField="Admin" HeaderText="ADMIN" />
                            <asp:CommandField ShowSelectButton="true" SelectText="SET ADMIN" ButtonType="Button" HeaderText="Convertir Admin" ControlStyle-CssClass="btn btn-sm btn-primary" />
                            <asp:CommandField ShowDeleteButton="true" DeleteText="Eliminar" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-danger" HeaderText="Eliminar" />
                        </Columns>
                    </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%} %>
</asp:Content>
