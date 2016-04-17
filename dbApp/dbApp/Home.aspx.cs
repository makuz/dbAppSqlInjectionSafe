using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace dbApp
{
    public partial class Home : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-C77TM1D;Initial Catalog=myDb;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from Persons";
            cmd.CommandType = CommandType.Text;

            SqlDataReader rdr = cmd.ExecuteReader();

            GridViewPersonTable.DataSource = rdr;
            GridViewPersonTable.DataBind();

            con.Close();

        }

        protected void AddButton_click(object sender, EventArgs e)
        {

            String name = Name.Text;
            String lastname = Lastname.Text;
            String adres = Adres.Text;
            int wiek = Int32.Parse(Age.Text);

            Person person = new Person(name, lastname, adres, wiek);


            con.Open();

            SqlCommand cmd = con.CreateCommand();

            cmd.CommandText = "Insert into Persons(name, lastname, adres, wiek) values (@name, @lastname, @adres, @wiek);";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = lastname;
            cmd.Parameters.Add("@adres", SqlDbType.VarChar).Value = adres;
            cmd.Parameters.Add("@wiek", SqlDbType.Int).Value = wiek;

            cmd.ExecuteNonQuery();

            con.Close();

            ShowEventData.Text = "zapisano osobe: " + person.ToString();

        }


        protected void RemoveButton_click(object sender, EventArgs e)
        {

            int Id = Int32.Parse(IdTextBox.Text);
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "delete from Persons where Id = @Id;";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            cmd.ExecuteNonQuery();

            con.Close();

            ShowEventData.Text = "osoba o id: " + Id + "zostala usunieta";
        }
    }
}