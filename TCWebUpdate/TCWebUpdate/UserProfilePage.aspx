<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Main.master" CodeBehind="UserProfilePage.aspx.cs" Inherits="TCWebUpdate.UserProfilePage" %>

<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v18.1, Version=18.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxSpreadsheet.v18.1, Version=18.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpreadsheet" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function onCustomCommandExecuted(s, e) {
            switch (e.commandName) {
                case "Save":
                    alert('tests');
                    richEdit.commands.fileSaveAs.execute();
                    break;
            }
        }

        function onDocumentChanged(s, e) {
            checkOnChanges();
        }

        function onEndCallback(s, e) {
            checkOnChanges();
        }

        function checkOnChanges() {
            var currectState = richEdit.commands.fileSave.getState();
            var saveButton = richEdit.GetRibbon().GetItemByName("Save");
            saveButton.SetEnabled(currectState.enabled);
        }
    </script>

    <dx:ASPxRichEdit ID="RichEdit" ClientInstanceName="richEdit" WorkDirectory="~\App_Data\docs" runat="server">
        <ClientSideEvents EndCallback="onEndCallback" DocumentChanged="onDocumentChanged"
            CustomCommandExecuted="onCustomCommandExecuted" />
    </dx:ASPxRichEdit>
</asp:Content>

