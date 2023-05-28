namespace Multitenant_Sharing.Models
{
    public class InviteUserResult
    {
        public InviteUserResult(string message, string url)
        {
            Message = message;
            Url = url;
        }

        public string Message { get; }
        public string Url { get; }

    }
}
