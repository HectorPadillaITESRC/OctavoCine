using ApiCine.Models.Entities;

namespace NoticiasOctavoAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        ItesrcneOctavoContext Context { get; set; }

        void Delete(T item);
        T? Get(object id);
        IEnumerable<T> GetAll();
        void Insert(T item);
        void Update(T item);
    }
}