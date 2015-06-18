<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.Entrance.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entrance</title>
    <meta charset="UTF-8" />
    <!-- dit doen we even niet <meta http-equiv="refresh" content="2"/>-->

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css" />
    <style>
        body {
            background-color: #f0f0f0;
        }

        header {
            background-color: rgb(58, 89, 149);
            background: linear-gradient(180deg, #337ab7 0, #265a88 100%);
            box-shadow: 0 1px 3px #8888BB;
            height: 40px;
            margin-bottom: 5px;
        }

        nav ul {
            text-align: center;
            padding: 0;
        }

        nav li:not(:last-child):after {
            content: "";
            color: #333;
            border-left: 1px solid #eee;
            border-right: 1px solid #444;
            width: 0;
            margin-left: 5px;
        }

        nav li {
            list-style: none;
            display: inline-block;
            color: #eee;
            text-shadow: 1px 0 #444, -1px 0 #444, 0 1px #444, 0 -1px #444;
        }

        main, aside {
            background: #f8f8f8;
            border-radius: 3px;
            box-shadow: 0 0 3px #8888BB;
            padding: 3px 8px;
            color: #555;
        }

        div#naam{
            position: relative;
            top: 50px;
            left: 300px;
        }
        div#adres{
            position: relative;
            top: 60px;
            left: 300px;
        }
        
    </style>
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
