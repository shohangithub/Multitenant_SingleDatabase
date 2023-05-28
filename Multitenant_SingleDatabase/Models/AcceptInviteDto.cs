namespace Multitenant_SingleDatabase.Models
{
    public class AcceptInviteDto
    {
        public string Verify { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
