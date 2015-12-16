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
                /*Response.Redirect("./login.aspx");*/                // 로그인창으로 redirect를 걸어준다
            }

            page_num = Request.QueryString["page_num"]; // 글을 쓰는 해당 사용자가 이전에 위치해 있었던 list페이지의 값 저장
            userLbl.Text = id_value;
        }

        // 등록 버튼 클릭시 실행되는 function -> 좀 길게 많이 진행되어야 할 부분이다
        protected void okBtn_Click(object sender, EventArgs e)
        {

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

        // 모든 내용 저장클릭버튼
        protected void save_Click(object sender, EventArgs e)
        {
            // 1. 출장기간 선택
            Response.Cookies["cal_Date1"].Value = cal_Date1.Text;
            Response.Cookies["cal_Date2"].Value = cal_Date1.Text;

            // 2. 교통수단 체크
            // 자동차가 체크되어 있다면
            // 체크값에 1을 집어넣고 아니면 0을 넣는다
            if (carCck.Checked == true)
            {
                Response.Cookies["carCck"].Value = "1";
            }
            else // 0을 넣어주는 이유는 혹시나 쿠키에 키 값이 없어 접근할 수 없게됨을 방지한다
            {
                Response.Cookies["carCck"].Value = "0";
            }

            // 기차
            if (trainCck.Checked == true)
            {
                Response.Cookies["trainCck"].Value = "1";
            }
            else
            {
                Response.Cookies["trainCck"].Value = "0";
            }

            // 버스
            if (busCck.Checked == true)
            {
                Response.Cookies["busCck"].Value = "1";
            }
            else
            {
                Response.Cookies["busCck"].Value = "0";
            }

            // 선박
            if (boatCck.Checked == true)
            {
                Response.Cookies["boatCck"].Value = "1";
            }
            else
            {
                Response.Cookies["boatCck"].Value = "0";
            }

            // 항공
            if (airCck.Checked == true)
            {
                Response.Cookies["airCck"].Value = "1";
            }
            else
            {
                Response.Cookies["airCck"].Value = "0";
            }
            // 체크버튼 확인 끝

            // - 추후수정필요 -
            //
            //
            //
            // - 계산된 값들을 받을 블록 - 


            // 4. 여비 항목 입력
            // 이 부분에서 한글로 진행되는 부분들은 모두 Server.UrlEncode로 인코딩을 해줘야한다
            Response.Cookies["user_name1"].Value = Server.UrlEncode(user_name1.Text);
            Response.Cookies["user_name2"].Value = Server.UrlEncode(user_name2.Text);
            Response.Cookies["user_name3"].Value = Server.UrlEncode(user_name3.Text);
            Response.Cookies["user_name4"].Value = Server.UrlEncode(user_name4.Text);

            Response.Cookies["position1"].Value = Server.UrlEncode(position1.Text);
            Response.Cookies["position2"].Value = Server.UrlEncode(position2.Text);
            Response.Cookies["position3"].Value = Server.UrlEncode(position3.Text);
            Response.Cookies["position4"].Value = Server.UrlEncode(position4.Text);

            Response.Cookies["pay_trans1"].Value = pay_trans1.Text;
            Response.Cookies["pay_trans2"].Value = pay_trans2.Text;
            Response.Cookies["pay_trans3"].Value = pay_trans3.Text;
            Response.Cookies["pay_trans4"].Value = pay_trans4.Text;

            Response.Cookies["pay_toll1"].Value = pay_toll1.Text;
            Response.Cookies["pay_toll2"].Value = pay_toll2.Text;
            Response.Cookies["pay_toll3"].Value = pay_toll3.Text;
            Response.Cookies["pay_toll4"].Value = pay_toll4.Text;

            Response.Cookies["pay_room1"].Value = pay_room1.Text;
            Response.Cookies["pay_room2"].Value = pay_room2.Text;
            Response.Cookies["pay_room3"].Value = pay_room3.Text;
            Response.Cookies["pay_room4"].Value = pay_room4.Text;

            Response.Cookies["pay_food1"].Value = pay_food1.Text;
            Response.Cookies["pay_food2"].Value = pay_food2.Text;
            Response.Cookies["pay_food3"].Value = pay_food3.Text;
            Response.Cookies["pay_food4"].Value = pay_food4.Text;

            Response.Cookies["pay_work1"].Value = pay_work1.Text;
            Response.Cookies["pay_work2"].Value = pay_work2.Text;
            Response.Cookies["pay_work3"].Value = pay_work3.Text;
            Response.Cookies["pay_work4"].Value = pay_work4.Text;

            // 5. 영수증 유무 체크
            // 유무체크는 위의 사항과 같은 방법으로 진행된다
            if (card_bill.Checked == true)
            {
                Response.Cookies["card_bill"].Value = "1";
            }
            else
            {
                Response.Cookies["card_bill"].Value = "0";
            }

            if (toll_bill.Checked == true)
            {
                Response.Cookies["toll_bill"].Value = "1";
            }
            else
            {
                Response.Cookies["toll_bill"].Value = "0";
            }

            if (transe_bill.Checked == true)
            {
                Response.Cookies["transe_bill"].Value = "1";
            }
            else
            {
                Response.Cookies["transe_bill"].Value = "0";
            }

            if (etc_bill.Checked == true)
            {
                Response.Cookies["etc_bill"].Value = "1";
            }
            else
            {
                Response.Cookies["etc_bill"].Value = "0";
            }
            // 영수증 유무 체크 끝

            // 6. 파일 업로드 유무 체크
            if (billload1.HasFile)
            {
                Response.Cookies["billload1"].Value = billload1.FileName.ToString();
            }
            else
            {
                Response.Cookies["billload1"].Value = "";
            }

            if (billload2.HasFile)
            {
                Response.Cookies["billload2"].Value = billload2.FileName.ToString();
            }
            else
            {
                Response.Cookies["billload2"].Value = "";
            }

            if (billload3.HasFile)
            {
                Response.Cookies["billload3"].Value = billload3.FileName.ToString();
            }
            else
            {
                Response.Cookies["billload3"].Value = "";
            }

            if (billload4.HasFile)
            {
                Response.Cookies["billload4"].Value = billload4.FileName.ToString();
            }
            else
            {
                Response.Cookies["billload4"].Value = "";
            }

            if (billload5.HasFile)
            {
                Response.Cookies["billload5"].Value = billload5.FileName.ToString();
            }
            else
            {
                Response.Cookies["billload5"].Value = "";
            }

            Response.Redirect("./paper_write.aspx");
        }
    }
}