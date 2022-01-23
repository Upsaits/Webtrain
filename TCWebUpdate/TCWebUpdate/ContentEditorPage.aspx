<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ContentEditorPage.aspx.cs" Inherits="TCWebUpdate.ContentEditorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function popupControl_Init(s, e) {
            //Synchronize the client variable's value with the confirm dialog checkbox' setting
            //dontAskConfirmation = cbDontAsk.GetChecked();
        }

        function OnExamStarted(s, e)
        {
            CallbackPanel.PerformCallback();
        }

        // Popup notification via a callback
        function CallbackComplete(s, e) {
            // result = left;top;content text
            var indexOfFirstSeparator = e.result.indexOf(';');
            var indexOfSecondSeparator = e.result.indexOf(';', indexOfFirstSeparator + 1)

            var msgType = parseInt(e.result.substring(0, indexOfFirstSeparator)); // 1:Ok, 2: YesNo
            var msgParam = parseInt(e.result.substring(indexOfFirstSeparator + 1, indexOfSecondSeparator));
            var msgText = e.result.substr(indexOfSecondSeparator + 1);

            popup.SetHeaderText("Webtrain-Portal");
            lblMsgBoxText.SetText(msgText);
            if (msgType == 1)
            {
                btnMsgBoxYes.SetVisible(false);
                btnMsgBoxNo.SetVisible(false);
                btnMsgBoxOk.SetVisible(true);
            }
            else {
                btnMsgBoxYes.SetVisible(true);
                btnMsgBoxNo.SetVisible(true);
                btnMsgBoxOk.SetVisible(false);
            }
            popup.ShowAtPos(300, 300);
        }
    </script>
    <dx:ASPxPopupControl runat="server" ID="popMessage" ClientInstanceName="popup" EncodeHtml="False" Modal="True">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxFormLayout runat="server" ColCount="8">
                    <Items>
                        <dx:LayoutItem Caption="" ColSpan="8" RowSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxLabel ID="lblMsgBoxText" ClientInstanceName="lblMsgBoxText" runat="server">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnMsgBoxYes" ClientInstanceName="btnMsgBoxYes" runat="server" Text="Ja">
                                        <ClientSideEvents Click="function(s, e) {popup.Hide(); callback.SendCallback('btnMsgBoxYes');}"/>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem>
                        </dx:EmptyLayoutItem>
                        <dx:LayoutItem Caption="" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnMsgBoxOk" ClientInstanceName="btnMsgBoxOk" runat="server" Text="Ok">
                                        <ClientSideEvents Click="function(s, e) {popup.Hide(); callback.SendCallback('btnMsgBoxOk');}"/>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem>
                        </dx:EmptyLayoutItem>
                        <dx:LayoutItem Caption="" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnMsgBoxNo" ClientInstanceName="btnMsgBoxNo" runat="server" Text="Nein">
                                        <ClientSideEvents Click="function(s, e) {popup.Hide(); callback.SendCallback('btnMsgBoxNo');}"/>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>

                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxCallback runat="server" ID="clbCallback" ClientInstanceName="callback" OnCallback="clbCallback_OnCallback">
        <ClientSideEvents CallbackComplete="CallbackComplete" />
    </dx:ASPxCallback>
    <dx:ASPxCallbackPanel ID="CallbackPanel1" runat="server" Width="100%" Height="100%" OnCallback="CallbackPanel1_OnCallback" ClientInstanceName="CallbackPanel1">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <dx:ASPxPanel ID="QuestionPanel" runat="server" Height="450px" Width="100%" Visible="true">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <dx:ASPxSplitter ID="Splitter1" ClientInstanceName="splitter1" runat="server" Height="100%" Width="100%">
                                <Panes>
                                    <dx:SplitterPane MinSize="400px" ShowCollapseBackwardButton="True" ScrollBars="Both">
                                        <ContentCollection>
                                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                                <dx:ASPxTreeView ID="treeView" runat="server" EnableCallBacks="True" ClientInstanceName="treeView" SyncSelectionMode="None"
                                                                 OnVirtualModeCreateChildren="treeView_VirtualModeCreateChildren" OnNodeClick="TreeView1_OnNodeClick" OnLoad="treeView_Load" OnUnload="treeView_Unload" AutoPostBack="True">
                                                </dx:ASPxTreeView>
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>
                                    <dx:SplitterPane Name="ContentUrlPane" ScrollBars="Auto">
                                        <ContentCollection>
                                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="10" Height="100%" Width="100%">
                                                            <Items>
                                                                <dx:LayoutGroup AlignItemCaptions="False" ColCount="10" ColSpan="10" GroupBoxDecoration="None" ShowCaption="False">
                                                                    <Items>
                                                                        <dx:LayoutItem Caption="" ColSpan="3">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxCheckBox ID="chkAnswer1" runat="server" CheckState="Unchecked" Visible="False">
                                                                                    </dx:ASPxCheckBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="7">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxLabel ID="lblAnswer1" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="3">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxCheckBox ID="chkAnswer2" runat="server" CheckState="Unchecked" Visible="False">
                                                                                    </dx:ASPxCheckBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="7">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxLabel ID="lblAnswer2" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="3">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxCheckBox ID="chkAnswer3" runat="server" CheckState="Unchecked" Visible="False">
                                                                                    </dx:ASPxCheckBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="7">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxLabel ID="lblAnswer3" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="3">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxCheckBox ID="chkAnswer4" runat="server" CheckState="Unchecked" Visible="False">
                                                                                    </dx:ASPxCheckBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="7">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxLabel ID="lblAnswer4" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="3">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer  runat="server">
                                                                                    <dx:ASPxCheckBox ID="chkAnswer5" runat="server" CheckState="Unchecked" Visible="False">
                                                                                    </dx:ASPxCheckBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="7">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer  runat="server">
                                                                                    <dx:ASPxLabel ID="lblAnswer5" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="3">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxCheckBox ID="chkAnswer6" runat="server" CheckState="Unchecked" Visible="False">
                                                                                    </dx:ASPxCheckBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="7">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer  runat="server">
                                                                                    <dx:ASPxLabel ID="lblAnswer6" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="3">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxCheckBox ID="chkAnswer7" runat="server" CheckState="Unchecked" Visible="False">
                                                                                    </dx:ASPxCheckBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="7">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxLabel ID="lblAnswer7" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="3">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxCheckBox ID="chkAnswer8" runat="server" CheckState="Unchecked" Visible="False">
                                                                                    </dx:ASPxCheckBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="7">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxLabel ID="lblAnswer8" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="3">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxCheckBox ID="chkAnswer9" runat="server" CheckState="Unchecked" Visible="False">
                                                                                    </dx:ASPxCheckBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="" ColSpan="7">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                                                    <dx:ASPxLabel ID="lblAnswer9" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                    </Items>
                                                                </dx:LayoutGroup>
                                                            </Items>
                                                        </dx:ASPxFormLayout>
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>
                                </Panes>    
                            </dx:ASPxSplitter>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
                <dx:ASPxPanel ID="ExamPanel" runat="server" Height="450px" Width="100%" Visible="false">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxFormLayout runat="server" ColCount="10" style="margin-bottom: 0px">
                                <Items>
                                    <dx:TabbedLayoutGroup ColSpan="10">
                                        <Items>
                                            <dx:LayoutGroup ColCount="10" Caption="Frage">
                                                <Items>
                                                    <dx:LayoutGroup ColSpan="10" Caption="" ColCount="10">
                                                        <Items>
                                                            <dx:LayoutItem Caption="" ColSpan="8">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamQuestion" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamProgress" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <dx:LayoutGroup Caption="" ColCount="5" ColSpan="5" RowSpan="5">
                                                        <Items>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxCheckBox ID="chkExamAnswer1" runat="server" CheckState="Unchecked">
                                                                        </dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamAnswer1" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxCheckBox ID="chkExamAnswer2" runat="server" CheckState="Unchecked">
                                                                        </dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamAnswer2" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxCheckBox ID="chkExamAnswer3" runat="server" CheckState="Unchecked">
                                                                        </dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamAnswer3" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxCheckBox ID="chkExamAnswer4" runat="server" CheckState="Unchecked">
                                                                        </dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamAnswer4" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxCheckBox ID="chkExamAnswer5" runat="server" CheckState="Unchecked">
                                                                        </dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamAnswer5" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxCheckBox ID="chkExamAnswer6" runat="server" CheckState="Unchecked">
                                                                        </dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamAnswer6" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer  runat="server">
                                                                        <dx:ASPxCheckBox ID="chkExamAnswer7" runat="server" CheckState="Unchecked">
                                                                        </dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamAnswer7" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxCheckBox ID="chkExamAnswer8" runat="server" CheckState="Unchecked">
                                                                        </dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamAnswer8" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxCheckBox ID="chkExamAnswer9" runat="server" CheckState="Unchecked">
                                                                        </dx:ASPxCheckBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="" ColSpan="3">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxLabel ID="lblExamAnswer9" runat="server">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                    <dx:LayoutItem Caption="" ColSpan="5">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxImage runat="server" ImageUrl="~/OnlineContent/Contents/Content/de/fragenkataloge/Templates/img/question.gif">
                                                                </dx:ASPxImage>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:TabbedLayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="10">
                    <Items>
                        <dx:EmptyLayoutItem ColSpan="10">
                        </dx:EmptyLayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnExamStart" Text="Übungsmodus" OnClick="btnExamStart_OnClick" runat="server" Visible="False">
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnWorkout" Text="Ausarbeiten" runat="server" Visible="False" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) { callback.SendCallback('btnWorkout'); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnNextQuestion" Text="Nächste Frage" AutoPostBack="False" runat="server" Visible="False" OnClick="btnNextQuestion_OnClick">
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem ColSpan="5">
                        </dx:EmptyLayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnSave" runat="server" Visible="False" AutoPostBack="False" Text="Speichern">
                                        <ClientSideEvents Click="function(s, e) { callback.SendCallback('btnSave'); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                    <dx:ASPxButton ID="btnExport" runat="server" Visible="False" Text="Download" OnClick="btnExport_OnClick">
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                    </Items>
                </dx:ASPxFormLayout>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxPopupControl ID="popupMessage" runat="server" AllowDragging="True" CloseAction="CloseButton" Modal="True">
    </dx:ASPxPopupControl>

</asp:Content>
