using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BTCDProject
{
    public class DBClass
    {
        /*
              이 클래스의 메소드 접근은 DBClass.메소드명()으로 해주시면 됩니다
              이 클래스의 모든 쿼리들은 SqlDataReader 객체를 반환하므로 사용하는 곳에서
              SqlDataReader 객체에 대입해 사용해주면 됩니다
          */

        public static SqlConnection conn;
        public static SqlDataReader reader;

        // 자원연결 : *************** 이곳의 서버, 아이디, 비밀번호를 변경해주세요 ***************
        public static void connect()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Server=172.101.0.4,1433;uid=sa;pwd=Sb11011101;database=ReportDB";
            conn.Open();
        }

        // 자원해제 :  *************** 연결 후에 '항상' connection 끊기 ***************
        public static void disconnect()
        {
            reader.Close();
            conn.Close();
        }

        // for login.aspx.cs --------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 로그인을 위한 쿼리
        public static SqlDataReader loginCheck(string id, string pw) // * user_id값이 아닌 일반 id값과 pw값을 가지고 처리합니다
        {
            string query = "SELECT USER_ID FROM USER_INFO WHERE ID = '" + id + "' AND PW = '" + pw + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            return reader;
        }

        // for join.aspx.cs --------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 회원가입시 아이디 체크를 위한 쿼리
        public static bool idCheck(string id)
        {
            bool idFlag = true;
            string count = "";
            string query = "select count(*) as count from user_info where id =" + "'" + id + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count = reader["count"].ToString();
            }
            if (count.Equals("1"))
            {
                idFlag = false;
            }
            return idFlag;
        }

        // 모든 flag가 1일 때 DB에 아이디 값 넣기 쿼리
        public static void insertId(string name, string id, string pw)
        {
            string query = "INSERT INTO USER_INFO(NAME, ID, PW) VALUES(@name, @id, @pw)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@pw", pw);
            reader = cmd.ExecuteReader();
        }

        // for list.aspx.cs --------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 리스트의 요소들을 위한 쿼리
        public static SqlDataReader listLoad(string user_id)
        {
            string query = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY report_id DESC) AS ROW_NUM, * FROM REPORTTBL WHERE user_id = " + user_id + ")" + "A";
            SqlCommand cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            return reader;
        }
        public static SqlDataAdapter listLoad2(string user_id)
        {
            string sql = " SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY report_id DESC) AS ROW_NUM, * FROM REPORTTBL WHERE user_id = " + user_id + ")" + "A";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter Adapt = new SqlDataAdapter(cmd);
            return Adapt;
        }

        // for paper_result.aspx.cs --------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 출장명령서에 데이터를 뿌리기 위한 쿼리 1
        // 기안자, 소속, 직위, 기간1,2, 출장지1,2, 메모1, 교통운임1,2, 통행료1,2, 숙박비1,2, 식비1,2, 일비1,2, 계1,2
        public static SqlDataReader reportLoad1(string report_id)
        {
            string query = "SELECT * FROM REPORTTBL WHERE report_id=" + report_id;
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        // 출장명령서에 데이터를 뿌리기 위한 쿼리 2 - 상단 체크값과 출장지 상세를 가져온다
        // 자차, 열차, 버스, 해운, 항공에 대한 체크값, 출발지, 경유지1,2,3,4,5, 도착지정보
        public static SqlDataReader reportLoad2(string report_id)
        {
            string query = "SELECT CAR, TRAIN, BUS, BOAT, AIR, START, DEPARTURE, MID_LOC1, MID_LOC2, MID_LOC3, MID_LOC4, MID_LOC5 FROM TRANSPORT WHERE REPORT_ID=" + report_id;
            SqlCommand cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            return reader;
        }

        // 출장명령서에 데이터를 뿌리기 위한 쿼리 3 - 하단 체크값을 가져온다
        // 카드, 톨비, 교통운임, 기타에 대한 영수증 체크값
        public static SqlDataReader reportLoad3(string report_id)
        {
            string query = "SELECT * FROM ATTACH_GROUP WHERE REPORT_ID=" + report_id;
            SqlCommand cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            return reader;
        }

        // for paper_write.aspx.cs --------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // 명령서의 가장 최근 값을 불러들이기위한 쿼리
        public static string getReportMax()
        {
            string max = "";
            string query = "SELECT MAX(report_id) as max from REPORTTBL";
            SqlCommand cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                max = reader["max"].ToString();
            }
            return max;
        }
    }

}
