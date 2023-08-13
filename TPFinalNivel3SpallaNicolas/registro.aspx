<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="TPFinalNivel3SpallaNicolas.registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-top: 3%;">
        <div class="col-2"></div>
        <div class="col-6">
            <h4>Regístrate</h4>
        </div>
    </div>
    <div class="row">
        <div class="col-2"></div>
        <div class="col-4">
            <div class="mb-3" style="margin-top: 4%;">
                <label for="txtEmail" class="form-label">Direccion de email</label>
                <asp:TextBox ID="txtEmail" AutoPostBack="true" TextMode="Email" OnTextChanged="txtEmail_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtEmail" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
                <asp:Label ID="lblTxtEmail" runat="server" Font-Size="Small" ForeColor="Red" Text=""></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-2"></div>
            <div class="col-4" style="display: flex; justify-content: space-around">
                <div class="mb-3">
                    <label for="txtPass" class="form-label">Constraseña</label>
                    <asp:TextBox ID="txtPass1" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Label ID="lblPass1" runat="server" Font-Size="Small" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div class="mb-3">
                    <label for="txtPass" class="form-label">Constraseña</label>
                    <asp:TextBox ID="txtPass2" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

        </div>
        <div class="col-2"></div>
            <div class="col-4" style="display: flex; justify-content: center;">
                <asp:Button ID="btnRegistro" OnClick="btnRegistro_Click" Style="margin-right: 10px;"
                    CssClass="btn btn-success" runat="server" Text="Aceptar" />
                <asp:Button ID="btnCancelar" CssClass="btn btn-outline-danger" Style="margin-right: 6px;" PostBackUrl="~/Default.aspx" runat="server" Text="Cancelar" />
                <asp:Label ID="lblTest" runat="server" Font-Size="Large" Text=""></asp:Label>
            </div>
    </div>

    <div class="mb-3">
    </div>
</asp:Content>
