<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="WebtrainPackages_02.aspx.cs" Inherits="TCWebUpdate.WebtrainPackages_02" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
        <dx:ASPxFormLayout ID="FormLayout1" runat="server" ColCount="2" Height="371px" ColumnCount="2">
            <Items>
                <dx:TabbedLayoutGroup ColSpan="2">
                    <Items>
                    <dx:LayoutItem ShowCaption="False" BackColor="#FFFFFF" HorizontalAlign="Left" VerticalAlign="Middle" ColSpan="2" Caption="SoftObject">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer  runat="server">
                                <dx:ASPxRoundPanel HeaderText="SoftObject Testserver 1" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="797px" ClientInstanceName="gridview_cid9999" OnLoad="ASPxGridView1_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=9999 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="SoftObject Testserver 2" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="797px" ClientInstanceName="gridview_cid9990" OnLoad="ASPxGridView2_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=9990 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="SoftObject Studio Franky" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Width="797px" ClientInstanceName="gridview_cid9991" OnLoad="ASPxGridView3_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=9991 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    </Items>
                </dx:TabbedLayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
</asp:Content>

