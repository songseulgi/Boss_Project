<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paper_write.aspx.cs" Inherits="BTCDProject.paper_write" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="paperstyle.css" />
    <link rel="stylesheet" type="text/css" href="common.css" />
    <title></title>
    <script src="//code.jquery.com/jquery-1.11.3.min.js"></script>
    <script src="//code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script>
        //map 데이터 가져오는 부분
        function receivedata(data1,data2) {
            alert(data1,data2);
            document.getElementById("start1").innerText = data1;
            document.getElementById("end").innerText = data2;
        }

        $(document).ready(function () {
            var sum = 0;
            $("#pay_total1").focusin(function () {
                if ($("#pay_trans1").length > 0) {
                    sum = parseInt($("#pay_trans1").val()) + parseInt($("#pay_toll1").val()) + parseInt($("#pay_room1").val()) + parseInt($("#pay_food1").val()) + parseInt($("#pay_work1").val())
                    $("#pay_total1").val(sum.toString());
                }
            });

            $("#pay_total2").focusin(function () {
                if ($("#pay_trans2").length > 0) {
                    sum = parseInt($("#pay_trans2").val()) + parseInt($("#pay_toll2").val()) + parseInt($("#pay_room2").val()) + parseInt($("#pay_food2").val()) + parseInt($("#pay_work2").val())
                    $("#pay_total2").val(sum.toString());
                }
            });
        });
    </script>
