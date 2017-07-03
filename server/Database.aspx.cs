using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class server_Database : System.Web.UI.Page
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
        sql = "SELECT * FROM Plan_lekcji";
       
        switch (Request["action"]) {
            case "CREATE":
                {
                    sql = "CREATE TABLE godziny (id int IDENTITY(1,1) PRIMARY KEY," +
                        "odG VARCHAR(2), odM VARCHAR(2), doG VARCHAR(2), doM VARCHAR(2)); " +

                     "CREATE TABLE dni (id int PRIMARY KEY, " +
                      "shortName VARCHAR(2), longName VARCHAR(15)); " +

                      "CREATE TABLE przedmioty(id int PRIMARY KEY, " +
                      "shortName VARCHAR(7), longName VARCHAR(25)); " +

                      "CREATE TABLE uzytkownicy(id int IDENTITY(1,1) PRIMARY KEY, " +
                      "login VARCHAR(30), haslo VARCHAR(30)); " +

                      "CREATE TABLE lekcje(id int IDENTITY(1,1) PRIMARY KEY, nrSali VARCHAR(30)," +
                      "dayFK INTEGER, hourFK INTEGER,  subjectFK INTEGER,  userFK INTEGER );";

                       
                    myResponse ="Utworzono tabele";
                    break;
                }
            case "DROP":
                {
                    sql = "DROP TABLE IF EXISTS godziny,dni,przedmioty,lekcje,uzytkownicy";
                    myResponse = "Usunieto tabele";
                    break;
                }
            case "INSERT":
                {
                    //---------------godziny-------------------------------
                    for (int i = 0; i < 14; i++)
                    {
                        sql = "INSERT INTO godziny ( odG, odM, doG, doM) VALUES( '0','00', '0','00'); ";
                        SqlCommand myCommand = new SqlCommand();
                        myCommand.CommandText = sql;
                        myCommand.Connection = conn;
                        try
                        {
                            myCommand.ExecuteNonQuery();
                            Response.Write(myResponse);
                        }
                        catch (Exception ex) { Response.Write(ex.Message); }
                        // wykonanie
                    }
                    myResponse = "Dodano 14 rekordow";
                    Response.Write(myResponse);
                    myResponse = "";
                    //--------------endif godziny--------------------------

                    //---------------nazwy dni-----------------------------
                    string[] shortNamesDni = { "PN", "WT", "SR", "CZ", "PT" };
                    string[] longNamesDni = { "poniedzialek", "wtorek", "sroda", "czwartek", "piatek" };

                    for (int i = 0; i < 5; i++)
                    {
                        sql = "INSERT INTO dni (id, shortName, longName) VALUES(" + (char)39 + (i + 1) + (char)39 + ", " + (char)39 + shortNamesDni[i] + (char)39 + ", " + (char)39 + longNamesDni[i] + (char)39 + ");";
                        SqlCommand myCommand = new SqlCommand();
                        myCommand.CommandText = sql;
                        myCommand.Connection = conn;
                        try
                        {
                            myCommand.ExecuteNonQuery();
                            Response.Write(myResponse);
                        }
                        catch (Exception ex) { Response.Write(ex.Message); }
                    }
                    //-------------endif nazwy dni------------------------
                    Response.Write(" Dodano dane do tabel");
                    //-------------przedmioty---------------------------
                    string[] shortNamesPrzedmioty = { "POL", "MAT", "ANG", "AK", "WF" };
                    string[] longNamesPrzedmioty = { "jezyk polski", "matematyka", "jezyk angielski", "aplikacje klienckie", "wychowanie fizyczne" };

                    for (int i = 0; i < 5; i++)
                    {
                        sql = "INSERT INTO przedmioty (id, shortName, longName) VALUES(" + (char)39 + (i + 1) + (char)39 + ", " + (char)39 + shortNamesPrzedmioty[i] + (char)39 + ", " + (char)39 + longNamesPrzedmioty[i] + (char)39 + ");";
                        SqlCommand myCommand = new SqlCommand();
                        myCommand.CommandText = sql;
                        myCommand.Connection = conn;
                        try
                        {
                            myCommand.ExecuteNonQuery();
                            Response.Write(myResponse);
                        }
                        catch (Exception ex) { Response.Write(ex.Message); }
                    }
                    //-------------endif przedmioty---------------------

                    
                   dodanie70rekordów:
                    {
                        sql = "";
                        for (int i = 0; i < 70; i++)
                        {
                            int dzien = i / 14 + 1;
                            int godzina = i %14 + 1;
                            int lekcja = dzien;
                            //lekcja = dzien + 1;
                            sql += "INSERT INTO lekcje VALUES(222," + (dzien) + "," + godzina + "," + + lekcja + ",1);";
                        }
                        try
                        {
                            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                        }
                        catch (Exception ex) { Response.Write(ex.Message); }
                    }
                    conn.Close();

                    break;
                }
            case "DELETE":
                {
                    sql = "DELETE FROM godziny  DBCC CHECKIDENT ('godziny', RESEED, 0); " +
                    "";
                    myResponse = "Usnieto rekordy";
                    break;
                }
            case "SELECT":
                {
                    sql= "SELECT * FROM godziny";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    /*Response.Write("<br />ilość wierszy w tabeli: " + dt.Rows.Count);
                    Response.Write("<br />ilość kolumn w tabeli: " + dt.Columns.Count);
                    Response.Write("<br />nazwa 1 kolumny: " + dt.Columns[0].ColumnName);
                    Response.Write("<br />wartość w 1 komórce 1 wiersza: " + dt.Rows[0][0]);*/

                     StringBuilder sb = new StringBuilder();
                     sb.Append("{\"godziny\":");
                     sb.AppendLine();
                     sb.Append("\t");sb.Append("[");
                     for (int i=0;i< dt.Rows.Count; i++)
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
                         if (i<dt.Rows.Count-1)
                             sb.Append(",");
                     }
                     sb.AppendLine();
                     sb.Append("\t"); sb.Append("]");
                     sb.AppendLine();
                     sb.Append("}");

                     Response.Write(sb.ToString()); 

                    //-------------dodanie relacji----------------------
                    
                    relacja1:
                    {
                        sql = "SELECT przedmioty.shortName, lekcje.nrSali " +
                        "FROM lekcje " +
                        "LEFT JOIN przedmioty ON(lekcje.subjectFK = przedmioty.id) " +
                        "LEFT JOIN uzytkownicy ON (lekcje.userFK = uzytkownicy.id) ";
                        sql += "WHERE(lekcje.userFK = 1 AND  lekcje.dayFK = " + 1 + ") ";
                    
                        SqlCommand myCommand = new SqlCommand();
                        myCommand.CommandText = sql;
                        myCommand.Connection = conn;
                        try
                        {
                            myCommand.ExecuteNonQuery();
                            Response.Write(myResponse);
                        }
                        catch (Exception ex) { Response.Write(ex.Message); }
                     
                        try
                        { 
                            SqlDataAdapter myDa = new SqlDataAdapter(sql, conn);
                            DataTable myDt = new DataTable();
                            myDa.Fill(myDt);
                        }
                        catch (Exception ex) { Response.Write(ex.Message); }
                    }

                    relacja2:
                    {
                        sql = "SELECT przedmioty.shortName, przedmioty.longName, lekcje.nrSali " +
                        "FROM lekcje " +
                        "LEFT JOIN dni ON(lekcje.dayFK = dni.id) " +
                        "LEFT JOIN przedmioty ON (lekcje.subjectFK = przedmioty.id) " +
                        "WHERE(lekcje.userFK = 1 AND  lekcje.dayFK = 1);";
                       
                        SqlCommand myCommand = new SqlCommand();
                        myCommand.CommandText = sql;
                        myCommand.Connection = conn;
                        try
                        {
                            myCommand.ExecuteNonQuery();
                            Response.Write(myResponse);
                        }
                        catch (Exception ex) { Response.Write(ex.Message); }
                        
                        try
                        {
                            SqlDataAdapter myDa = new SqlDataAdapter(sql, conn);
                            DataTable myDt = new DataTable();
                            myDa.Fill(myDt);
                        }
                        catch (Exception ex) { Response.Write(ex.Message); }
                    }

                    //-------------endif dodanie relacji----------------

                    conn.Close();

                    break;
                }
            default:
                {
                    sql = "CREATE TABLE one (id INTEGER); DROP TABLE one";
                    break;
                }
        }
        //z autoinkrementacją co 1
        //string sql = "CREATE TABLE nazwaTabeli (id INTEGER IDENTITY(1,1), poleA VARCHAR(10), poleB VARCHAR(20))";
        SqlCommand command = new SqlCommand();
        command.CommandText = sql;
        command.Connection = conn;
        try
        {
            command.ExecuteNonQuery();
            Response.Write(myResponse);
        }
        catch(Exception ex) { Response.Write(ex.Message); }
        
         // wykonanie
        conn.Close();

    }
}