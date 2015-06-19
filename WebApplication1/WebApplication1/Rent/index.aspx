<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.Rent.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rent</title>
    <meta charset="UTF-8" />
    <!-- dit doen we even niet <meta http-equiv="refresh" content="2"/>-->

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="../resource/style/theme.css" />
        <script language="javascript">
        function lbHuur_DoubleClick() {
            /* we will change value of this hidden field so 
                     that in 
                     page load event we can identify event.
                            */
            document.forms[0].lbHuurHidden.value = "doubleclicked";
            document.forms[0].submit();
        }
</script>  
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
                        <asp:TextBox ID="tbZoek" runat="server"></asp:TextBox>
                        <asp:Button ID="btnZoek" class="btn btn-default" runat="server" Text="Zoeken" ></asp:Button>
                    </div>
                    <!-- ### Event Lijst ### -->
                    <div class="form-group">
                        <label >Toon</label>
                        <asp:CheckBox ID="cbAlles" runat="server" />
                        <asp:Label ID="lblAlles" runat="server" Text="Alles"></asp:Label>
                        <asp:CheckBox ID="cbHuurbaar" runat="server" />
                        <asp:Label ID="lblHuurbaar" runat="server" Text="Huurbaar"></asp:Label>
                        <asp:CheckBox ID="cbVerhuurd" runat="server" />
                        <asp:Label ID="lblVerhuurd" runat="server" Text="Verhuurd"></asp:Label>
                    </div>                    
                </aside>
            </div>
            <div class="col-xs-8">
                <main id="eventbeheer" runat="server">
                    <!-- ### listbox ### -->
                    <div class="form-group">
                        <h1>Keuze</h1>                        
                        <label for:"lbHuur">Huur:</label><asp:ListBox ID="lbHuur" ondblclick="lbHuur_DoubleClick()" runat="server"></asp:ListBox>
                        <input type="hidden" name="lbHuurHidden" />
                        
                    </div>
                    <!-- ### Item Huren ### -->
                    <div class="form-group">
                        <h1>Item Huren</h1>
                        <label for="tbItemID">Item ID:</label><asp:TextBox ID="tbItemID" runat="server"></asp:TextBox>
                        <label for="calStart">Startdatum:</label><asp:Calendar ID="calStart" runat="server"></asp:Calendar>
                        <label for="tbBarcode">Barcode:</label><asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>
                        <label for="calEind">Einddatum:</label><asp:Calendar ID="calEind" runat="server"></asp:Calendar>
                        <asp:CheckBox ID="cbVerhuurd2" runat="server" /><label for="cbVerhuurd2">Verhuurd</label>
                        <asp:Button ID="btnHuur" class="btn btn-default" runat="server" Text="Huur" ></asp:Button>
                    </div>
                    <hr />
                    <!-- ### Plek Huren ### -->
                    <div class="form-group">
                        <h1>Plek Huren</h1>
                        <label for="dtpDateStartEvent">Startdatum:</label><asp:Calendar ID="dtpDateStartEvent" runat="server"></asp:Calendar>
                        <label for="dtpDateEndEvent">Einddatum:</label><asp:Calendar ID="dtpDateEndEvent" runat="server"></asp:Calendar>
                        <asp:Button ID="btnHuurPlek" class="btn btn-default" runat="server" Text="Huur" ></asp:Button>
                    </div>                    
                    <hr />                    
			    </main>
            </div>
        </div>
    </form>
</body>
</html>
