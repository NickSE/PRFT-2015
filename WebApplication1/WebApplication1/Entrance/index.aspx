<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.Entrance.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entrance</title>
    <meta charset="UTF-8" />
    <!-- dit doen we even niet <meta http-equiv="refresh" content="2"/>-->

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="../resource/style/theme.css" />
</head>
<body>
    <form id="Entrance" runat="server">
    <div>        
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
    </div>
        <div>
            <label for="tbID">ID:</label><asp:TextBox ID="tbID" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="tbBarcode">Barcode:</label><asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnZoek" runat="server" Text="Zoek!" />
        </div>        
        <div>
            <asp:Button ID="btnAanwezig" runat="server" Text="Aanwezigen" />
        </div>
        <div>
            <asp:Button ID="btnLink" runat="server" Text="Link!" />
        </div>        
        <div id="Naam" runat="server">
            <!--<label for="pnlProfiel">Profiel</label>            
            <asp:Panel ID="pnlProfiel" runat="server">                                           
            </asp:Panel>!-->
        </div>
        <div id="Adres" runat="server">
        </div>
        <div id="Betaald" runat="server">
        </div>        
    </form>
</body>
</html>
