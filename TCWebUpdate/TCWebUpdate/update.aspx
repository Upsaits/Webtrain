<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="update.aspx.cs" Inherits="TCWebUpdate.update" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update_de</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:label id="Label_Header"
			runat="server" Font-Bold="True" Font-Names="arial" ForeColor="SteelBlue" Width="752px"
			Height="23px">Verfügbare Updates</asp:label>
        <br />
        Aktuellste Änderungen/Fehlerbehebungen 3.1.0.23:<br />&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Font-Bold="False" Font-Names="Calibri" Height="146px" ReadOnly="True" TextMode="MultiLine" Width="1075px" Font-Size="Medium" style="margin-top: 19px" OnTextChanged="TextBox1_TextChanged">BUGFIXES:
            - ServerName nach Autom. Suche richtig setzen
            - fix Hang-ups beim Beenden
            - doppelter Dialogaufruf beim Beenden
            - Client: Hostnamen in IP-Eingabe ermöglichen
        </asp:TextBox>        
        <br />
        <br />
        Aktuellste Änderungen/Fehlerbehebungen 3.1.0.24:<br />&nbsp;&nbsp;
        <asp:TextBox ID="TextBox3" runat="server" Font-Bold="False" Font-Names="Calibri" Height="142px" ReadOnly="True" TextMode="MultiLine" Width="1075px" Font-Size="Medium" style="margin-top: 19px" OnTextChanged="TextBox1_TextChanged">FEATURES:
            - Automatisches Auswerten von Aufgaben
            - Animationsvorbereiungen Ersatz Flash
BUGFIXES
            - IP-Adress-Eingabefehler
        </asp:TextBox>        
        <br />
        <br />
        Aktuellste Änderungen/Fehlerbehebungen 3.1.0.25:<br />&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" runat="server" Font-Bold="False" Font-Names="Calibri" Height="109px" ReadOnly="True" TextMode="MultiLine" Width="1075px" Font-Size="Medium" style="margin-top: 19px" OnTextChanged="TextBox1_TextChanged">FEATURES:
            - Studio Version
        </asp:TextBox>        
        <br />
        <br />
        Aktuellste Änderungen/Fehlerbehebungen 3.1.0.26:<br />&nbsp;&nbsp;
        <asp:TextBox ID="TextBox4" runat="server" Font-Bold="False" Font-Names="Calibri" Height="150px" ReadOnly="True" TextMode="MultiLine" Width="1075px" Font-Size="Medium" style="margin-top: 19px" OnTextChanged="TextBox1_TextChanged">FEATURES:
            - xAPI Einbindung als Prototyp
BUGFIXES:
            - Fehler bei Aktionen löschen
        </asp:TextBox>        
        <dx:ASPxNavBar ID="NavBarDownload" runat="server" OnItemClick="NavBarDownload_ItemClick" Width="171px" Height="100px"><Groups>
                <dx:NavBarGroup Text="Patches">
                    <Items>
                        <dx:NavBarItem NavigateUrl="Patches/P2021-P1-5.sfx.exe" Text="Version 3.1.0.23">
                        </dx:NavBarItem> 
                        <dx:NavBarItem NavigateUrl="Patches/P2021-P1-6.sfx.exe" Text="Version 3.1.0.24">
                        </dx:NavBarItem>
                        <dx:NavBarItem NavigateUrl="Patches/P2021-P1-7.sfx.exe" Text="Version 3.1.0.25">
                        </dx:NavBarItem>
                        <dx:NavBarItem NavigateUrl="Patches/P2021-P1-8.sfx.exe" Text="Version 3.1.0.26">
                        </dx:NavBarItem>
                    </Items>
                </dx:NavBarGroup>
            </Groups>
        </dx:ASPxNavBar>
        <br />
    <br /><br /></form>
</body>
</html>
