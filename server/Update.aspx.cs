using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class Update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connstr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security = True; Connect Timeout = 30";
        SqlConnection conn = new SqlConnection(connstr);
        try
        {
            conn.Open();
            //Response.Write("Polaczenie otwarte ");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            conn.Close();
            //Response.Write("Polaczenie zamkniete ");
        }

        conn.Open();
        string sql = "";
        string myResponse = "";
        //bez autoinkrementacji - ta wersja jest na dziś
        sql = "SELECT * FROM godziny";

        //UPDATOWANIE
            string hours = Request["hours"];
            string minutes = Request["minutes"];
            string column = Request["column"];
            string row = Request["row"];
        if (column == "1")
            sql = "UPDATE godziny SET odG=" + hours + ", odM=" + minutes + "WHERE id=" + row;//'Alfred Schmidt', odM='Hamburg'";
        else if (column == "2")
            sql = "UPDATE godziny SET doG=" + hours + ", doM=" + minutes + "WHERE id=" + row;
        //END OF: UPDATOWANIE

        SqlCommand command = new SqlCommand();
        command.CommandText = sql;
        command.Connection = conn;
        try
        {
            command.ExecuteNonQuery();
            Response.Write(myResponse);
        }
        catch (Exception ex) { Response.Write(ex.Message); }

        conn.Close();

    }
}