using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using MySql.Data.MySqlClient;

namespace Bot
{
    static class General
    {
        internal static ORACLEClass db;
        internal static IniFile cfg = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\settings.ini");


        public static double[] read_and_sumup(string p)
        {
            List<double> the_list00 = new List<double>();
            List<double> the_list90 = new List<double>();

            //first line contains the header
            string[] lines = File.ReadAllLines(p);

            int dest_size;

            if (lines.Count() > 0)
            {
                dest_size = ((lines.Count() + 1) / 2);

                //if (lines[dest_size]

                Array.Resize(ref lines, dest_size);

            }
            else
            {
                Console.WriteLine("No lines at " + Path.GetFileName(p));
                return null;
            }

            //string line;
            string res;
            int f_pos;
            int empty_pos;

            double tmp;
            string band;

            foreach (string line in lines)
            {
                //zero
                empty_pos = f_pos = -1;
                band = res = null;

                f_pos = line.LastIndexOf('F');

                if (f_pos > -1)
                {
                    empty_pos = line.LastIndexOf(' ', f_pos - 1);

                    if (empty_pos > -1)
                    {
                        empty_pos += 1;
                        res = line.Substring(empty_pos, f_pos - empty_pos);

                        tmp = General.try_parse_double(res);

                        band = line.Substring(84, 2);

                        if (band == "00")
                        {
                            //if (!the_list40.Contains(tmp) && !the_list50.Contains(tmp))
                            the_list00.Add(tmp);
                        }
                        else if (band == "90")
                        {
                            //if (!the_list40.Contains(tmp) && !the_list50.Contains(tmp))
                            the_list90.Add(tmp);
                        }
                    }
                }
            }

            return new double[] { the_list00.Sum(), the_list90.Sum() };
        }

        public static string log_ers(string date_rec,string field2, string error)
        {
            try
            {
                MYSQLClass logdb;
                MySqlException dberr;

                logdb = new MYSQLClass("Server=localhost;Database=bot;Uid=root;Password=;", out dberr);

                if (dberr != null)
                {
                    return "mysql error - " + dberr.Message;
                }

                //write to mysql logdb
                MySqlParameter parameter = null;
                MySqlCommand command = new MySqlCommand("INSERT INTO TMP (date_rec, field2, error) " +
                    " values (@date_rec, @field2, @error)", logdb.GetConnection());

                //date_rec
                parameter = command.CreateParameter();
                parameter.ParameterName = "@date_rec";
                parameter.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                command.Parameters.Add(parameter);
                //field2
                parameter = command.CreateParameter();
                parameter.ParameterName = "@field2";
                parameter.Value = check4null(field2);
                command.Parameters.Add(parameter);

                //error
                parameter = command.CreateParameter();
                parameter.ParameterName = "@error";
                parameter.Value = check4null(error);
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
                //write to mysql logdb

                if (logdb != null)
                {
                    logdb.Close();
                }
            }
            catch (Exception x)
            {
                return "log_sps - " + x.Message + " stack trace : " + x.StackTrace;
            }

            return null;
        }
		
        internal static string check4null(string s)
        {
            return s == null ? "" : s;
        }

        internal static string[] GetFiles(string sourceFolder, string filters, System.IO.SearchOption searchOption)
        {
            return filters.Split('|').SelectMany(filter => System.IO.Directory.GetFiles(sourceFolder, filter, searchOption)).ToArray();
        }


        internal static string SliceSTR(string STR, string STR1, string STR2, int StartIndex)
        {
            try
            {

                int i1 = STR.IndexOf(STR1, StartIndex);
                if (i1 < 0) return ""; else i1 += 1;

                int i2 = STR.IndexOf(STR2, i1 + 1);
                if (i2 < 0) return "";

                return STR.Substring(i1, i2 - i1).Trim();
            }
            catch
            {
                return "";
            }
        }

        #region " FIND DATES FROM KKK "

        internal static string get_dates_from_KKK_file(string fpath)
        {
            DataTable dt = ImportDelimitedFile(fpath, "|", false);

            if (dt == null)
            {
                //MessageBox.Show(fpath + "\r\n\r\n is empty..", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "";
            }

            string k = "";

            var resolve = from r in dt.AsEnumerable()
                          group r by new { date = r.Field<string>("no5") } into fieldb_grp
                          select new
                          {
                              date = fieldb_grp.Key.date
                          };

            foreach (var item in resolve)
            {
                k += item.date.Insert(4, "-").Insert(7, "-") + "*";
            }

            return k;
        }


