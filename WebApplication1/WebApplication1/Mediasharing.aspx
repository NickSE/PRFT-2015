<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mediasharing.aspx.cs" Inherits="WebApplication1.Mediasharing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Hallo </title>
	<meta charset="UTF-8"/>
	<!-- dit doen we even niet <meta http-equiv="refresh" content="2"/>-->
	
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css"/>
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css"/>
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
					Hallo
					</option>
				</select>
			</aside>
		</div>
		<div class="col-xs-8">
			<main id="media" runat ="server">
			xx
			</main>
		</div>
	</div>
    <div class="form-group">
  <label for="comment">Comment:</label>
  <textarea class="form-control" rows="5" id="comment"></textarea>
</div>   
</body>
</html>
