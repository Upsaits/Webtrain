<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TCWebUpdate.WebForm1" %>
<%@ Register assembly="DevExpress.Web.ASPxRichEdit.v18.1, Version=18.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    namespace="DevExpress.Web.ASPxRichEdit" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>How to create a custom save button in ASPxRichEdit with automatic enabling/disabling</title>
    <script type="text/javascript">
        function onCustomCommandExecuted(s, e) {
            switch (e.commandName) {
                case "Save":
                    alert('tests');
                    richEdit.commands.fileSaveAsDialog.execute();
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
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxRichEdit ID="RichEdit" ClientInstanceName="richEdit" WorkDirectory="~\App_Data\WorkDirectory" runat="server">
            <ClientSideEvents EndCallback="onEndCallback" DocumentChanged="onDocumentChanged"
                CustomCommandExecuted="onCustomCommandExecuted" />
        </dx:ASPxRichEdit>
    </form>
</body>
</html>
