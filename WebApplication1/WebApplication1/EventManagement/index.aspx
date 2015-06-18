<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.EventManagement.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EventBeheer</title>
    <meta charset="UTF-8" />
    <!-- dit doen we even niet <meta http-equiv="refresh" content="2"/>-->

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="../StyleSheet.css" />
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
                    <!-- ### Locatie Lijst ### -->
                    <div class="form-group">
                        <label for="lbLocation">Locaties:</label>
                        <asp:ListBox ID="lbLocation" runat="server"></asp:ListBox>
                        <asp:Button ID="btnRemoveLocation" class="btn btn-default" runat="server" Text="Locatie Verwijderen" OnClick="btnRemoveLocation_Click"></asp:Button>
                    </div>
                    <!-- ### Event Lijst ### -->
                    <div class="form-group">
                        <label for="lbEvent">Events:</label>
                        <asp:ListBox ID="lbEvent" runat="server"></asp:ListBox>
                        <asp:Button ID="btnRemoveEvent" class="btn btn-default" runat="server" Text="Event Verwijderen" OnClick="btnRemoveEvent_Click"></asp:Button>
                    </div>
                    <!-- ### Plek Lijst ### -->
                    <div class="form-group">
                        <label for="lbPlek">Plekken:</label>
                        <asp:ListBox ID="lbPlek" runat="server"></asp:ListBox>
                        <asp:Button ID="btnRemovePlek" class="btn btn-default" runat="server" Text="Plek Verwijderen" OnClick="btnRemovePlek_Click"></asp:Button>
                    </div>
                </aside>
            </div>
            <div class="col-xs-8">
                <main id="eventbeheer" runat="server">
                    <!-- ### Locatie aanmaken ### -->
                    <div class="form-group">
                        <h1>Locatie aanmaken</h1>
                        <label for="tbNameLocation">Naam:</label><asp:TextBox ID="tbNameLocation" runat="server"></asp:TextBox>
                        <label for="tbStreetLocation">Straat:</label><asp:TextBox ID="tbStreetLocation" runat="server"></asp:TextBox>
                        <label for="tbNrLocation">Nr:</label><asp:TextBox ID="tbNrLocation" runat="server"></asp:TextBox>
                        <label for="tbPostcodeLocation">Postcode:</label><asp:TextBox ID="tbPostcodeLocation" runat="server"></asp:TextBox>
                        <label for="tbCityLocation">Plaats:</label><asp:TextBox ID="tbCityLocation" runat="server"></asp:TextBox>

                        <asp:Button ID="btnCreateLocation" class="btn btn-default" runat="server" Text="Locatie Aanmaken" OnClick="btnCreateLocation_Click"></asp:Button>
                    </div>
                    <hr />
                    <!-- ### Event aanmaken ### -->
                    <div class="form-group">
                        <h1>Event aanmaken</h1>
                        <label for="dbLocationEvent">Locatie:</label><asp:DropDownList ID="dbLocationEvent" runat="server"></asp:DropDownList>
                        <label for="tbNameEvent">Naam:</label><asp:TextBox ID="tbNameEvent" runat="server"></asp:TextBox>

                        <label for="dtpDateStartEvent">Startdatum:</label><asp:Calendar ID="dtpDateStartEvent" runat="server"></asp:Calendar>
                        <label for="dtpDateEndEvent">Einddatum:</label><asp:Calendar ID="dtpDateEndEvent" runat="server"></asp:Calendar>

                        <label for="tbMaxEvent">Maximale bezoekers:</label><asp:TextBox ID="tbMaxEvent" runat="server"></asp:TextBox>

                        <asp:Button ID="btnCreateEvent" class="btn btn-default" runat="server" Text="Event Aanmaken" OnClick="btnCreateEvent_Click"></asp:Button>
                    </div>
                    <hr />
                    <!-- ### plek aanmaken ### -->
                    <div class="form-group">
                        <h1>Plek aanmaken</h1>
                        <label for="dbLocationPlek">Locatie:</label><asp:DropDownList ID="dbLocationPlek" runat="server"></asp:DropDownList>
                        <label for="tbNrPlek">Nr:</label><asp:TextBox ID="tbNrPlek" runat="server"></asp:TextBox>
                        <label for="tbCapacityPlek">Capaciteit:</label><asp:TextBox ID="tbCapacityPlek" runat="server"></asp:TextBox>

                        <asp:ListBox ID="lbSpecificationPlek" runat="server"></asp:ListBox>
                        <asp:Button ID="btnRemoveSpecificationPlek" class="btn btn-default" runat="server" Text="Specificatie Verwijderen" OnClick="btnRemoveSpecificationPlek_Click"></asp:Button>

                        <label for="dbSpecificationPlek">Specificatie:</label><asp:DropDownList ID="dbSpecificationPlek" runat="server"></asp:DropDownList>
                        <label for="tbValuePlek">Waarde:</label><asp:TextBox ID="tbValuePlek" runat="server"></asp:TextBox>
                        <asp:Button ID="btnAddSpecificationPlek" class="btn btn-default" runat="server" Text="Specificatie Toevoegen" OnClick="btnAddSpecificationPlek_Click"></asp:Button>

                        <asp:Button ID="btnCreatePlek" class="btn btn-default" runat="server" Text="Plek Aanmaken" OnClick="btnCreatePlek_Click"></asp:Button>
                    </div>
			    </main>
            </div>
        </div>
    </form>
</body>
</html>
