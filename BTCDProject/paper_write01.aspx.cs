using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace BTCDProject
{
    public partial class paper_write01 : System.Web.UI.Page
    {
        public string user_id = "";
        public string page_num = "";
        public string id_value = "";

        // 페이지 로드되었을 때 function
        protected void Page_Load(object sender, EventArgs e)
        {

            // 세션의 값이 존재하는지 확인 -> 즉 로그인이 되어있는가 확인
            if (Session["user_id"] != null)                       // 세션에 값이 존재하면 (로그인이 되어있다)
            {
                user_id = Session["user_id"].ToString(); // user_id 변수에 Session의 user_id값을 복사한다
                id_value = Session["id"].ToString();
            }
            else                                                  // 세션에 값이 들어있지 않다면 (로그인 실패)
            {
                Response.Redirect("./login.aspx");                // 로그인창으로 redirect를 걸어준다
            }

            page_num = Request.QueryString["page_num"]; // 글을 쓰는 해당 사용자가 이전에 위치해 있었던 list페이지의 값 저장
            userLbl.Text = id_value;
        }

        // 등록 버튼 클릭시 실행되는 function -> 좀 길게 많이 진행되어야 할 부분이다
        protected void okBtn_Click(object sender, EventArgs e)
        {
            string max = "";
            string source = @"Server=localhost;uid=sa;pwd=Sb11011101;database=ReportDB";

            //1. db 연결
            SqlConnection conn = new SqlConnection(source); // 연결 객체 생성
            conn.Open();

            //2. 명령어
            string sql = "INSERT INTO REPORTTBL( USER_ID, NAME, NAME2, CAL_DATE1, CAL_DATE2, POSITION, POSITION2, POSITION3, POSITION4, PAY_TRANS1, PAY_TRANS2" + " PAY_ROOM1, PAY_ROOM2, PAY_TOLL1, PAY_TOLL2, PAY_ROOM1, PAY_ROOM2, PAY_FOOD1, PAY_FOOD2, PAY_WORK1, PAY_WORK2, LABEL_MEMO1, LABEL_MEMO2, LABEL_MEMO3, LABEL_MEMO4)"
              + " VALUES( @user_id, @name, @name2, @cal_date1, @cal_date2, @position, @position2, @position3, @position4," + " @pay_trans1, @pay_trans2, @pay_room1, @pay_room2, @pay_food1, @pay_food2, @pay_work1, @pay_work2, @pay_total1, @pay_total2)";    // 명령어 생성
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@name", user_name.Text);
            cmd.Parameters.AddWithValue("@name2", user_name2.Text);
            cmd.Parameters.AddWithValue("@cal_date1", cal_Date1.Text);
            cmd.Parameters.AddWithValue("@cal_date2", cal_Date2.Text);
            cmd.Parameters.AddWithValue("@term2", term2.Text);
            cmd.Parameters.AddWithValue("@location1", location1.Text);
            cmd.Parameters.AddWithValue("@location2", location2.Text);
            cmd.Parameters.AddWithValue("@memo1", memo1.Text);
            cmd.Parameters.AddWithValue("@pay_trans1", pay_trans1.Text);
            cmd.Parameters.AddWithValue("@pay_trans2", pay_trans2.Text);
            cmd.Parameters.AddWithValue("@pay_toll1", pay_toll1.Text);
            cmd.Parameters.AddWithValue("@pay_toll2", pay_toll2.Text);
            cmd.Parameters.AddWithValue("@pay_room1", pay_room1.Text);
            cmd.Parameters.AddWithValue("@pay_room2", pay_room2.Text);
            cmd.Parameters.AddWithValue("@pay_food1", pay_food1.Text);
            cmd.Parameters.AddWithValue("@pay_food2", pay_food2.Text);
            cmd.Parameters.AddWithValue("@pay_work1", pay_work1.Text);
            cmd.Parameters.AddWithValue("@pay_work2", pay_work2.Text);
            cmd.Parameters.AddWithValue("@pay_total1", pay_total1.Text);
            cmd.Parameters.AddWithValue("@pay_total2", pay_total2.Text);
            cmd.Parameters.AddWithValue("@label_memo1", memo_pay.Text);
            cmd.Parameters.AddWithValue("@label_memo2", label_memo2.Text);
            cmd.Parameters.AddWithValue("@label_memo3", label_memo3.Text);
            cmd.Parameters.AddWithValue("@label_memo4", label_memo4.Text);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            // attach_group 테이블에 넣을 가장 최신의 report_id 받기
            string sql2 = "SELECT MAX(report_id) as max from REPORTTBL";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                max = reader["max"].ToString();
            }
            reader.Close();

            // 상단 체크값 넣기
            string sql4 = "INSERT INTO TRANSPORT(REPORT_ID, CAR, TRAIN, BUS, BOAT, AIR, START, DEPARTURE) VALUES(@report_id, @car, @train, @bus, @boat, @air, @start, @departure)";
            SqlCommand cmd4 = new SqlCommand(sql4, conn);

            cmd4.Parameters.AddWithValue("@report_id", max); // 현재 DB에 들어간 report_id의 최대값을 받아와야하므로

            if (carCck.Checked == true)
                cmd4.Parameters.AddWithValue("@car", 1 + "");
            else
                cmd4.Parameters.AddWithValue("@car", 0 + "");

            if (trainCck.Checked == true)
                cmd4.Parameters.AddWithValue("@train", 1 + "");
            else
                cmd4.Parameters.AddWithValue("@train", 0 + "");

            if (busCck.Checked == true)
                cmd4.Parameters.AddWithValue("@bus", 1 + "");
            else
                cmd4.Parameters.AddWithValue("@bus", 0 + "");

            if (boatCck.Checked == true)
                cmd4.Parameters.AddWithValue("@boat", 1 + "");
            else
                cmd4.Parameters.AddWithValue("@boat", 0 + "");

            if (airCck.Checked == true)
                cmd4.Parameters.AddWithValue("@air", 1 + "");
            else
                cmd4.Parameters.AddWithValue("@air", 0 + "");

            cmd4.Parameters.AddWithValue("@start", start.Text);
            cmd4.Parameters.AddWithValue("@departure", departure.Text);

            reader = cmd4.ExecuteReader(); // 실행
            reader.Close();                // 자원해제

            // 하단 체크값 넣기
            string sql3 = "INSERT INTO ATTACH_GROUP(USER_ID, REPORT_ID, CARD_BILL, TOLL_BILL, TRANSE_BILL, ETC_BILL)"
                            + "VALUES(@user_id, @report_id, @card_bill, @toll_bill, @transe_bill, @etc_bill)";
            SqlCommand cmd3 = new SqlCommand(sql3, conn);

            cmd3.Parameters.AddWithValue("@user_id", user_id);
            cmd3.Parameters.AddWithValue("@report_id", max);

            // flag 값들을 1과 0으로 구분하자
            if (card_bill.Checked == true)
                cmd3.Parameters.AddWithValue("@card_bill", 1 + "");
            else
                cmd3.Parameters.AddWithValue("@card_bill", 0 + "");

            if (toll_bill.Checked == true)
                cmd3.Parameters.AddWithValue("@toll_bill", 1 + "");
            else
                cmd3.Parameters.AddWithValue("@toll_bill", 0 + "");

            if (transe_bill.Checked == true)
                cmd3.Parameters.AddWithValue("@transe_bill", 1 + "");
            else
                cmd3.Parameters.AddWithValue("@transe_bill", 0 + "");

            if (etc_bill.Checked == true)
                cmd3.Parameters.AddWithValue("@etc_bill", 1 + "");
            else
                cmd3.Parameters.AddWithValue("@etc_bill", 0 + "");

            reader = cmd3.ExecuteReader();


            // 최종 자원해제
            reader.Close();
            conn.Close();

            // 리스트 페이지로 redirect (필요한 값 : 사용자 id값, 들어오기 전 받았던 페이지 번호 값)
            Response.Redirect("./list.aspx?page_num=" + page_num);
        }

        // 지도 버튼
        protected void mapBtn_Click(object sender, EventArgs e)
        {
            string url = "./map.aspx";
            string s = "window.open('" + url + "','width=800,height=800,resizable=yes'); ";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session["user_id"] = "";
            Session["id"] = "";
            Session.Abandon();
            Response.Redirect("./login.aspx");
        }

        protected void writeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./paper_write.aspx");
        }

        protected void listBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./list.aspx");
        }
    }
}