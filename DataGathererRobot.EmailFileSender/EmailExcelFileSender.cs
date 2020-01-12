using Microsoft.Office.Interop.Outlook;

namespace DataGathererRobot.EmailFileSender
{
    public class EmailExcelFileSender : IEmailFileSender
    {
        private const string FilePath = "C:\\Temp\\microwaves.xlsx";
        private const string Subject = "Microwaves";

        private readonly Application _outlook;

        public EmailExcelFileSender()
        {
            _outlook = new Application();
        }

        public void SendFile(string email)
        {
            MailItem mail = (MailItem)_outlook.CreateItem(OlItemType.olMailItem);
            mail.Attachments.Add(FilePath);
            mail.Subject = Subject;
            mail.To = email;
            mail.Send();

            _outlook.Quit();
        }
    }
}
