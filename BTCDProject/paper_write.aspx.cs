using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTCDProject
{
    public partial class paper_write : System.Web.UI.Page

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
                /*Response.Redirect("./login.aspx");*/                // 로그인창으로 redirect를 걸어준다
            }
            // **********************************************************************************************************

            //report_id = Request.QueryString["report_id"];
            //page_num = Request.QueryString["page_num"];
            userLbl.Text = id_value;

            // Server.UrlDecode를 써주는 이유는 한글이 깨지는것을 방지하기 위함
            name.Text = Server.UrlDecode(Request.Cookies["user_name1"].Value); // 기안자
            // 소속
            position.Text = Server.UrlDecode(Request.Cookies["position1"].Value); // 직위
            term1.Text = Request.Cookies["cal_date1"].Value; // 출장기간

            // 교통수단 체크 유무

            if (Request.Cookies["carCck"].Value.Equals("1"))
            {
                carCck.Checked = true;
            }

            if (Request.Cookies["trainCck"].Value.Equals("1"))
            {
                trainCck.Checked = true;
            }

            if (Request.Cookies["busCck"].Value.Equals("1"))
            {
                busCck.Checked = true;
            }

            if (Request.Cookies["boatCck"].Value.Equals("1"))
            {
                boatCck.Checked = true;
            }

            if (Request.Cookies["airCck"].Value.Equals("1"))
            {
                airCck.Checked = true;
            }

            // 영수증 체크 유무
            if (Request.Cookies["card_bill"].Value.Equals("1"))
            {
                card_bill.Checked = true;
            }
            if (Request.Cookies["toll_bill"].Value.Equals("1"))
            {
                toll_bill.Checked = true;
            }
            if (Request.Cookies["transe_bill"].Value.Equals("1"))
            {
                transe_bill.Checked = true;
            }
            if (Request.Cookies["etc_bill"].Value.Equals("1"))
            {
                etc_bill.Checked = true;
            }


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