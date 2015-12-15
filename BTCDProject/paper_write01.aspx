<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paper_write01.aspx.cs" Inherits="BTCDProject.paper_write01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="calendar.css" />
    <link rel="stylesheet" type="text/css" href="common.css" />
    <title></title>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.8.18/themes/base/jquery-ui.css" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script src="http://code.jquery.com/ui/1.8.18/jquery-ui.min.js"></script>
    <script src="https://apis.daum.net/maps/maps3.js?apikey=f333e298a02ed205a65a9b5858c36d21&libraries=services"></script>
  
    <script>
        $(document).ready(function () {
            $("#cal_Date1").datepicker({
                showOn: 'button',
                //buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: 'img/calendar.png',
                dateFormat: 'yy/mm/dd',
                constrainInput: true

            });
            $(".ui-datepicker-trigger").mouseover(function () {
                $(this).css('cursor', 'pointer');
            });
            $("#cal_Date2").datepicker({
                showOn: 'button',
                //buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: 'img/calendar.png',
                dateFormat: 'yy/mm/dd',
                constrainInput: true
            });

        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="Container">
            <div class="headerwrap">
                <div class="Top_bar">
                    <a href="" class="com_logo"></a>
                    <div id="status">
                        <asp:Button ID="logoutBtn" Text="로그아웃" runat="server" CssClass="out_btn" />
                        <asp:Label CssClass="idLbl" Text="&nbsp;님&nbsp;|&nbsp;" runat="server"></asp:Label><asp:Label ID="userLbl" CssClass="idLbl" Text="송슬기" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="navimenu">
                <div class="menu_title">
                    <a href="" class="write_icon"></a>
                    <a href="" class="list_icon"></a>
                </div>
            </div>
            <div class="contentBox">
                <div class="content">
                    <div class="content_title">출 장 명 령 서</div>
                    <div class="content_att">
                        <div class="label">1. 출장 기간을 선택하세요.</div>
                        <div class="yearsearch">
                            <asp:TextBox ID="cal_Date1" CssClass="dateresult" runat="server"></asp:TextBox>
                        </div>
                        <span>~</span>
                        <div class="yearsearch">
                            <asp:TextBox ID="cal_Date2" CssClass="dateresult" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content_att">
                        <div class="label">2.교통수단을 체크하세요.</div>
                        <div class="expense_select">
                            <asp:CheckBox ID="carCck" runat="server" /><span>자차</span>
                            <asp:CheckBox ID="trainCck" runat="server" /><span>기차</span>
                            <asp:CheckBox ID="busCck" runat="server" /><span>버스</span>
                            <asp:CheckBox ID="boatCck" runat="server" /><span>선박</span>
                            <asp:CheckBox ID="airCck" runat="server" /><span>항공</span>
                        </div>
                    </div>
                    <div class="content_att">
                        <div class="label">3. 출장지를 입력하세요.</div>
                        <div id="map"></div>
                        <script type="text/javascript" src="//apis.daum.net/maps/maps3.js?apikey=f333e298a02ed205a65a9b5858c36d21"></script>
                        <script>
                            var container = document.getElementById('map');
                            var options = {
                                center: new daum.maps.LatLng(33.450701, 126.570667),
                                level: 3
                            };

                            var map = new daum.maps.Map(container, options);
                        </script>
                        <div class="map_search">
                            <asp:TextBox ID="search_text" CssClass="" runat="server"></asp:TextBox>
                            <asp:Button ID="search_btn" Text="검 색" CssClass="searchbtn btnhover" runat="server" />
                        </div>
                        <div class="map_result"></div>
                    </div>
                    <div class="content_att">
                        <div class="label">4. 여비 항목을 입력하세요. (동승자가 있을 경우 추가 입력 가능.) </div>
                        <div class="expensewrap">
                            <div class="select_result">
                                <div class="label">이름</div>
                                <div class="label">직급</div>
                                <div class="label">통행료</div>
                                <div class="label">숙박비</div>
                                <div class="label">식비</div>
                                <div class="label">일비</div>
                            </div>
                            <div class="select_result">
                                <asp:TextBox ID="user_name" runat="server"></asp:TextBox>
                                <asp:TextBox ID="position" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_toll1" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_room1" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_food1" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_work1" runat="server"></asp:TextBox>
                            </div>
                            <div class="select_result">
                                <asp:TextBox ID="user_name2" runat="server"></asp:TextBox>
                                <asp:TextBox ID="position2" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_toll2" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_room2" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_food2" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_work2" runat="server"></asp:TextBox>
                            </div>
                            <div class="select_result">
                                <asp:TextBox ID="user_name3" runat="server"></asp:TextBox>
                                <asp:TextBox ID="position3" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_toll3" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_room3" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_food3" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_work3" runat="server"></asp:TextBox>
                            </div>
                            <div class="select_result">
                                <asp:TextBox ID="user_name4" runat="server"></asp:TextBox>
                                <asp:TextBox ID="position4" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_toll4" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_room4" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_food4" runat="server"></asp:TextBox>
                                <asp:TextBox ID="pay_work4" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="content_att">
                        <div class="label">5. 영수증을 체크하세요.(파일 첨부 가능.)</div>
                        <div class="expense_select">
                            <asp:CheckBox ID="card_bill" runat="server" /><span>신용카드 영수증</span>
                            <asp:CheckBox ID="toll_bill" runat="server" /><span>통행료 영수증</span>
                            <asp:CheckBox ID="transe_bill" runat="server" /><span>기차/버스/선박/항공기 영수증</span>
                            <asp:CheckBox ID="etc_bill" runat="server" /><span>기타 영수증</span>
                        </div>
                        <div class="bill_upload">
                            <asp:FileUpload ID="billload1" runat="server" />
                            <asp:FileUpload ID="billload2" runat="server" />
                            <asp:FileUpload ID="billload3" runat="server" />
                            <asp:FileUpload ID="billload4" runat="server" />
                            <asp:FileUpload ID="billload5" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="bottom_btn">
                    <asp:Button ID="save" runat="server" Text="등록" Cssclass="save_btn btnhover"/>
                    <asp:Button ID="cancel" runat="server" Text="취소" Cssclass="cancel_btn btnhover" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
