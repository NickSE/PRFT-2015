<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventBeheer.aspx.cs" Inherits="WebApplication1.EventBeheer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EventBeheer</title>
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
    <form id="feventbeheer" runat="server">
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
                    <!-- ### evenement zoeken ### -->
                    <div class="form-group">
                        <label for="zoeken_event">Evenement Zoeken:</label><input type="text" id="zoeken_event" />
                        <asp:Button ID="btnZoekenEvenement" class="btn btn-default" runat="server" Text="Zoeken" OnClick="zoekenEvenement_Click"></asp:Button>
                    </div>
                    <!-- ### evenement verwijderen ### -->
                    <div class="form-group">
                        <select name="list_event" size="10">
                            <option value="asd">asd</option>
                            <option value="asd">asd</option>
                            <option value="asd">asd</option>
                            <option value="asd">asd</option>
                        </select>
                        <asp:Button ID="btnVerwijderenEvenement" class="btn btn-default" runat="server" Text="Verwijderen" OnClick="verwijderenEvenement_Click"></asp:Button>
                    </div>
                </aside>
            </div>
            <div class="col-xs-8">
                <main id="eventbeheer" runat="server">
                    <!-- ### evenement aanmaken ### -->
                    <div class="form-group">
                        <label for="naam_event">Naam:</label><input type="text" id="naam_event"/>
                        <label for="koste_event">Koste:</label><input type="text" id="koste_event"/>
                        <label for="van_event">Van:</label><input type="text" id="van_event"/>
                        <label for="tot_event">Tot:</label><input type="text" id="tot_event"/>
                        <label for="beschrijving_event">Beschrijving</label><textarea class="form-control" id="beschrijving_event"></textarea>
                        <asp:Button ID="btnAanmakenEvenement" class="btn btn-default" runat="server" Text="Aanmaken" OnClick="aanmakenEvenement_Click"></asp:Button>
                    </div>
                    <!-- ### Persoon toevoegen aan evenement ### -->
                    <div class="form-group">
                        <label for="naam_persoon">Naam:</label><input type="text" id="naam_persoon"/>
                        <label for="password_persoon">Password:</label><input type="text" id="password_persoon"/>
                        <asp:Button ID="btnToevoegenPersoon" class="btn btn-default" runat="server" Text="Aanmaken" OnClick="toevoegenPersoon_Click"></asp:Button>
                    </div>
                    <!-- ### Persoon zoeken ### -->
                    <div class="form-group">
                        <label for="zoeken_persoon">Persoon Zoeken:</label><input type="text" id="zoeken_persoon"/>
                        <asp:Button ID="btnZoekenPersoon" class="btn btn-default" runat="server" Text="Zoeken" OnClick="zoekenPersoon_Click"></asp:Button>
                    </div>
                    <!-- ### Persoon verwijderen ### -->
                    <div class="form-group">
                        <select name="list_persoon" size="10">
                            <option value="asd">asd</option>
                            <option value="asd">asd</option>
                            <option value="asd">asd</option>
                            <option value="asd">asd</option>
                        </select>
                        <asp:Button ID="btnVerwijderenPersoon" class="btn btn-default" runat="server" Text="Verwijderen" OnClick="verwijderenPersoon_Click"></asp:Button>
                    </div>
			    </main>
            </div>
        </div>
    </form>
</body>
</html>
