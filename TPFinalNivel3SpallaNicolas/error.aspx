<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="TPFinalNivel3SpallaNicolas.error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-top: 5%; margin-left: 5%;">
        <div class="col-2"></div>
        <div class="col-4" style="margin-top: 6em;">
            <asp:Label ID="lblError" CssClass="h5" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="col-2">
            <asp:Image ImageUrl="https://cdn.pixabay.com/photo/2017/02/12/21/29/false-2061132_640.png" Style="width: 300px; height: 300px;"
                runat="server" />
        </div>
    </div>

</asp:Content>
