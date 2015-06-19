<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.UserManagement.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GebruikerBeheer</title>
    <meta charset="UTF-8" />
    <!-- dit doen we even niet <meta http-equiv="refresh" content="2"/>-->

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="../resource/style/theme.css" />
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
                        <asp:Button ID="btnInfoUser" class="btn btn-default" runat="server" Text="Toon info" OnClick="btnInfoUser_Click"></asp:Button>
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
                        <label for="tbEmailUser">Email:</label><asp:TextBox ID="tbEmailUser" runat="server"></asp:TextBox>

                        <label for="startdateRes">Startdatum:</label><asp:Calendar ID="startdateRes" runat="server"></asp:Calendar>
                        <label for="enddateRes">Einddatum:</label><asp:Calendar ID="enddateRes" runat="server"></asp:Calendar>
                        <label for="cbPayedRes">Betaald:</label><asp:CheckBox ID="cbPayedRes" runat="server"></asp:CheckBox>

                        <asp:Button ID="btnCreateUser" class="btn btn-default" runat="server" Text="Gebruiker Aanmaken" OnClick="btnCreateUser_Click"></asp:Button>
                    </div>
			    </main>
            </div>
        </div>
    </form>
</body>
</html>
