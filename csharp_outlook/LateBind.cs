    public class LateBinding
    {
        internal void SendMail(string filePath, string subject, string body, string recipientEmail)
        {
            try
            {
                // Late binding to create Outlook application
                Type outlookType = Type.GetTypeFromProgID("Outlook.Application");
                if (outlookType == null)
                {
                    throw new Exception("Outlook is not installed or cannot be found.");
                }
                dynamic outlookApp = Activator.CreateInstance(outlookType);//Marshal.GetActiveObject("Outlook.Application");
                
                // Create a new mail item
                dynamic mailItem = outlookApp.CreateItem(0); // 0 corresponds to olMailItem
                
                // Set email properties
                mailItem.Subject = subject;
                mailItem.Body = body;
                dynamic recipients = mailItem.Recipients;
                dynamic recipient = recipients.Add(recipientEmail);
                
                // Attach the file if it exists
                if (File.Exists(filePath))
                {
                    mailItem.Attachments.Add(filePath);
                }
                else
                {
                    throw new FileNotFoundException("The file does not exist.", filePath);
                }
                
                // Display the email for the user
                mailItem.Display(true); // true means the email can be edited before sending
            }
            catch (System.Runtime.InteropServices.COMException comEx)
            {
                throw new Exception("COM Exception: {comEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Error: {ex.Message}");
            }
        }
    }