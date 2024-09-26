using MimeKit;

namespace Market.Auth.Application.Services.AccountService;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}
public class EmailService : IEmailService
{
    private readonly string? _smtpServer = "smtp.gmail.com";
    private int _smtpPort = 587;
    private readonly string _from = "hamzaqulovogabek@gmail.com";
    private readonly string _password = "qpbe fvee ftiy qanb";
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        /*
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_from),
            To = { new MailAddress(to) },
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
        {
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_from, _password);
            await smtpClient.SendMailAsync(mailMessage);
        }
        */
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(string.Empty, _from));
        message.To.Add(new MailboxAddress(string.Empty, to));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body
        };
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            await client.ConnectAsync(_smtpServer, 587, MailKit.Security.SecureSocketOptions.Auto);
            await client.AuthenticateAsync(_from, _password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

}
