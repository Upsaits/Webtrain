<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="DownloadsPage.aspx.cs" Inherits="TCWebUpdate.DownloadsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" EnableTheming="True" Theme="Aqua" Width="100%" Height="100%">
        <Items>
            <dx:LayoutItem Caption="1. Schritt: Webtrain Einrichten:" HelpText="Bitte klicken sie auf diesen Link und dann auf 'öffnen' oder 'starten'" RowSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxHyperLink runat="server" NavigateUrl="Remote/TCInstallCD_Remote.sfx.exe" Font-Names="Arial" Font-Size="11pt" Text="Webtrain-Remote-Installer.sfx.exe" Theme="Aqua" ID="Hyperlink1">
                        </dx:ASPxHyperLink>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <HelpTextSettings Position="Top" />
                <CaptionSettings Location="Top" VerticalAlign="Middle" />
            </dx:LayoutItem>
            <dx:EmptyLayoutItem>
            </dx:EmptyLayoutItem>
            <dx:LayoutItem Caption="2. Schritt: Persönlichen VPN-Zugang Einrichten" HelpText="Bitte klicken sie auf diesen Link und dann auf 'öffnen' oder 'starten'" ShowCaption="True">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server" >
                        <dx:ASPxHyperLink runat="server" NavigateUrl="OpenVPN/OpenVPN_Installer_mairfr.sfx.exe" Font-Names="Arial" Font-Size="11pt" Text="OpenVPN-Installer.sfx.exe" Theme="Aqua" ID="Hyperlink2">
                        </dx:ASPxHyperLink>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                <HelpTextSettings Position="Top" />
                <CaptionSettings Location="Top" VerticalAlign="Middle" />
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
