namespace AuthPermissions.AspNetCore.GetDataKeyCode
{
    /// <summary>
    /// This is the interface used by the GetDataKeyFilterFromUser and <see cref="DataKeyQueryExtension"/>
    /// </summary>
    public interface IGetDataKeyFromUser
    {
        /// <summary>
        /// The DataKey to be used for multi-tenant applications
        /// </summary>
        string DataKey { get; }
    }
}
