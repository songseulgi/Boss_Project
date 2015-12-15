using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTCDProject
{
    public partial class paper_result : System.Web.UI.Page
    {
        public string report_id = "";
        public string user_id = "";
        public string page_num = "";
        public string id_value = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // ***************** list와 write 그리고 result 페이지에 공통적으로 들어가서 체크되어야 할 부분 ***************** 
            // 세션의 값이 존재하는지 확인 -> 즉 로그인이 되어있는가 확인

            if (Session["user_id"] != null)                       // 세션에 값이 존재하면 (로그인이 되어있다)
            {
                user_id = Session["user_id"].ToString();          // user_id 변수에 Session의 user_id값을 복사한다
                id_value = Session["id"].ToString();
            }
            else                                                  // 세션에 값이 들어있지 않다면 (로그인 실패)
            {
                Response.Redirect("./login.aspx");                // 로그인창으로 redirect를 걸어준다
            }
            // **********************************************************************************************************

            report_id = Request.QueryString["report_id"];
            page_num = Request.QueryString["page_num"];
            userLbl.Text = id_value;

            string source = @"Server=localhost;uid=sa;pwd=Sb11011101;database=ReportDB";

            // 1. db 연결
            SqlConnection conn = new SqlConnection(source);
            conn.Open();

            // 2. 명령어
            string sql = "SELECT * FROM REPORTTBL WHERE report_id=" + report_id;
            SqlCommand cmd = new SqlCommand(sql, conn);

            // 3. 명령어 실행
            SqlDataReader reader = cmd.ExecuteReader();

            // 4. 페이지에 보여주기

            try
            {
                while (reader.Read())
                {
                    this.name.Text = reader["name"].ToString();
                    this.company.Text = reader["company"].ToString();
                    this.position.Text = reader["position"].ToString();
                    this.term1.Text = reader["term1"].ToString();
                    this.term2.Text = reader["term2"].ToString();
                    this.location1.Text = reader["location1"].ToString();
                    this.location2.Text = reader["location2"].ToString();
                    this.memo1.Text = reader["memo1"].ToString();
                    this.pay_trans1.Text = reader["pay_trans1"].ToString();
                    this.pay_trans2.Text = reader["pay_trans2"].ToString();
                    this.pay_toll1.Text = reader["pay_toll1"].ToString();
                    this.pay_toll2.Text = reader["pay_toll2"].ToString();
                    this.pay_room1.Text = reader["pay_room1"].ToString();
                    this.pay_room2.Text = reader["pay_room2"].ToString();
                    this.pay_food1.Text = reader["pay_food1"].ToString();
                    this.pay_food2.Text = reader["pay_food2"].ToString();
                    this.pay_work1.Text = reader["pay_work1"].ToString();
                    this.pay_work2.Text = reader["pay_work2"].ToString();
                    this.pay_total1.Text = reader["pay_total1"].ToString();
                    this.pay_total2.Text = reader["pay_total2"].ToString();
                    this.label_memo1.Text = reader["label_memo1"].ToString();
                    this.label_memo2.Text = reader["label_memo2"].ToString();
                    this.label_memo3.Text = reader["label_memo3"].ToString();
                    this.label_memo4.Text = reader["label_memo4"].ToString();
                }
            }
            catch (Exception ee)
            {
                Response.Write(ee.Message);
            }

            // 5. db close
            reader.Close();


            // 상단 체크값 가져오기
            string sql3 = "SELECT CAR, TRAIN, BUS, BOAT, AIR, START, DEPARTURE FROM TRANSPORT WHERE REPORT_ID=" + report_id;
            SqlCommand cmd3 = new SqlCommand(sql3, conn);
            reader = cmd3.ExecuteReader();

            while (reader.Read())
            {
                if (reader["car"].ToString().Equals("1"))
                {
                    carCck.Checked = true;
                }

                if (reader["train"].ToString().Equals("1"))
                {
                    trainCck.Checked = true;
                }

                if (reader["bus"].ToString().Equals("1"))
                {
                    busCck.Checked = true;
                }

                if (reader["boat"].ToString().Equals("1"))
                {
                    boatCck.Checked = true;
                }

                if (reader["air"].ToString().Equals("1"))
                {
                    airCck.Checked = true;
                }

                start1.Text = reader["start"].ToString();
                end.Text = reader["departure"].ToString();
            }

            reader.Close();

            // 하단 체크값 가져오기
            string sql2 = "SELECT * FROM ATTACH_GROUP WHERE REPORT_ID=" + report_id;
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            reader = cmd2.ExecuteReader();

            while (reader.Read())
            {
                if (reader["card_bill"].ToString().Equals("1"))
                {
                    card_bill.Checked = true;
                }

                if (reader["toll_bill"].ToString().Equals("1"))
                {
                    toll_bill.Checked = true;
                }

                if (reader["transe_bill"].ToString().Equals("1"))
                {
                    transe_bill.Checked = true;
                }

                if (reader["etc_bill"].ToString().Equals("1"))
                {
                    etc_bill.Checked = true;
                }
            }

            reader.Close();
            conn.Close();
        }

        // 돌아가기 버튼클릭
        protected void backBtn_Click(object sender, EventArgs e)
        {
            // 해당 페이지에 hidden으로 저장한 page_num값, 로그인한 아이디 값을 가지고 
            // list.aspx로 리다이렉트
            Response.Redirect("./list.aspx?page_num=" + page_num);
        }

        // 프린트 버튼클릭


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