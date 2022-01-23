<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Login.aspx.cs" Inherits="TCWebUpdate.Account.Login" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="accountHeader">
    <h2>
        Anmeldung</h2>
    <p>
        Bitte melden sie sich mit Benutzername und Passwort an. Falls sie noch keinen Zugang haben, müssen sie sich <a href="Register.aspx">registrieren</a>.</p>
</div>
<dx:ASPxLabel ID="lblEMail" runat="server" AssociatedControlID="tbEmail" Text="E-Mail:" Theme="Aqua"/>
<div class="form-field">
    <dx:ASPxTextBox ID="tbEMail" runat="server" Width="200px">
        <ValidationSettings ValidationGroup="LoginUserValidationGroup">
            <RequiredField ErrorText="E-Mail ist erforderlich." IsRequired="true" />
        </ValidationSettings>
    </dx:ASPxTextBox>
</div>
<dx:ASPxLabel ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Passwort:" Theme="Aqua"/>
<div class="form-field">
    <dx:ASPxTextBox ID="tbPassword" runat="server" Password="true" Width="200px">
        <ValidationSettings ValidationGroup="LoginUserValidationGroup">
            <RequiredField ErrorText="Passwort ist erforderlich." IsRequired="false" />
        </ValidationSettings>
    </dx:ASPxTextBox>
</div>
<dx:ASPxButton ID="btnLogin" runat="server" Text="Anmelden" ValidationGroup="LoginUserValidationGroup" OnClick="btnLogin_Click" Theme="Aqua">
</dx:ASPxButton>
</asp:Content>