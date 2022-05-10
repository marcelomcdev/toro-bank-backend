using ToroBank.Application.Common.Wrappers;
using ToroBank.Domain.Common;

namespace ToroBank.Application.Common.Interfaces.Repositories
{
    public interface IGenericRepository<T, TKey>
    where T : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
    {
        Task<T> GetByIdAsync(TKey id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<PagedResponse<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
