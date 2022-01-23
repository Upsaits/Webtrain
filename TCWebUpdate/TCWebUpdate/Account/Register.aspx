<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Register.aspx.cs" Inherits="TCWebUpdate.Account.Register" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
    <div class="accountHeader">
    <h2>
        Erstellen eines Zugangs</h2>
    <p>Bitte geben sie die nötigen Informationen ein.</p>
        <p>Passwörter müssen mindestens 6 Zeichen lang sein</p>
</div>
    <dx:ASPxLabel ID="lblFirstName" runat="server" AssociatedControlID="tbFirstName" Text="Vorname:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbFirstName" runat="server" Width="200px">
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField ErrorText="Vorname muß angegeben werden." IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblMiddleName" runat="server" AssociatedControlID="tbMiddleName" Text="2.Vorname:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbMiddleName" runat="server" Width="200px">
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblLastName" runat="server" AssociatedControlID="tbLastName" Text="Nachname:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbLastName" runat="server" Width="200px">
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField ErrorText="Nachname muß angegeben werden." IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblEmail" runat="server" AssociatedControlID="tbEmail" Text="E-mail:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbEmail" runat="server" Width="200px">
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField ErrorText="E-mail muß angegeben werden." IsRequired="true" />
                <RegularExpression ErrorText="Email ungültig" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Passwort:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server" Width="200px">
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblConfirmPassword" runat="server" AssociatedControlID="tbConfirmPassword" Text="Paßwort bestätigen:" />
    <div class="form-field">
        <dx:ASPxTextBox ID="tbConfirmPassword" Password="true" runat="server" Width="200px">
            <ClientSideEvents Validation="function(s, e) {
                var originalPasswd = Password.GetText();
                var currentPasswd = s.GetText();
                e.isValid = (originalPasswd  == currentPasswd );
                e.errorText = 'The Password and Confirmation Password must match.';
            }" />
        </dx:ASPxTextBox>
    </div>
    <dx:ASPxButton ID="btnCreateUser" runat="server" Text="Zugang erstellen" ValidationGroup="RegisterUserValidationGroup"
        OnClick="btnCreateUser_Click">
    </dx:ASPxButton>
</asp:content>