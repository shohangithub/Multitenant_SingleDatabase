namespace Common.CommonAdmin
{
    public class UserOrTenantDto
    {
        public UserOrTenantDto(bool user, string name)
        {
            Type = user ? "User" : "Tenant";
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }
    }
}
