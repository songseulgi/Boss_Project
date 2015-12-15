<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="join.aspx.cs" Inherits="BTCDProject.join" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- jQuery를 이용해서 keypress가 일어났을 경우에는 항상 flag를 0으로 바꿔준다  -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script>

        // 회원가입시 id, 비밀번호의 유효성 검사
        // 이름은 2글자~10글자의 제한을 둔다
        // id는 크게 글자 수 제한을 두지 않는다
        // 
        $(document).ready(function () {

            $("#nameBox").focusout(function () {
                if ($("#nameBox").val().length > 1 && $("#nameBox").val().length < 11) {
                    $("#nameLbl").text('1');
                }
                else {
                    $("#nameLbl").text('0');
                }
            });

            $("#idBox").keydown(function () {       // ID에 대한 flag가 1이 되더라도
                $("#idLbl").text('0');              // idBox에 키보드 조작이 생기면
            });                                     // flag를 다시 0으로 바꿔준다



        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="nateLbl" runat="server"></asp:Label>
            <table>
                <colgroup>
                    <col width="20%" />
                    <col width="60%" />
                    <col width="10%" />
                    <col width="10%" />
                </colgroup>

                <tr>
                    <td>Name </td>
                    <td>
                        <asp:TextBox ID="nameBox" runat="server"></asp:TextBox>
                    </td>
                    <asp:HiddenField ID="nameFlag" runat="server" Value="0" />
                    <td>
                        <asp:Label ID="nameLbl" runat="server" Text="0"></asp:Label>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td>ID </td>
                    <td>
                        <asp:TextBox ID="idBox" runat="server"></asp:TextBox>
                    </td>
                    <asp:HiddenField ID="idFlag" runat="server" Value="0" />
                    <td>
                        <asp:Label ID="idLbl" runat="server" Text="1"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="chkBtn" runat="server" Text="중복확인" OnClick="chkBtn_Click" />
                    </td>
                </tr>

                <tr>
                    <td>PW </td>
                    <td>
                        <asp:TextBox ID="pwBox" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <asp:HiddenField ID="pwFlag" runat="server" Value="0" />
                    <td>
                        <asp:Label ID="pwLbl" runat="server" Text="1"></asp:Label></td>
                </tr>

                <tr>
                    <td>PW 재확인</td>
                    <td>
                        <asp:TextBox ID="pwrBox" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="pwrLbl" runat="server" Text="1"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Button ID="okBtn" runat="server" Text="회원등록" OnClick="okBtn_Click" />
        </div>
    </form>
</body>
</html>
