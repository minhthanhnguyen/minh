using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace minhCore1.Services
{
    public static class SendGridService
    {
        public static async Task SendTestEmail(string apiKey)
        {
            try
            {
                var client = new SendGridClient(apiKey);

                // Send a Single Email using the Mail Helper
                var from = new EmailAddress("minhthanhnguyen82@gmail.com", "Minh Nguyen");
                var subject = "Testing email!";
                var to = new EmailAddress("minhthanhnguyen82@gmail.com", "Minh Nguyen");
                var plainTextContent = "Testing email content!";
                var htmlContent = "<strong>Testing email content!</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
