﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mediasharing</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css"/>

</head>
<body>
    <div class="form-group">
  <label for="username">Name:</label>
  <input type="text" class="form-control" id="username" runat ="server"/><!-- dit wordt het inloggen (gebruikersnaam)-->
</div>
<div class="form-group">
  <label for="password">Password:</label>
  <input type="password" class="form-control" id="password" runat ="server"/><!-- dit wordt het inloggen(wachtwoord)-->
</div>
    <div><button type="submit" class="btn btn-default" id="btnInlog" runat="server">Log in!</button></div>
    <form id="form1" runat="server">
    <div>
    <div id="JeMoeder" runat="server">Hallo</div>
    </div>
    </form>
    <a href ="Mediasharing.aspx">Mediasharing</a>
</body>
</html>
