using AuthPermissions.BaseCode.DataLayer.Classes.SupportTypes;
using System.ComponentModel.DataAnnotations;


namespace Common.CommonAdmin
{
    public class EmailAndUserNameDto
    {
        public EmailAndUserNameDto(string email, string userName)
        {
            Email = email;
            UserName = userName;
        }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(AuthDbConstants.EmailSize)]
        public string Email { get; private set; }

        [MaxLength(AuthDbConstants.UserNameSize)]
        public string UserName { get; private set; }
    }
}
