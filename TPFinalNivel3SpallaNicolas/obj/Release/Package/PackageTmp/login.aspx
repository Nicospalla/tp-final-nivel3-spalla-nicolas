<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TPFinalNivel3SpallaNicolas.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-2"></div>
        <div class="col">
            <div class="mb-3" style="margin-top: 4%;">
                <label for="txtEmail" class="form-label">Direccion de email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                <asp:Label ID="lblTxtEmail" runat="server" Font-Size="Small" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <label for="txtPass" class="form-label">Constraseña</label>
                <asp:TextBox ID="txtPass" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                <asp:Label ID="lblTxtPass" runat="server"  Font-Size="Small" Text=""></asp:Label>
            </div>
            <div class="mb-3">
                <asp:Button ID="btnLogin" CssClass="btn btn-success" OnClick="btnLogin_Click" runat="server" Text="Aceptar" />
                <asp:Button ID="btnCancelar" CssClass="btn btn-outline-danger" PostBackUrl="~/Default.aspx" runat="server" Text="Cancelar" />
            </div>
            <div>
                <asp:Label ID="lblTest" runat="server" style="font-size:Large;font-family: monospace;font-style: oblique;color: red;" Font-Size="Large" Text=""></asp:Label>
            </div>
            <div>
                <asp:Button ID="recuPass" runat="server" OnClick="recuPass_Click"  Text="Recuperar Password" CssClass="btn btn-success" />
                <asp:Label ID="lblRecuPass" runat="server" ForeColor="Red" Text=""></asp:Label>
            </div>
        </div>
        <div class="col-6"></div>
    </div>
</asp:Content>
