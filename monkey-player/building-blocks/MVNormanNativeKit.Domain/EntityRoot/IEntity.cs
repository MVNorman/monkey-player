namespace MVNormanNativeKit.Domain.EntityRoot
{
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
