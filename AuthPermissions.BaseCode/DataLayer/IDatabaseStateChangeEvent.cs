using AuthPermissions.BaseCode.DataLayer.EfCode;

namespace AuthPermissions.BaseCode.DataLayer
{
    /// <summary>
    /// This is an optional service for the <see cref="AuthPermissionsDbContext"/> which
    /// allows you register event handlers 
    /// </summary>
    public interface IDatabaseStateChangeEvent
    {
        /// <summary>
        /// This is called within the <see cref="AuthPermissionsDbContext"/> constructor.
        /// It allows you to register the events you need.
        /// </summary>
        void RegisterEventHandlers(AuthPermissionsDbContext context);
    }
}
