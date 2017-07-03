using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class server_DownloadData : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connstr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security = True; Connect Timeout = 30";
        SqlConnection conn = new SqlConnection(connstr);
        string sql = "";
        string myResponse = "";
        sql = "SELECT * FROM godziny";
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataTable dt = new DataTable();
        da.Fill(dt);
        conn.Open();

        /*Response.Write("<br />ilość wierszy w tabeli: " + dt.Rows.Count);
        Response.Write("<br />ilość kolumn w tabeli: " + dt.Columns.Count);
        Response.Write("<br />nazwa 1 kolumny: " + dt.Columns[0].ColumnName);
        Response.Write("<br />wartość w 1 komórce 1 wiersza: " + dt.Rows[0][0]);*/

        StringBuilder sb = new StringBuilder();
        sb.Append("{\"godziny\":");
        sb.AppendLine();
        sb.Append("\t"); sb.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            sb.AppendLine(); sb.Append("\t"); sb.Append("\t");
            sb.Append("{\"id\":\"");
            sb.Append(dt.Rows[i][0]);
            sb.Append("\",");
            sb.Append("\"odG\":\"");
            sb.Append(dt.Rows[i][1]);
            sb.Append("\",");
            sb.Append("\"odM\":\"");
            sb.Append(dt.Rows[i][2]);
            sb.Append("\",");
            sb.Append("\"doG\":\"");
            sb.Append(dt.Rows[i][3]);
            sb.Append("\",");
            sb.Append("\"doM\":\"");
            sb.Append(dt.Rows[i][4]);
            sb.Append("\"}");
            if (i < dt.Rows.Count - 1)
                sb.Append(",");
        }
        sb.AppendLine();
        sb.Append("\t"); sb.Append("],");
        sb.AppendLine();

        sb.Append("\"dzisiaj\":");
        sb.AppendLine();
        sb.Append("\t[");
        sb.AppendLine();
        //tutaj zawartośc "dzisiaj":


        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL
        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL
        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL
        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL
        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL

        string dzisiaj = "2"; //1,2,3,4,5 dni tygodnia
        int idUsera = 1; // 1 - domyślnie tworzony user
        //sb.Append("{\"id\":\"3\"}");

        

        sql = "SELECT przedmioty.shortName, przedmioty.longName, lekcje.nrSali " +
                    "FROM lekcje " +
                    "LEFT JOIN dni ON(lekcje.dayFK = dni.id) " +
                    "LEFT JOIN przedmioty ON (lekcje.subjectFK = przedmioty.id) " +
                    "WHERE(lekcje.userFK ="+ idUsera +"AND  lekcje.dayFK = "+dzisiaj+");";

        SqlCommand myCommand = new SqlCommand();
        myCommand.CommandText = sql;
        myCommand.Connection = conn;
        try
        {
            myCommand.ExecuteNonQuery();
        }
        catch (Exception ex) { Response.Write(ex.Message); }

        try
        {
            SqlDataAdapter myDa = new SqlDataAdapter(sql, conn);
            DataTable myDt = new DataTable();
            myDa.Fill(myDt);
                
            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                if(i!=0)
                    sb.AppendLine();
                sb.Append("\t\t{");
                sb.Append("\"shortName\":\"");
                sb.Append(myDt.Rows[i][0]);
                sb.Append("\",\"longName\":\"");
                sb.Append(myDt.Rows[i][1]);
                sb.Append("\",\"nrSali\":");
                sb.Append(myDt.Rows[i][2]);
                if (i < myDt.Rows.Count - 1)
                    sb.Append("},");
                else
                    sb.Append("}");
            }
                   
        }
        catch (Exception ex) { Response.Write(ex.Message); }
        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL
        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL
        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL
        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL
        //SQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQLSQL

        sb.AppendLine();
        sb.Append("\t],");
        sb.AppendLine();
        
        

        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        sql = "SELECT przedmioty.shortName, lekcje.nrSali " +
        "FROM lekcje " +
        "LEFT JOIN przedmioty ON(lekcje.subjectFK = przedmioty.id) " +
        "LEFT JOIN uzytkownicy ON (lekcje.userFK = uzytkownicy.id) ";
        sql += "WHERE lekcje.userFK = " + idUsera + ";";
        myCommand = new SqlCommand();
        myCommand.CommandText = sql;
        myCommand.Connection = conn;
        try
        {
            myCommand.ExecuteNonQuery();
        }
        catch (Exception ex) { Response.Write(ex.Message); }

        try
        {
            SqlDataAdapter myDa = new SqlDataAdapter(sql, conn);
            DataTable myDt = new DataTable();
            myDa.Fill(myDt);
            sb.Append("\"tydzien\":");
            sb.AppendLine();
            sb.Append("\t[");
            sb.AppendLine();
            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                sb.Append("\t\t{");
                sb.Append("\"shortName\":\"");
                sb.Append(myDt.Rows[i][0]);
                sb.Append("\",");
                sb.Append("\"nrSali\":\"");
                sb.Append(myDt.Rows[i][1]);
                sb.Append("\"");
                if (i< myDt.Rows.Count-1)
                    sb.Append("},");
                else
                    sb.Append("}");
                sb.AppendLine();
                /*if (i != 0)
                    sb.AppendLine();
                sb.Append("\t\t{");
                sb.Append("\"shortName\":\"");
                sb.Append(myDt.Rows[i][0]);
                sb.Append("\",\"longName\":\"");
                sb.Append(myDt.Rows[i][1]);
                sb.Append("\",\"nrSali\":");
                sb.Append(myDt.Rows[i][2]);
                if (i < myDt.Rows.Count - 1)
                    sb.Append("},");
                else
                    sb.Append("}");*/
            }
            sb.Append("\t]");
            sb.AppendLine();
        }
        catch (Exception ex) { Response.Write(ex.Message); }
        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        ///SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        //////SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2SQL2
        sb.Append("}");
        Response.Write(sb.ToString());





        conn.Close();
    }
}