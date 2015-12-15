<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BTCDProject.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="loginstyle.css" />
    <title>출장명령시스템 - 로그인</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="loginDiv">
                <table>
                    <colgroup>
                        <col width="20%" />
                        <col width="80%" />
                    </colgroup>
                    <tr>
                        <td colspan="2">
                            <div id="img"></div>
                        </td>
                    </tr>
                    <tr>
                        <td> ID </td>
                        <td> <asp:textBox ID="idBox" runat="server" placeholder="아이디를 입력해주세요" CssClass="input"></asp:textBox></br> </td>
                    </tr>
                    <tr>
                        <td> PW </td>
                        <td> <asp:textBox ID="pwBox" TextMode="Password" runat="server" placeholder="패스워드를 입력해주세요" CssClass="input"></asp:textBox></br> </td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:label id="warnLabel" runat="server" CssClass="lbl"></br></asp:label></td>
                    </tr>
                    <tr>
                        <td colspan="2"> 
                            <!--<asp:Button ID="joinBtn" CssClass="btn" runat="server" Text="회원가입" /> -->
                            <asp:Button ID="okBtn" CssClass="btn" runat="server" Text="로그인" OnClick="okBtn_Click" /> 
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
