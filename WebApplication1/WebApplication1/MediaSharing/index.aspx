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
					<li id="user" runat="server">Profile</li>
					<li>Logout</li>
				</ul>
			</nav>
		</div>
	</header>
	<div class="container">
		<div class="col-xs-4">
			<aside>
				Categorieën				
				<select id="categories" runat="server">
				</select>
			</aside>
		</div>
		<div class="col-xs-8">
			<main id="media">
			    <article class="new">
                    <ul class="nav nav-tabs" id="newCon">
                        <li class="active"><a href="#message">Bericht</a></li>
                        <li><a href="#file">Bestand</a></li>
                        <li><a href="#category">Categorie</a></li>
                    </ul>

                    <form runat="server" id="newMessage">
                    <div class="tab-content">
                        <div class="tab-pane active" id="message">
                            <div class="form-group">
                                <label for="titel">Titel</label>
                                <input class="form-control" type="text" id="titel" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="inhoud">Bericht</label>
                                <input class="form-control" type="text" id="inhoud" runat="server" />
                            </div>
                                <input class="btn btn-primary btn-lg btn-block" type="submit" runat="server" id="sendMessage" value="Verstuur" />
                        </div>
                        <div class="tab-pane" id="file">
                            <div class="form-group">
                                <label for="filePath">Bestand</label>
                                <asp:FileUpload runat="server" id="filePath" class="form-control" />
                            </div>
                                <input class="btn btn-primary btn-lg btn-block" type="submit" runat="server" id="sendFile" value="Verstuur" />
                        </div>
                        <div class="tab-pane" id="category">
                            <div class="form-group">
                                <label for="cat">Naam</label>
                                <input class="form-control" type="text" id="cat" runat="server" />
                            </div>
                                <input class="btn btn-primary btn-lg btn-block" type="submit" runat="server" id="sendCategory" value="Verstuur" />
                        </div>
                    </div>
                    </form>
			    </article>
                <hr />
                <div id="article_list" runat="server">                    
                </div>
			</main>
		</div>
	</div>


    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <script src="http://getbootstrap.com/dist/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('#newCon a').click(function (e) {
            e.preventDefault()
            $(this).tab('show')
        });

        function Like(id){
            call(id, "Like");
        }

        function Report(id) {
            call(id, "Report");
        }

        function React(id) {
            call(id, "React");
        }

        function call(id, method) {
            $.ajax({
                type: "POST",
                url: "index.aspx/"+method,
                data: '{id: ' + id + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d);
                }
            });
        }
    </script>
</body>
</html>
