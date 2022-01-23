<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="WebtrainPackages_01.aspx.cs" Inherits="TCWebUpdate.WebtrainPackages_01" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
        <dx:ASPxFormLayout ID="FormLayout1" runat="server" ColCount="2" Height="371px" ColumnCount="2">
            <Items>
                <dx:TabbedLayoutGroup ColSpan="2">
                    <Items>
                    <dx:LayoutItem ShowCaption="False" BackColor="#FFFFFF" HorizontalAlign="Left" VerticalAlign="Middle" ColSpan="2" Caption="Lehrsääle">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer  runat="server">
                                <dx:ASPxRoundPanel HeaderText="Bfi Metzentrum Attnang-Puchheim PC-Raum: CNC" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="797px" ClientInstanceName="gridview_cid1002" OnLoad="ASPxGridView1_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1002 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="Bfi Metzentrum Attnang-Puchheim PC-Raum: Pneumatik" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="797px" ClientInstanceName="gridview_cid1005" OnLoad="ASPxGridView2_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1005 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="Bfi Metzentrum Attnang-Puchheim PC-Raum: Lehrsaal 3" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Width="797px" ClientInstanceName="gridview_cid1006" OnLoad="ASPxGridView3_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1006 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="Bfi Metzentrum Attnang-Puchheim PC-Raum: Server 2016" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" Width="797px" ClientInstanceName="gridview_cid1019" OnLoad="ASPxGridView4_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1019 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" BackColor="#FFFFFF" HorizontalAlign="Left" VerticalAlign="Middle" ColSpan="2" Caption="Trainer Gruppe1">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer  runat="server">
                                <dx:ASPxRoundPanel HeaderText="Spittaler Hans-Peter" runat="server" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView5" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" Width="797px" ClientInstanceName="gridview_cid1009" OnLoad="ASPxGridView5_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1009 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="Geri Schimpl" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView6" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource6" Width="797px" ClientInstanceName="gridview_cid1012" OnLoad="ASPxGridView6_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource6" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1012 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="Böhmi" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView7" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource7" Width="797px" ClientInstanceName="gridview_cid1011" OnLoad="ASPxGridView7_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource7" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1011 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" BackColor="#FFFFFF" HorizontalAlign="Left" VerticalAlign="Middle" ColSpan="2" Caption="Trainer Gruppe2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer  runat="server">
                                <dx:ASPxRoundPanel HeaderText="Rosa Desch" runat="server" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView8" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource8" Width="797px" ClientInstanceName="gridview_cid1010" OnLoad="ASPxGridView8_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource8" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1010 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="Mike Schachinger" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView9" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource9" Width="797px" ClientInstanceName="gridview_cid1013" OnLoad="ASPxGridView9_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource9" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1013 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="Stadi" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView10" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource10" Width="797px" ClientInstanceName="gridview_cid1014" OnLoad="ASPxGridView10_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource10" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1014 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" BackColor="#FFFFFF" HorizontalAlign="Left" VerticalAlign="Middle" ColSpan="2" Caption="Trainer Gruppe3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer  runat="server">
                                <dx:ASPxRoundPanel HeaderText="Pauline Fitzinger" runat="server" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView11" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource11" Width="797px" ClientInstanceName="gridview_cid1017" OnLoad="ASPxGridView11_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource11" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1017 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="Waltraud Fürlinger" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView12" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource12" Width="797px" ClientInstanceName="gridview_cid1016" OnLoad="ASPxGridView12_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource12" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1016 and type='Library'"/>
                                        </dx:PanelContent>
                                      </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxRoundPanel HeaderText="Kak" runat="server" Width="337px" AllowCollapsingByHeaderClick="True" ShowCollapseButton="True">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView13" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource13" Width="797px" ClientInstanceName="gridview_cid1015" OnLoad="ASPxGridView13_Load">
                                            </dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource13" runat="server" providerName="MySql.Data.MySqlClient"
                                                               SelectCommand="select * from wtpackage where licenseId=1015 and type='Library'"/>
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

