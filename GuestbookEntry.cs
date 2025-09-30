//klass som representerar ett inlägg i gästboken
public class GuestbookEntry
{
    public string Owner { get; set; }
    public string Message { get; set; }

    public GuestbookEntry(string owner, string message)
    {
        Owner = owner;
        Message = message;
    }
}
