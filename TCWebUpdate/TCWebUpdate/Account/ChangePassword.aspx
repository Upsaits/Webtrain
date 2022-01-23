<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="ChangePassword.aspx.cs" Inherits="TCWebUpdate.ChangePassword" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="accountHeader">
    <h2>
        Passwort ändern</h2>
    <p>Benutzen sie die Eingabefelder um das Passwort zu ändern.</p>
    <p>Paßwörter müssen mindestens 6 Zeichen lang sein.</p>
</div>

<br />
<dx:ASPxLabel ID="lblCurrentPassword" runat="server" Text="Altes Paßwort:" />
<div class="form-field">
    <dx:ASPxTextBox ID="tbCurrentPassword" runat="server" Password="true" Width="200px">
        <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup">
            <RequiredField ErrorText="Old Password is required." IsRequired="true" />
        </ValidationSettings>
    </dx:ASPxTextBox>
</div>
<dx:ASPxLabel ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Passwort:" />
<div class="form-field">
    <dx:ASPxTextBox ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server"
        Width="200px">
        <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup">
            <RequiredField ErrorText="Password is required." IsRequired="true" />
        </ValidationSettings>
    </dx:ASPxTextBox>
</div>
<dx:ASPxLabel ID="lblConfirmPassword" runat="server" AssociatedControlID="tbConfirmPassword"
    Text="Password bestätigen:" />
<div class="form-field">
    <dx:ASPxTextBox ID="tbConfirmPassword" Password="true" runat="server" Width="200px">
        <ValidationSettings ValidationGroup="ChangeUserPasswordValidationGroup">
            <RequiredField ErrorText="Confirm Password is required." IsRequired="true" />
        </ValidationSettings>
        <ClientSideEvents Validation="function(s, e) {
            var originalPasswd = Password.GetText();
            var currentPasswd = s.GetText();
            e.isValid = (originalPasswd  == currentPasswd );
            e.errorText = 'The Password and Confirmation Password must match.';
        }" />
    </dx:ASPxTextBox>
</div>
<dx:ASPxButton ID="btnChangePassword" runat="server" Text="Paßwort ändern" ValidationGroup="ChangeUserPasswordValidationGroup"
    OnClick="btnChangePassword_Click">
</dx:ASPxButton>
</asp:Content>