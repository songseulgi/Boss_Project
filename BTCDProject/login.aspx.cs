using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTCDProject
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["test"] = "hithere";    // 처음 
            this.pwBox.Attributes["onkeypress"] = "if(event.keyCode==13) {" +
                Page.GetPostBackEventReference(this.okBtn) + "; return false;}";    // 추후에는 수정이 필요한 부분
        }

        protected void okBtn_Click(object sender, EventArgs e)
        {

            if (idBox.Text.Length == 0) // 아이디 값 비어있다면
            {
                this.warnLabel.Text = "아이디를 입력해주세요";
            }
            else if (pwBox.Text.Length == 0) // 비밀번호 값 비어있다면
            {
                this.warnLabel.Text = "비밀번호를 입력해주세요";
            }

            if (idBox.Text.Length == 0 && pwBox.Text.Length == 0) // 둘 다 비어이있다면
            {
                this.warnLabel.Text = "아이디/비밀번호를 입력해주세요";
            }
            else if (idBox.Text.Length != 0 && pwBox.Text.Length != 0) // 둘 다 비어있지 않다면
            {
                // 여기에서 DB와 확인을 하여 맞을경우, user_id값을 얻어내고 list page에 redirect해준다
                this.warnLabel.Text = "";

                string source = @"Server=localhost;uid=sa;pwd=Sb11011101;database=ReportDB";

                // db 연결
                SqlConnection conn = new SqlConnection(source);
                conn.Open();

                // 명령어 작성
                string sql = "SELECT USER_ID FROM USER_INFO WHERE ID='" + idBox.Text + "' AND PW='" + pwBox.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                // 명령어 실행
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // 찍히는 값이 있다면 id와 pw가 매칭된다는 이야기이므로 이 블록에 들어온다
                {
                    this.warnLabel.Text = reader["user_id"].ToString(); // 값 찍히는것 확인완료
                    Session["user_id"] = reader["user_id"].ToString();
                    Session["id"] = idBox.Text;
                    Response.Redirect("./list.aspx?page_num=0");
                }
                else // 찍히는 값이 없다면 id와 pw의 매칭이 실패했다는 이야기이므로 이 블록에 들어온다
                {
                    this.warnLabel.Text = "아이디/비밀번호를 확인해주세요";
                }

                // db 연결 종료
                reader.Close();
                conn.Close();
            }
        }
    }
}