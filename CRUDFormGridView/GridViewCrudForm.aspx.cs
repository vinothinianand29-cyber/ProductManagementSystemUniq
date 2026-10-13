using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDFormGridView
{
    public partial class GridViewCrudForm : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ViewEmpDetails();
            }
                

        }

        protected void ViewEmpDetails()
        {
            using(SqlConnection con=new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM EmployeeDetails",con);   
                DataTable dt=new DataTable();
                da.Fill(dt);
                EmployeeData.DataSource = dt;
                EmployeeData.DataBind();
                                
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int eid = Convert.ToInt32(EmpId.Text);
            string ename = EmpName.Text;
            int esalary = Convert.ToInt32(EmpSalary.Text);
            string ecity= EmpCity.Text;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Insert into EmployeeDetails Values(@id,@name,@Salary,@City)", con);
                cmd.Parameters.AddWithValue("@Id", eid);
                cmd.Parameters.AddWithValue("@name", ename);
                cmd.Parameters.AddWithValue("@Salary", esalary);
                cmd.Parameters.AddWithValue("@City",ecity);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            ViewEmpDetails();

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EmployeeData.EditIndex = e.NewEditIndex;
            ViewEmpDetails();

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userDetail = Convert.ToInt32(EmployeeData.DataKeys[e.RowIndex].Value);
            TextBox nametxts = EmployeeData.Rows[e.RowIndex].FindControl("txtEmployeeName") as TextBox;
            string newName=nametxts.Text;
            TextBox salary = EmployeeData.Rows[e.RowIndex].FindControl("txtEmployeeSalary") as TextBox;
            string newSalary = salary.Text;
            TextBox city = EmployeeData.Rows[e.RowIndex].FindControl("txtEmployeeCity") as TextBox;
            string newCity = city.Text;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("UPDATE EmployeeDetails SET EmployeeName=@name,EmployeeSalary=@Salary,EmployeeCity=@City Where EmployeeId=@Id",con);
                cmd.Parameters.AddWithValue("@Id", userDetail);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@Salary", newSalary);
                cmd.Parameters.AddWithValue("@City", newCity);

                con.Open();
                cmd.ExecuteNonQuery();

            }

            EmployeeData.EditIndex = -1;
            ViewEmpDetails();

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userDetail = Convert.ToInt32(EmployeeData.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM EmployeeDetails WHERE EmployeeId=@Id", con);
                cmd.Parameters.AddWithValue("@Id", userDetail);
                con.Open() ;
                cmd.ExecuteNonQuery() ;
            }
            ViewEmpDetails();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EmployeeData.EditIndex = -1;
            ViewEmpDetails();
        }
    }
}