        public static DataTable ImportDelimitedFile(string filename, string delimiter, bool first_is_column_names)
        {

            DataTable dt = new DataTable();


            using (StreamReader file = new StreamReader(filename))
            {
                //read the first line
                string line = file.ReadLine();

                if (line == null)
                    return null;

                //split the first line to create columns to datatable!
                string[] columns = line.Split(Convert.ToChar(delimiter));// Regex.Split(line, "|");

                for (int i = 0; i < columns.Count(); i++)
                {
                    if (first_is_column_names)
                        dt.Columns.Add(columns[i].Replace("\"", ""));
                    else
                        dt.Columns.Add("no" + i.ToString());
                }

                if (!first_is_column_names)
                {
                    //rewind reader to start!
                    file.DiscardBufferedData();
                    file.BaseStream.Seek(0, SeekOrigin.Begin);
                    file.BaseStream.Position = 0;
                }

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Trim().Length > 0)
                    {
                        line = line.Replace("\"", "");
                        string[] rows = line.Split(Convert.ToChar(delimiter));//Regex.Split(line, delimiter);
                        dt.Rows.Add(rows);

                    }
                }


            }

            return dt;
        }

        #endregion

        internal static string parse_days(string str)
        {
            string[] arr = str.Split(Convert.ToChar("*"));

            string output = "";
            foreach (string line in arr)
            {
                if (line.Length > 0)
                    output += humanize_date(DateTime.Parse(line)) + ", ";
            }

            if (output.EndsWith(", "))
                output = output.Substring(0, output.Length - 2);


            return output;
        }

        internal static string humanize_date(DateTime dt)
        {
            string suffix;

            switch (dt.Day)
            {
                case 1:
                case 21:
                case 31:
                    suffix = "st";
                    break;
                case 2:
                case 22:
                    suffix = "nd";
                    break;
                case 3:
                case 23:
                    suffix = "rd";
                    break;
                default:
                    suffix = "th";
                    break;
            }

            //return string.Format("{0:dddd} {0:MMMM} {1}{2}, {0:yyyy}", dt, dt.Day, suffix);
            return string.Format("{0:dddd} {1}{2}", dt, dt.Day, suffix);
        }

        internal static void SendEmail(string subject, string body, string logfile_filepath, string APP_folder, bool sendto_others)
        {
            MailAddress from = new MailAddress("server@server.com", "Bot mail!");
            MailAddress to = new MailAddress("user1@server.com");
            MailMessage message = new MailMessage(from, to);

            if (sendto_others)
            {
                message.CC.Add("user2@server.com");
                message.CC.Add("user3@server.com");
            }

            if (logfile_filepath != null)
            {
                string logfile_filepath2 = logfile_filepath + "!log.txt";

                //add attachment
                Attachment data = new Attachment(logfile_filepath2, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(logfile_filepath2);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(logfile_filepath2);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(logfile_filepath2);
                message.Attachments.Add(data);
                //add attachment
            }

            //trainto DMLScript.txt
            if (File.Exists(APP_folder + "DMLScript.txt"))
            {
                string logfile_filepath3 = APP_folder + "DMLScript.txt";

                //add attachment
                Attachment data = new Attachment(logfile_filepath3, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(logfile_filepath3);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(logfile_filepath3);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(logfile_filepath3);
                message.Attachments.Add(data);
                //add attachment
            }

            //trainto Rollback.txt
            if (File.Exists(APP_folder + "Rollback.txt"))
            {
                string logfile_filepath4 = APP_folder + "Rollback.txt";

                //add attachment
                Attachment data = new Attachment(logfile_filepath4, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(logfile_filepath4);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(logfile_filepath4);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(logfile_filepath4);
                message.Attachments.Add(data);
                //add attachment
            }


            message.SubjectEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.server.com");
            smtpClient.Send(message);
        }

        public static double try_parse_double(string val)
        {
            double g;

            if (double.TryParse(val, out g))
                return g;
            else
                return 0;
        }
    }
}
