<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head><meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><title>
	CalcDistance
</title>
    <script src="https://apis.skplanetx.com/tmap/js?version=1&format=javascript&appKey=79fff6a6-68dd-39f1-a694-e99ed450544a"></script>
    <script src="https://apis.daum.net/maps/maps3.js?apikey=f333e298a02ed205a65a9b5858c36d21&libraries=services"></script>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="http://code.jquery.com/jquery-1.8.2.min.js"></script>
    
    <link rel="stylesheet" type="text/css" href="common.css" />
  
    <script type="text/javascript">

        var map;
        //var lonlat, pr_3857, pr_4326;   // 좌표변환 관련
        var markers;                    // 레이어 관련 변수
        var count = 0;                  // 출발,도착을 확인 하기위한 count
        var startX, startY;
        var endX, endY;
        var start_point, end_point;
        var total_Dis, total_Fare, total_time;

        var resultDis = new Array();
        var resultFare = new Array();
        var resultTime = new Array();

        var sum_dis = 0;
        var sum_fare = 0;
        var sum_time = 0;

        var option = {
            version: "1",
            format: 'xml',
            tollgateFareOption: "2",        // 요금 가중치 정보(2:최적 요금-기본값)
            reqCoordType: "WGS84GEO",       // 좌표계 유형 :  경위도
            speed: "60",
            carType: "1",                   // 자동차 종류(1:승용차, 2:중형승합차, 3:대형승합차, 4:대형화물차, 5:특수화물차, 6: 경차)
            resCoordType: "WGS84GEO"
        }

        //= 14218837.61816
        //= 4563253.74504
        //초기화 함수
        function initTmap() {
            // 4560197.7989427,
            centerLL = new Tmap.LonLat(14145677.4, 4511257.6);
            map = new Tmap.Map({
                div: 'map_div',
                width: '75%',
                height: '600px',
                transitionEffect: "resize",
                animation: true
            });

            map.events.register("click", null, onClickMap);

        };

        function onClickMap(evt) {

            var lonlat = map.getLonLatFromPixel(evt.xy);
            console.log(lonlat.lon);
            console.log(lonlat.lat);

            // 출발점과 도착점 구분
            if (count % 2 == 0) {       // 출발점을 때의 좌표

                startX = lonlat.lon;    //경도
                startY = lonlat.lat;    //위도
                start_point = map.getLonLatFromPixel(evt.xy);
                console.log("시작점 확인 :" + start_point);

            } else {                    // 도착점일 때의 좌표

                endX = lonlat.lon;
                endY = lonlat.lat;
                end_point = map.getLonLatFromPixel(evt.xy);
                console.log("도착점 확인 :" + end_point);
            }

            console.log("시작점 : " + start_point + ", 도착점" + end_point);
            var routeFormat = new Tmap.Format.KML({ extractStyles: true, extractAttributes: true });

            var urlStr = "https://apis.skplanetx.com/tmap/routes?version=1&format=xml";
            urlStr += "&startX=" + startX;
            urlStr += "&startY=" + startY;
            urlStr += "&endX=" + endX;
            urlStr += "&endY=" + endY;
            urlStr += "&appKey=79fff6a6-68dd-39f1-a694-e99ed450544a";

            var prtcl = new Tmap.Protocol.HTTP({
                url: urlStr,
                format: routeFormat
            });

            var routeLayer = new Tmap.Layer.Vector("route", { protocol: prtcl, strategies: [new Tmap.Strategy.Fixed()] });
            routeLayer.events.register("featuresadded", routeLayer, onDrawnFeatures);
            routeLayer.events.register("loadend", routeLayer, onCompleteLoadGetDistanceLonLat);
            var tdata = new Tmap.TData();
            tdata.events.register("onComplete", tdata, onCompleteLoadGetDistanceLonLat);
            map.addLayer(routeLayer);

            count++; 0
            console.log("마우스 클릭 수 : " + count);

            // totalDistance 가져오기
            // Xml Data Parsing
            if (end_point != null) {

                var dis_start = new Tmap.Geometry.Point(startX, startY);
                var dis_end = new Tmap.Geometry.Point(endX, endY);
                var dis_cal = (dis_start.distanceTo(dis_end, option) / 1000).toFixed(2);      // 소수점 2째자리까지 계산

                loadGetDistanceLonLat(lonlat);
            }

        }

        function loadGetDistanceLonLat(lonlat) {
            var tData = new Tmap.TData();

            var Address = tData.getAddressFromLonLat(lonlat, option);
            console.log("주소 : " + Address);
            //console.log("loadGetDistanceLonLat : " + tData.getAddressFromLonLat(lonlat, option));
        }

        function onCompleteLoadGetDistanceLonLat(o) {
            //console.log("onCompleteLoadGetDistanceLonLat");
            var xml = $(this.responseXML);
            /*
             sum_dis : 총 거리값
             sum_fare : 톨비 값
             sum_time : 총 이동시간
            */

            total_Dis = (($(o.response.priv.responseXML).find("totalDistance").text()) / 1000).toFixed(2);
            total_Fare = $(o.response.priv.responseXML).find("totalFare").text();
            total_time = (($(o.response.priv.responseXML).find("totalTime").text()) / 60).toFixed(0);
            //resultDis = parseFloat(total_Dis);

            //console.log(resultDis);
            resultDis.push(total_Dis);
            resultFare.push(total_Fare);
            resultTime.push(total_time);

            sum_dis += Number(resultDis[resultDis.length - 1]);
            sum_fare += Number(resultFare[resultFare.length - 1]);
            sum_time += Number(resultTime[resultTime.length - 1]);

            console.log("sum_dis : " + sum_dis);
            console.log("sum_fare : " + sum_fare);
            console.log("sum_time : " + sum_time);

            //console.log(resultDis);
            //console.log(total_Dis);

            if (count >= 2) {
                var hour;
                console.log("이동거리 : " + sum_dis + "km");
                console.log("총 통행료 : " + sum_fare + "원");
                //console.log(resultDis);

                if (sum_time > 60) {

                    hour = parseInt(sum_time / 60);
                    sum_time = parseInt(sum_time % 60);
                    console.log("총 이동시간 : " + hour + "시간 " + sum_time + "분");

                } else
                    console.log("총 이동시간 : " + sum_time + "분");
            }
            //console.log(jQuery(this.responseXML).find("fullAddress").text())

            document.getElementById("TotalDistance").value = sum_dis; //총 거리
            document.getElementById("TotalFare").value = sum_fare; //총 톨비
            document.getElementById("TotalTime").value = sum_time;  //총 시간
        }

        function onLoadSuccess() {
            var totalDistance = this.loadGetDistanceLonLat();

            console.log(this);
            //document.getElementById('result').value = totalDistance;
            console.log("onLoadSuccess : " + jQuery(this.responseXML).find("totalDistance").text());
        }

        //경로 그리기 후 해당영역으로 줌
        function onDrawnFeatures(e) {
            map.zoomToExtent(this.getDataExtent());
        }

        // 주소 검색 부분

        function geocodingSearch() {

            //markers.clearMarkers();

            var selectSido = jQuery('#selectSido').val();
            var selectGu = jQuery('#selectGu').val();
            var selectDong = jQuery('#selectDong').val();
            var bunji = jQuery('#bunji').val();
            var detailAddress = jQuery("#detailAddress").val();
            var selectSidoName = jQuery("#selectSido option[value=" + $("#selectSido").val() + "] ").text();
            var selectGuName = jQuery("#selectGu option[value=" + $("#selectGu").val() + "] ").text();
            var selectDongName = jQuery("#selectDong option[value=" + $("#selectDong").val() + "] ").text();

            var encodingSido = encodeURIComponent(selectSidoName);
            var encodingGu = encodeURIComponent(selectGuName);
            var encodingDong = encodeURIComponent(selectDongName);
            var encodingBunji = encodeURIComponent(bunji);
            var encodingDetailAddress = encodeURIComponent(detailAddress);

            if (selectSido == '00') {
                alert('검색할 지역을 선택해주세요.')
            } else if (selectGu == '00') {
                alert('검색할 구/군을 선택해주세요.')
            } else if (selectDong == '00') {
                alert('검색할 동을 선택해주세요.')
            } else {
                tData = new Tmap.TData();

                var search = {
                    version: "1",
                    bunji: encodingBunji,
                    coordType: "WGS84GEO"
                }

                var detailSearch = {
                    version: "1",
                    bunji: encodingBunji,
                    detailAddress: encodingDetailAddress,
                    coordType: "WGS84GEO"
                }

                if (detailAddress != '') {
                    tData.getGeoFromAddress(encodingSido, encodingGu, encodingDong, encodingBunji, detailSearch);
                } else {
                    tData.getGeoFromAddress(encodingSido, encodingGu, encodingDong, encodingBunji, search);
                }
                tData.events.register("onComplete", tData, onCompleteLoadGetGeoFromAddress);
                //tData.events.register("onProgress", tData, onProgressLoadPoiData);
                //tData.events.register("onError", tData, onErrorLoadPoiData);


            }
        }

        function newAddressGeocodingSearch() {

            //markers.clearMarkers();

            var selectSidoF02 = jQuery('#selectSidoF02').val();
            var selectGuF02 = jQuery('#selectGuF02').val();
            var bunjiF02 = jQuery('#bunjiF02').val();
            var newAddress = jQuery("#newAddress").val();
            var selectSidoF02Name = jQuery("#selectSidoF02 option[value=" + $("#selectSidoF02").val() + "] ").text();
            var selectGuF02Name = jQuery("#selectGuF02 option[value=" + $("#selectGuF02").val() + "] ").text();
            var newAddress = jQuery('#newAddress').val();
            var bunjiF02 = jQuery('#bunjiF02').val();
            var encodingSido = encodeURIComponent(selectSidoF02Name);
            var encodingGu = encodeURIComponent(selectGuF02Name);
            var encodingDong = encodeURIComponent(newAddress);
            var encodingBunji = encodeURIComponent(bunjiF02);

            if (selectSidoF02 == '00') {

                alert('검색할 지역을 선택해주세요.')

            } else if (selectGuF02 == '00') {

                alert('검색할 구/군을 선택해주세요.')

            } else if (newAddress == '') {

                alert('검색할 도로명 주소를 입력해주세요.')

            } else {

                tData = new Tmap.TData();

                var city = {

                    version: "1",
                    bunji: encodingBunji,
                    addressFlag: "F02", //새주소 검색시 필수 파라미터
                    coordType: "WGS84GEO"

                }

                tData.getGeoFromAddress(encodingSido, encodingGu, encodingDong, encodingBunji, city);
                tData.events.register("onComplete", tData, onCompleteLoadGetGeoFromAddress);
                //tData.events.register("onProgress", tData, onProgressLoadPoiData);
                //tData.events.register("onError", tData, onErrorLoadPoiData);

            }
        }


        function onCompleteLoadGetGeoFromAddress() {

            var size = new Tmap.Size(22, 30);
            var offset = new Tmap.Pixel(-(size.w / 2), -size.h);

            if (jQuery(this.responseXML).find("coordinateInfo").text() != '') {

                var newX = jQuery(this.responseXML).find("lon").text();
                var newY = jQuery(this.responseXML).find("lat").text();
                var x = jQuery(this.responseXML).find("coordinateInfolat").text();
                var y = jQuery(this.responseXML).find("coordinateInfolat").text();
                var city_do = jQuery(this.responseXML).find("city_do").text();
                var gu_gun = jQuery(this.responseXML).find("gu_gun").text();
                var legalDong = jQuery(this.responseXML).find("legalDong").text();
                var bunji = jQuery(this.responseXML).find("bunji").text();
                var newRoadName = jQuery(this.responseXML).find("roadName").text();
                var newBuildngIndex = jQuery(this.responseXML).find("buildngIndex").text();
                var trLonLat = get3857LonLat(x, y)      // 좌표계 변환
                var newTrLonLat = get3857LonLat(newX, newY)
                var icon = new Tmap.IconHtml("<imgsrc=http://map.nate.com/img/contents/ico_spot.png />", size, offset);

                if (newX != '') {

                    var label = new Tmap.Label("&nbsp;주소 : " + city_do + " " + gu_gun + " " + newRoadName + newBuildngIndex);

                    marker = new Tmap.Markers(newTrLonLat, icon, label)
                    markers.addMarker(marker);
                    map.setCenter(newTrLonLat, 15)

                } else {

                    var label = new Tmap.Label("&nbsp;주소 : " + city_do + " " + gu_gun + " " + legalDong + " " + bunji);

                    marker = new Tmap.Markers(trLonLat, icon, label)
                    markers.addMarker(marker);
                    map.setCenter(trLonLat, 15)

                }

                marker.events.register('mouseover', marker, onMarkerOver);
                marker.events.register('mouseout', marker, onMarkerOut);

            } else if (jQuery(this.responseXML).find("error").text() != '') {

                var errorMessage = jQuery(this.responseXML).find("error message").text();

                alert(errorMessage);
            } else {
                alert('제공되지 않는 주소 범위입니다. ');
            }
        }

        function clearMarkers() {
            for (var attr in markers) {
                markers[attr].points = [];
            }

            mapObj.clearOverlays(MARKER_KEY);
            mapObj.clearOverlays(LINE_KEY);
        }



        var addressArray = [

["", "", ""],
["강원도", "춘천시", "효자동"],
["강원도", "춘천시", "석사동"],
["강원도", "춘천시", "퇴계동"],
["강원도", "춘천시", "후평동"],
["강원도", "춘천시", "소양동"],
["강원도", "춘천시", "삼천동"],
["강원도", "춘천시", "조운동"],
["강원도", "춘천시", "교동"],
["강원도", "강릉시", "주문진"],
["강원도", "강릉시", "교동"],
        ];

        var sidoArr = [];
        var guArr = [];
        var dongArr = [];


        jQuery(document).ready(function () {


            var leng = addressArray.length;

            var htmlString = "";

            //시도/구군/동리 배열처리

            for (var i = 1; i < leng; i++) {

                if (addressArray[i][0] != addressArray[(i - 1)][0]) {
                    sidoArr.push(addressArray[i][0])
                    guArr.push([])
                    dongArr.push([])
                }

                if (addressArray[i][1] != addressArray[(i - 1)][1]) {
                    guArr[guArr.length - 1].push(addressArray[i][1])
                    dongArr[dongArr.length - 1].push([])
                }

                if (addressArray[i][2] != addressArray[(i - 1)][2]) {
                    dongArr[dongArr.length - 1][dongArr[dongArr.length - 1].length - 1].push(addressArray[i][2])
                }
            }

            for (var i = 0; i < sidoArr.length; i++) {
                jQuery('#selectSido').append('<option value="' + i + '">' + sidoArr[i] + '</option>')
                jQuery('#selectSidoF02').append('<option value="' + i + '">' + sidoArr[i] + '</option>')
            }

            //지번주소 시도 선택시

            jQuery('#selectSido').change(function () {
                jQuery('#selectGu').html('<option value="00">선택하세요.</option>');
                jQuery('#selectDong').html('<option value="00">선택하세요.</option>');
                jQuery('#bunji').val('');
                jQuery('#detailAddress').val('');
                var selectSido = jQuery('#selectSido').val();
                for (var i = 0; i < guArr[selectSido].length; i++) {
                    jQuery('#selectGu').append('<option value="' + i + '">' + guArr[selectSido][i] + '</option>')
                }
            });

            //지번주소 구군 선택시

            jQuery('#selectGu').change(function () {

                var selectSido = jQuery('#selectSido').val();
                var selectGu = jQuery('#selectGu').val();

                jQuery('#selectDong').html('<option value="00">선택하세요.</option>');
                jQuery('#bunji').val('');
                jQuery('#detailAddress').val('');

                for (var i = 0; i < dongArr[selectSido][selectGu].length; i++) {
                    jQuery('#selectDong').append('<option value="' + i + '">' + dongArr[selectSido][selectGu][i] + '</option>')
                }

            });

            //지번주소 동리 선택시
            jQuery('#selectDong').change(function () {
                jQuery('#bunji').val('');
                jQuery('#detailAddress').val('');
            });

            //새주소 시도 선택시
            jQuery('#selectSidoF02').change(function () {

                jQuery('#selectGuF02').html('<option value="00">선택하세요.</option>');
                jQuery('#newAddress').val('');
                jQuery('#bunjiF02').val('');

                var selectSidoF02 = jQuery('#selectSidoF02').val();
                for (vari = 0; i < guArr[selectSidoF02].length; i++) {
                    jQuery('#selectGuF02').append('<option value="' + i + '">' + guArr[selectSidoF02][i] + '</option>')
                }
            });

            //새주소 구군 선택시
            jQuery('#selectGuF02').change(function () {
                var selectSido = jQuery('#selectSidoF02').val();
                var selectGu = jQuery('#selectGuF02').val();
            });

            //검색 구분 선택시
            jQuery("#selectAddressFlag").change(function () {

                //markers.clearMarkers();
                jQuery("#selectSido [value=00]").attr("selected", "selected")
                jQuery("#selectSidoF02 [value=00]").attr("selected", "selected")
                jQuery('#selectGu').html('<option value="00">선택하세요.</option>');
                jQuery('#selectGuF02').html('<option value="00">선택하세요.</option>');
                jQuery('#selectDong').html('<option value="00">선택하세요.</option>');
                jQuery('#newAddress').val('');
                jQuery('#bunji').val('');
                jQuery('#bunjiF02').val('');
                jQuery('#detailAddress').val('');

                var selectAddressFlag = jQuery('#selectAddressFlag').val();
                if (selectAddressFlag == 'F01') {
                    jQuery('#searchF01').css('display', 'block');
                    jQuery('#searchF02').css('display', 'none');
                } else if (selectAddressFlag == 'F02') {
                    jQuery('#searchF01').css('display', 'none');
                    jQuery('#searchF02').css('display', 'block');
                } else {
                    jQuery('#searchF01').css('display', 'none');
                    jQuery('#searchF02').css('display', 'none');
                }
            });
        });
        //경유지 텍스트박스 추가 부분
        function add_text() {
            // pre_set 에 있는 내용을 읽어와서 처리..
            var div = document.createElement('div');
            div.innerHTML = document.getElementById('pre_set').innerHTML;
            document.getElementById('field').appendChild(div);

            if (div++) {
                var i = 0;
                i++;
                $('input[type="text"]').addClass(i);
            }
        }

        function remove_item(obj) {
            // obj.parentNode 를 이용하여 삭제
            document.getElementById('field').removeChild(obj.parentNode);
        }
        //page_write로 넘겨주는 부분
        function sendData() {
            var data1 = $("#start").val();
            var data2 = $("#end").val();

            window.opener.receivedata(data1,data2);
            self.close();
        }

    
    </script>

