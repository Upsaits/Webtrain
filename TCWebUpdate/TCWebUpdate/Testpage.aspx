<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Testpage.aspx.cs" Inherits="TCWebUpdate.Testpage" %>

<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="10" Height="100%" Width="100%">
            <Items>
                <dx:LayoutGroup ColCount="10" ColSpan="10" RowSpan="5" VerticalAlign="Bottom">
                    <Items>
                        <dx:EmptyLayoutItem>
                        </dx:EmptyLayoutItem>
                    </Items>
                    <SettingsItems VerticalAlign="Bottom" />
                </dx:LayoutGroup>
                <dx:LayoutGroup ColCount="10" ColSpan="10" RowSpan="10">
                    <Items>
                        <dx:LayoutItem ColSpan="3" RowSpan="5">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="ASPxFormLayout1_E1" runat="server" Width="100%" Height="100%">
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ColSpan="7">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="ASPxFormLayout1_E2" runat="server" Width="100%" Height="100%">
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem ColSpan="10">
                        </dx:EmptyLayoutItem>
                        <dx:EmptyLayoutItem ColSpan="10">
                        </dx:EmptyLayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
</asp:Content>
    
