namespace Corazon
{
    public static class RepositoryExtensions
    {
        public static void EnsureExists<T, TIdentity>(this IRepository<T, TIdentity> self, TIdentity id)
            where T : AggregateRoot<TIdentity> 
            where TIdentity : Identity, new()
        {
            self.Get(id);
        }

        public static T Get<T, TIdentity>(this IRepository<T, TIdentity> self, TIdentity id)
            where T : AggregateRoot<TIdentity>
            where TIdentity : Identity, new()
        {
            var found = self.Find(id);
            if (found == null)
            {
                throw new EntityNotFoundException<T>(id);
            }

            return found;
        }
    }
}