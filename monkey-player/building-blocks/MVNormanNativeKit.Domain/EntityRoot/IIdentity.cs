namespace MVNormanNativeKit.Domain.EntityRoot
{
    /// <summary>
    /// Super type for all Identity types with generic Id
    /// </summary>
    public interface IIdentity<TId>
    {
        TId Id { get; }
    }
}
