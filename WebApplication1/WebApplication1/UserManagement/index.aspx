<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.UserManagement.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GebruikerBeheer</title>
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
    </style>
</head>
<body>
    <form id="EventManagement" runat="server">
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
                    <!-- ### Gebruiker Lijst ### -->
                    <div class="form-group">
                        <label for="lbUser">Gebruikers:</label>
                        <asp:ListBox ID="lbUser" runat="server"></asp:ListBox>
                        <asp:Button ID="btnRemoveUser" class="btn btn-default" runat="server" Text="Gebruiker Verwijderen" OnClick="btnRemoveUser_Click"></asp:Button>
                    </div>
                </aside>
            </div>
            <div class="col-xs-8">
                <main id="eventbeheer" runat="server">
                    <!-- ### Gebruiker aanmaken ### -->
                    <div class="form-group">
                        <h1>Gebruiker aanmaken</h1>

                        <label for="tbNameUser">Voornaam:</label><asp:TextBox ID="tbNameUser" runat="server"></asp:TextBox>
                        <label for="tbInsertionUser">Tussenvoegsel:</label><asp:TextBox ID="tbInsertionUser" runat="server"></asp:TextBox>
                        <label for="tbLastnameUser">Achternaam:</label><asp:TextBox ID="tbLastnameUser" runat="server"></asp:TextBox>
                        <label for="tbStreetUser">Straat:</label><asp:TextBox ID="tbStreetUser" runat="server"></asp:TextBox>
                        <label for="tbNumberUser">Huisnummer:</label><asp:TextBox ID="tbNumberUser" runat="server"></asp:TextBox>
                        <label for="tbCityUser">Woonplaats:</label><asp:TextBox ID="tbCityUser" runat="server"></asp:TextBox>
                        <label for="tbBanknrUser">Banknummer:</label><asp:TextBox ID="tbBanknrUser" runat="server"></asp:TextBox>

                        <asp:Button ID="btnCreateUser" class="btn btn-default" runat="server" Text="Gebruiker Aanmaken" OnClick="btnCreateUser_Click"></asp:Button>
                    </div>
			    </main>
            </div>
        </div>
    </form>
</body>
</html>
