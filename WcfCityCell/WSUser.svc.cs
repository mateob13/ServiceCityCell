using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WcfCityCell
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class wsUser : IWSUser
    {

        SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["00_MASTER"]);

        public int ObtenerLogin(string UserLogin, string Password)
        {


            
            int mensaje = 0;
            List<string> UserPassword = new List<string>();
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT [Name],[Mail] FROM [CityCell].[dbo].[Tbl_Users] WHERE [UserLogin] = @User_Id AND [Password] = @Password", con);
                cmd.Parameters.AddWithValue("@User_Id", UserLogin);
                cmd.Parameters.AddWithValue("@Password", Password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    mensaje = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string name = dt.Rows[i]["Name"].ToString();
                        string mail = dt.Rows[i]["Mail"].ToString();

                        UserPassword.Add(name);
                        UserPassword.Add(mail);
                    }

                }
                else
                {
                    mensaje = 0;
                }
                con.Close();
            }
            return mensaje;

        }

        public int RegisterNow(string Name, string NumIndentification, string Mail, string UserId, string Password)
        {
            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-N8965M7; Initial Catalog=CityCell; User id=sa; Password=admin;");
            int msjRes = 0;
            List<string> UserRegister = new List<string>();
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [CityCell].[dbo].[Tbl_Users]([Name], [Identification], [Mail], [UserLogin], [Password]) VALUES (@NAME, @INDENTIFICATION_ID, @MAIL, @USER_ID, @PASSWORD)", con);
                cmd.Parameters.AddWithValue("@NAME", Name);
                cmd.Parameters.AddWithValue("@INDENTIFICATION_ID", NumIndentification);
                cmd.Parameters.AddWithValue("@MAIL", Mail);
                cmd.Parameters.AddWithValue("@USER_ID", UserId);
                cmd.Parameters.AddWithValue("@PASSWORD", Password);
                cmd.ExecuteNonQuery();

                int MsjRespuesta = ObtenerLogin(UserId, Password);

                if (MsjRespuesta == 1)
                {
                    msjRes = 1;
                }
                else
                {
                    msjRes = 0;
                }
                con.Close();
            }
            return msjRes;
        }

        public DataSet ObtenerProductos()
        {


            //SqlConnection con = new SqlConnection("Data Source=DESKTOP-N8965M7; Initial Catalog=CityCell; User id=sa; Password=admin;");
            
            List<string> UserPassword = new List<string>();
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [CityCell].[dbo].[Tbl_Products]", con);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                DataSet ds = new DataSet();
                    
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "Customers");

                    con.Close();
                    return ds;

                
            }

        }

    }
}
