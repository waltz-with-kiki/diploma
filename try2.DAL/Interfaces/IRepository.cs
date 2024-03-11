using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace try2.DAL.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        IQueryable<T> Items { get; }

        T Get(long id);

        Task GetAsync(long id);

        T Add(T item);

        Task<T> AddAsync(T item);

        void Update(T item);

        Task UpdateAsync(T item);

        void Remove(long id);

        Task RemoveAsync(long id);
    }
}
