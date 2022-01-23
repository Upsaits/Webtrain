<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LaunchContent.aspx.cs" Inherits="TCWebUpdate.LaunchContent" %>

<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxFormLayout ID="formLayout" runat="server" EnableTheming="True" Height="100px" Theme="Aqua" Width="500px">
            <Items>
                <dx:LayoutGroup Caption="Inhalts-Titel" ColCount="3">
                    <Items>
                        <dx:LayoutItem Caption="" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxLabel ID="lblDescription" Text="Das ist ein Test um zu sehen wie sich das Problem äußert, wenn wir mehrere" runat="server">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="" HorizontalAlign="Right">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnLaunch" runat="server" Width="100px" EnableTheming="True" Text="Start" Theme="Aqua" OnClick="btnLaunch_Click">
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    </div>
    </form>
</body>
</html>
