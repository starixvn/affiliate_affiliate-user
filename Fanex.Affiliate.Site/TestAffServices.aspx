<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAffServices.aspx.cs" Inherits="Fanex.Affiliate.Site.TestAffServices" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Affiliate's Services</title>
</head>
<body>
    <h2>Affiliate's Services</h2>
    <h3>1. Affiliate's Email Information</h3>
    <form id="form1" runat="server">
    <div>
        <table border="0">
            <tr>
                <td style="width:100px; text-align:right; font-weight:bold;">Site Name:</td>
                <td style="width:200px;"><input id="siteName" runat="server" type="text" style="width: 200px;" /></td>
            </tr>
            <tr>
                <td style="width:100px; text-align:right; font-weight:bold;">Btag:</td>
                <td style="width:200px;"><input id="bTag" runat="server" type="text" style="width: 200px;" /></td>
            </tr>
            <tr>
                <td style="width:100px; text-align:right;"></td>
                <td style="width:200px;"><asp:Button ID="btnSubmit" runat="server" Text="Invoke Service" OnClick="GetAffiliateEmailInfo" /></td>
            </tr>
        </table>
    </div>
    </form>
    <u><b>Returned XML:</b></u> <br />
    <table border="0">
        <tr>
            <td style="width:100px; text-align:right; font-weight:bold;">Affiliate ID:</td>
            <td style="width:200px;"><label id="returnedAffID" runat="server" style="color:Red; font-weight:bold;" /></td>
        </tr>
        <tr>
            <td style="width:100px; text-align:right; font-weight:bold;">Full Name:</td>
            <td style="width:200px;"><label id="returnedFullName" runat="server" style="color:Blue; font-weight:bold;" /></td>
        </tr>
        <tr>
            <td style="width:100px; text-align:right; font-weight:bold;">Username:</td>
            <td style="width:120px;"><label id="returnedUsername" runat="server" style="color:Blue; font-weight:bold;" /></td>
        </tr>
        <tr>
            <td style="width:100px; text-align:right; font-weight:bold;">Email:</td>
            <td style="width:120px;"><label id="returnedEmail" runat="server" style="color:Blue; font-weight:bold;" /></td>
        </tr>
    </table>
</body>
</html>
