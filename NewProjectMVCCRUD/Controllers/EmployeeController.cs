using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace NewProjectMVCCRUD.Controllers
{
    public class EmployeeController: Controller
    {
        string cm = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        public ActionResult Index()
        {
            List<Employee> list = new List<Employee>();
            MySqlConnection con=new MySqlConnection(cm);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Employee",con);
            MySqlDataReader dr=cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(
                    new Employee {

                        Id = Convert.ToInt32(dr["id"]);
                        Name = dr["name"].ToString();
                        Age = Convert.ToInt32(dr["age"]);
                        Gender = dr["gender"].ToString();
                        City = dr["city"].ToString();
                        IsActive = Convert.ToBoolean(dr["IsActive"]);

                    });
            }

            return View(list);
        }


        
    }
}