namespace Mailling.Core;

public class Mail
{
    public string From { get; set; }
    public string To { get; set; }
    public string Copy { get; set; }
    public string BlindCopy { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public string Message { get; set; } = string.Empty;
}