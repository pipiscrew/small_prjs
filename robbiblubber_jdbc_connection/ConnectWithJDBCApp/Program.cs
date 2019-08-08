using Robbiblubber.Extend.SQL.JDBC.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectWithJDBCApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string sqlite_filepath = AppDomain.CurrentDomain.BaseDirectory + "\\test.sqlite";

            JdbcConnection cn = new JdbcConnection("jdbc:sqlite:" + sqlite_filepath, "org.sqlite.JDBC");

            //if doesnt exist, will be created
            cn.Open();

            JdbcCommand cmd;
            JdbcReader re = null;

            try
            {
                //check if table exists
                cmd = cn.CreateCommand("SELECT * FROM USERS");
                re = cmd.ExecuteReader();
            }
            catch { }

            if (re == null || !re.Read())
            {
                //create the USERS table
                cmd = cn.CreateCommand("CREATE TABLE [users] (user_id INTEGER PRIMARY KEY, user_mail TEXT, user_password TEXT, user_level INTEGER)");
                cmd.ExecuteNonQuery();
            }

            //insert one record
            cmd = cn.CreateCommand("INSERT INTO [users] (user_mail, user_password, user_level) VALUES ('test@test.com', '123456', 1)");
            cmd.ExecuteNonQuery();


            //select the table
            cmd = cn.CreateCommand("SELECT * FROM USERS");
            re = cmd.ExecuteReader();

            //select the rows
            while (re.Read())
            {
                for (int i = 0; i < re.FieldCount; i++)
                {
                    Console.WriteLine(re.GetString(i) + "\r\n");
                }

            }
            re.Close();
            cn.Close();

            Console.WriteLine("Press enter to close.");
            Console.ReadLine();
        }
    }
}
