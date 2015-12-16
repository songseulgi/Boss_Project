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

        protected void save_Click(object sender, EventArgs e)
        {
            
        }
    }
}