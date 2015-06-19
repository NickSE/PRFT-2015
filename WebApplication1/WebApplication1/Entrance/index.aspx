<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.Entrance.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entrance</title>
    <meta charset="UTF-8" />
    <!-- dit doen we even niet <meta http-equiv="refresh" content="2"/>-->

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="../StyleSheet.css" />      
</head>
<body>
    <form id="Entrance" runat="server">       
        <header class="col-xs-12 container">
            <div class="col-xs-3">LOGO </div>
            <div class="col-xs-5">
                <nav>
                    <ul>
                        <li>Profile</li>
                        <li>Logout</li>
                    </ul>
                </nav>
            </div>
        </header>
        <div class="container">
            <div class="col-xs-4">
                <aside>
                    <!-- ### Zoeken en linken en lijstopvragen ### -->
                    <label for="tbID">ID:</label><asp:TextBox ID="tbID" runat="server"></asp:TextBox>
                    <label for="tbBarcode">Barcode:</label><asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>
                    <asp:Button ID="btnZoek" runat="server" Text="Zoek!" />
                    <asp:Button ID="btnLink" runat="server" Text="Link!" />
                    <asp:Button ID="btnAanwezig" runat="server" Text="Aanwezigen" OnClick="btnAanwezig_Click" />
                </aside>
            </div>
            <div class="col-xs-8">
                <main id="eventbeheer" runat="server">
                    <!-- ### Locatie aanmaken ### -->
                    <div class="form-group">
                        <h1>Account informatie</h1>
                        <div id="Naam" runat="server">
                        <!--<label for="pnlProfiel">Profiel</label>            
                        <asp:Panel ID="pnlProfiel" runat="server">                                           
                        </asp:Panel>!-->
                        </div>
                        <div id="Adres" runat="server"></div>
                        <div id="Betaald" runat="server"></div>
                    </div>
                    <hr />
                    <label for="lbPresent">Presentielijst</label>
                    <asp:ListBox ID="lbPresent" runat="server"></asp:ListBox>
			    </main>
            </div>
        </div>        
    </form>
</body>
</html>
