using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTCDProject
{
    public partial class list : System.Web.UI.Page
    {
        public string user_id = "";
        public string page_num = "";
        public const string PAGE_SIZE = "5";
        public string id_value = "";

        // 페이지가 로드될 때 동작하는 function
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션의 값이 존재하는지 확인 -> 즉 로그인이 되어있는가 확인
            if (Session["user_id"] != null)              // 세션에 값이 존재하면
            {
                user_id = Session["user_id"].ToString(); // user_id 변수에 Session의 user_id값을 복사한다
                id_value = Session["id"].ToString();
            }
            else                                         // 세션에 값이 들어있지 않다면 
            {
                Response.Redirect("./login.aspx");       // 로그인창으로 redirect를 걸어준다
            }

            page_num = Request.QueryString["page_num"];  // 리스트를 불러올 때 이전 페이지가 몇 번째 페이지인지 전달받기

            userLbl.Text = id_value;

            DataGridt.AllowPaging = true;
            DataGridt.PageSize = 10;
            DataGridt.PagerStyle.Mode = PagerMode.NumericPages;
            DataGridt.PagerStyle.PageButtonCount = 5;

            if (!Page.IsPostBack)
            {
                Reload();
            }
        }

        private void Reload()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Server=localhost;uid=sa;pwd=Sb11011101;database=ReportDB";
            conn.Open();
            string query = "DECLARE @paging_size INT DECLARE @page INT SET @paging_size = 5 SET @page = "
                            + page_num
                            + " SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY report_id DESC) AS ROW_NUM, * FROM report WHERE user_id = "
                            + user_id
                            + ")"
                            + "A WHERE ROW_NUM > (@paging_size * @page) AND ROW_NUM <= (@paging_size * (@page + 1))";
            string sql = " SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY report_id DESC) AS ROW_NUM, * FROM REPORTTBL WHERE user_id = "
                            + user_id
                            + ")"
                            + "A";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter Adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            Adapt.Fill(ds, "board_seongho");
            DataGridt.DataSource = ds;
            DataGridt.DataBind();
            conn.Close();
        }

        private void DataGridt_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGridt.CurrentPageIndex = e.NewPageIndex;
            DataGridt.DataBind();
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

        protected void DataGridt_PageIndexChanged1(object source, DataGridPageChangedEventArgs e)
        {
            DataGridt.CurrentPageIndex = e.NewPageIndex;
            DataGridt.DataBind();
            Reload();
        }
    }
}