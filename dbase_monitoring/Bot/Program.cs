using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Oracle.DataAccess.Client;
using System.DrUOIng;
using OfficeOpenXml;
using System.Diagnostics;
using Microsoft.WinAny.IO;

namespace Bot
{



    class Program
    {
        //SAP report
        static string report_sap = "";
        static string report_debug_sap = "";

        // invoice start - last
        static string invoice_start = "0";
        static string invoice_end = "0";

        //log file
        static StreamWriter outfile = null;

        //ERS
        static string APP_Folder = @"\\server1\prod\OUT\ARCHIVE\";
        static string APP_SAP_Folder = @"\\server1\prod\IN\PROD\";
        static string APP_STAT_Folder = @"\\server1\prod\";
        static string APP_payment_folder = @"\\server1\PROD\";
        static string APP_dest_dir;
        static string GOAL_file_count;

        //APP invoice start - last
        static string APP_invoice_start = "0";
        static string APP_invoice_end = "0";

        //send mail to others
        static bool mail_othAPP = false;

        //the today folder
        static string date_now;

        static void Main(string[] args)
        {

            //
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //


            //lock to user + machine
            string machine = Environment.MachineName.ToLower();
            string run_user = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToUpper();

            if (machine != "xx" && machine != "yy")
            {
                Environment.Exit(-1);
                return;
            }
            else if (run_user != "EMEA\\BATMAN")
            {
                Environment.Exit(-1);
                return;
            }


            //check for active processes
            Process mysqld = Process.GetProcessesByName("mysqld").FirstOrDefault();
            Process xampp = Process.GetProcessesByName("xampp-control").FirstOrDefault();

            if (mysqld == null || xampp == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("XAMPP or MYSQL not running!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Environment.Exit(-1);
                return;
            }


            //All with ENG culture
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            date_now = DateTime.Now.ToString("yyyyMMdd_HHmmss");

          
            APP_dest_dir = AppDomain.CurrentDomain.BaseDirectory + @"\APP\" + date_now + "\\";

            if (!Directory.Exists(APP_dest_dir))
            {
                Directory.CreateDirectory(APP_dest_dir);
            }

            if (DateTime.Now.Year != 2018)
                return;

            //open/create log file
            outfile = new StreamWriter(APP_dest_dir + "!log.txt", true);

            if (args.Length > 0)
            {
                mail_othAPP = !args.Contains("-nomail");

                switch (args[0].ToLower())
                {

                    case "-ersmonitoring":
                        APP_monitoring();
                        break;
                    default:
                        break;
                }

            }

            if (outfile != null)
                outfile.Close();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (outfile != null)
                outfile.Close();

            log(e.Exception.StackTrace);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (outfile != null)
                outfile.Close();


            Exception f = (Exception)e.ExceptionObject;

            if (f.StackTrace != null)
                log(f.StackTrace);
            else
                log(f.Message);
        }

        private static void APP_monitoring()
        {
            string err = null;
            string db_TIRES_invoice_count = "0";
            string db_STAGE_invoice_count = "0";
            string success_db_invoice_recordcount_log = "0";
            string success_db_NIAOU_recordcount_log = "0";
            string success_db_product_recordcount_log = "0";
            string success_db_NIAOU_recordcount = "0";
            object db_orderdetails_duplicate_count = null;
            string db_PAYMENTS_count = "0";
            string db_PAYMENTS_DETAIL_count = "0";
            string db_stage_proceedY = "0";
            string db_stage_proceedN = "0";
            string db_stage_invoice_types = "";
            string db_stage_invoice_XXX = "";

            //
            int KKK_exists = 0;
            int LLL_exists = 0;
            int III_exists = 0;
            double file_OOO_sumup = 0;
            double DB_OOO_sumup = 0;
            double file_LLL_sumup = 0;
            double DB_LLL_sumup = 0;
            double file_III_sumup = 0;
            double DB_III_sumup = 0;
            object tmp = null;
            string GOAL_report = "";

            List<db_record> GOAL = null;
            string APP_FISH_log = "";
            string kjdshfsdljfds = "";
            Dictionary<string, string> APP_SAP = null;
            bool GOAL_error_files_vs_dbase = false;
            bool score = false;
            bool APP_papAPP = false;
            bool OOO_bepapAPP = false;
            bool OOO_betablepapAPP = false;
            string log_table = "";
            string PPlog_table = "";
            string APP_FISH_NIAOUtrainto = "";
            string APP_FISH_log_report = "0";
            string FISH_APP_report = "0";

            DataTable dT_travelto_duplicate = null;


            //MONTH STATUS
            bool UOI = false; 
            string dt_dashboard_count = "0";

            string mail_subject = "";

            log("--APP Monitoring - started--");


            try
            {

                string u = General.cfg.Read("user");
                string p = General.cfg.Read("pass");

                log("Connecting to SERVER.XXXXX");

                /////////////////////////////////////////////////////
                string conn = "Data Source=(DESCRIPTION=(ADDRESS_LIST="
                            + "(ADDRESS=(PROTOCOL=TCP)(HOST=serverX.server.com)(PORT=0000)))"
                            + "(CONNECT_DATA=(SID=DDDDDD)));"
                            + "User Id=" + u + " ;Password=" + p + ";";

                OracleException x = null;
                General.db_APP = new ORACLEClass(conn, out x);

                if (x != null)
                {
                    log("Connection Error -- " + x.Message);
                    return;
                }
                /////////////////////////////////////////////////////

       
                log("Connected!");

                //////////////////////////////////////
                ///////////////// GOAL FILES START
                GOAL = find_GOAL_ties();

                if (GOAL != null)
                {
                    //validation - Found files equal with DB Values 
                    foreach (db_record item in GOAL)
                    {
                        //when one of the records has 0 value, means no file for this.
                        if (item.field_value == 0)
                            continue;

                        if (item.match_file == null)
                        {
                            GOAL_error_files_vs_dbase = true;
                            GOAL_report += "<span style='color:red;'>ERROR *** The db value SUM(" + item.field_name + ") which equals " + item.field_value.ToString() + " couldnt find a pair file" + "</span><br>";
                            log("ERROR *** The db value SUM(" + item.field_name + ") which equals " + item.field_value.ToString() + " couldnt find a pair file");
                        }
                        else
                        {
                            GOAL_report += item.match_file.filename + " equals with SUM(" + item.field_name + ") which are " + item.field_value.ToString() + "<br>";
                            log(item.match_file.filename + " equals with SUM(" + item.field_name + ") which are " + item.field_value.ToString());
                        }
                    }
                }
                else
                {
                    GOAL_error_files_vs_dbase = true;
                    log("No GOAL files found!");
                }
                ///////////////// GOAL FILES END
                //////////////////////////////////////


                ////////////////////////////////
                //Copy any PAYMENTS files
                var directoryp = new DirectoryInfo(APP_payment_folder);
                List<FileInfo> filesp = directoryp.GetFiles()
                                        .Where(file => file.LastWriteTime > DateTime.Today).ToList();
                foreach (FileInfo item in filesp)
                {
                    log("Copying " + item.Name);
                    File.Copy(APP_payment_folder + item.Name, APP_dest_dir + item.Name);
                    log(item.Name + " copied locally");
                }


                //////////////////////////////////////
                ///////////////// APP XXXXXXX FILES START
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(APP_dest_dir);
                foreach (FileInfo f in dir.GetFiles())
                {
                    //QQQ File - Process START
                    if (f.Name.ToLower().StartsWith("oooo"))
                    {
                        log("OOO file found (" + f.Name + ")");

                        try
                        {
                            file_OOO_sumup = process_KKK_LLL_III_file(f.FullName);
                            log("File OOO SumUP : " + file_OOO_sumup.ToString());
                            KKK_exists = 1;
                        }
                        catch (Exception ui)
                        {
                            log("ERROR *** when processing OOO file (" + f.Name + ") - " + ui.Message);
                        }

                        if (file_OOO_sumup > 0)
                        {
                            tmp = General.db.ExecuteSQLScalar(Bot.Properties.Resources.oracle_APP_OOO_sum_q.Replace("{month}", DateTime.Today.Month.ToString()).Replace("{year}", DateTime.Today.Year.ToString()));
                            if (tmp != null)
                            {
                                DB_OOO_sumup = General.try_parse_double(tmp.ToString());
                                log("DBASE UUU SumUP : " + DB_OOO_sumup.ToString());
                            }
                            else
                                log("DBASE UUU SumUP : the return value is NULL");

                        }
                    }
                    //XXXXXXX File - Process END

                    //YYYYYYYYYY File - Process START
                    if (f.Name.ToLower().StartsWith("lkk"))
                    {
                        log("PPP file found (" + f.Name + ")");

                        try
                        {
                            file_LLL_sumup = process_KKK_LLL_III_file(f.FullName);
                            log("File PPP Sumup : " + file_LLL_sumup.ToString());
                            LLL_exists = 1;
                        }
                        catch (Exception mmm)
                        {
                            log("ERROR *** when processing PPP file (" + f.Name + ") - " + mmm.Message);
                        }

                        if (file_LLL_sumup > 0)
                        {
                            tmp = General.db.ExecuteSQLScalar(Bot.Properties.Resources.oracle_APP_LLL_sum_q.Replace("{month}", DateTime.Today.Month.ToString()).Replace("{year}", DateTime.Today.Year.ToString()));
                            if (tmp != null)
                            {
                                DB_LLL_sumup = General.try_parse_double(tmp.ToString());
                                log("DBASE PPP Sumup : " + DB_LLL_sumup.ToString());
                            }
                            else
                                log("DBASE PPP Sumup : the return value is NULL");

                        }
                    }
                    //YYYYYYYYYY File - Process END


                    //GGGGG File - Process START
                    if (f.Name.ToLower().StartsWith("sadasd"))
                    {
                        log("HHH file found (" + f.Name + ")");

                        try
                        {
                            file_III_sumup = process_KKK_LLL_III_file(f.FullName);
                            log("File HHH Sumup : " + file_III_sumup.ToString());
                            III_exists = 1;
                        }
                        catch (Exception sdas)
                        {
                            log("ERROR *** when processing HHH file (" + f.Name + ") - " + sdas.Message);
                        }

                        if (file_III_sumup > 0)
                        {
                            tmp = General.db.ExecuteSQLScalar(Bot.Properties.Resources.oracle_APP_III_sum_q.Replace("{month}", DateTime.Today.Month.ToString()).Replace("{year}", DateTime.Today.Year.ToString()));
                            if (tmp != null)
                            {
                                DB_III_sumup = General.try_parse_double(tmp.ToString());
                                log("DBASE HHH Sumup : " + DB_III_sumup.ToString());
                            }
                            else
                                log("DBASE HHH Sumup : the return value is NULL");

                        }
                    }
                    //GGGGG File - Process END
                }

                ///////////////// APP XXXXX FILES END
                //////////////////////////////////////

                /////////////////////////////////////////////////////////
                ///////////////// process SAP files from SAPINPUT START
                try
                {
                    APP_SAP = APP_SAP_Files();
                }
                catch (Exception erssap)
                {
                    log("ERROR *** When processing SAP files " + erssap.Message);
                }
                ///////////////// process SAP files from SAPINPUT RND
                /////////////////////////////////////////////////////////

                // using (new Impersonation("EMEA", "BATMAN", "x"))
                {

                    ///
                    //get all today files from APP papAPP + GRILLS report 
                    var directory = new DirectoryInfo(APP_STAT_Folder);
                    List<FileInfo> files = directory.GetFiles()
                                            .Where(file => file.LastWriteTime > DateTime.Today).ToList();

                    string file_robin;
                    foreach (FileInfo item in files)
                    {
                        //todo : should be saved to dbase 
                        file_robin = item.Name;
                        log("Copying " + file_robin);
                        File.Copy(APP_STAT_Folder + file_robin, APP_dest_dir + file_robin);
                        log(file_robin + " copied locally");

                        if (item.Name.ToLower().StartsWith("score"))
                            score = true;

                        if (item.Name.ToLower().Contains("APP_papers"))
                            APP_papAPP = true;

                        if (item.Name.ToLower().Contains("OOO_bepapers"))
                            OOO_bepapAPP = true;

                        if (item.Name.ToLower().Contains("OOO_betablepapers"))
                            OOO_betablepapAPP = true;

                    }


                }

                //STAGE records proceed and not
                log("STAGE proceed YES - select count(distinct CODE) from OWNER.APP_INVOICES_STG where CODE between " + APP_SAP["first_invoice"] + " and " + APP_SAP["last_invoice"] + "  and PROCESSED_YN='Y'");
                db_stage_proceedY = General.db.ExecuteSQLScalar("select count(distinct CODE) from OWNER.APP_INVOICES_STG where CODE between " + APP_SAP["first_invoice"] + " and " + APP_SAP["last_invoice"] + "  and PROCESSED_YN='Y'").ToString();
                log("Result    >> " + db_stage_proceedY);

                log("STAGE proceed NO - select count(distinct CODE) from OWNER.APP_INVOICES_STG where CODE between " + APP_SAP["first_invoice"] + " and " + APP_SAP["last_invoice"] + "  and PROCESSED_YN='N'");
                db_stage_proceedN = General.db.ExecuteSQLScalar("select count(distinct CODE) from OWNER.APP_INVOICES_STG where CODE between " + APP_SAP["first_invoice"] + " and " + APP_SAP["last_invoice"] + "  and PROCESSED_YN='N'").ToString();
                log("Result    >> " + db_stage_proceedY);


                //YOYO
                log("YOYO PAYMENTS validation - select count(*) from OWNER.FISH_APP_PAYMENTSCOUPON where lastprocessedTP >= to_date('" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 03:30:00','YYYY-MM-WWW HH24:MI:SS')");
                db_PAYMENTS_count = General.db.ExecuteSQLScalar("select count(*) from OWNER.FISH_APP_PAYMENTSCOUPON where lastprocessedTP >= to_date('" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 03:30:00','YYYY-MM-WWW HH24:MI:SS')").ToString();
                log("Result    >> " + db_PAYMENTS_count);

                log("YOYO 545645 validation - select count(*) from OWNER.FISH_APP_545645COUPON where lastprocessedTP >= to_date('" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 03:30:00','YYYY-MM-WWW HH24:MI:SS')");
                db_PAYMENTS_DETAIL_count = General.db.ExecuteSQLScalar("select count(*) from OWNER.FISH_APP_545645COUPON where lastprocessedTP >= to_date('" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 03:30:00','YYYY-MM-WWW HH24:MI:SS')").ToString();
                log("Result    >> " + db_PAYMENTS_DETAIL_count);

                ///////////////// papAPP have been successfully interfaced to robin
                ///////////////////////////////////////////////////////////////////////////

                log("APP_TIRES query - select count(*) from OWNER.APP_TIRES where INVOICENO between " + APP_SAP["first_invoice"] + " and " + APP_SAP["last_invoice"]);
                db_TIRES_invoice_count = General.db.ExecuteSQLScalar("select count(*) from OWNER.APP_TIRES where INVOICENO between " + APP_SAP["first_invoice"] + " and " + APP_SAP["last_invoice"]).ToString();
                log("Result    >> " + db_TIRES_invoice_count);

                log("APP_INVOICES_STG query - select count(*) from OWNER.APP_INVOICES_STG where CODE between " + APP_SAP["first_invoice"] + " and " + APP_SAP["last_invoice"]);
                db_STAGE_invoice_count = General.db.ExecuteSQLScalar("select count(*) from OWNER.APP_INVOICES_STG where CODE between " + APP_SAP["first_invoice"] + " and " + APP_SAP["last_invoice"]).ToString();
                log("Result    >> " + db_STAGE_invoice_count);

                log("Verification for Order Details duplicate - " + Bot.Properties.Resources.APP_validation.Replace("{month}", DateTime.Today.AddDays(-1).ToString("MM")).Replace("{year}", DateTime.Today.AddDays(-1).ToString("yyyy")));
                db_orderdetails_duplicate_count = General.db.ExecuteSQLScalar(Bot.Properties.Resources.APP_validation.Replace("{month}", DateTime.Today.AddDays(-1).ToString("MM")).Replace("{year}", DateTime.Today.AddDays(-1).ToString("yyyy")));
                //log("Result    >> " + db_orderdetails_duplicate_count);

                ///todo : should be saved to dbase 
                if (APP_SAP["invoice_file"] == "1")
                {
                    //is not correct, with SYSDATE takes the now date + time... 
                    log("invoice file exists - query dbase log table via : select count(*) from OWNER.APP_FISH_log where IDENTIFIER='1' and source = 'SP_BallonInvoices' and (lastprocessed between SYSDATE-1 and SYSDATE) ORDER BY  theID desc");
                    success_db_invoice_recordcount_log = General.db.ExecuteSQLScalar("select count(*) from OWNER.APP_FISH_log where IDENTIFIER='1' and source = 'SP_BallonInvoices' and (lastprocessed between SYSDATE-1 and SYSDATE) ORDER BY  theID desc").ToString();
                    log("Result    >> " + success_db_invoice_recordcount_log);
                }

                if (APP_SAP["NIAOU_file"] == "1")
                {
                    log("NIAOU file exists - query dbase log table via : select count(*) from OWNER.APP_FISH_log where IDENTIFIER='1' and source = 'SP_123NIAOU456' and (lastprocessed between SYSDATE-1 and SYSDATE) ORDER BY  theID desc");
                    success_db_NIAOU_recordcount_log = General.db.ExecuteSQLScalar("select count(*) from OWNER.APP_FISH_log where IDENTIFIER='1' and source = 'SP_123NIAOU456' and (lastprocessed between SYSDATE-1 and SYSDATE) ORDER BY  theID desc").ToString();
                    log("Result    >> " + success_db_NIAOU_recordcount_log);

                    log("NIAOU file exists - query dbase - count NIAOU records : select count(*) from OWNER.APP_SAP_NIAOUHIERARCHY");
                    success_db_NIAOU_recordcount = General.db.ExecuteSQLScalar("select count(*) from OWNER.APP_SAP_NIAOUHIERARCHY").ToString();
                    log("Result    >> " + success_db_NIAOU_recordcount);
                }

                if (APP_SAP["product_file"] == "1")
                {
                    log("product file exists - query dbase log table via : select count(*) from OWNER.APP_FISH_log where IDENTIFIER='1' and source = 'SP_21312321' and (lastprocessed between SYSDATE-1 and SYSDATE) ORDER BY  theID desc");
                    success_db_product_recordcount_log = General.db.ExecuteSQLScalar("select count(*) from OWNER.APP_FISH_log where IDENTIFIER='1' and source = 'SP_21312321' and (lastprocessed between SYSDATE-1 and SYSDATE) ORDER BY  theID desc").ToString();
                    log("Result    >> " + success_db_product_recordcount_log);
                }

                //if any result, there is an error
                log("Check for error at log table - select count(*) from OWNER.APP_FISH_log where ERRORNUMBER NOT IN ('LOG','0') AND lastprocessed like sysdate");
                APP_FISH_log = General.db.ExecuteSQLScalar("select count(*) from OWNER.APP_FISH_log where ERRORNUMBER NOT IN ('LOG','0') AND lastprocessed like sysdate").ToString();
                log("Result    >> " + APP_FISH_log);

                //replication run
                log("Check if replication run - select count(*) from OWNER.FISH_APP_systemvarsCOUPON  where name='THUNDER' and varvalueCOUPON=1 and VARKEYCOUPON='JO'");
                kjdshfsdljfds = General.db.ExecuteSQLScalar("select count(*) from OWNER.FISH_APP_systemvarsCOUPON  where name='THUNDER' and varvalueCOUPON=1 and VARKEYCOUPON='JO'").ToString();
                log("Result    >> " + kjdshfsdljfds);



                ////////////////////////////////////////////
                //when payments done - export XLSs [START]
                if (KKK_exists == 1 && LLL_exists == 1)
                {
                    string export;
                    export = export_APP_payments("HOT");

                    if (export != null)
                    {
                        if (err == null)
                            err = export;
                        else
                            err += export;
                    }

                    export = export_APP_payments("TOT");

                    if (export != null)
                    {
                        if (err == null)
                            err = export;
                        else
                            err += export;
                    }

                    mail_subject = "[ZZZ + TOT] - ";
                }

                if (III_exists == 1)
                {
                    string export;

                    export = export_APP_payments("22");

                    if (export != null)
                    {
                        if (err == null)
                            err = export;
                        else
                            err += export;
                    }

                    mail_subject = "[QQ] - ";
                }
                //when payments done - export XLSs [END]
                ////////////////////////////////////////////

                //dashboard_peter
                log("Quering dashboard - " + Bot.Properties.Resources.dashboard_minimal);
                object dt_dashboard = General.db.ExecuteSQLScalar(Bot.Properties.Resources.dashboard_minimal);
                if (dt_dashboard != null)
                {
                    try
                    {
                        dt_dashboard_count = dt_dashboard.ToString();
                    }
                    catch
                    {
                        dt_dashboard_count += "0";
                    }
                }

                //db_stage_invoice_types
                log("Quering Invoice Types - " + Bot.Properties.Resources.APP_invoice_types.Replace("{start}", APP_SAP["first_invoice"]).Replace("{end}", APP_SAP["last_invoice"]));
                DataTable dt_inv_types = General.db.GetDATATABLE(Bot.Properties.Resources.APP_invoice_types.Replace("{start}", APP_SAP["first_invoice"]).Replace("{end}", APP_SAP["last_invoice"]));
                if (dt_inv_types != null && dt_inv_types.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow item in dt_inv_types.Rows)
                        {
                            db_stage_invoice_types += item["INVOICE_TYPE"] + " : " + item["countt"] + "<br>";
                        }
                    }
                    catch
                    {
                        db_stage_invoice_types += "error occured";
                    }
                }

                //db_stage_XXX_processed YES
                dt_inv_types = null;
                log("Quering Invoice Types - " + Bot.Properties.Resources.APP_invoices_XXX.Replace("{start}", APP_SAP["first_invoice"]).Replace("{end}", APP_SAP["last_invoice"]));
                dt_inv_types = General.db.GetDATATABLE(Bot.Properties.Resources.APP_invoices_XXX.Replace("{start}", APP_SAP["first_invoice"]).Replace("{end}", APP_SAP["last_invoice"]));
                if (dt_inv_types != null && dt_inv_types.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow item in dt_inv_types.Rows)
                        {
                            db_stage_invoice_XXX += item["CODE"] + ",";
                        }

                        if (db_stage_invoice_XXX.Length > 1)
                            db_stage_invoice_XXX = db_stage_invoice_XXX.Substring(0,db_stage_invoice_XXX.Length-1);

                    }
                    catch
                    {
                        db_stage_invoice_XXX += "error occured";
                    }
                }


                //APP_FISH_NIAOUtrainTO [start]
                log("Verification for traintos duplicate - " + Bot.Properties.Resources.travelto_duplicate);
                string PPlog_NIAOUtravelto_q = "select NIAOUnumber, NIAOUsitetrainto, NIAOUsitenumber,NIAOUsitebilltoref,lastprocessed,NIAOUsitename from OWNER.APP_FISH_NIAOUtrainTO where NIAOUsitenumber in (  select NIAOUsitenumber From OWNER.APP_FISH_NIAOUtrainTO WHERE ACTIVE = 'A' group by NIAOUsitenumber having count (NIAOUsitenumber) > 1 ) order by NIAOUsitenumber asc";
                dT_travelto_duplicate = General.db.GetDATATABLE(PPlog_NIAOUtravelto_q);
                if (dT_travelto_duplicate != null && dT_travelto_duplicate.Rows.Count > 0)
                {
                    //used to construct traintos - DMLScript + Rollback scripts
                    string travelto_update = "update OWNER.APP_FISH_NIAOUtrainTO set Active='I', lastprocessed=sysdate where \r\n";
                    string travelto_rollback = "update OWNER.APP_FISH_NIAOUtrainTO set Active='A', lastprocessed=sysdate where \r\n";
                    string where_condition = "";
                    //used to construct traintos - DMLScript + Rollback scripts

                    APP_FISH_NIAOUtrainto = "<br><br><h3>APP_FISH_NIAOUtrainTO (RFC - 1126545)</h3><table style='font-family: verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #c0c0c0;border-collapse: collapse;  width:755px; margin: 0 auto;'><thead><tr><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>NIAOU Number</th><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>trainto</th><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>NIAOUsitenumber</th><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>NIAOUsitebilltoref</th><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>lastprocessed</th><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>NIAOUsitename</th></tr></thead><tbody>";
                    foreach (DataRow item in dT_travelto_duplicate.Rows)
                    {
                        //used to construct traintos - DMLScript + Rollback scripts
                        where_condition += " (NIAOUNUMBER='" + item["NIAOUnumber"] + "' and NIAOUSITEtrainTO='" + item["NIAOUsitetrainto"] + "') or \r\n";

                        APP_FISH_NIAOUtrainto += "<tr>" + "<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>" + item["NIAOUnumber"] + "</td>" + "<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>" + item["NIAOUsitetrainto"] + "</td>" + "</td>" + "<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>" + item["NIAOUsitenumber"] + "</td>" + "</td>" + "<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>" + item["NIAOUsitebilltoref"] + "</td>" + "<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>" + item["lastprocessed"] + "</td>" + "<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>" + item["NIAOUsitename"] + "</td>" + "</tr>";
                    }

                    APP_FISH_NIAOUtrainto += "</tbody></table><br><br>";

                    //used to construct DMLScript + Rollback scripts
                    where_condition = where_condition.Substring(0, where_condition.Length - 5);
                    travelto_update += where_condition + "; \r\ncommmit;";
                    travelto_rollback += where_condition + "; \r\ncommmit;";

                    try
                    {
                        File.WriteAllText(APP_dest_dir + "DMLScript.txt", travelto_update);
                        File.WriteAllText(APP_dest_dir + "Rollback.txt", travelto_rollback);
                    }
                    catch { }
                }
                else
                    APP_FISH_NIAOUtrainto = "No rows from OWNER.APP_FISH_NIAOUtrainTO";
                //APP_FISH_NIAOUtrainTO [end]

                //mirror the ##FISH_APP_SYSTEMVARSCOUPON LOG## TABLE!! to mail! [start]
                string log_table_q = "select lastprocessedTP, system_vars_id_icsCOUPON,varkeyCOUPON,varvalueCOUPON from OWNER.FISH_APP_SYSTEMVARSCOUPON lat where lastprocessedTP between to_date('" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00','YYYY-MM-WWW HH24:MI:SS') and to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + " 23:59:59','YYYY-MM-WWW HH24:MI:SS') and countrycodeCOUPON = 'UK'  order by varvalueCOUPON,lastprocessedTP desc";
                DataTable dT = General.db.GetDATATABLE(log_table_q);

                if (dT != null)
                {
                    //for the dbase
                    FISH_APP_report = dT.Rows.Count.ToString();

                    log_table = "<br><br><h3>FISH_APP_SYSTEMVARSCOUPON</h3><span style='font-size:12px;font-family:tahoma arial;'>between to_date('" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00','YYYY-MM-WWW HH24:MI:SS') and to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + " 23:59:59','YYYY-MM-WWW HH24:MI:SS')</span>&nbsp;&nbsp;<span style='font-weight:bold;color:red'>" + FISH_APP_report + "</span><table style='font-family: verdana,arial,sans-serif;		font-size:11px;		color:#333333;		border-width: 1px;		border-color: #c0c0c0;		border-collapse: collapse;  width:755px; margin: 0 auto;'><thead><tr><th style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>lastprocessedTP</th><th style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>system_var_id_ics</th><th style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>varkey</th><th style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>varvalue</th></tr></thead><tbody>";
                    foreach (DataRow item in dT.Rows)
                    {
                        if (item["varvalueCOUPON"].ToString().ToUpper() == "UOI")
                            UOI = true;
                        //else if (item["varvalueCOUPON"].ToString().ToUpper() == "ONL")
                        //    ONL = true;


                        log_table += "<tr>" + "<td style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>" + item["lastprocessedTP"] + "</td>" + "<td style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>" + item["system_vars_id_icsCOUPON"] + "</td>" + "<td style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>" + item["varkeyCOUPON"] + "</td>" + "<td style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>" + item["varvalueCOUPON"] + "</td>"
                            + "</tr>";
                    }

                    if (UOI)
                        mail_subject += "*month closed* - ";

                    log_table += "</tbody></table><br><br>";
                }
                else
                    log_table = "No rows from OWNER.FISH_APP_SYSTEMVARSCOUPON";
                //mirror the ##FISH_APP_SYSTEMVARSCOUPON LOG## TABLE!! to mail! [end]


                //PPlog_table [start]
                string PPlog_table_q = "select lastprocessed,message from OWNER.APP_FISH_LOG where lastprocessed between to_date('" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00','YYYY-MM-WWW HH24:MI:SS') and to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + " 23:59:59','YYYY-MM-WWW HH24:MI:SS') and (message like '%Finished%' or message like '%report generated%') order by lastprocessed desc";
                dT = General.db.GetDATATABLE(PPlog_table_q);
                if (dT != null)
                {
                    //for the dbase
                    APP_FISH_log_report = dT.Rows.Count.ToString();

                    PPlog_table = "<br><br><h3>APP_FISH_LOG</h3><span style='font-size:12px;font-family:tahoma arial;'>between to_date('" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00','YYYY-MM-WWW HH24:MI:SS') and to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + " 23:59:59','YYYY-MM-WWW HH24:MI:SS') and (message like '%Finished%' or message like '%report generated%')</span>&nbsp;&nbsp;<span style='font-weight:bold;color:red'>" + APP_FISH_log_report + "</span><table style='font-family: verdana,arial,sans-serif;		font-size:11px;		color:#333333;		border-width: 1px;		border-color: #c0c0c0;		border-collapse: collapse;  width:755px; margin: 0 auto;'><thead><tr><th style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>lastprocessed</th><th style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>message</th></tr></thead><tbody>";
                    foreach (DataRow item in dT.Rows)
                    {
                        PPlog_table += "<tr>" + "<td style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>" + item["lastprocessed"] + "</td>" + "<td style='border-width: 1px;		padding: 8px;		border-style: solid;		border-color: #c0c0c0;'>" + item["message"] + "</td>" + "</tr>";
                    }

