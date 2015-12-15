<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paper_result.aspx.cs" Inherits="BTCDProject.paper_result" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="paperstyle.css" />
    <link rel="stylesheet" type="text/css" href="common.css" />
    <link rel="stylesheet" type="text/css" href="print.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class='wrapper'>

            <!-- 헤더 -->
            <div class="header">
                <div id="img">
                    <!-- img는 common.css에서 처리해준다 -->
                </div>
                <div id="status">
                    <asp:Label ID="userLbl" CssClass="idLbl" Text="손님Label" runat="server"></asp:Label><asp:Label CssClass="idLbl" Text="님&nbsp; |" runat="server"></asp:Label>
                    <asp:Button ID="logoutBtn" Text="로그아웃" runat="server" OnClick="logoutBtn_Click" />
                </div>
            </div>

            <!-- 네비 -->
            <div class="nav">
                <div class="btn">
                    <asp:ImageButton ID="writeB" ImageUrl="~/img/write.png" runat="server" OnClick="writeBtn_Click" />
                </div>
                <div class="btn">
                    <asp:ImageButton ID="listB" ImageUrl="~/img/list.png" runat="server" OnClick="listBtn_Click" />
                </div>
            </div>

            <!-- 컨텐트 -->
            <div class="content">
                <h2>출장 명령서</h2>

                <!-- 상단 테이블 -->
                <asp:HiddenField ID="idHid" runat="server" />
                <asp:HiddenField ID="noHid" runat="server" />
                <table border="1">
                    <colgroup>
                        <col width="10%" />
                        <col width="25%" />
                        <col width="5%" />
                        <col width="20%" />
                        <col width="20%" />
                        <col width="20%" />
                    </colgroup>
                    <tr>
                        <td>기안자</td>
                        <td>
                            <asp:TextBox ID="name" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td rowspan="4">결</br ></br> 재</td>
                        <td>담당</td>
                        <td>팀장</td>
                        <td>대표이사</td>
                    </tr>
                    <tr>
                        <td>소&nbsp;&nbsp;&nbsp;속 </td>
                        <td>
                            <asp:TextBox ID="company" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td rowspan="3"></td>
                        <td rowspan="3"></td>
                        <td rowspan="3"></td>
                    </tr>
                    <tr>
                        <td>직&nbsp;&nbsp;&nbsp;위 </td>
                        <td>
                            <asp:TextBox ID="position" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <!-- 정산일은 나중에 정한다 -->
                        <td>정산일</td>
                        <td>
                            <input type="text" /></td>
                    </tr>
                </table>

                <!-- 중단 테이블 -->
                <table border="1">
                    <colgroup>
                        <col width="10%" />
                        <col width="45%" />
                        <col width="45%" />
                    </colgroup>
                    <tr>
                        <td colspan="7"><b>여비규정 제4조 규정에 의거 다음과 같이 출장을 명함.</b></td>
                    </tr>
                    <tr>
                        <td rowspan="5">출장</br >복명</td>
                        <td>출장기간</td>
                        <td>출장지</td>
                    </tr>

                    <tr>
                        <td>
                            <asp:TextBox ID="term1" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="location1" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:TextBox ID="term2" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="location2" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:TextBox ID="term3" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="location3" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:TextBox ID="term4" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="location4" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td>목적</br>및</br>내용 </td>
                        <td colspan="4">
                            <asp:TextBox ID="memo1" mode="multiline" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <!-- 하단 테이블 -->
                <table border="1">
                    <colgroup>
                        <col width="10%" />
                        <col width="12.85%" />
                        <col width="12.85%" />
                        <col width="12.85%" />
                        <col width="12.85%" />
                        <col width="12.85%" />
                        <col width="12.85%" />
                        <col width="12.85%" />
                    </colgroup>
                    <tr>
                        <td colspan="8"><b>여비규정 제10조의1 규정에 의거 아래와 같이 관련 증빙을 첨부하여 정산함.</b></td>
                    </tr>
                    <tr>
                        <td rowspan="9">여비</br >정산</td>
                        <td colspan="7">
                            <font style="font-size: 12px;">
                            이동경로(교통수단 :
                                <asp:CheckBox ID ="carCck" GroupName ="transport" Text ="자차" runat ="server" Enabled="false">
                                </asp:CheckBox>
                                <asp:CheckBox ID ="trainCck" GroupName ="transport" Text ="기차" runat ="server" Enabled="false">
                                </asp:CheckBox>
                                <asp:CheckBox ID ="busCck" GroupName ="transport" Text ="버스" runat ="server" Enabled="false">
                                </asp:CheckBox>
                                <asp:CheckBox ID ="boatCck" GroupName ="transport" Text ="선박" runat ="server" Enabled="false">
                                </asp:CheckBox>
                                <asp:CheckBox ID ="airCck" GroupName ="transport" Text ="항공" runat="server" Enabled="false">
                                </asp:CheckBox>
                            )
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td>출발</td>
                        <td>경유</td>
                        <td>경유</td>
                        <td>경유</td>
                        <td>경유</td>
                        <td>경유</td>
                        <td>도착</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="start1" runat="server" Text="plaintext"></asp:Label></br >                       
                        </td>
                        <td>
                            <asp:Label ID="mid_loc1" runat="server" Text=""></asp:Label></br >
                            <asp:Label ID="move_dis1" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="mid_loc2" runat="server" Text=""></asp:Label></br >
                            <asp:Label ID="move_dis2" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="mid_loc3" runat="server" Text=""></asp:Label></br >
                            <asp:Label ID="move_dis3" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="mid_loc4" runat="server" Text=""></asp:Label></br >
                            <asp:Label ID="move_dis4" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="mid_loc5" runat="server" Text=""></asp:Label></br >
                            <asp:Label ID="move_dis5" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="end" runat="server" Text=""></asp:Label></br >
                            <asp:Label ID="total_dis" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>교통운임</td>
                        <td>통행료</td>
                        <td>숙박비</td>
                        <td>식&nbsp;&nbsp;비 </td>
                        <td>일&nbsp;&nbsp;비 </td>
                        <td colspan="2">계</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="pay_trans1" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_toll1" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_room1" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_food1" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_work1" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td colspan="2">
                            <asp:TextBox ID="pay_total1" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="pay_trans2" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_toll2" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_room2" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_food2" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_work2" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td colspan="2">
                            <asp:TextBox ID="pay_total2" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="pay_trans3" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_toll3" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_room3" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_food3" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="pay_work3" runat="server" ReadOnly="true"></asp:TextBox></td>
                        <td colspan="2">
                            <asp:TextBox ID="pay_total3" runat="server" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="7"></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="7">
                            <asp:Label ID="total_trans" Text="교통운임" runat="server"></asp:Label><asp:TextBox ID="label_memo1" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:Label ID="total_room" Text="숙박비" runat="server"></asp:Label><asp:TextBox ID="label_memo2" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:Label ID="total_food" Text="식비" runat="server"></asp:Label><asp:TextBox ID="label_memo3" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:Label ID="total_work" Text="일비" runat="server"></asp:Label><asp:TextBox ID="label_memo4" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <font style="font-size: 12px">
                                    * 첨부 :
                                    <asp:CheckBox ID ="card_bill" runat ="server" text ="신용카드 영수증" Enabled="false"/>
                                    <asp:CheckBox ID ="toll_bill" runat ="server" text ="통행료 영수증" Enabled="false"/>
                                    <asp:CheckBox ID ="transe_bill" runat ="server" text ="기차/버스/선박/항공기 영수증" Enabled="false"/>
                                    <asp:CheckBox ID ="etc_bill" runat ="server" text ="기타 영수증" Enabled="false"/>
                            </font>
                        </td>
                    </tr>
                </table>
                <div class="btn_group">
                    <!-- 취소의 경우에는 redirect시 page값을 이전 페이지에서 받아온 값으로 다시 보내준다 -->
                    <asp:Button ID="backBtn" runat="server" Text="목록으로" OnClick="backBtn_Click" />
                    <!-- 등록의 경우에는 redirect시 무조건 page값을 0으로 보내준다 -->
                    <button class="print" value="출력" onclick="window.print()" />
                </div>
            </div>
        </div>
    </form>

</body>
</html>
