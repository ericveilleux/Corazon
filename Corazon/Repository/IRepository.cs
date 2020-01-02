namespace Corazon
{
    /// <summary>
    /// Implements the base contract for the Repository pattern.
    /// The repository is the astraction in the domain that is used to perform persistence of the domain objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdentity"></typeparam>
    public interface IRepository<T, in TIdentity>
        where T : AggregateRoot<TIdentity>
        where TIdentity : Identity, new()
    {
        T Find(TIdentity id);

        void Save(T instance);
    }
}