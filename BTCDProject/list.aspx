<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="BTCDProject.list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="~/common.css" />
    <!-- 리스트에 스타일 적용 -->
    <title>출장목록</title>
</head>
<body>
    <!-- 주로 aspx.cs에서 작업하게됩니다 -->
    <form id="form1" runat="server">
        <div class="wrapper">

            <!-- 헤더 -->
            <div class="header">
                <div id="img">
                    <!-- img는 common.css에서 처리해준다 -->
                </div>
                <div id="status">
                    <asp:Label ID="userLbl" CssClass="idLbl" Text="손님Label" runat="server"></asp:Label><asp:Label CssClass="idLbl" text="님&nbsp; |" runat="server"></asp:Label>
                    <asp:Button ID="logoutBtn" Text="로그아웃" runat="server" OnClick="logoutBtn_Click"/>
                </div>
            </div>

            <!-- 네비 -->
            <div class="nav">
                <div class="btn">
                    <asp:ImageButton ID="writeB" ImageUrl="~/img/write.png" runat="server" OnClick="writeBtn_Click"/>
                </div>
                <div class="btn">
                    <asp:ImageButton ID="listB" ImageUrl="~/img/list.png" runat="server" OnClick="listBtn_Click"/>
                </div>
            </div>

            <!-- 컨텐츠 -->
            <div class="content">
                <!-- table은 datagrid를 사용해 구현 -->
                <asp:Label ID="tableLbl" CssClass="lbl" Text="Business Travel Command Document" runat="server" /><br /><br />
                            <asp:DataGrid ID="DataGridt" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="None" OnPageIndexChanged="DataGridt_PageIndexChanged1">
                                <AlternatingItemStyle BackColor="#F3F3F5" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="row_num" HeaderText="글번호" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    
                                    <asp:HyperLinkColumn DataTextField="term1" HeaderText="출장일" DataNavigateUrlField="report_id" DataNavigateUrlFormatString="paper_result.aspx?report_id={0}" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="400px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:HyperLinkColumn>
                                    <asp:BoundColumn DataField="location1" HeaderText="출장지" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            </asp:DataGrid>
            </div>

        </div>
    </form>
</body>
</html>