                    PPlog_table += "</tbody></table><br><br>";
                }
                else
                    PPlog_table = "No rows from OWNER.APP_FISH_LOG";
                //PPlog_table [start]

                if (General.db != null && db_orderdetails_duplicate_count == null)
                {
                    //General.db_ers.Close();

                    //log("Disconnected by SERVER.XXXX");
                }
                else
                    log("Leave the connection open at SERVER.XXXX to check for duplicates");
            }
            catch (Exception x)
            {
                log("Catch an error APPMONITORING " + x.Message + " stack trace : " + x.StackTrace);
                err = x.Message + " stack trace : " + x.StackTrace;
            }

            //validation for null values 
            if (GOAL_file_count == null)
                GOAL_file_count = "0";
            if (GOAL == null)
            {
                GOAL = new List<db_record>();
                GOAL.Add(new db_record(0, "APP_SAP_GOALCalculationTOT.GOAL", null));
                GOAL.Add(new db_record(0, "APP_SAP_GOALCalculation.GOAL", null));
                GOAL_report = "no records found";
            }

            string orderdetails_duplicate_count = "0";
            if (db_orderdetails_duplicate_count != null)
                orderdetails_duplicate_count = "1";



            //myssql log
            string mysql_log_result = General.log_ers(date_rec, field2, error, err);

            if (mysql_log_result != null)
                log("MySQLError - " + mysql_log_result);

            log("--APP Monitoring - ended--");

            string mail_body = "<span style='color:#1F497D;margin:0cm;margin-bottom:.0001pt;font-size:11.0pt;font-family:\"Calibri\",\"sans-serif\";'>Good Morning All,<br><br>Please find below APP job status on " + General.humanize_date(DateTime.Today) + ".<br><br>";
            mail_body += report_sap + "<br>";

            //validation
            if (APP_FISH_log != "0")
                mail_body += "<span style='color:red;'>ERROR - APP_FISH_log return records.</span>" + "<br>";

            if (kjdshfsdljfds != "1")
                mail_body += "<span style='color:red;'>ERROR - kjdshfsdljfds the result is not equal with 1.</span>" + "<br>";

            if (db_orderdetails_duplicate_count != null)
                mail_body += "<span style='color:red;'>ERROR - duplicate record(s) found in order details. Quering for payments, a new mail will be send.</span>" + "<br>";

            if (dT_travelto_duplicate != null && dT_travelto_duplicate.Rows.Count > 0)
                mail_body += "<span style='color:red;'>ERROR - duplicate trainto record(s) found in APP_FISH_NIAOUtrainTO.</span>" + "<br>";

            if (APP_SAP["invoice_file"] == "1" && success_db_invoice_recordcount_log != "1")
                mail_body += "<span style='color:red;'>ERROR - Invoice file exists but the record count !=1 at APP_FISH_log.</span>" + "<br>";

            if (APP_SAP["NIAOU_file"] == "1" && success_db_NIAOU_recordcount_log != "1")
                mail_body += "<span style='color:red;'>ERROR - NIAOU file exists but the record count !=1 at APP_FISH_log.</span>" + "<br>";

            if (APP_SAP["product_file"] == "1" && success_db_product_recordcount_log != "1")
                mail_body += "<span style='color:red;'>ERROR - Product file exists but the record count !=1 at APP_FISH_log.</span>" + "<br>";

            if (APP_SAP["flat_cust"] != success_db_NIAOU_recordcount)
                mail_body += "<span style='color:red;'>ERROR - NIAOU file exists but the file lines != APP_sap_NIAOUhierarchy record count.</span>" + "<br>";


            //validation

            if (GOAL_error_files_vs_dbase)
                mail_body += "<span style='color:red;'>ERROR on GOALs!</span>" + "<br>";
            else
                mail_body += "-The GOALs are generated and interfaced to SAP. Amount in GOAL files is matching with the amount calculated from GOAL tables." + "<br>";

            if (III_exists == 1)
            {
                mail_body += "-Please find attached the WWW payments generated for the month of " + DateTime.Today.AddMonths(-1).ToString("MMMM yyyy") + ".<br>";

                if (file_III_sumup != DB_III_sumup)
                    mail_body += "<span style='color:red;'>ERROR - WWW file sum (" + file_III_sumup.ToString() + ") is not equals with dbase sum (" + DB_III_sumup.ToString() + ")</span>" + "<br>";
                else
                    report_debug_sap += "WWW File SUM : " + file_III_sumup.ToString() + "<br>WWW dbase SUM : " + DB_III_sumup.ToString() + @".<br>Files exported to \\comp\APP\" + date_now + @"\payments\" + "<br>";

            }
            else if (KKK_exists == 1 && LLL_exists == 1)
            {
                mail_body += "-Please find attached the ZZZ & QQQ payments generated for the month of " + DateTime.Today.AddMonths(-1).ToString("MMMM yyyy") + ".<br>";

                if (file_LLL_sumup != DB_LLL_sumup)
                    mail_body += "<span style='color:red;'>ERROR - QQQ file sum (" + file_LLL_sumup.ToString() + ") is not equals with dbase sum (" + DB_LLL_sumup.ToString() + ")</span>" + "<br>";
                else
                    report_debug_sap += "ZZZ File SUM : " + file_LLL_sumup.ToString() + "<br>ZZZ dbase SUM : " + DB_LLL_sumup.ToString() + @".<br>Payments exported to \\comp\APP\" + date_now + @"\payments\" + "<br>";

                if (file_OOO_sumup != DB_OOO_sumup)
                    mail_body += "<span style='color:red;'>ERROR - QQQ file sum (" + file_OOO_sumup.ToString() + ") is not equals with dbase sum (" + DB_OOO_sumup.ToString() + ")</span>" + "<br>";
                else
                    report_debug_sap += "QQQ File SUM : " + file_OOO_sumup.ToString() + "<br>QQQ dbase SUM : " + DB_OOO_sumup.ToString() + @".<br>Payments exported to \\comp\APP\" + date_now + @"\payments\" + " <br>";
            }
            else if (UOI)
                report_debug_sap += @"GOALs generated to \\comp\APP\" + date_now + @"\GOALs\" + "<br>";

            if (APP_papAPP || OOO_bepapAPP || OOO_betablepapers)
            {
                mail_body += "-papAPP have been successfully interfaced to robin.<br>";
                mail_subject += "[robin] - ";
            }

            if (score)
            {
                mail_body += "###from Oracle to the YOYO(UI). All payments are synchronized in YOYO.<br>-The GRILLS report has successfully generated.<br>###The GRILLS shared with Quodem successfully.<br>";
            }
            else
                mail_subject += "[GRILLS missing] - ";

            if (APP_SAP["flat_unique_inv"] != APP_SAP["stage_versus_flat"] && APP_SAP["flat_unique_inv"] != APP_SAP["tires_versus_flat"])
                mail_body += "<span style='color:red;'>ERROR - Database invoice count (XXX or STAGE) no match with flat file.</span>" + "<br>";
            else
            {
                mail_body += "-" + APP_SAP["flat_unique_inv"] + " invoices sent by SAP." + "<br>";
                mail_body += "-" + APP_SAP["tires_versus_flat"] + " invoices have been loaded to APP main table." + "<br>";

                if (dt_dashboard_count != APP_SAP["tires_versus_flat"])
                    mail_body += "<span style='color:red;'>";
                else
                    mail_body += "<span>";

                mail_body += "-" + dt_dashboard_count + " invoices by the new invoice dashboard." + "</span><br>";
            }

            mail_body += "-<b><span style='color:#E46C0A'>APP system is up to date with all sales</span></b>.<br>";

            mail_body += "<br><br>";
            mail_body += "Please let us know in case you need further details.";

            if (db_stage_invoice_XXX.Length > 0)
                mail_body += "<br><br><br><h3>XXX invoices (processed) :</h3> " + db_stage_invoice_XXX;

             mail_body +="<br><br><br><h3>SAP dbase info:</h3>";
            mail_body += "Payment Count (" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + ") : " + db_PAYMENTS_count.ToString() + "<br>";
            mail_body += "Payment Details Count (" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + ") : " + db_PAYMENTS_DETAIL_count.ToString() + "<br><br>";
            mail_body += report_debug_sap + APP_FISH_NIAOUtrainto + log_table + PPlog_table + "<br></span>";

            //close log file to send it as attachment
            outfile.Close();
            outfile = null;

            try
            {
                File.WriteAllText(APP_dest_dir + "mail.html", mail_body);

                //Export GOALs
                if (UOI)
                {
                    export_GOALs();
                }

                try
                {
                    if (err != null)
                        General.SendEmail(mail_subject + "APP Error Report for " + DateTime.Today.ToString("yyyyMMdd"), "Error occurred, please check the attachment." + mail_body, APP_dest_dir, null, mail_others);
                    else
                    {
                        General.SendEmail(mail_subject + "APP daily job status ran successfully on " + General.humanize_date(DateTime.Now) + " of " + DateTime.Now.ToString("MMMM yyyy"), mail_body, APP_dest_dir, APP_dest_dir, mail_others);
                    }
                }
                catch (Exception x) { File.WriteAllText(APP_dest_dir + "\\!error.txt", x.Message + "\r\n\r\n" + x.StackTrace); }

                //ORDER DETAIL - DUPLICATE
                if (db_orderdetails_duplicate_count != null)
                {
                    find_payments();
                }

                if (General.db != null)
                {
                    General.db.Close();
                    
                }

            }
            catch (Exception x)
            {
                File.WriteAllText(APP_dest_dir + "\\!error2.txt", x.Message + "\r\n\r\n" + x.StackTrace);
            }

            //after analyze del all source files
            SecureDeleteExtensions.Delete(new DirectoryInfo(APP_dest_dir), OverwriteAlgorithm.Quick);
        }

        private static void export_GOALs()
        {
            export_APP_GOALs("HOT");
            export_APP_GOALs("TOT");
            export_APP_GOALs("22");
        }



        private static void export_APP_GOALs(string type)
        {
            DataTable dT;
            string q = "";
            string APP_dir = APP_dest_dir + "GOALs"; // +@"\APP\";

            if (!Directory.Exists(APP_dir))
                Directory.CreateDirectory(APP_dir);

            //****************************************************** 
            DateTime dtp_APP = DateTime.Now.AddMonths(-1);
            //******************************************************

            if (type == "HOT")
                q = Bot.Properties.Resources.APP_LLL_GOALs_Monthly_Report.Replace("{month}", dtp_ERS.ToString("MM")).Replace("{year}", dtp_ERS.ToString("yyyy"));
            else if (type == "TOT")
                q = Bot.Properties.Resources.APP_OOO_GOALs_Monthly_Report.Replace("{month}", dtp_ERS.ToString("MM")).Replace("{year}", dtp_ERS.ToString("yyyy"));
            else if (type == "22")
                q = Bot.Properties.Resources.APP_III_GOALs_Monthly_Report.Replace("{month}", dtp_ERS.ToString("MM")).Replace("{year}", dtp_ERS.ToString("yyyy"));


            //if (File.Exists(APP_dir + "\\" + type + "_GOALs" + dtp_ERS.ToString("MMMM") + ".xlsx"))
            //{
            //    MessageBox.Show("File already exists!!");
            //    return;
            //}

            dT = General.db.GetDATATABLE(q);

            if (dT == null || dT.Rows.Count == 0)
            {
                File.WriteAllText(APP_dir + "\\" + type + "_GOALsERROR" + dtp_ERS.ToString("MMMM") + ".txt", type + " returns 0rows, operation aborted. using query" + q);

                return;
            }


            //export sql used
            File.WriteAllText(APP_dir + "\\" + type + "_GOALs" + dtp_ERS.ToString("MMMM") + ".sql", q);

            //export XLS
            FileInfo newfILE = new FileInfo(APP_dir + "\\" + type + "_GOALs" + dtp_ERS.ToString("MMMM") + ".xlsx");

            using (ExcelPackage package = new ExcelPackage(newfILE))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(type + " - " + dtp_ERS.ToString("MMMM"));
                //ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                int row = 1;
                int col = 0;

                foreach (DataColumn col_item in dT.Columns)
                {
                    col += 1;
                    worksheet.Cells[row, col].Value = col_item.Caption;
                }

                foreach (DataRow item in dT.Rows)
                {
                    //init @ xls
                    row += 1;
                    col = 0;

                    foreach (var col_item in dT.Columns)
                    {
                        col += 1;
                        worksheet.Cells[row, col].Value = item[col - 1].ToString();
                    }
                }

                worksheet.Cells["A1:F1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(Color.Blue);
                worksheet.Cells["A1:F1"].Style.Font.Color.SetColor(Color.White);

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                //xls properties
                package.Workbook.Properties.Title = "APP " + dtp_ERS.ToString("MMM-yyyy");
                package.Workbook.Properties.Author = "BATMAN";
                package.Workbook.Properties.Company = "BATMAN COMPANY";

                package.Save();
            }

        }

        private static void find_payments()
        {
            DataTable dupl_validation = General.db.GetDATATABLE(Bot.Properties.Resources.APP_validation.Replace("{month}", DateTime.Today.AddDays(-1).ToString("MM")).Replace("{year}", DateTime.Today.AddDays(-1).ToString("yyyy")));

            string mail_body = "";
            //string resp = "";
            string final_q = "";
            string final_q_html = "";

            foreach (DataRow item in dupl_validation.Rows)
            {
                final_q += " (NIAOU_NO_sdfsdfds = " + item["NIAOUnumberparent"].ToString() + " and PRODUCT_CODE_sdfsdfds = '" + item["theCode"].ToString() + "') OR ";
                final_q_html += " (NIAOU_NO_sdfsdfds = " + item["NIAOUnumberparent"].ToString() + " and PRODUCT_CODE_sdfsdfds = '" + item["theCode"].ToString() + "') OR <br>";
            }

            if (final_q.Length == 0)
                final_q = " 0 = 1     ";

            DataTable dT = General.db.GetDATATABLE("select PRODUCT_CODE_sdfsdfds,LISTPRICECOUPON from OWNER.FISH_APP_545645COUPON where (" + final_q.Substring(0, final_q.Length - 4) + " ) and SYSTEM_MONTH_sdfsdfds=" + DateTime.Today.AddDays(-1).ToString("MM") + " and SYSTEM_YEAR_sdfsdfds=" + DateTime.Today.AddDays(-1).ToString("yyyy") + "  group by PRODUCT_CODE_sdfsdfds,LISTPRICECOUPON order by PRODUCT_CODE_sdfsdfds asc");

            if (dT != null && dT.Rows.Count > 0)
            {

                mail_body = "<br><br><h3>OrderDetails Duplicate</h3><table style='font-family: verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #c0c0c0;border-collapse: collapse;  width:755px; margin: 0 auto;'><thead><tr><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>Product</th><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>Invoice Price</th></tr></thead><tbody>";

                foreach (DataRow item in dT.Rows)
                {
                    mail_body += "<tr>" + "<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>" + item["PRODUCT_CODE_sdfsdfds"] + "</td>" + "<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'>" + item["LISTPRICECOUPON"] + "</td>" + "</tr>";
                }

                mail_body += "</tbody></table><br><br>";


            }

            if (string.IsNullOrEmpty(mail_body))
                mail_body = "No payments found.";
            else
            {
                if (final_q.Length > 9)
                    mail_body += "<br><br>select asd,asdasd from OWNER.asdasd where (<br>" + final_q_html.Substring(0, final_q_html.Length - 8) + "<br> ) and asdasdas=" + DateTime.Today.AddDays(-1).ToString("MM") + " and asdasdsa=" + DateTime.Today.AddDays(-1).ToString("yyyy") + "  group by Pasdasdsa order by asdasd asc<br><br>";

                File.WriteAllText(APP_dest_dir + "mail-duplicates.html", mail_body);
            }

            General.SendEmail("[Duplicate Status] APP daily job status ran successfully on " + General.humanize_date(DateTime.Now) + " of " + DateTime.Now.ToString("MMMM yyyy"), mail_body, null, null, mail_others);

            ///////////////////// DUPLICATES SECOND PASS
            if (mail_body != "No payments found.")
            {
                string duplicate_analysis_pass2 = duplicate_analysis_second_pass();

                File.WriteAllText(APP_dest_dir + "mail-duplicates_2nd_pass.html", duplicate_analysis_pass2);

                General.SendEmail("[Duplicate Status Second Pass] APP daily job status ran successfully on " + General.humanize_date(DateTime.Now) + " of " + DateTime.Now.ToString("MMMM yyyy"), duplicate_analysis_pass2, null, null, mail_others);
            }
        }

        private static string duplicate_analysis_second_pass(){
            //>>>>>>>>> check for duplicates
            string q = Bot.Properties.Resources.duplicate_sql0.Replace("{month}", DateTime.Today.AddDays(-1).ToString("MM")).Replace("{year}", DateTime.Today.AddDays(-1).ToString("yyyy"));
            DataTable dt1 = General.db.GetDATATABLE(q);

            string final_q = "";
            foreach (DataRow item in dt1.Rows)
            {
                final_q += " (asdasd = " + item.ItemArray[3] + " and asdasd = '" + item.ItemArray[2] + "') OR ";
            }

            if (final_q.Length == 0)
            {
                return "My lords, no payments found! (x1)";
            }

            //>>>>>>>>> check, if duplicates exist to payments
           string q2 = "select o,k,o from OWNER.kkkk where (" + final_q.Substring(0, final_q.Length - 4) + " ) and kkjk=" + DateTime.Today.AddDays(-1).ToString("MM") + " and SYSTEM_YEAR_sdfsdfds=" + DateTime.Today.AddDays(-1).ToString("yyyy") + "  group by asdsad order by sd,asd asc";

            DataTable dT2 = General.db.GetDATATABLE(q2);

            if (dT2 == null && dT2.Rows.Count == 0)
            {
                return "My lords, no payments found (x2)!";
            }


            //>>>>>>>>> GET UNIQUE FROM DATATABLE 
              ////////////////////////////////////
            //extract unique from each col
            var customAPP_d = (from r in dT2.AsEnumerable()
                             group r by r.Field<string>("k") into grp
                             select new
                             {
                                 NIAOU = grp.Key,
                             });

            List<string> customAPP_unique = new List<string>();
            foreach (var item in customAPP_d)
                customAPP_unique.Add(item.NIAOU);


            //
            var products_d = (from r in dT2.AsEnumerable()
                            group r by r.Field<string>("o") into grp
                            select new
                            {
                                product = grp.Key,
                            });

            List<string> products_unique = new List<string>();
            foreach (var item in products_d)
                products_unique.Add(item.product);



            //
            var prices_d = (from r in dT2.AsEnumerable()
                          group r by r.Field<double>("p") into grp
                          select new
                          {
                              price = grp.Key,
                          });

            List<string> prices_unique = new List<string>();

            foreach (var item in prices_d)
                prices_unique.Add(item.price.ToString());


            string cust,prod,pric;
            cust = String.Join(Environment.NewLine, customAPP_unique);
            prod = String.Join(Environment.NewLine, products_unique);
            pric = String.Join(Environment.NewLine, prices_unique);


            //QUERY DETAILS 
            string[] customAPP = cust.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] products = prod.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] prices = pric.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);


            string sql_customAPP = "";
            foreach (string item in NIAOUs)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    sql_customAPP += item.Trim() + ", ";
            }

            string sql_products = "";
            foreach (string item in products)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    sql_products += "'" + item.Trim() + "', ";
            }

            string sql_prices = "";
            foreach (string item in prices)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    sql_prices += item.Trim() + ", ";
            }

            if (sql_NIAOUs.Length == 0 || sql_products.Length == 0 || sql_prices.Length == 0)
            {
                return "no payments found (x3)!";
            }

            sql_customAPP = sql_NIAOUs.Substring(0, sql_NIAOUs.Length - 2);
            sql_products = sql_products.Substring(0, sql_products.Length - 2);
            sql_prices = sql_prices.Substring(0, sql_prices.Length - 2);

            string sql1 = Bot.Properties.Resources.duplicate_sql1.Replace("{month}", DateTime.Today.AddDays(-1).ToString("MM")).Replace("{year}", DateTime.Today.AddDays(-1).ToString("yyyy"));

            //if (chkgroupby.Checked)
            //    sql1 = sql1.Replace("group by ", "--group by ");

            sql1 = sql1.Replace("{prod}", sql_products).Replace("{cust}", sql_NIAOUs).Replace("{pric}", sql_prices);

            //Merge DEAL COLUMN
            DataTable dt2 = General.db.GetDATATABLE(sql1);
            dt2.Columns.Add("DEAL");

            object deal;
            foreach (DataRow item in dt2.Rows)
            {
                if (item.ItemArray[6] == DBNull.Value)
                    continue;

                deal = General.db.ExecuteSQLScalar("select g from OWNER.FISH_APP_PdN PD1 where " +
                                              " PD1.k=" + DateTime.Today.AddDays(-1).ToString("MM") +
                                              " and PD1.o=" + DateTime.Today.AddDays(-1).ToString("yyyy") +
                                              " and PD1.j = '" + item.ItemArray[1] + "' and PD1.oy = " + item.ItemArray[4]);

                if (deal != null)
                    item[dt2.Columns.Count - 1] = deal.ToString();
                else
                    item[dt2.Columns.Count - 1] = "Cant find the DEAL! wtf?";
            }

            //HTML
            //export to HTML
            string html = "<table style='font-family: verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #c0c0c0;border-collapse: collapse;  width:755px; margin: 0 auto;'>";
            string salesforceURL = "https://google.com/_ui/search/ui/UnifiHOTearchResults?str=";
            string td = "<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #c0c0c0;'><nobr>";

            html += "<tr>";
            html += td + "NIAOU</td>" + td + "Product Code</td>" + td + "Description</td>" + td + "Quantity</td>" + td + "Invoice Price</td>" + td + "Invoice No</td>" + td + "Invoice Type</td>" + td + "Invoice Date</td>" + td + "Imported to ERS</td>" + td + "DEAL</td>";
            html += "</tr>";
            foreach (DataRow item in dt2.Rows)
            {
                // REM to export all - but only the today will have the DEAL
                if (item.ItemArray[6] == DBNull.Value)
                    continue;

                html += "<tr>";

                for (int i = 0; i < item.Table.Columns.Count; i++)
                {
                    if (i == 9) //item.Table.Columns.Count - 1)
                    {
                        html += td + "<a href='" + salesforceURL + item[i].ToString() + "'>" + item[i].ToString() + "</a></td>";

                    }
                    else if (i == 7 || i==8) //item.Table.Columns.Count - 2 || i == item.Table.Columns.Count - 3)
                        html += td + ((DateTime)item[i]).ToString("dd/MM/yyyy") + "</td>";
                    else
                        html += td + item[i] + "</td>";
                }

                html += "</tr>";
            }
            html += "</table>";

            return html;

        }

        private static string check_combination(string theCode, string NIAOUnumberparent)
        {
            string q = Bot.Properties.Resources.APP_validation_payments.Replace("{month}", DateTime.Today.AddDays(-1).ToString("MM")).Replace("{year}", DateTime.Today.AddDays(-1).ToString("yyyy"));
            string q2;

            q2 = q.Replace("{NIAOUnumberparent}", NIAOUnumberparent).Replace("{theCode}", theCode);

            object res = General.db.ExecuteSQLScalar(q2);

            if (res != null)
                return "<strong>theCode:" + theCode + " w/ NIAOUnumberparent : " + NIAOUnumberparent + " return row(s).</strong> <br><span style='font-size:9.0pt'>" + q2 + "</span><br><br>";
            else
                return "";
        }

        private static string export_APP_payments(string type)
        {
            string err = null;

            try
            {
                DataTable dT;
                string q;
                string APP_dir = APP_dest_dir + "payments\\";

                if (!Directory.Exists(APP_dir))
                    Directory.CreateDirectory(APP_dir);

                if (type == "HOT")
                    q = Bot.Properties.Resources.APP_LLL_Monthly_Report.Replace("{month}", DateTime.Today.ToString("MM")).Replace("{year}", DateTime.Today.ToString("yyyy"));
                else if (type == "TOT")
                    q = Bot.Properties.Resources.APP_OOO_Monthly_Report.Replace("{month}", DateTime.Today.ToString("MM")).Replace("{year}", DateTime.Today.ToString("yyyy"));
                else if (type == "22")
                    q = Bot.Properties.Resources.APP_III_Monthly_Report.Replace("{month}", DateTime.Today.ToString("MM")).Replace("{year}", DateTime.Today.ToString("yyyy"));
                else
                {
                    log("export_APP_payments procedure ERROR - Error cant find the type you ask!!");
                    return "export_APP_payments procedure ERROR - Error cant find the type you ask!!";
                }

                if (File.Exists(APP_dir + "\\" + type + "_Payments_" + DateTime.Today.AddMonths(-1).ToString("MMMM") + ".xlsx"))
                {
                    log("export_APP_payments procedure ERROR - File already exists!!");
                    return "export_APP_payments procedure ERROR - File already exists!!";
                }

                dT = General.db.GetDATATABLE(q);

                if (dT.Rows.Count == 0)
                {
                    log("export_APP_payments procedure ERROR - " + type + " returns 0rows, operation aborted");
                    return "export_APP_payments procedure ERROR - " + type + " returns 0rows, operation aborted";
                }

                //txt_APP_payments.Text = txt_APP_payments.Text.Replace("{prev_month}", dtp_ERS.Value.AddMonths(-1).ToString("MMMM")).Replace("{this_month}", dtp_ERS.Value.ToString("MMMM"));

                //export sql used
                File.WriteAllText(APP_dir + "\\" + type + "_Payments_" + DateTime.Today.AddMonths(-1).ToString("MMMM") + ".sql", q);


                FileInfo newfILE = new FileInfo(APP_dir + "\\" + type + "_Payments_" + DateTime.Today.AddMonths(-1).ToString("MMMM") + ".xlsx");

                using (ExcelPackage package = new ExcelPackage(newfILE))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(type + " - " + DateTime.Today.AddMonths(-1).ToString("MMMM"));
                    //ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                    int row = 1;
                    int col = 0;

                    foreach (DataColumn col_item in dT.Columns)
                    {
                        col += 1;
                        worksheet.Cells[row, col].Value = col_item.Caption;
                    }

                    foreach (DataRow item in dT.Rows)
                    {
                        //init @ xls
                        row += 1;
                        col = 0;

                        foreach (var col_item in dT.Columns)
                        {
                            col += 1;

                            if (col == 6) // number
                            {
                                //string g = string.Format("{0:N2}", double.Parse(item[col - 1].ToString()));
                                worksheet.Cells[row, col].Value = double.Parse(item[col - 1].ToString());
                            }
                            else
                                worksheet.Cells[row, col].Value = item[col - 1].ToString();
                        }
                    }


                    //worksheet.Cells["F2:" + "F:" + worksheet.Dimension.Rows].Style.Numberformat.Format = "0.00";

                    worksheet.Cells["A1:F1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(Color.Blue);
                    worksheet.Cells["A1:F1"].Style.Font.Color.SetColor(Color.White);

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    //xls properties
                    package.Workbook.Properties.Title = "APP " + DateTime.Today.AddMonths(-1).ToString("MMM-yyyy");
                    package.Workbook.Properties.Author = "BATMAN";
                    package.Workbook.Properties.Company = "BATMAN COMPANY";

                    package.Save();
                }
            }
            catch (Exception x)
            {
                log("Catch an error epxortERSpayments " + x.Message + " stack trace : " + x.StackTrace);
                err = x.Message + " stack trace : " + x.StackTrace;
            }

            return err;
        }

        private static Dictionary<string, string> APP_SAP_Files()
        {
            //APP read settings
            APP_invoice_start = General.cfg.Read("APP_invoice_start");
            APP_invoice_end = General.cfg.Read("APP_invoice_end");

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("invoice_file", "0");
            dictionary.Add("product_file", "0");
            dictionary.Add("NIAOU_file", "0");

            bool inv_exists = false;
            bool cust_exists = false;
            bool prod_exists = false;

            //using (new Impersonation("EMEA", "BATMAN", "x"))
            {
                //get all today files from APP archive zone
                var directory = new DirectoryInfo(APP_SAP_Folder);
                List<FileInfo> files = directory.GetFiles()
                                        .Where(file => file.LastWriteTime > DateTime.Today).ToList();

                string sap_file;


                foreach (FileInfo item in files)
                {
                    sap_file = item.Name;
                    log("Copying " + sap_file);

                    File.Copy(APP_SAP_Folder + sap_file, APP_dest_dir + sap_file);

                    log(sap_file + " copied locally");

                    if (sap_file.ToLower().StartsWith("xxxx"))
                    {
                        cust_exists = true;

                        if (dictionary.ContainsKey("NIAOU_file"))
                            dictionary["NIAOU_file"] = "1";

                        IEnumerable<string> inv_file = File.ReadLines(APP_dest_dir + sap_file);

                        dictionary.Add("flat_cust", inv_file.Count().ToString());
                    }

                    if (sap_file.ToLower().StartsWith("productload"))
                    {
                        prod_exists = true;
                        if (dictionary.ContainsKey("product_file"))
                            dictionary["product_file"] = "1";
                    }

                    if (sap_file.ToLower().StartsWith("invoiceload") && !inv_exists)
                    {
                        inv_exists = true;

                        if (dictionary.ContainsKey("invoice_file"))
                            dictionary["invoice_file"] = "1";

                        log("Invoice Process starting...");

                  


                        //open local file, find invoice number first and last line.

                        IEnumerable<string> inv_file = File.ReadLines(APP_dest_dir + sap_file);

                        string start_inv = inv_file.First();
                        string end_inv = inv_file.Last();

                        start_inv = General.SliceSTR(start_inv, "~", "~", 0);
                        end_inv = General.SliceSTR(end_inv, "~", "~", 0);

                        dictionary.Add("first_invoice", start_inv);
                        dictionary.Add("last_invoice", end_inv);

                        log("First Invoice is " + start_inv + " and Last is " + end_inv);

                        //inxe.dat - analysis export [start]
                        string[] analysis = anayze_invoice_file(APP_dest_dir + sap_file);

                        dictionary.Add("flat_unique_inv", analysis[0]);
                        dictionary.Add("tires_versus_flat_not_exist", analysis[1]);
                        dictionary.Add("tires_versus_flat", analysis[2]);
                        dictionary.Add("stage_versus_flat", analysis[3]);
                        dictionary.Add("sapinv_versus_flat", analysis[4]);
                        //inxe.dat - analysis export [end]

                        if (APP_invoice_end.Length > 1)
                        {
                            int prev_invoice_end = int.Parse(APP_invoice_end);
                            int tmp = int.Parse(start_inv) - 1;

                            if (prev_invoice_end != tmp)
                            {
                                log("Warning! Last invoice number is not in a row with today invoice start. " + APP_invoice_end + " vs " + start_inv);
                                report_debug_sap += "<span style='color:red;'><br>Warning! Last invoice number is not in a row with today invoice start. " + APP_invoice_end + " vs " + start_inv + "</span><br><br>";
                            }
                            else
                            {
                                log("√ Invoice Number match with previous one!");
                                report_debug_sap += "<br>√ Invoice Number match with previous one!<br><br>";
                            }
                        }

                        //store the values
                        General.cfg.Write("APP_invoice_start", start_inv);
                        General.cfg.Write("APP_invoice_end", end_inv);

                        log("Invoice Process ended...");
                    }
                    else if (sap_file.ToLower().StartsWith("inxe") && inv_exists)
                    {
                        log("******Alternative Invoice Found... Analysis aborted automatically.");
                    }

                }
            }

            report_sap = "- SAP files have been loaded in to APP (" +
                        (inv_exists ? "Invoices," : "") +
                        (cust_exists ? "NIAOUs," : "") +
                        (prod_exists ? "Products," : "");

            if (report_sap.EndsWith(","))
            {
                report_sap = report_sap.Substring(0, report_sap.Length - 1);
            }

            report_sap += ").";


            if (!dictionary.ContainsKey("first_invoice"))
                dictionary.Add("first_invoice", "0");

            if (!dictionary.ContainsKey("last_invoice"))
                dictionary.Add("last_invoice", "0");


            if (!dictionary.ContainsKey("flat_unique_inv"))
                dictionary.Add("flat_unique_inv", "0");

            if (!dictionary.ContainsKey("tires_versus_flat_not_exist"))
                dictionary.Add("tires_versus_flat_not_exist", "0");

            if (!dictionary.ContainsKey("tires_versus_flat"))
                dictionary.Add("tires_versus_flat", "0");

            if (!dictionary.ContainsKey("stage_versus_flat"))
                dictionary.Add("stage_versus_flat", "0");

            if (!dictionary.ContainsKey("sapinv_versus_flat"))
                dictionary.Add("sapinv_versus_flat", "0");

            if (!dictionary.ContainsKey("flat_cust"))
                dictionary.Add("flat_cust", "0");

            return dictionary;
        }

        private static string[] anayze_invoice_file(string fl)
        {
            string[] ret = new string[] { "0", "0", "0", "0", "0" };

            //0 - flat unique inv
            //1 - TIRES doesnt exist
            //2 - TIRES imported inv
            //3 - STAGE imported inv
            //4 - SAPINV imported inv

            try
            {
                // create a writer and open the file
                TextWriter tw = new StreamWriter(APP_dest_dir + "invoice_analysis" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString() + ".txt");
                TextWriter tw_inv = new StreamWriter(APP_dest_dir + "invoices_unique" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString() + ".txt");
                TextWriter tw_inv_dbase = new StreamWriter(APP_dest_dir + "invoices_dont_exist_to_dbase" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString() + ".txt");

                DataTable dt = General.ImportDelimitedFile(fl, "~", false);
                int dt_count = 0;
                string q = "";
                int inv_count = 0;
                int lst_inv_count = 0;

                List<string> queue = new List<string>();

                if (dt != null)
                {
                    dt_count = dt.AsEnumerable().GroupBy(row => row.Field<string>("no1")).Count(); 
                    var dt_recs = dt.AsEnumerable().GroupBy(row => row.Field<string>("no1")).ToList();



                    foreach (var item in dt_recs)
                    {
                        tw_inv.WriteLine(item.Key);

                        q += "'" + item.Key + "',";

                        inv_count += 1;
                        lst_inv_count += 1;
                        if (lst_inv_count == 999)
                        {
                            queue.Add(q.Substring(0, q.Length - 1));
                            lst_inv_count = 0;
                            q = "";
                        }
                    }

                    if (q.EndsWith(","))
                    {
                        queue.Add(q.Substring(0, q.Length - 1));
                    }

                    //export the different
                    DataTable dt_invoices = General.db.GetDATATABLE("select DISTINCT REF from OWNER.APP_TIRES where INVOICENO between " + dt_recs[0].Key + " and " + dt_recs[dt_recs.Count - 1].Key);
                    var dt_invoices_dbase = dt_invoices.AsEnumerable().GroupBy(row => row.Field<string>("REF")).ToList();
                    var difference = dt_recs.Where(f => !dt_invoices_dbase.Any(a => a.Key == f.Key));

                    //Console.WriteLine(difference.Count().ToString());
                    foreach (var item in difference)
                    {
                        tw_inv_dbase.WriteLine(item.Key);
                    }
                }

                tw.WriteLine("**************************************************************************************************");
                tw.WriteLine("" + Path.GetFileName(fl) + "\t Unique Invoices Found : " + dt_count.ToString());
                tw.WriteLine("**************************************************************************************************");

                string final_q;
                string final_q_stage;
                string final_q_sap;
                int c_comma = 0;
                int not_imported = 0;
                int step_imported = 0;

                int tires_counter = 0;
                int stage_counter = 0;
                int sapinvoices_counter = 0;
                foreach (var item in queue)
                {
                    c_comma = item.Count(x => x == ',') + 1;

                    tw.WriteLine("\r\n\r\nQuery " + c_comma.ToString() + " invoices");

                    //prod
                    final_q = "select count(DISTINCT REF) from OWNER.APP_TIRES where REF in (" + item + ")";
                    step_imported = int.Parse(General.db.ExecuteSQLScalar(final_q).ToString());
                    tw.WriteLine("\t" + step_imported.ToString() + " recs found by query " + final_q);
                    tires_counter += step_imported;
                    not_imported += c_comma - step_imported;

                    ///stage
                    final_q_stage = "select count(DISTINCT CODE) from OWNER.APP_INVOICES_STG where CODE in (" + item + ")";
                    step_imported = int.Parse(General.db.ExecuteSQLScalar(final_q_stage).ToString());
                    tw.WriteLine("\t" + General.db.ExecuteSQLScalar(final_q_stage).ToString() + " recs found by query " + final_q_stage);
                    stage_counter += step_imported;

                    //sap
                    final_q_sap = "select count(DISTINCT SAP_NO) from OWNER.APP_SAP_INVOICES where SAP_NO in (" + item + ")";
                    step_imported = int.Parse(General.db.ExecuteSQLScalar(final_q_sap).ToString());
                    tw.WriteLine("\t" + General.db.ExecuteSQLScalar(final_q_sap).ToString() + " recs found by query " + final_q_sap);
                    sapinvoices_counter += step_imported;
                }

                ret[0] = dt_count.ToString();
                ret[1] = not_imported.ToString();
                ret[2] = tires_counter.ToString();
                ret[3] = stage_counter.ToString();
                ret[4] = sapinvoices_counter.ToString();

                log("flat file - unique invoices : " + dt_count.ToString());
                log("TIRES - dbase invoices not exist : " + not_imported.ToString());

                tw.WriteLine("\r\n\t--------------------------------------------------------------------------------------------------");
                tw.WriteLine("\t" + Path.GetFileName(fl) + "\t Invoices Missed on APP_TIRES : " + not_imported.ToString());
                tw.WriteLine("\t--------------------------------------------------------------------------------------------------\r\n\r\n");

                // close the stream
                tw.Close();
                tw_inv.Close();
                tw_inv_dbase.Close();

            }
            catch { }

            return ret;
        }



        private static double process_KKK_LLL_III_file(string p)
        {
            List<double> col_nums = new List<double>();

            using (StreamReader reader = new StreamReader(p))
            {
                //skip first line 
                reader.ReadLine();

                string line;
                int pos;
                while ((line = reader.ReadLine()) != null)
                {
                    pos = line.LastIndexOf("ASKD");
                    if (pos > -1)
                    {
                        pos += 4;
                        col_nums.Add(General.try_parse_double(line.Substring(pos, 11).Trim()));
                    }
                }

            }

            return double.Parse(string.Format("{0:N2}", col_nums.Sum()));
        }

        private static List<db_record> find_GOAL_ties()
        {
            List<db_record> db_values = APP_get_oracle_values();

            if (db_values == null)
            {
                log("Cant DBASE values - operation aborted!");
                return null;
            }

            //here holds the APP files
            List<the_APP_file> file_vals = new List<the_APP_file>();

            // using (new Impersonation("EMEA", "BATMAN", "x"))
            {
                //get all today files from APP archive zone
                var directory = new DirectoryInfo(APP_Folder);
                List<FileInfo> files = directory.GetFiles()
                                        .Where(file => file.LastWriteTime > DateTime.Today).ToList();

                if (files == null)
                {
                    log("No GOAL files found for today!");
                    return null;
                }

                if (files.Count() == 0)
                {
                    log("The GOAL file count is 0!");
                    return null;
                }

                double[] sum_file;

                /////////////////////////////////////////// GET VALUES FROM GOAL FILES - start
                foreach (FileInfo item in files)
                {
                    sum_file = null;
                    log("Copying " + item.Name);
                    File.Copy(APP_Folder + item.Name, APP_dest_dir + item.Name);
                    log(item.Name + " copied successfully!");

                    if (item.Name.ToLower().StartsWith("sdfsdfsdf"))
                    {
                        log("Processing GOAL file : " + item.Name);

                        try
                        {
                            sum_file = General.read_and_sumup(APP_dest_dir + item.Name);
                        }
                        catch (Exception xs)
                        {
                            log("Error when processing " + item.Name + " Error : " + xs.Message);
                        }

                        //error occured
                        if (sum_file == null)
                            continue;

                        log("File 00s :  " + sum_file[0].ToString());
                        log("File 90s :  " + sum_file[1].ToString());
                        log("File sum up :  " + (sum_file[0] - sum_file[1]).ToString());

                        //the_APP_file - roundup to 2decimal.
                        file_vals.Add(new the_APP_file(item.Name, sum_file[0], sum_file[1]));
                    }
                }
            }
            /////////////////////////////////////////// GET VALUES FROM GOAL FILES - end

            //generic proc even we have more than two files, because one file is equal with one of DB values
            foreach (the_APP_file item in file_vals)
            {
                if (db_values[0].field_value == item.sumup)
                    db_values[0].match_file = item;

                if (db_values[1].field_value == item.sumup)
                    db_values[1].match_file = item;
            }

            GOAL_file_count = file_vals.Count.ToString();

            if (file_vals.Count > 2)
            {
                foreach (the_APP_file item in file_vals)
                {
                    foreach (the_APP_file item2 in file_vals)
                    {
                        if (item == item2)
                            continue;

                        //todo : merge the file to report
                        if (db_values[0].match_file == null && db_values[0].field_value == (item.sumup + item2.sumup))
                        {
                            item.filename = item.filename + " + " + item2.filename;
                            db_values[0].match_file = item;
                        }

                        if (db_values[1].match_file == null && db_values[1].field_value == (item.sumup + item2.sumup))
                        {
                            item.filename = item.filename + " + " + item2.filename;
                            db_values[1].match_file = item;
                        }
                    }
                }
            }

            return db_values;

        }

        private static List<db_record> APP_get_oracle_values()
        {
            double val1 = 0;
            double val2 = 0;

            try
            {
                //DataTable dT = General.db_ers.GetDATATABLE(Bot.Properties.Resources.oracle_APP_q);
                log("query dbase for last xzx && yyy record - " + Bot.Properties.Resources.APP_oracle_LLL_III_get_id_from_job_ends.Replace("{ydate}", DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy")));
                DataTable dT_LLL_00 = General.db.GetDATATABLE(Bot.Properties.Resources.APP_oracle_LLL_III_get_id_from_job_ends.Replace("{ydate}", DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy")));
                log("query dbase for last yyyy record - " + Bot.Properties.Resources.APP_oracle_KKK_get_id_from_job_ends.Replace("{ydate}", DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy")));
                DataTable dT_FFFJ = General.db.GetDATATABLE(Bot.Properties.Resources.APP_oracle_KKK_get_id_from_job_ends.Replace("{ydate}", DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy")));

                DataTable dT;

                //if something wrong - get the default query with sysdate
                if (dT_LLL_00 == null || dT_FFFJ == null)
                {
                    if (dT_LLL_00 == null)
                        log("xx && yy last record for yday is null!");
                    if (dT_LLL_00 == null)
                        log("xx last record for yday is null!");

                    log("trying to execute the normal query with sysdate - point1");

                    dT = General.db.GetDATATABLE(Bot.Properties.Resources.oracle_APP_q);
                }

                if (dT_LLL_00.Rows[0][0] == DBNull.Value || dT_FFFJ.Rows[0][0] == DBNull.Value)
                {
                    if (dT_LLL_00.Rows[0][0] == DBNull.Value)
                        log("ZZZ && WWW - field is null!");
                    if (dT_FFFJ.Rows[0][0] == null)
                        log("UUU  - field is null!");

                    log("trying to execute the normal query with sysdate - point2");

                    dT = General.db.GetDATATABLE(Bot.Properties.Resources.oracle_APP_q);
                }
                else
                {
                    log("query with the latest xxx&&yy + llll record id - " + Bot.Properties.Resources.APP_oracle_APP_q2.Replace("{APP_III_id}", dT_LLL_00.Rows[0][0].ToString()).Replace("{KKK_id}", dT_FFFJ.Rows[0][0].ToString()));
                    dT = General.db.GetDATATABLE(Bot.Properties.Resources.APP_oracle_APP_q2.Replace("{APP_III_id}", dT_LLL_00.Rows[0][0].ToString()).Replace("{KKK_id}", dT_FFFJ.Rows[0][0].ToString()));
                }


                if (dT == null)
                    return null;



                if (dT.Rows[0][0] != DBNull.Value)
                    if (dT.Rows[0][0] != null)
                    {
                        val1 = General.try_parse_double(dT.Rows[0][0].ToString());
                    }

                if (dT.Rows.Count > 1)
                {
                    if (dT.Rows[1][0] != DBNull.Value)
                        if (dT.Rows[1][0] != null)
                        {
                            val2 = General.try_parse_double(dT.Rows[1][0].ToString());
                        }
                }
            }
            catch (Exception ex)
            {
                log("ORACLE Connection Error - " + ex.Message);
            }

            List<db_record> tmp = new List<db_record>();
            tmp.Add(new db_record(val1, "APP_SAP_GOALCalculationTOT.GOAL", null));
            tmp.Add(new db_record(val2, "APP_SAP_GOALCalculation.GOAL", null));

            log("DBASE QQQ SUM is " + val1.ToString());
            log("DBASE ZZZ & WWW SUM is " + val2.ToString());

            return tmp;
        }


        private static void log(string s)
        {
            string t = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " >> " + s;

            outfile.WriteLine(t);
            Console.WriteLine(t);
        }





        private class the_APP_file
        {
            public string filename { get; set; }
            public double a00s { get; set; }
            public double a90s { get; set; }
            public double sumup { get; set; }

            public the_APP_file(string filename, double a00s, double a90s)
            {
                this.filename = filename;
                this.a00s = a00s;
                this.a90s = a90s;
                this.sumup = double.Parse(string.Format("{0:N2}", a00s - a90s));
            }
        }

        private class db_record
        {
            public double field_value { get; set; }
            public string field_name { get; set; }
            public the_APP_file match_file { get; set; }

            public db_record(double field_value, string field_name, the_APP_file match_file)
            {
                this.field_value = field_value;
                this.field_name = field_name;
                this.match_file = match_file;
            }
        }

    }
}
