<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br/> MachineName:  <%= System.Environment.MachineName %>
        <br/> UserName:  <%= System.Environment.UserName %>
        <br/> LocalAddr:  <%= Request.ServerVariables["LOCAL_ADDR"] %>
        <br/> RemoteAddr:  <%= Request.ServerVariables["REMOTE_ADDR"] %>
        <br/> RemoteHost:  <%= Request.ServerVariables["REMOTE_HOST"] %>
        <br /> IPv4 Local Address:  <asp:Label ID="lblIPv4Address" runat="server" />
    </div>
        <hr />
        <div>
            IP Ping: <asp:TextBox ID="tbRemoteIP" runat="server"></asp:TextBox>  <asp:Button ID="btnPing" runat="server" Text="Ping" OnClick="btnPing_Click" />
            <asp:Label ID="lblPingResult" runat="server" />
        </div>
        <hr />
        <div>
            Traceroute: <asp:TextBox id="tbTraceRouteIP" runat="server"></asp:TextBox> <asp:Button ID="btnTraceRoute" runat="server" Text="TraceRoute" OnClick="btnTraceRoute_Click" />
            <asp:Label ID="lblTraceRouteResult" runat="server"></asp:Label>

        </div>
    </form>
</body>
</html>


