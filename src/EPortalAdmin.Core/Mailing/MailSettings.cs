namespace EPortalAdmin.Core.Mailing;

public class MailSettings
{
    public MailSettings()
    {
        Server = string.Empty;
        Port = 0;
        SenderFullName = string.Empty;
        SenderEmail = string.Empty;
        UserName = string.Empty;
        Password = string.Empty;
    }

    public MailSettings(
        string server,
        int port,
        string senderFullName,
        string senderEmail,
        string userName,
        string password,
        bool authenticationRequired
    )
    {
        Server = server;
        Port = port;
        SenderFullName = senderFullName;
        SenderEmail = senderEmail;
        UserName = userName;
        Password = password;
        AuthenticationRequired = authenticationRequired;
    }

    public readonly static string AppSettingsKey = "MailSettings";
    public string Server { get; set; }
    public int Port { get; set; }
    public string SenderFullName { get; set; }
    public string SenderEmail { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool AuthenticationRequired { get; set; }
    public string? DkimPrivateKey { get; set; }
    public string? DkimSelector { get; set; }
    public string? DomainName { get; set; }
    public static MailSettings Default => new()
    {
        AuthenticationRequired = false,
        DkimPrivateKey = "secret dkim key",
        DkimSelector = "dkim selector",
        DomainName = "domain name",
        Password = "Passw0rd",
        Port = 25,
        SenderEmail = "emailsendertest@gmail.com",
        SenderFullName = "E-Portal Admin",
        Server = "127.0.0.1",
        UserName = "eportaladmin"
    };

}

