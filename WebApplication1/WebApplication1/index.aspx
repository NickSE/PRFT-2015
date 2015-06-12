<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.Scherm.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mediasharing</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css"/>
</head>
<body>
    <div class="col-md-4"></div>
    <form id="login" runat="server" class="col-md-4">
        <p id="error" runat="server" class="bg-danger text-center"></p>
        <div class="form-group">
            <label for="username">Name:</label>
            <input type="text" class="form-control" value="" id="username" runat ="server"/>
        </div>
        <div>
            <button type="submit" name="inloggen" class="btn btn-default btn-block" id="btnInlog" runat="server">Log in!</button>
        </div>  
    </form>
    <div class="col-md-4"></div>
</body>
</html>
