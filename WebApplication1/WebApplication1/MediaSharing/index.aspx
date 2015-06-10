<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.Scherm.Mediasharing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Hallo </title>
	<meta charset="UTF-8"/>
	<!-- dit doen we even niet <meta http-equiv="refresh" content="2"/>-->
	
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css"/>
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css"/>
    <link rel="stylesheet" href="../resource/style/theme.css" />
</head>
<body>
	<header class="col-xs-12 container">
		<div class="col-xs-3"> LOGO </div>
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
				Categorieën				
				<select>
					<option>
					    Loading...
					</option>
				</select>
			</aside>
		</div>
		<div class="col-xs-8">
			<main id="media">
			    <article class="new">
                    <form name="_newMessage" method="post" action="index.aspx" id="_newMessage" runat="server">
                        <asp:TextBox ID="_message" placeholder="Bericht" runat="server" required="required"></asp:TextBox>
                        <br />
                        <asp:FileUpload ID="_attachmeht" runat="server" />
                        <br />
                        <asp:Button ID="sendNewMessage" runat="server" Text="Verstuur" />
                    </form>
			    </article>
                <hr />
                <div id="article_list" runat="server">                    
                </div>
			</main>
		</div>
	</div>
</body>
</html>