</head>
<!--초기화 함수 정의-->
<body onload="initTmap()">

    <!--지도 div 정의-->
    <div id="map_div" style="float:left;"></div>
    

    <%--<p class="point_name">출　발　지</p>
    <input type="text" id="start" value="출발지를 입력하세요." onfocus="this.value=''" style="margin:5px 5px 5px 0"/><input type="button" value=" 추가 " onclick="add_text()">
    <br />

    <div class="field_Box">
    <div id="field"></div><div id="pre_set" style="display:none">
       <p class="point_name">경　유　지</p> <input type="text" name="" value="" style="width:169px;margin-bottom:5px"> <input type="button" value=" 추가 " onclick="add_text()" style="margin-right:5px"><input type="button" value=" 삭제 " onclick="remove_item(this)">
    </div>
    <p class="point_name">도　착　지</p>
    <input type="text" id="end" value="도착지를 입력하세요." onfocus="this.value=''" />
    <br />
    
    <div class="dot_10"></div>
    
    </div>
    
    <p class="point_name">총　거　리</p>
    <input type="text" id="TotalDistance" style="margin:5px 5px 5px 0"/> <br />
    
    <p class="point_name">총 통&nbsp;행&nbsp;료</p>
    <input type="text" id="TotalFare" style="margin:5px 5px 5px 0"/> <br />
    
    <p class="point_name">총 소요시간</p>
    <input type="text" id="TotalTime" style="margin:5px 5px 5px 0"/> <br />
    
    <input type="button" value="등록" onclick="sendData()" />--%>

    <p class="point_name">출　발　지</p>
    <input type="text" id="start" value="출발지를 입력하세요." onfocus="this.value=''" style="margin:5px 200px 5px 0;float:left;"/>
    
    <p class="point_name">경　유　지</p> 
    <input type="text" id="mid_loc1" value="경유지를 입력하세요." onfocus="this.value=''" style="margin:5px 200px 5px 0;float:left;"/> 
    <p class="point_name">　　　　　</p> 
    <input type="text" id="mid_loc2" value="경유지를 입력하세요." onfocus="this.value=''" style="margin:5px 200px 5px 0;float:left;"/> 
    <p class="point_name">　　　　　</p> 
    <input type="text" id="mid_loc3" value="경유지를 입력하세요." onfocus="this.value=''" style="margin:5px 200px 5px 0;float:left;"/> 
    <p class="point_name">　　　　　</p> 
    <input type="text" id="mid_loc4" value="경유지를 입력하세요." onfocus="this.value=''" style="margin:5px 200px 5px 0;float:left;"/> 
    <p class="point_name">　　　　　</p> 
    <input type="text" id="mid_loc5" value="경유지를 입력하세요." onfocus="this.value=''" style="margin:5px 200px 5px 0;float:left;"/> 
    
    <p class="point_name">도　착　지</p>
    <input type="text" id="end" value="도착지를 입력하세요." onfocus="this.value=''"  style="margin:5px 200px 5px 0;float:left;"/>
    
    <div class="dot_10"></div>
    
   
    
    <p class="point_name">총　거　리</p>
    <input type="text" id="TotalDistance" style="margin:5px 200px 5px 0"/> <br />
    
    <p class="point_name">총 통&nbsp;행&nbsp;료</p>
    <input type="text" id="TotalFare" style="margin:5px 200px 5px 0"/> <br />
    
    <p class="point_name">총 소요시간</p>
    <input type="text" id="TotalTime" style="margin:5px 200px 5px 0"/> <br />
    
    <input type="button" value="등록" onclick="sendData()" class="uploadbtn"/>

<!-- Visual Studio Browser Link -->
<script type="application/json" id="__browserLink_initializationData">
    {"appName":"Chrome","requestId":"e3960c0138a6499d8f855bead54ed9b4"}
</script>
<script type="text/javascript" src="http://localhost:6949/ed57e0f026e14e4aa55c6ef052d6bb70/browserLink" async="async"></script>
<!-- End Browser Link -->

</body>
</html>
