<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="CampusPage.aspx.cs" Inherits="TCWebUpdate.CampusPage" %>

<%@ Register assembly="DevExpress.Web.ASPxSpreadsheet.v18.1, Version=18.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpreadsheet" tagprefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <dx:ASPxSpreadsheet ID="ASPxSpreadsheet1" runat="server" Visible="False" OnInit="ASPxSpreadsheet1_Init" RibbonMode="None" ShowFormulaBar="False" Height="10%" Width="10%">
        </dx:ASPxSpreadsheet>
        
        <dx:ASPxTimer ID="timerSync" runat="server" Enabled="False" Interval="30000" OnTick="timerSync_Tick" />
        <dx:ASPxFormLayout ID="formLayout1" runat="server" Height="100%" Width="100%" Theme="Aqua">
            <Items>
                <dx:TabbedLayoutGroup>
                    <Items>
                        <dx:LayoutGroup Caption="Attnang-Puchheim" ColCount="2">
                            <Items>
                                <dx:LayoutItem Caption="">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView2" runat="server" DataSourceID="SqlDataSource2" EnableTheming="True" Theme="Aqua" Width="100%" Font-Size="Small"></dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:testConnectionString %>" ProviderName="<%$ ConnectionStrings:testConnectionString.ProviderName %>" SelectCommand="SELECT surname as 'Vorname', lastname as 'Nachname', username as 'Benutzername', email as 'E-Mail', activated as 'Aktiv', className as 'Klasse' FROM wtuser WHERE locationId=1"></asp:SqlDataSource>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="" HorizontalAlign="Right" VerticalAlign="Top">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="SqlDataSource1" EnableTheming="True" Theme="Aqua"></dx:ASPxGridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:testConnectionString %>" ProviderName="<%$ ConnectionStrings:testConnectionString.ProviderName %>" SelectCommand="SELECT name as 'Webtrain-Server', licenseId as 'Lizenz-Id' FROM wtserver WHERE locationId=1"></asp:SqlDataSource>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutGroup Caption="Excel-Tabelle" RowSpan="2" ColSpan="2">
                                    <Items>
                                        <dx:LayoutGroup Caption="Auto-Update" ColCount="3">
                                            <Items>
                                                <dx:LayoutItem Caption="Datei:" HorizontalAlign="Left" RowSpan="2" VerticalAlign="Top">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxTextBox ID="edtSyncURLFile" Theme="Aqua" runat="server" Width="289px" AutoCompleteType="Disabled" ClientInstanceName="edtTest" EnableClientSideAPI="True" Text="http://194.112.244.74/Franky/notenliste.xlsx">
                                                                <ClientSideEvents TextChanged="function(s, e) { OnTextChangedHandler(s); }" />
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="" RowSpan="2" HorizontalAlign="Left" VerticalAlign="Top">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxButton ID="btnToggleTimer" runat="server" EnableTheming="True" Text="Start" Theme="Aqua" OnClick="btnToggleTimer_Click" UseSubmitBehavior="False">
                                                            </dx:ASPxButton>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Stichtag">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxCalendar ID="calendar" runat="server" Height="200px" AutoPostBack="True" OnSelectionChanged="calendar_SelectionChanged" Theme="Aqua" SelectedDate="2019-04-08">
                                                            </dx:ASPxCalendar>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Status" ColSpan="2">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxLabel ID="lblSyncResult" runat="server" ClientInstanceName="lblResult">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="" HorizontalAlign="Right">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server">
                                                            <dx:ASPxButton ID="btnSynchronize" runat="server" HorizontalAlign="Right" OnClick="btnSynchronize_Click" Text="Daten Übertragen" Theme="Aqua">
                                                            </dx:ASPxButton>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Height="209px" Width="800px" OnFilesUploaded="ASPxFileManager1_FilesUploaded" Theme="Aqua">
                                                        <Settings RootFolder="~/Excel" ThumbnailFolder="~/Thumb/" EnableMultiSelect="True" />
                                                        <SettingsFileList View="Details">
                                                        </SettingsFileList>
                                                        <SettingsEditing AllowDelete="True" />
                                                        <SettingsFolders Visible="False" />
                                                        <SettingsToolbar ShowCopyButton="False" ShowCreateButton="False" ShowDownloadButton="False" ShowFilterBox="False" ShowMoveButton="False" ShowRenameButton="False">
                                                        </SettingsToolbar>
                                                        <SettingsUpload AutoStartUpload="True">
                                                            <AdvancedModeSettings EnableFileList="True" EnableMultiSelect="True">
                                                            </AdvancedModeSettings>
                                                        </SettingsUpload>
                                                    </dx:ASPxFileManager>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:EmptyLayoutItem>
                                        </dx:EmptyLayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="SoftObject" ColCount="2" Visible="False">
                        </dx:LayoutGroup>
                    </Items>
                </dx:TabbedLayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    </div>
</asp:Content>
