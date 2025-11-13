    public class InteropOutlook
    {
       public void SendMailViaOutlookInterop(string filepath)
        {
            //using Outlook = Microsoft.Office.Interop.Outlook;
            //reference C:\Program Files\Microsoft Office\root\Office16\ADDINS\Microsoft Power Query for Excel Integrated\bin\Microsoft.Office.Interop.Outlook.dll

             Outlook.Application outlookApp;
             try
             {
                outlookApp = (Outlook.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application");
             }
             catch (Exception f)
             {
                 Console.WriteLine(f.ToString());
                 outlookApp = new Outlook.Application();
             }
             Outlook.MailItem mail = outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
             mail.Subject = "Here is your file";
             mail.Body = "Please see the attached file.";
             mail.Attachments.Add(filepath);
             mail.Display(true); // Pops up the compose window
        }
    }