</head>
<body>
    <!-- 명령서 작성페이지 -->
    <form id="form1" runat="server">
        
            <div class='wrapper'>

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
                    <h2 align='center'> 출장 명령서 </h2>
                    
                    <!-- 상단 테이블 -->
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
                            <td> 기안자</td >
                            <td><asp:TextBox ID="name" runat="server"></asp:TextBox></td>
                            <td rowspan="4"> 결</br></br> 재</td >
                            <td> 담당</td >
                            <td> 팀장</td >
                            <td> 대표이사</td >
                        </tr>
                        <tr>
                            <td> 소&nbsp;&nbsp;&nbsp;속 </td>
                            <td><asp:TextBox ID="company" runat="server"></asp:TextBox></td>
                            <td rowspan="3"></ td>
                            <td rowspan="3"></ td>
                            <td rowspan="3"></ td>
                        </tr>
                        <tr>
                            <td> 직&nbsp;&nbsp;&nbsp;위 </td>
                            <td><asp:TextBox ID="position" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <!-- 정산일은 나중에 정한다 -->
                            <td> 정산일</td >
                            <td><input type ="text" /></ td>
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
                            <td colspan="7"><b>여비규정 제4조 규정에 의거 다음과 같이 출장을 명함.</b></ td>
                        </tr>
                        <tr>
                            <td rowspan="5"> 출장</br >복명</ td>
                            <td> 출장기간</td>
                            <td> 출장지</td>
                        </tr>

                        <tr>
                            <td><asp:TextBox ID="term1" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="location1" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td><asp:TextBox ID="term2" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="location2" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td><asp:TextBox ID="term3" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="location3" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td><asp:TextBox ID="term4" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="location4" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td> 목적</br>및</br>내용 </td>
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
                            <td colspan="8"><b>여비규정 제10조의1 규정에 의거 아래와 같이 관련 증빙을 첨부하여 정산함.</b></td >
                        </tr>
                    
                        <tr>
                            <td rowspan="9"> 여비</br >정산</ td>
                            <td colspan="7">
                                <font style=" font-size: 12px;">
                                이동경로(교통수단 :
                                    <asp:CheckBox ID="carCck" runat="server" Text="자차" />
                                    <asp:CheckBox ID="trainCck" runat="server" Text="기차" />
                                    <asp:CheckBox ID="busCck" runat="server" Text="버스" />
                                    <asp:CheckBox ID="boatCck" runat="server" Text="선박" />
                                    <asp:CheckBox ID="airCck" runat="server" Text="항공" />
                                )
                                    <asp:Button ID="mapBtn" runat="server" Text="경로설정" OnClick="mapBtn_Click" />
                                </font>
                            </td>
                        </tr>

                        <tr>
                            <td> 출발</td >
                            <td> 경유</td >
                            <td> 경유</td >
                            <td> 경유</td >
                            <td> 경유</td >
                            <td> 경유</td >
                            <td> 도착</td >
                        </tr>

                        <tr>
                            <td>
                                <asp:TextBox ID="start" runat="server" Text=""></asp:TextBox>
                                <asp:Label ID ="start1" runat ="server" Text =""></asp:Label></br >                       
                            </td>
                            <td>
                                <asp:Label ID ="mid_loc1" runat ="server" Text =""></asp:Label></br >
                                <asp:Label ID ="move_dis1" runat ="server" Text =""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID ="mid_loc2" runat ="server" Text =""></asp:Label></br >
                                <asp:Label ID ="move_dis2" runat ="server" Text =""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID ="mid_loc3" runat ="server" Text =""></asp:Label></br >
                                <asp:Label ID ="move_dis3" runat ="server" Text =""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID ="mid_loc4" runat ="server" Text =""></asp:Label></br >
                                <asp:Label ID ="move_dis4" runat ="server" Text =""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID ="mid_loc5" runat ="server" Text =""></asp:Label></br >
                                <asp:Label ID ="move_dis5" runat ="server" Text =""></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="departure" runat="server" Text=""></asp:TextBox>
                                <asp:Label ID ="end" runat ="server" Text =""></asp:Label></br >
                                <asp:Label ID ="total_dis" runat ="server" Text =""></asp:Label>
                            </td>
                        </tr>
                   
                        <tr>
                            
                            <td> 교통운임</td >
                            <td> 통행료</td >
                            <td> 숙박비</td >
                            <td> 식&nbsp;&nbsp;비 </td>
                            <td> 일&nbsp;&nbsp;비 </td>
                            <td colspan="2"> 계</td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="pay_trans1" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_toll1" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_room1" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_food1" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_work1" runat="server"></asp:TextBox></td>
                            <td colspan="2"><asp:TextBox ID="pay_total1" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="pay_trans2" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_toll2" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_room2" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_food2" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_work2" runat="server"></asp:TextBox></td>
                            <td colspan="2"><asp:TextBox ID="pay_total2" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="pay_trans3" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_toll3" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_room3" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_food3" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="pay_work3" runat="server"></asp:TextBox></td>
                            <td colspan="2"><asp:TextBox ID="pay_total3" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="7"><br /></td >
                        </tr>
                        <tr>
                            <td align="left" colspan="7">
                                <asp:label ID="total_trans" Text="교통운임" runat="server"></asp:label><asp:TextBox ID="memo_pay" runat="server"></asp:TextBox>
                                <asp:label ID="total_room" Text="숙박비" runat="server"></asp:label><asp:TextBox ID="label_memo2" runat="server" />
                                <asp:label ID="total_food" Text="식비" runat="server"></asp:label><asp:TextBox ID="label_memo3" runat="server" />
                                <asp:label ID="total_work" Text="일비" runat="server"></asp:label><asp:TextBox ID="label_memo4" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <font style=" font-size: 12px">
                                        * 첨부 :
                                        <asp:CheckBox ID ="card_bill" runat ="server" text ="신용카드 영수증"/>
                                        <asp:CheckBox ID ="toll_bill" runat ="server" text ="통행료 영수증"/>
                                        <asp:CheckBox ID ="transe_bill" runat ="server" text ="기차/버스/선박/항공기 영수증"/>
                                        <asp:CheckBox ID ="etc_bill" runat ="server" text ="기타 영수증"/>
                                </font>
                            </td>
                        </tr>
                    </table>
                    <!-- 등록의 경우에는 redirect시 무조건 page값을 0으로 보내준다 -->
                    <input type="reset" />
                    <asp:Button ID ="okBtn" runat ="server" Text ="등록" onclick="okBtn_Click"/>
            </div>
        </div>
       
    </form >

</body>
</html>
